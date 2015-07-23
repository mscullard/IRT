using System;
using System.Collections.Generic;
using System.IO;
using IRT.ModelParameters;

namespace IRT.Data
{
    public class QuestionLoader
    {
        private readonly string _filename;

        public QuestionLoader(string filename)
        {
            _filename = filename;
        }

        public List<Question> LoadQuestions()
        {
            List<Question> questions = new List<Question>();
            using (var reader = new StreamReader(_filename))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var question = GetNextQuestion(line);

                    questions.Add(question);
                }
            }

            return questions;
        }

        private static Question GetNextQuestion(string line)
        {
            string[] words = line.Split(' ');
            int questionNumber = Convert.ToInt32(words[0]);

            IModelParameters modelParameters;
            if (words.Length == 4)
            {
                double alpha = Convert.ToDouble(words[1]);
                double delta = Convert.ToDouble(words[2]);
                double chi = Convert.ToDouble(words[3]);
                modelParameters = new ThreeParamModelParameters(alpha, delta, chi);
            }
            else if (words.Length == 3)
            {
                double alpha = Convert.ToDouble(words[1]);
                double delta = Convert.ToDouble(words[2]);
                modelParameters = new TwoParamModelParameters(alpha, delta);
            }
            else
            {
                throw new NotImplementedException();
            }

            Question question = new Question()
            {
                ModelParameters = modelParameters,
                QuestionNumber = questionNumber
            };
            return question;
        }
    }
}