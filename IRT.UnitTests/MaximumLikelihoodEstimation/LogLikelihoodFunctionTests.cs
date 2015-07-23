using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IRT.Mathematics;
using IRT.ModelParameters;
using IRT.ProbabilityFunctions;
using IRT.ThetaEstimation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IRT.UnitTests.MaximumLikelihoodEstimation
{
    [TestClass]
    public class LogLikelihoodFunctionTests
    {
        private double Tolerance = 1e-8;

        [TestMethod]
        public void LogLikelihood_SingleCorrectResponse_ReturnsCorrectValue()
        {
            double alpha = .3;
            double delta = .1;
            double chi = .2;
            ThreeParamModelParameters modelParameters = new ThreeParamModelParameters(alpha, delta, chi);
            List<IModelParameters> modelParameterList = new List<IModelParameters>();
            modelParameterList.Add(modelParameters);

            LogLikelihoodFunction logLikelihoodFunction = new LogLikelihoodFunction(modelParameterList);

            double theta = .4;
            List<int> responseVector = new List<int>() { 1 };
            double logLikelihood = logLikelihoodFunction.LogLikelihood(responseVector, theta);

            IProbabilityFunction probabilityFunction = new ThreeParamProbabilityFunction(modelParameters);
            double p = probabilityFunction.ProbabilityOfCorrectResponse(theta);
            double expectedLikelihood = Math.Log(p);
            Assert.AreEqual(expectedLikelihood, logLikelihood, Tolerance);
        }

        [TestMethod]
        public void LogLikelihood_SingleIncorrectResponse_ReturnsCorrectValue()
        {
            double alpha = .3;
            double delta = .1;
            double chi = .2;
            ThreeParamModelParameters modelParameters = new ThreeParamModelParameters(alpha, delta, chi);
            List<IModelParameters> modelParameterList = new List<IModelParameters>();
            modelParameterList.Add(modelParameters);

            LogLikelihoodFunction logLikelihoodFunction = new LogLikelihoodFunction(modelParameterList);

            double theta = .4;
            List<int> responseVector = new List<int>() { 0 };
            double logLikelihood = logLikelihoodFunction.LogLikelihood(responseVector, theta);

            IProbabilityFunction probabilityFunction = new ThreeParamProbabilityFunction(modelParameters);
            double p = probabilityFunction.ProbabilityOfCorrectResponse(theta);
            double expectedLikelihood = Math.Log(1 - p);
            Assert.AreEqual(expectedLikelihood, logLikelihood, Tolerance);
        }

        [TestMethod]
        public void LogLikelihood_TwoCorrectResponses_ReturnsCorrectValue()
        {
            double alpha1 = .3;
            double delta1 = .1;
            double chi1 = .2;
            ThreeParamModelParameters modelParameters1 = new ThreeParamModelParameters(alpha1, delta1, chi1);

            double alpha2 = .5;
            double delta2 = .6;
            double chi2 = .7;
            ThreeParamModelParameters modelParameters2 = new ThreeParamModelParameters(alpha2, delta2, chi2);

            List<IModelParameters> modelParameterList = new List<IModelParameters>();
            modelParameterList.Add(modelParameters1);
            modelParameterList.Add(modelParameters2);

            LogLikelihoodFunction logLikelihoodFunction = new LogLikelihoodFunction(modelParameterList);

            double theta = .4;
            List<int> responseVector = new List<int>() { 1, 1 };
            double logLikelihood = logLikelihoodFunction.LogLikelihood(responseVector, theta);

            IProbabilityFunction probabilityFunction1 = new ThreeParamProbabilityFunction(modelParameters1);
            double p1 = probabilityFunction1.ProbabilityOfCorrectResponse(theta);
            IProbabilityFunction probabilityFunction2 = new ThreeParamProbabilityFunction(modelParameters2);
            double p2 = probabilityFunction2.ProbabilityOfCorrectResponse(theta);
            double expectedLikelihood = Math.Log(p1) + Math.Log(p2);

            Assert.AreEqual(expectedLikelihood, logLikelihood, Tolerance);
        }

        [TestMethod]
        public void LogLikelihood_TwoIncorrectResponses_ReturnsCorrectValue()
        {
            double alpha1 = .3;
            double delta1 = .1;
            double chi1 = .2;
            ThreeParamModelParameters modelParameters1 = new ThreeParamModelParameters(alpha1, delta1, chi1);

            double alpha2 = .5;
            double delta2 = .6;
            double chi2 = .7;
            ThreeParamModelParameters modelParameters2 = new ThreeParamModelParameters(alpha2, delta2, chi2);

            List<IModelParameters> modelParameterList = new List<IModelParameters>();
            modelParameterList.Add(modelParameters1);
            modelParameterList.Add(modelParameters2);

            LogLikelihoodFunction logLikelihoodFunction = new LogLikelihoodFunction(modelParameterList);

            double theta = .4;
            List<int> responseVector = new List<int>() { 0, 0 };
            double logLikelihood = logLikelihoodFunction.LogLikelihood(responseVector, theta);

            IProbabilityFunction probabilityFunction1 = new ThreeParamProbabilityFunction(modelParameters1);
            double p1 = probabilityFunction1.ProbabilityOfCorrectResponse(theta);
            IProbabilityFunction probabilityFunction2 = new ThreeParamProbabilityFunction(modelParameters2);
            double p2 = probabilityFunction2.ProbabilityOfCorrectResponse(theta);
            double expectedLikelihood = Math.Log(1 -p1) + Math.Log(1 - p2);

            Assert.AreEqual(expectedLikelihood, logLikelihood, Tolerance);
        }

        [TestMethod]
        public void LogLikelihood_OneIncorrectResponseOneCorrectResponse_ReturnsCorrectValue()
        {
            double alpha1 = .3;
            double delta1 = .1;
            double chi1 = .2;
            ThreeParamModelParameters modelParameters1 = new ThreeParamModelParameters(alpha1, delta1, chi1);

            double alpha2 = .5;
            double delta2 = .6;
            double chi2 = .7;
            ThreeParamModelParameters modelParameters2 = new ThreeParamModelParameters(alpha2, delta2, chi2);

            List<IModelParameters> modelParameterList = new List<IModelParameters>();
            modelParameterList.Add(modelParameters1);
            modelParameterList.Add(modelParameters2);

            LogLikelihoodFunction logLikelihoodFunction = new LogLikelihoodFunction(modelParameterList);

            double theta = .4;
            List<int> responseVector = new List<int>() { 1, 0 };
            double logLikelihood = logLikelihoodFunction.LogLikelihood(responseVector, theta);

            IProbabilityFunction probabilityFunction1 = new ThreeParamProbabilityFunction(modelParameters1);
            double p1 = probabilityFunction1.ProbabilityOfCorrectResponse(theta);
            IProbabilityFunction probabilityFunction2 = new ThreeParamProbabilityFunction(modelParameters2);
            double p2 = probabilityFunction2.ProbabilityOfCorrectResponse(theta);
            double expectedLikelihood = Math.Log(p1) + Math.Log(1 - p2);

            Assert.AreEqual(expectedLikelihood, logLikelihood, Tolerance);
        }

        [TestMethod]
        public void LogLikelihoodDerivative_MultipleResponses_MatchesFiniteDifferenceDerivative()
        {
            double alpha1 = .3;
            double delta1 = .1;
            double chi1 = .2;
            ThreeParamModelParameters modelParameters1 = new ThreeParamModelParameters(alpha1, delta1, chi1);

            double alpha2 = .5;
            double delta2 = .6;
            double chi2 = .7;
            ThreeParamModelParameters modelParameters2 = new ThreeParamModelParameters(alpha2, delta2, chi2);

            double alpha3 = .1;
            double delta3 = .2;
            double chi3 = .4;
            ThreeParamModelParameters modelParameters3 = new ThreeParamModelParameters(alpha2, delta2, chi2);

            List<IModelParameters> modelParameterList = new List<IModelParameters>();
            modelParameterList.Add(modelParameters1);
            modelParameterList.Add(modelParameters2);
            modelParameterList.Add(modelParameters3);

            LogLikelihoodFunction logLikelihoodFunction = new LogLikelihoodFunction(modelParameterList);

            double theta = .4;
            List<int> responseVector = new List<int>() { 1, 0, 1 };
            OneDimensionalFunction function = x => logLikelihoodFunction.LogLikelihood(responseVector, x);
            FiniteDifferencer finiteDifferencer = new FiniteDifferencer(function);

            double logLikelihoodDerivative = logLikelihoodFunction.LogLikelihoodFirstDerivative(responseVector, theta);
            double finiteDifferenceDerivative = finiteDifferencer.ApproximateDerivative(theta);

            Assert.AreEqual(finiteDifferenceDerivative, logLikelihoodDerivative, Tolerance);
        }

        [TestMethod]
        public void LogLikelihoodSecondDerivative_MultipleResponses_MatchesFiniteDifferenceDerivative()
        {
            double alpha1 = .3;
            double delta1 = .1;
            double chi1 = .2;
            ThreeParamModelParameters modelParameters1 = new ThreeParamModelParameters(alpha1, delta1, chi1);

            double alpha2 = .5;
            double delta2 = .6;
            double chi2 = .7;
            ThreeParamModelParameters modelParameters2 = new ThreeParamModelParameters(alpha2, delta2, chi2);

            double alpha3 = .1;
            double delta3 = .2;
            double chi3 = .4;
            ThreeParamModelParameters modelParameters3 = new ThreeParamModelParameters(alpha2, delta2, chi2);

            List<IModelParameters> modelParameterList = new List<IModelParameters>();
            modelParameterList.Add(modelParameters1);
            modelParameterList.Add(modelParameters2);
            modelParameterList.Add(modelParameters3);

            LogLikelihoodFunction logLikelihoodFunction = new LogLikelihoodFunction(modelParameterList);

            double theta = .4;
            List<int> responseVector = new List<int>() { 1, 0, 1 };
            OneDimensionalFunction derivativeFunction = x => logLikelihoodFunction.LogLikelihoodFirstDerivative(responseVector, x);
            FiniteDifferencer finiteDifferencer = new FiniteDifferencer(derivativeFunction);

            double logLikelihoodDerivative = logLikelihoodFunction.LogLikelihoodSecondDerivative(responseVector, theta);
            double finiteDifferenceDerivative = finiteDifferencer.ApproximateDerivative(theta);

            Assert.AreEqual(finiteDifferenceDerivative, logLikelihoodDerivative, Tolerance);
        }
    }
}
