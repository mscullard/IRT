using System;
using IRT.ModelParameters;

namespace IRT.ProbabilityFunctions
{
    // OneDimensionalFunction supplies the probability of obtaining a correct score on an item 
    public class TwoParamProbabilityFunction : IProbabilityFunction
    {
        private readonly double _delta;
        private readonly double _alpha;

        public TwoParamProbabilityFunction(TwoParamModelParameters parameters)
        {
            _alpha = parameters.Alpha;
            _delta = parameters.Delta;
        }

        public double ProbabilityOfCorrectResponse(double theta)
        {
            double exponential = Math.Exp(_alpha*(theta - _delta));
            double probability = exponential/(1 + exponential);

            return probability;
        }

        public double FirstThetaDerivative(double theta)
        {
            double p = ProbabilityOfCorrectResponse(theta);
            double derivative = _alpha*p*(1 - p);

            return derivative;
        }

        public double SecondThetaDerivative(double theta)
        {
            double p = ProbabilityOfCorrectResponse(theta);
            double firstDerivative = FirstThetaDerivative(theta);
            double derivative = _alpha * firstDerivative * (1 - 2*p);

            return derivative;
        }
    }
}
