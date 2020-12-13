using System;
using System.Collections.Generic;
using System.Text;

namespace AirBrake.Models
{
    public class Question
    {
        public string QuestionName1 { get; set; }
        public string QuestionName2 { get; set; }
        public string QuestionName3 { get; set; }
        public string PictureName { get; set; }
        public string SpeechName { get; set; }
        public string DifficultyFlags { get; set; }
        public string Incompatibilities { get; set; }
        public string LegalRef { get; set; }
        public string ManualRef { get; set; }
        public int EffectiveDate { get; set; }
        public int IneffectiveDate { get; set; }
        public string Text { get; set; }
        public string Answer1 { get; set; }
        public string Answer2 { get; set; }
        public string Answer3 { get; set; }
        public string Answer4 { get; set; }
        public string Answer5 { get; set; }
        public string Description { get; set; }
        public bool PictureRequired { get; set; }
        public int CorrectAnswer { get; set;
        }
    }
}
