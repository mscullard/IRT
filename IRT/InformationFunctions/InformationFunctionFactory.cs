using System;
using IRT.ModelParameters;

namespace IRT.InformationFunctions
{
    public class InformationFunctionFactory
    {
        public IItemInformationFunction Build(IModelParameters modelParameters)
        {
            if (modelParameters.GetType() == typeof (TwoParamModelParameters))
            {
                return new TwoParamItemInformationFunction((TwoParamModelParameters) modelParameters);
            }
            if (modelParameters.GetType() == typeof(ThreeParamModelParameters))
            {
                return new ThreeParamItemInformationFunction((ThreeParamModelParameters) modelParameters);
            }

            throw new NotImplementedException();
        }
    }
}
