using System;
using IRT.Mathematics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IRT.UnitTests.Mathematics
{
    [TestClass]
    public class NewtonRhapsonSolverTests
    {
        [TestMethod]
        public void FindRoot_PolynomialExactIntialGuess_ReturnsExactRoot()
        {
            OneDimensionalFunction f = x => x*x - 4;
            OneDimensionalFunction fPrime = x => 2*x;

            const double initialGuess = 2;
            NewtonRhapsonSolver solver = new NewtonRhapsonSolver(f, fPrime, initialGuess);
            
            var root = solver.FindRoot();

            Assert.AreEqual(initialGuess, root);
        }

        [TestMethod]
        public void FindRoot_QuadraticIncorrectIntialGuess_ReturnsRootWithinTolerance()
        {
            OneDimensionalFunction f = x => x * x;
            OneDimensionalFunction fPrime = x => 2 * x;

            const double initialGuess = 4;
            NewtonRhapsonSolver solver = new NewtonRhapsonSolver(f, fPrime, initialGuess);

            var root = solver.FindRoot();

            Assert.IsTrue(Math.Abs(f(root)) < solver.Tolerance);
        }

        [TestMethod]
        public void FindRoot_SinFunction_ReturnsRootWithinTolerance()
        {
            OneDimensionalFunction f = Math.Sin;
            OneDimensionalFunction fPrime = Math.Cos;

            const double initialGuess = .5;
            NewtonRhapsonSolver solver = new NewtonRhapsonSolver(f, fPrime, initialGuess);

            var root = solver.FindRoot();

            Assert.IsTrue(Math.Abs(f(root)) < solver.Tolerance);
        }

        [TestMethod]
        public void FindRoot_CosFunction_ReturnsRootWithinTolerance()
        {
            OneDimensionalFunction f = Math.Cos;
            OneDimensionalFunction fPrime = x => -Math.Sin(x);

            const double initialGuess = .5;
            NewtonRhapsonSolver solver = new NewtonRhapsonSolver(f, fPrime, initialGuess);

            var root = solver.FindRoot();

            Assert.IsTrue(Math.Abs(f(root)) < solver.Tolerance);
        }
    }
}
