using System;
using System.Collections.Generic;
using System.Linq;
using IRT.ModelParameters;

namespace IRT.ThetaEstimation
{
    public class LogLikelihoodFunction
    {
        private readonly List<IModelParameters> _modelParametersList;
        private readonly ProbabilityFunctionFactory _probabilityFunctionFactory;

        public LogLikelihoodFunction(List<IModelParameters> modelParametersList)
        {
            _modelParametersList = modelParametersList;
            _probabilityFunctionFactory = new ProbabilityFunctionFactory();
        }

        // See A.2 on page 347 of Ayala
        public double LogLikelihood(List<int> responseVector, double theta)
        {
            ValidateResponseVector(responseVector);

            double sum = 0;
            for (int i = 0; i < responseVector.Count; i++)
            {
                var probabilityFunction = _probabilityFunctionFactory.Build(_modelParametersList[i]);
                double p = probabilityFunction.ProbabilityOfCorrectResponse(theta);

                var x = responseVector[i];
                sum += x*Math.Log(p) + (1 - x)*Math.Log(1 - p);
            }

            return sum;
        }

        private void ValidateResponseVector(List<int> responseVector)
        {
            if (responseVector.Any(i => i != 0 && i != 1))
            {
                throw new Exception("Reponse vector must consist of only 0's or 1's.");
            }
        }

        // First derivative of A.2 on page 347 of Ayala
        public double LogLikelihoodFirstDerivative(List<int> responseVector, double theta)
        {
            double sum = 0;
            for (int i = 0; i < responseVector.Count; i++)
            {
                var probabilityFunction = _probabilityFunctionFactory.Build(_modelParametersList[i]);

                double p = probabilityFunction.ProbabilityOfCorrectResponse(theta);
                double pPrime = probabilityFunction.FirstThetaDerivative(theta);

                var x = responseVector[i];
                sum += pPrime*(x/p - (1 - x)/(1 - p));
            }

            return sum;
        }

        // Second derivative of A.2 on page 347 of Ayala
        public double LogLikelihoodSecondDerivative(List<int> responseVector, double theta)
        {
            double sum = 0;
            for (int i = 0; i < responseVector.Count; i++)
            {
                var probabilityFunction = _probabilityFunctionFactory.Build(_modelParametersList[i]);

                double p = probabilityFunction.ProbabilityOfCorrectResponse(theta);
                double pPrime = probabilityFunction.FirstThetaDerivative(theta);
                double pDoublePrime = probabilityFunction.SecondThetaDerivative(theta);

                var x = responseVector[i];
                double term1 = pDoublePrime*(x/p - (1 - x)/(1 - p));
                double term2 = pPrime*pPrime*(-x/(p*p) - (1 - x)/((1 - p)*(1 - p)));
                sum += term1 + term2;
            }

            return sum;
        }
    }
}
