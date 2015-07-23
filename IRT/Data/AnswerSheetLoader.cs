using System;
using System.Collections.Generic;
using System.IO;

namespace IRT.Data
{
    public class AnswerSheetLoader
    {
        private readonly string _filename;

        public AnswerSheetLoader(string filename)
        {
            _filename = filename;
        }

        public Dictionary<int, int> LoadAnswerSheet()
        {
            Dictionary<int, int> scoreSheet = new Dictionary<int, int>();
            using (var reader = new StreamReader(_filename))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] words = line.Split(' ');
                    int questionNumber = Convert.ToInt32(words[0]);
                    int itemScore = Convert.ToInt32(words[1]);

                    scoreSheet[questionNumber] = itemScore;
                }
            }

            return scoreSheet;
        }
    }
}