using IRT.InformationFunctions;
using IRT.ModelParameters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IRT.UnitTests.InformationFunctions
{
    [TestClass]
    public class TwoParamItemInformationFunctionTests
    {
        private const double Tolerance = 1e-3;

        [TestMethod]
        // Second line of the table on page 379 of Ayala.
        public void GetInformation_SecondValueLineFromAyala_ReturnsCorrectInfo()
        {
            TwoParamModelParameters modelParameters = new TwoParamModelParameters(2.954, .560);
            TwoParamItemInformationFunction informationFunction = new TwoParamItemInformationFunction(modelParameters);
            const double theta = .3;
            var calculatedInformation = informationFunction.GetInformation(theta);

            const double expectedInfo = 1.889;
            Assert.AreEqual(expectedInfo, calculatedInformation, Tolerance);
        }
    }
}
