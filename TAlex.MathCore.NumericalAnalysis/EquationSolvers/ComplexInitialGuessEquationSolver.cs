using System;


namespace TAlex.MathCore.NumericalAnalysis.EquationSolvers
{
    /// <summary>
    /// Represents the abstract base class for classes implementing
    /// algorithms for finding roots of nonlinear equations
    /// of a complex variable for a given initial guess for the root.
    /// </summary>
    public abstract class ComplexInitialGuessEquationSolver : ComplexEquationSolver
    {
        #region Fields

        private Complex _x0;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the initial value for the algorithm.
        /// </summary>
        public Complex InitialGuess
        {
            get
            {
                return _x0;
            }

            set
            {
                _x0 = value;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the ComplexInitialGuessEquationSolver class.
        /// </summary>
        protected ComplexInitialGuessEquationSolver()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the ComplexInitialGuessEquationSolver class
        /// with the specified target function and initial guess for the root.
        /// </summary>
        /// <param name="function">A delegate that specifies the target function.</param>
        /// <param name="initialGuess">The initial guess for the root.</param>
        protected ComplexInitialGuessEquationSolver(Function1Complex function, Complex initialGuess)
            : base(function)
        {
            _x0 = initialGuess;
        }

        /// <summary>
        /// Initializes a new instance of the ComplexInitialGuessEquationSolver class
        /// with the specified target function, initial guess for the root and tolerance.
        /// </summary>
        /// <param name="function">A delegate that specifies the target function.</param>
        /// <param name="initialGuess">The initial guess for the root.</param>
        /// <param name="tolerance">The tolerance used in the convergence test.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// tolerance must be non negative.
        /// </exception>
        protected ComplexInitialGuessEquationSolver(Function1Complex function, Complex initialGuess, double tolerance)
            : base(function, tolerance)
        {
            _x0 = initialGuess;
        }

        #endregion
    }
}
