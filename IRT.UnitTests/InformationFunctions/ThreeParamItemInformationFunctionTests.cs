using System;
using IRT.InformationFunctions;
using IRT.ModelParameters;
using IRT.ProbabilityFunctions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IRT.UnitTests.InformationFunctions
{
    [TestClass]
    public class ThreeParamItemInformationFunctionTests
    {
        private const double Tolerance = 1e-3;

        [TestMethod]
        // Second line of the table on page 379 of Ayala.  The three parameter model matches two parameter model when chi = 0
        public void GetInformation_SecondValueLineFromAyala_ReturnsCorrectInfo()
        {
            ThreeParamModelParameters modelParameters = new ThreeParamModelParameters(2.954, .560, 0);
            ThreeParamItemInformationFunction informationFunction = new ThreeParamItemInformationFunction(modelParameters);
            const double theta = .3;
            var calculatedInformation = informationFunction.GetInformation(theta);

            const double expectedInfo = 1.889;
            Assert.AreEqual(expectedInfo, calculatedInformation, Tolerance);
        }

        [TestMethod]
        public void GetInformation_NonzeroChi_ReturnsCorrectInfo()
        {
            double alpha = 2;
            double chi = .5;
            double delta = 1;
            ThreeParamModelParameters modelParameters = new ThreeParamModelParameters(alpha, delta, chi);
            ThreeParamItemInformationFunction informationFunction = new ThreeParamItemInformationFunction(modelParameters);
            const double theta = .3;

            ThreeParamProbabilityFunction probabilityFunction = new ThreeParamProbabilityFunction(new ThreeParamModelParameters(alpha, delta, chi));
            double p = probabilityFunction.ProbabilityOfCorrectResponse(theta);
            double expectedInfo = alpha*alpha*Math.Pow((p - chi)/(1 - chi), 2)*((1 - p)/p);

            var calculatedInformation = informationFunction.GetInformation(theta);
            Assert.AreEqual(expectedInfo, calculatedInformation, Tolerance);
        }
    }
}
