using System;


namespace TAlex.MathCore.NumericalAnalysis.EquationSolvers
{
    /// <summary>
    /// Represents the solver of equation of a real variable that uses the Müller's algorithm.
    /// </summary>
    /// <remarks>
    /// Müller's method uses three points, constructs the parabola
    /// through these three points, and takes the intersection
    /// of the x-axis with the parabola to be the next approximation.
    /// </remarks>
    public class MullerEquationSolver : InitialGuessEquationSolver
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the MullerEquationSolver class.
        /// </summary>
        public MullerEquationSolver()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the MullerEquationSolver class
        /// with the specified target function and initial guess for the root.
        /// </summary>
        /// <param name="function">A delegate that specifies the target function.</param>
        /// <param name="initialGuess">The initial guess for the root.</param>
        public MullerEquationSolver(Function1Real function, double initialGuess)
            : base(function, initialGuess)
        {
        }

        /// <summary>
        /// Initializes a new instance of the MullerEquationSolver class
        /// with the specified target function, initial guess for the root and tolerance.
        /// </summary>
        /// <param name="function">A delegate that specifies the target function.</param>
        /// <param name="initialGuess">The initial guess for the root.</param>
        /// <param name="tolerance">The tolerance used in the convergence test.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// tolerance must be non negative.
        /// </exception>
        public MullerEquationSolver(Function1Real function, double initialGuess, double tolerance)
            : base(function, initialGuess, tolerance)
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns the best approximation to the root of the nonlinear equation.
        /// </summary>
        /// <returns>The best approximation to the root.</returns>
        /// <exception cref="NotConvergenceException">
        /// The algorithm does not converged for a certain number of iterations.
        /// </exception>
        public override double Solve()
        {
            if (Math.Abs(Function(InitialGuess)) <= Tolerance)
            {
                IterationsNeeded = 0;
                return InitialGuess;
            }

            Function1Real f = Function;

            double x0 = InitialGuess;
            double x1 = x0 + 2.0 * Tolerance * ((Math.Abs(x0) > 1.0) ? x0 : 1.0);
            double x2 = x1 + 2.0 * Tolerance * ((Math.Abs(x1) > 1.0) ? x1 : 1.0);

            for (int i = 0; i < MaxIterations; i++)
            {
                double q = (x2 - x1) / (x1 - x0);

                double A = q * f(x2) - q * (1 + q) * f(x1) + q * q * f(x0);
                double B = (2 * q + 1) * f(x2) - (1 + q) * (1 + q) * f(x1) + q * q * f(x0);
                double C = (1 + q) * f(x2);

                double D = Math.Sqrt(B * B - 4 * A * C);
                double den = (Math.Abs(B + D) >= Math.Abs(B - D)) ? B + D : B - D;

                double x3 = x2 - (x2 - x1) * ((2 * C) / den);

                if (Math.Abs(f(x3)) <= Tolerance)
                {
                    IterationsNeeded = i + 1;
                    return x3;
                }

                x0 = x1;
                x1 = x2;
                x2 = x3;
            }

            IterationsNeeded = -1;
            throw new NotConvergenceException();
        }

        #endregion
    }
}
