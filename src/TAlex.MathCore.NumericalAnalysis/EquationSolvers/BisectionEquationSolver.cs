using System;


namespace TAlex.MathCore.NumericalAnalysis.EquationSolvers
{
    /// <summary>
    /// Represents the solver of equation of a real variable that uses the bisection algorithm.
    /// </summary>
    /// <remarks>
    /// Bisection method is a root-finding algorithm which repeatedly bisects an interval
    /// then selects a subinterval in which a root must lie for further processing.
    /// It is a very simple and robust method, but it is also relatively slow.
    /// </remarks>
    public class BisectionEquationSolver : RootBracketingEquationSolver
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the BisectionEquationSolver class.
        /// </summary>
        public BisectionEquationSolver()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the BisectionEquationSolver class
        /// with the specified target function and bracketing interval.
        /// </summary>
        /// <param name="function">A delegate that specifies the target function.</param>
        /// <param name="lowerBound">The lower bound of the interval.</param>
        /// <param name="upperBound">The upper bound of the interval.</param>
        public BisectionEquationSolver(Func<double, double> function, double lowerBound, double upperBound)
            : base(function, lowerBound, upperBound)
        {
        }

        /// <summary>
        /// Initializes a new instance of the BisectionEquationSolver class
        /// with the specified target function, bracketing interval and tolerance.
        /// </summary>
        /// <param name="function">A delegate that specifies the target function.</param>
        /// <param name="lowerBound">The lower bound of the interval.</param>
        /// <param name="upperBound">The upper bound of the interval.</param>
        /// <param name="tolerance">The tolerance used in the convergence test.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// tolerance must be non negative.
        /// </exception>
        public BisectionEquationSolver(Func<double, double> function, double lowerBound, double upperBound, double tolerance)
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

            double a = LowerBound;
            double b = UpperBound;

            if (Math.Sign(f(a)) == Math.Sign(f(b)))
                throw new InvalidOperationException("The function values on the end points must be of opposite signs.");

            for (int i = 0; i < MaxIterations; i++)
            {
                double x = (a + b) / 2.0;

                if (Math.Sign(f(a)) != Math.Sign(f(x)))
                    b = x;
                else
                    a = x;

                if (Math.Abs(f(x)) <= Tolerance)
                {
                    IterationsNeeded = i + 1;
                    return x;
                }
            }

            IterationsNeeded = -1;
            throw new NotConvergenceException();
        }

        #endregion
    }
}
