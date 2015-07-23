namespace IRT.ModelParameters
{
    public class ThreeParamModelParameters : IModelParameters
    {
        public ThreeParamModelParameters(double alpha, double delta, double chi)
        {
            Alpha = alpha;
            Delta = delta;
            Chi = chi;
        }

        public double Alpha;
        public double Delta;
        public double Chi;
    }
}