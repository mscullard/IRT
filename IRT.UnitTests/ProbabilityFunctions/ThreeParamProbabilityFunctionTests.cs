﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IRT.Mathematics;
using IRT.ModelParameters;
using IRT.ProbabilityFunctions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IRT.UnitTests.ProbabilityFunctions
{
    [TestClass]
    public class ThreeParamProbabilityFunctionTests
    {
        private double Tolerance = 1e-8;

        [TestMethod]
        // In this case the exponent is zero
        public void ProbabilityOfCorrectReponse_DeltaEqualsTheta_ReturnsOneHalf()
        {
            double alpha = .2;
            double delta = .3;
            double chi = .4;
            ThreeParamModelParameters parameters = new ThreeParamModelParameters(alpha, delta, chi);
            ThreeParamProbabilityFunction probabilityFunction = new ThreeParamProbabilityFunction(parameters);

            double theta = delta;
            double p = probabilityFunction.ProbabilityOfCorrectResponse(theta);

            double expectedProbability = chi + (1 - chi)*.5;
            Assert.AreEqual(expectedProbability, p);
        }

        [TestMethod]
        public void ProbabilityOfCorrectReponse_DeltaNotEqualToTheta_ReturnsCorrectValue()
        {
            double alpha = .2;
            double delta = .3;
            double chi = .4;
            ThreeParamModelParameters parameters = new ThreeParamModelParameters(alpha, delta, chi);
            ThreeParamProbabilityFunction probabilityFunction = new ThreeParamProbabilityFunction(parameters);

            double theta = .1;
            double p = probabilityFunction.ProbabilityOfCorrectResponse(theta);

            double expectedValue = chi + (1 - chi)*Math.Exp(alpha * (theta - delta)) / (1 + Math.Exp(alpha * (theta - delta)));
            Assert.IsTrue(Math.Abs(expectedValue - p) < Tolerance);
        }

        [TestMethod]
        public void FirstThetaDerivative_NonTrivialInput_CloseToFiniteDifferenceValue()
        {
            double alpha = .2;
            double delta = .3;
            double chi = .4;
            ThreeParamModelParameters parameters = new ThreeParamModelParameters(alpha, delta, chi);
            ThreeParamProbabilityFunction probabilityFunction = new ThreeParamProbabilityFunction(parameters);
            FiniteDifferencer finiteDifferencer = new FiniteDifferencer(x => probabilityFunction.ProbabilityOfCorrectResponse(x));

            double theta = .1;
            double derivative = probabilityFunction.FirstThetaDerivative(theta);
            double approxDerivative = finiteDifferencer.ApproximateDerivative(theta);

            Assert.IsTrue(Math.Abs(derivative - approxDerivative) < Tolerance);
        }

        [TestMethod]
        public void SecondThetaDerivative_NonTrivialInput_CloseToFiniteDifferenceValue()
        {
            double alpha = .2;
            double delta = .3;
            double chi = .4;
            ThreeParamModelParameters parameters = new ThreeParamModelParameters(alpha, delta, chi);
            ThreeParamProbabilityFunction probabilityFunction = new ThreeParamProbabilityFunction(parameters);
            FiniteDifferencer finiteDifferencer = new FiniteDifferencer(x => probabilityFunction.FirstThetaDerivative(x));

            double theta = .1;
            double secondDerivative = probabilityFunction.SecondThetaDerivative(theta);
            double approxSecondDerivative = finiteDifferencer.ApproximateDerivative(theta);

            Assert.IsTrue(Math.Abs(secondDerivative - approxSecondDerivative) < Tolerance);
        }

    }
}
