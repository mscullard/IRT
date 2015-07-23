namespace IRT.Mathematics
{
    // Class uses the finite difference method to approximate the derivative of the given function.  This is not as accurate as an analytic derivative but works for any function which is
    // differentiable and defined on the domain and does not require that one explicitly know the derivative.  This can be helpful for testing that derivative formulas have been derived 
    // and implemented correctly.
    public class FiniteDifferencer
    {
        private readonly OneDimensionalFunction _function;
        private readonly double delta = 1e-4;

        public FiniteDifferencer(OneDimensionalFunction function)
        {
            _function = function;
        }

        public double ApproximateDerivative(double x)
        {
            double x2 = x + delta;
            double x1 = x - delta;

            double deltaY = _function(x2) - _function(x1);
            double deltaX = x2 - x1;

            double approxDerivative = deltaY/deltaX;

            return approxDerivative;
        }
    }
}
