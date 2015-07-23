using System;
using System.Collections.Generic;
using System.Linq;
using IRT.Data;
using IRT.ModelParameters;
using IRT.ProbabilityFunctions;

namespace IRT
{
    public class StandardErrorOfEstimateCalculator
    {
        private readonly ProbabilityFunctionFactory _probabilityFunctionFactory;

        public StandardErrorOfEstimateCalculator()
        {
            _probabilityFunctionFactory = new ProbabilityFunctionFactory();
        }

        // The standard error of estimate (SEE) for the location parameter theta.  The general formula is given by (2.11) on page 28.  
        public double Calculate(List<QuestionInfo> questionHistory, double theta)
        {
            List<IModelParameters> modelParametersList = questionHistory.Select(x => x.Question.ModelParameters).ToList();
            List<int> responseVector = questionHistory.Select(x => (int) x.Score).ToList();


            double sum = 0;
            for (int i = 0; i < responseVector.Count; i++)
            {
                IModelParameters modelParameters = modelParametersList[i];
                IProbabilityFunction probabilityFunction = _probabilityFunctionFactory.Build(modelParameters);

                double p = probabilityFunction.ProbabilityOfCorrectResponse(theta);
                double pPrime = probabilityFunction.FirstThetaDerivative(theta);

                sum += pPrime*pPrime/(p*(1 - p));
            }

            return 1/Math.Sqrt(sum);
        }
    }
}
