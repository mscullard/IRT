using IRT.ModelParameters;
using IRT.ProbabilityFunctions;

namespace IRT.InformationFunctions
{
    public class ThreeParamItemInformationFunction : IItemInformationFunction
    {
        private readonly IProbabilityFunction _probabilityFunction;
        private readonly double _alpha;
        private readonly double _chi;

        public ThreeParamItemInformationFunction(ThreeParamModelParameters modelParameters)
        {
            _alpha = modelParameters.Alpha;
            _chi = modelParameters.Chi;

            _probabilityFunction = new ThreeParamProbabilityFunction(modelParameters);
        }

        // The item information function for the three parameter model.  See formula (6.12) on page 144 in Ayala.  Note that 'p_j' here represents 
        // the three parameter model probability of obtaining a correct answer
        public double GetInformation(double theta)
        {
            double p = _probabilityFunction.ProbabilityOfCorrectResponse(theta);
            double term1 = (p - _chi)/(1 - _chi);
            double term2 = (1 - p)/p;

            return _alpha*_alpha*term1*term1*term2;
        }
    }
}
