using System;
using IRT.ModelParameters;
using IRT.ProbabilityFunctions;

namespace IRT
{
    public class ProbabilityFunctionFactory
    {
        public IProbabilityFunction Build(IModelParameters parameters)
        {
            if (parameters.GetType() == typeof (ThreeParamModelParameters))
            {
                return new ThreeParamProbabilityFunction((ThreeParamModelParameters)parameters);
            }
            if (parameters.GetType() == typeof(TwoParamModelParameters))
            {
                return new TwoParamProbabilityFunction((TwoParamModelParameters)parameters);
            }

            throw new NotImplementedException();
        }

    }
}
