using System;
using IRT.ModelParameters;

namespace IRT.ProbabilityFunctions
{
    public class ThreeParamProbabilityFunction : IProbabilityFunction
    {
        private readonly double _alpha;
        private readonly double _delta;
        private readonly double _chi;
        private TwoParamProbabilityFunction _twoParamProbabilityFunction;

        // The two parameter model corresponding to this three parameter model.  It uses the same alpha and delta but does not include the chi parameter.
        public TwoParamProbabilityFunction CorrespondingTwoParamProbFunction
        {
            get
            {
                if (_twoParamProbabilityFunction == null)
                {
                    TwoParamModelParameters parameters = new TwoParamModelParameters(_alpha, _delta);
                    _twoParamProbabilityFunction = new TwoParamProbabilityFunction(parameters);
                }

                return _twoParamProbabilityFunction;
            }
        }

        public ThreeParamProbabilityFunction(ThreeParamModelParameters parameters)
        {
            _alpha = parameters.Alpha;
            _delta = parameters.Delta;
            _chi = parameters.Chi;
        }

        public double ProbabilityOfCorrectResponse(double theta)
        {
            double exponential = Math.Exp(_alpha * (theta - _delta));
            double probability = _chi + (1 - _chi) * exponential / (1 + exponential);

            return probability;
        }

        public double FirstThetaDerivative(double theta)
        {
            double twoParamModelDeriv = CorrespondingTwoParamProbFunction.FirstThetaDerivative(theta);
            double derivative = (1 - _chi)*twoParamModelDeriv;

            return derivative;
        }

        public double SecondThetaDerivative(double theta)
        {
            double twoParamModelSecondDeriv = CorrespondingTwoParamProbFunction.SecondThetaDerivative(theta);
            double derivative = (1 - _chi) * twoParamModelSecondDeriv;

            return derivative;
        }
    }
}
