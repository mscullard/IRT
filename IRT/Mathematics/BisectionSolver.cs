using System;

namespace IRT.Mathematics
{
    // Alternative to Newton-Rhapson.  Slower, but may work when Newton-Rhapson does not.
    public class BisectionSolver : IRootSolver
    {
        private readonly OneDimensionalFunction _function;
        private readonly double _initialLowerBound;
        private readonly double _initialUpperBound;

        public readonly double Tolerance = 1e-5;
        public readonly int MaxIterations = 30;

        public BisectionSolver(OneDimensionalFunction function, double lowerBound, double upperBound)
        {
            _function = function;
            _initialLowerBound = lowerBound;
            _initialUpperBound = upperBound;
            ValidateBounds();
        }

        private void ValidateBounds()
        {
            double lower = _function(_initialLowerBound);
            double upper = _function(_initialUpperBound);

            if (SignsEqual(lower, upper))
            {
                throw new Exception("Cannot apply bisection root solver because the upper and lower bounds to use are the same sign.");
            }
        }

        // Returns true if both are positive or both are negative.  False if one or both is zero.
        private bool SignsEqual(double f1, double f2)
        {
            return f1*f2 > 0;
        }

        public double FindRoot()
        {
            double lowerBound = _initialLowerBound;
            double upperBound = _initialUpperBound;

            if (RootExistsOnBoundraries())
            {
                return GetBoundaryRoot(lowerBound, upperBound);
            }

            for (int i = 0; i < MaxIterations; i++)
            {
                double fLower = _function(lowerBound);
                double fUpper = _function(upperBound);

                double midPoint = (lowerBound + upperBound)/2;
                double fMid = _function(midPoint);
                if (IsRoot(midPoint))
                {
                    return midPoint;
                }

                if (SignsEqual(fMid, fLower))
                {
                    lowerBound = midPoint;
                }
                else
                {
                    upperBound = midPoint;
                }
            }

            throw new Exception("The bisection root finder was unable to find a root within " + MaxIterations + " iteraitons.");
        }

        private bool RootExistsOnBoundraries()
        {
            return IsRoot(_initialLowerBound) || IsRoot(_initialUpperBound);
        }

        private double GetBoundaryRoot(double lowerBound, double upperBound)
        {
            if (IsRoot(_initialLowerBound))
            {
                return lowerBound;
            }
            if (IsRoot(_initialUpperBound))
            {
                return upperBound;
            }

            throw new Exception("Call to get boundary root with boundary values which are not roots.");
        }

        private bool IsRoot(double x)
        {
            double y = _function(x);
            return Math.Abs(y) < Tolerance;
        }
    }
}
