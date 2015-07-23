using System;
using IRT.Mathematics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IRT.UnitTests
{
    [TestClass]
    public class FiniteDifferencerTests
    {
        private double Tolerance = 1e-4;

        [TestMethod]
        public void ApproximateDerivative_SimpleQuadraticAtZeroDerivative_ReturnsApproxDerivativeWithinTolerance()
        {
            OneDimensionalFunction f = x => x*x;

            FiniteDifferencer finiteDifferencer = new FiniteDifferencer(f);
            var approxDerivative = finiteDifferencer.ApproximateDerivative(0);

            double expectedDerivative = 0;
            CheckWithinTolerance(expectedDerivative, approxDerivative);
        }

        [TestMethod]
        public void ApproximateDerivative_SimpleQuadraticAtNonZeroDerivative_ReturnsApproxDerivativeWithinTolerance()
        {
            OneDimensionalFunction f = x => x * x;

            FiniteDifferencer finiteDifferencer = new FiniteDifferencer(f);
            var approxDerivative = finiteDifferencer.ApproximateDerivative(.5);

            double expectedDerivative = 2*.5;
            CheckWithinTolerance(expectedDerivative, approxDerivative);
        }

        [TestMethod]
        public void ApproximateDerivative_CosAtZeroDerivative_ReturnsApproxDerivativeWithinTolerance()
        {
            OneDimensionalFunction f = Math.Cos;

            FiniteDifferencer finiteDifferencer = new FiniteDifferencer(f);
            var approxDerivative = finiteDifferencer.ApproximateDerivative(0);

            double expectedDerivative = 0;
            CheckWithinTolerance(expectedDerivative, approxDerivative);
        }

        [TestMethod]
        public void ApproximateDerivative_CosAtNonZeroDerivative_ReturnsApproxDerivativeWithinTolerance()
        {
            OneDimensionalFunction f = Math.Cos;

            FiniteDifferencer finiteDifferencer = new FiniteDifferencer(f);
            double x = 1;
            var approxDerivative = finiteDifferencer.ApproximateDerivative(x);

            double expectedDerivative = -Math.Sin(x);
            CheckWithinTolerance(expectedDerivative, approxDerivative);
        }

        private void CheckWithinTolerance(double expectedValue, double approxDerivative)
        {
            Assert.IsTrue(Math.Abs(approxDerivative - expectedValue) < Tolerance);
        }
    }
}
