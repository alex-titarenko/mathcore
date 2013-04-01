using System;


namespace TAlex.MathCore.NumericalAnalysis.EquationSolvers
{
    /// <summary>
    /// Represents the solver of equation of a real variable that uses the Newton-Raphson algorithm.
    /// </summary>
    /// <remarks>
    /// Newton's method, also called the Newton-Raphson method, is a root-finding
    /// algorithm that uses the first few terms of the Taylor series
    /// of a function in the vicinity of a suspected root.
    /// </remarks>
    public class NewtonEquationSolver : InitialGuessEquationSolver
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the NewtonEquationSolver class.
        /// </summary>
        public NewtonEquationSolver()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the NewtonEquationSolver class
        /// with the specified target function and initial guess for the root.
        /// </summary>
        /// <param name="function">A delegate that specifies the target function.</param>
        /// <param name="initialGuess">The initial guess for the root.</param>
        public NewtonEquationSolver(Function1Real function, double initialGuess)
            : base(function, initialGuess)
        {
        }

        /// <summary>
        /// Initializes a new instance of the NewtonEquationSolver class
        /// with the specified target function, initial guess for the root and tolerance.
        /// </summary>
        /// <param name="function">A delegate that specifies the target function.</param>
        /// <param name="initialGuess">The initial guess for the root.</param>
        /// <param name="tolerance">The tolerance used in the convergence test.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// tolerance must be non negative.
        /// </exception>
        public NewtonEquationSolver(Function1Real function, double initialGuess, double tolerance)
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

            Function1Real func = Function;

            double x = InitialGuess;
            double xOld;

            for (int i = 0; i < MaxIterations; i++)
            {
                xOld = x;
                x = x - (func(x) * Tolerance) / (func(x + Tolerance) - func(x));

                if (Math.Abs(func(x)) <= Tolerance)
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
