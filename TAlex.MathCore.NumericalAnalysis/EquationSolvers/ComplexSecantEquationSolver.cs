using System;


namespace TAlex.MathCore.NumericalAnalysis.EquationSolvers
{
    /// <summary>
    /// Represents the solver of equation of a complex variable that uses the secand algorithm.
    /// </summary>
    /// <remarks>
    /// Secant method is a root-finding algorithm that uses a succession
    /// of roots of secant lines to better approximate a root of a function.
    /// </remarks>
    public class ComplexSecantEquationSolver : ComplexInitialGuessEquationSolver
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the ComplexSecantEquationSolver class.
        /// </summary>
        public ComplexSecantEquationSolver()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the ComplexSecantEquationSolver class
        /// with the specified target function and initial guess for the root.
        /// </summary>
        /// <param name="function">A delegate that specifies the target function.</param>
        /// <param name="initialGuess">The initial guess for the root.</param>
        public ComplexSecantEquationSolver(Func<Complex, Complex> function, Complex initialGuess)
            : base(function, initialGuess)
        {
        }

        /// <summary>
        /// Initializes a new instance of the ComplexSecantEquationSolver class
        /// with the specified target function, initial guess for the root and tolerance.
        /// </summary>
        /// <param name="function">A delegate that specifies the target function.</param>
        /// <param name="initialGuess">The initial guess for the root.</param>
        /// <param name="tolerance">The tolerance used in the convergence test.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// tolerance must be non negative.
        /// </exception>
        public ComplexSecantEquationSolver(Func<Complex, Complex> function, Complex initialGuess, double tolerance)
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
        public override Complex Solve()
        {
            if (Complex.Abs(Function(InitialGuess)) <= Tolerance)
            {
                IterationsNeeded = 0;
                return InitialGuess;
            }

            Func<Complex, Complex> func = Function;

            Complex a = InitialGuess;
            Complex b = a + 2.0 * Tolerance * ((Complex.Abs(a) > 1.0) ? a : 1.0);

            for (int i = 0; i < MaxIterations; i++)
            {
                Complex x = b - func(b) * (b - a) / (func(b) - func(a));

                if (Complex.Abs(func(x)) <= Tolerance)
                {
                    IterationsNeeded = i + 1;
                    return x;
                }

                a = b;
                b = x;
            }

            IterationsNeeded = -1;
            throw new NotConvergenceException();
        }

        #endregion
    }
}
