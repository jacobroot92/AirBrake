using AirBrake.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AirBrake.Utils
{
    public class QuestionParser
    {
        public Question ParsedQuestion;

        public QuestionParser()
        {
            ParsedQuestion = new Question();
        }

        public QuestionPart identifyQuestionPart(string line, QuestionPart previousPart)
        {
            switch (previousPart)
            {
                case QuestionPart.QuestionName:
                    return QuestionPart.PictureSpeechName;
                case QuestionPart.PictureSpeechName:
                    return QuestionPart.DifficultyFlagsRefs;
                case QuestionPart.DifficultyFlagsRefs:
                    return QuestionPart.Dates;
                case QuestionPart.Dates:
                    if (line != string.Empty)
                        return QuestionPart.QuestionText;
                    else
                        return QuestionPart.QuestionAnswer5;
                case QuestionPart.QuestionText:
                        return QuestionPart.QuestionAnswer1;
                case QuestionPart.QuestionAnswer1:
                    return QuestionPart.QuestionAnswer2;
                case QuestionPart.QuestionAnswer2:
                    return QuestionPart.QuestionAnswer3;
                case QuestionPart.QuestionAnswer3:
                    return QuestionPart.QuestionAnswer4;
                case QuestionPart.QuestionAnswer4:
                    return QuestionPart.QuestionAnswer5;
                case QuestionPart.QuestionAnswer5:
                    return QuestionPart.QuestionDescription;
                default:
                    return QuestionPart.QuestionName;
            }
        }

        public void parseLine(string line, QuestionPart questionPart)
        {
            //if(line != string.Empty)
            //{
                switch (questionPart)
                {
                    case QuestionPart.QuestionName:
                        parseQuestionName(line);
                        break;
                    case QuestionPart.PictureSpeechName:
                        parsePictureSpeechName(line);
                        break;
                    case QuestionPart.DifficultyFlagsRefs:
                        parseDifficultyFlagsRefs(line);
                        break;
                    case QuestionPart.Dates:
                        parseDates(line);
                        break;
                    case QuestionPart.QuestionText:
                        ParsedQuestion.Text = line;
                        break;
                    case QuestionPart.QuestionAnswer1:
                        parseAnswer(line, 1);
                        break;
                    case QuestionPart.QuestionAnswer2:
                        parseAnswer(line, 2);
                        break;
                    case QuestionPart.QuestionAnswer3:
                        parseAnswer(line, 3);
                        break;
                    case QuestionPart.QuestionAnswer4:
                        parseAnswer(line, 4);
                        break;
                    case QuestionPart.QuestionAnswer5:
                        parseAnswer(line, 5);
                        break;
                    case QuestionPart.QuestionDescription:
                        ParsedQuestion.Description = line;
                        break;
                    default:
                        break;
                }
            //}
        }

        private void parseQuestionName(string line)
        {
            string[] questionNames = line.Split(' ');
            ParsedQuestion.QuestionName1 = questionNames.Length > 0 ? questionNames[0] : string.Empty;
            ParsedQuestion.QuestionName2 = questionNames.Length > 1 ? questionNames[1] : string.Empty;
            ParsedQuestion.QuestionName3 = questionNames.Length > 2 ? questionNames[2] : string.Empty;
        }

        private void parsePictureSpeechName(string line)
        {
            string[] names = line.Split(' ');
            if (names.Length > 0)
            {
                ParsedQuestion.PictureRequired = names[0].Contains("pictures/%");
                ParsedQuestion.PictureName = names[0].Split(new[] { "pictures/" }, StringSplitOptions.None)[1].Replace("%", string.Empty);
            }
            else
            {
                ParsedQuestion.PictureRequired = false;
                ParsedQuestion.PictureName = string.Empty;
            }

            ParsedQuestion.SpeechName = names.Length > 1 ? names[1].Split(new[] { "speeches/" }, StringSplitOptions.None)[1] : string.Empty;
        }

        private void parseDifficultyFlagsRefs(string line)
        {
            string cleanedLine = line.Replace("\"", string.Empty);
            string[] parsedItems = cleanedLine.Split('%');

            if(parsedItems.Length > 0)
            {
                string[] difficultyFlagsIncompabilities = parsedItems[0].Split(new[] { ',' }, 2);
                ParsedQuestion.DifficultyFlags = difficultyFlagsIncompabilities.Length > 0 ? difficultyFlagsIncompabilities[0] : string.Empty;
                ParsedQuestion.Incompatibilities = difficultyFlagsIncompabilities.Length > 1 ? difficultyFlagsIncompabilities[1] : string.Empty;
            }
            else
            {
                ParsedQuestion.DifficultyFlags = string.Empty;
                ParsedQuestion.Incompatibilities = string.Empty;
            }

            ParsedQuestion.LegalRef = parsedItems.Length > 1 ? parsedItems[1].Trim() : string.Empty;
            ParsedQuestion.ManualRef = parsedItems.Length > 2 ? parsedItems[2].Trim() : string.Empty;
        }

        private void parseDates(string line)
        {
            if(line != string.Empty)
            {
                string[] dates = line.Split(' ');
                if (dates.Length > 0)
                    ParsedQuestion.EffectiveDate = int.Parse(dates[0]);
                if (dates.Length > 1)
                    ParsedQuestion.EffectiveDate = int.Parse(dates[1]);
            }
        }

        private void parseAnswer(string line, int answerNumber)
        {
            if (line.Contains('*'))
            {
                ParsedQuestion.CorrectAnswer = answerNumber;
                line = line.Replace("*", string.Empty);
            }

            switch (answerNumber)
            {
                case 1:
                    ParsedQuestion.Answer1 = line;
                    break;
                case 2:
                    ParsedQuestion.Answer2 = line;
                    break;
                case 3:
                    ParsedQuestion.Answer3 = line;
                    break;
                case 4:
                    ParsedQuestion.Answer4 = line;
                    break;
                case 5:
                    ParsedQuestion.Answer5 = line;
                    break;
            }
        }

        public void resetQuestion()
        {
            ParsedQuestion = new Question();
        }
    }
}
