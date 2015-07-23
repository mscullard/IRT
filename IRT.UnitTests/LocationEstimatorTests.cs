using System;
using System.Collections.Generic;
using System.Linq;
using IRT.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IRT.UnitTests
{
    [TestClass]
    // Tests use setup on page 379 of Ayala.  Note that since we are using fewer questions, the questions numbers do not match.
    public class LocationEstimatorTests
    {
        private double Tolerance = 1e-3;

        private QuestionLoader _questionLoader = new QuestionLoader("C:\\IRT\\ModelParameters.txt");
        private AnswerSheetLoader _answerSheetLoader = new AnswerSheetLoader("C:\\IRT\\MockScoreSheet.txt");
        private AnswerSheetLoader _answerSheetLoader3 = new AnswerSheetLoader("C:\\IRT\\MockScoreSheet3.txt");
        private AnswerSheetLoader _answerSheetLoader4 = new AnswerSheetLoader("C:\\IRT\\MockScoreSheet4.txt");

        [TestMethod]
        public void EstimatePersonLocation_DataFromAyala_ReturnsCorrectFinalTheta()
        {
            LocationEstimator locationEstimator = new LocationEstimator(_questionLoader, _answerSheetLoader);
            const int maxNumQuestions = 24;
            List<QuestionInfo> questionHistory = locationEstimator.EstimatePersonLocation(maxNumQuestions);

            double estimatedTheta = (double) questionHistory.Last().ThetaEstimate;
            Assert.IsTrue(Math.Abs(estimatedTheta - 1.098) < Tolerance);
        }

        [TestMethod]
        public void EstimatePersonLocation_DataFromAyala_ReturnsCorrectFinalSee()
        {
            LocationEstimator locationEstimator = new LocationEstimator(_questionLoader, _answerSheetLoader);
            const int maxNumQuestions = 24;
            List<QuestionInfo> questionHistory = locationEstimator.EstimatePersonLocation(maxNumQuestions);

            double estimatedSee = (double)questionHistory.Last().SEE;
            Assert.IsTrue(Math.Abs(estimatedSee - .192) < Tolerance);
        }

        [TestMethod]
        public void EstimatePersonLocation_DataFromAyala_ReturnsCorrectFinalInfo()
        {
            LocationEstimator locationEstimator = new LocationEstimator(_questionLoader, _answerSheetLoader);
            const int maxNumQuestions = 24;
            List<QuestionInfo> questionHistory = locationEstimator.EstimatePersonLocation(maxNumQuestions);

            double estimatedInformation = (double)questionHistory.Last().Information;
            Assert.IsTrue(Math.Abs(estimatedInformation - .901) < Tolerance);
        }

        [TestMethod]
        public void EstimatePersonLocation_DataFromAyala_ReturnsCorrectFinalQuestion()
        {
            LocationEstimator locationEstimator = new LocationEstimator(_questionLoader, _answerSheetLoader);
            const int maxNumQuestions = 24;
            List<QuestionInfo> questionHistory = locationEstimator.EstimatePersonLocation(maxNumQuestions);

            int finalQuestion = questionHistory.Last().Question.QuestionNumber;
            Assert.IsTrue(finalQuestion == 205);
        }

        [TestMethod]
        public void EstimatePersonLocation_CompletesAnswerSheet3()
        {
            try
            {
                LocationEstimator locationEstimator = new LocationEstimator(_questionLoader, _answerSheetLoader3);
                List<QuestionInfo> questionHistory = locationEstimator.EstimatePersonLocation(24);
 
                Assert.IsTrue(true);
            }
            catch(Exception e)
            {
                Assert.IsTrue(false);
            }
        }
 
        [TestMethod]
        public void EstimatePersonLocation_CompletesAnswerSheet4()
        {
            try
            {
                LocationEstimator locationEstimator = new LocationEstimator(_questionLoader, _answerSheetLoader4);
                List<QuestionInfo> questionHistory = locationEstimator.EstimatePersonLocation(24);
 
                Assert.IsTrue(true);
            }
            catch (Exception e)
            {
                Assert.IsTrue(false);
            }
        }
    }
}