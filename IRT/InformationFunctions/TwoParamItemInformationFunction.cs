using IRT.ModelParameters;
using IRT.ProbabilityFunctions;

namespace IRT.InformationFunctions
{
    public class TwoParamItemInformationFunction : IItemInformationFunction
    {
        private readonly IProbabilityFunction _twoParamProbabilityFunction;
        private readonly double _alpha;

        public TwoParamItemInformationFunction(TwoParamModelParameters modelParameters)
        {
            _alpha = modelParameters.Alpha;
            _twoParamProbabilityFunction = new TwoParamProbabilityFunction(modelParameters);
        }

        // The item information function for the two parameter model.  See formula (5.3) on page 102 of Ayala
        public double GetInformation(double theta)
        {
            double p = _twoParamProbabilityFunction.ProbabilityOfCorrectResponse(theta);

            return _alpha*_alpha*p*(1-p);
        }
    }
}
