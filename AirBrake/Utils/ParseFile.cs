using AirBrake.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;


namespace AirBrake.Utils
{
    public class ParseFile
    {
        public static DataTable parseFile(string fileName)
        {
            QuestionParser questionParser = new QuestionParser();
            List<Question> questions = new List<Question>();

            string path = Path.Combine(Environment.CurrentDirectory, @"QuestionFiles\", fileName);
            //Skipping first line since it's version block
            string[] lines = File.ReadAllLines(path).Skip(1).ToArray();

            QuestionPart questionPart = QuestionPart.StartOfFile;

            foreach(var line in lines)
            {
                if(line != string.Empty || (line == string.Empty && questionPart != QuestionPart.QuestionDescription))
                {
                    questionPart = questionParser.identifyQuestionPart(line, questionPart);
                    questionParser.parseLine(line, questionPart);

                    if(questionPart == QuestionPart.QuestionDescription)
                    {
                        questions.Add(questionParser.ParsedQuestion);
                        questionParser.resetQuestion();
                    }
                }
            }

            return CreateDataTable(questions);
        }

        //From https://stackoverflow.com/questions/18746064/using-reflection-to-create-a-datatable-from-a-class/18746525
        public static DataTable CreateDataTable<T>(IEnumerable<T> list)
        {
            Type type = typeof(T);
            var properties = type.GetProperties();

            DataTable dataTable = new DataTable();
            foreach (PropertyInfo info in properties)
            {
                dataTable.Columns.Add(new DataColumn(info.Name, Nullable.GetUnderlyingType(info.PropertyType) ?? info.PropertyType));
            }

            foreach (T entity in list)
            {
                object[] values = new object[properties.Length];
                for (int i = 0; i < properties.Length; i++)
                {
                    values[i] = properties[i].GetValue(entity);
                }

                dataTable.Rows.Add(values);
            }

            return dataTable;
        }
    }
}
