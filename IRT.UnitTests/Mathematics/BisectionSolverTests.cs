using System;
using IRT.Mathematics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IRT.UnitTests.Mathematics
{
    [TestClass]
    public class BisectionSolverTests
    {
        [TestMethod]
        public void FindRoot_PolynomialLowerBoundIsZero_ReturnsExactRoot()
        {
            OneDimensionalFunction f = x => x * x - 4;

            double lowerBound = -2;
            double upperBound = 3;
            BisectionSolver solver = new BisectionSolver(f, lowerBound, upperBound);

            var root = solver.FindRoot();

            Assert.AreEqual(lowerBound, root);
        }

        [TestMethod]
        public void FindRoot_QuadraticIncorrectIntialGuess_ReturnsRootWithinTolerance()
        {
            OneDimensionalFunction f = x => x * x - 4;

            double lowerBound = -1;
            double upperBound = 3;
            BisectionSolver solver = new BisectionSolver(f, lowerBound, upperBound);

            var root = solver.FindRoot();

            Assert.IsTrue(Math.Abs(f(root)) < solver.Tolerance);
        }

        [TestMethod]
        public void FindRoot_SinFunction_ReturnsRootWithinTolerance()
        {
            OneDimensionalFunction f = Math.Sin;

            double lowerBound = -3*Math.PI / 4;
            double upperBound = Math.PI/2;
            BisectionSolver solver = new BisectionSolver(f, lowerBound, upperBound);

            var root = solver.FindRoot();

            Assert.IsTrue(Math.Abs(f(root)) < solver.Tolerance);
        }

        [TestMethod]
        public void FindRoot_CosFunction_ReturnsRootWithinTolerance()
        {
            OneDimensionalFunction f = Math.Cos;

            double lowerBound = -Math.PI/2;
            double upperBound = Math.PI;
            BisectionSolver solver = new BisectionSolver(f, lowerBound, upperBound);

            var root = solver.FindRoot();

            Assert.IsTrue(Math.Abs(f(root)) < solver.Tolerance);
        }
    }
}
