namespace IRT.ModelParameters
{
    public class TwoParamModelParameters : IModelParameters
    {
        public TwoParamModelParameters(double alpha, double delta)
        {
            Alpha = alpha;
            Delta = delta;
        }

        public double Alpha;
        public double Delta;
    }
}
