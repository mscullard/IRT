using System;

namespace IRT.Mathematics
{
    public class NewtonRhapsonSolver : IRootSolver
    {
        private readonly OneDimensionalFunction _function;
        private readonly OneDimensionalFunction _fDerivative;
        private readonly double _initialGuess;
        public readonly double Tolerance = 1e-8;
        public readonly int MaxIterations = 50;

        // Newton Rhapson root finder for one dimensional functions.
        public NewtonRhapsonSolver(OneDimensionalFunction function, OneDimensionalFunction fDerivative, double initialGuess)
        {
            _function = function;
            _fDerivative = fDerivative;
            _initialGuess = initialGuess;
        }

        // Assumes that a stationary point is not encountered in the search
        public double FindRoot()
        {
            double old_x = _initialGuess;
            for (int i = 0; i < MaxIterations; i++)
            {
                if (WithinTolerance(old_x))
                {
                    return old_x;
                }
                double derivative = _fDerivative(old_x);
                VerifyNotStationary(derivative);
                double x = old_x - _function(old_x)/derivative;
                old_x = x;
            }

            throw new Exception("The Newton-Rhapson solver was unable to find root within " + MaxIterations + " iterations.");
        }

        private void VerifyNotStationary(double derivative)
        {
            if (derivative == 0)
            {
                throw new Exception("The Newton-Rhapson solver encountered a stationary point while searching for a root.");
            }
        }

        private bool WithinTolerance(double x)
        {
            double y = _function(x);
            return Math.Abs(y) < Tolerance;
        }
    }
}
