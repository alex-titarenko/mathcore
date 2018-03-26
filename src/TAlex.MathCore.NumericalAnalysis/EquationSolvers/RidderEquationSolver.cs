using System;


namespace TAlex.MathCore.NumericalAnalysis.EquationSolvers
{
    /// <summary>
    /// Represents the solver of equation of a real variable that uses the Ridder's algorithm.
    /// </summary>
    /// <remarks>
    /// Ridder's method is a root-finding algorithm based on
    /// the false position method and the use of an exponential function
    /// to successively approximate a root of a function.
    /// </remarks>
    public class RidderEquationSolver : RootBracketingEquationSolver
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the RidderEquationSolver class.
        /// </summary>
        public RidderEquationSolver()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the RidderEquationSolver class
        /// with the specified target function and bracketing interval.
        /// </summary>
        /// <param name="function">A delegate that specifies the target function.</param>
        /// <param name="lowerBound">The lower bound of the interval.</param>
        /// <param name="upperBound">The upper bound of the interval.</param>
        public RidderEquationSolver(Func<double, double> function, double lowerBound, double upperBound)
            : base(function, lowerBound, upperBound)
        {
        }

        /// <summary>
        /// Initializes a new instance of the RidderEquationSolver class
        /// with the specified target function, bracketing interval and tolerance.
        /// </summary>
        /// <param name="function">A delegate that specifies the target function.</param>
        /// <param name="lowerBound">The lower bound of the interval.</param>
        /// <param name="upperBound">The upper bound of the interval.</param>
        /// <param name="tolerance">The tolerance used in the convergence test.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// tolerance must be non negative.
        /// </exception>
        public RidderEquationSolver(Func<double, double> function, double lowerBound, double upperBound, double tolerance)
            : base(function, lowerBound, upperBound, tolerance)
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns the best approximation to the root of the nonlinear equation.
        /// </summary>
        /// <returns>The best approximation to the root.</returns>
        /// <exception cref="System.InvalidOperationException">
        /// The function values on the points LowerBound and UpperBound have the same signs.
        /// </exception>
        /// <exception cref="NotConvergenceException">
        /// The algorithm does not converged for a certain number of iterations.
        /// </exception>
        public override double Solve()
        {
            Func<double, double> f = Function;

            double x1 = LowerBound;
            double x2 = UpperBound;

            if (Math.Sign(f(x1)) == Math.Sign(f(x2)))
                throw new InvalidOperationException("The function values on the end points must be of opposite signs.");


            for (int i = 0; i < MaxIterations; i++)
            {
                double x3 = (x1 + x2) / 2.0;
                double x4 = x3 + (x3 - x1) * ((Math.Sign(f(x1) - f(x2)) * f(x3)) / Math.Sqrt(f(x3) * f(x3) - f(x1) * f(x2)));

                if (f(x1) * f(x4) < 0.0)
                    x2 = x4;
                else
                    x1 = x4;

                if (Math.Abs(f(x4)) <= Tolerance)
                {
                    IterationsNeeded = i + 1;
                    return x4;
                }
            }

            IterationsNeeded = -1;
            throw new NotConvergenceException();
        }

        #endregion
    }
}
