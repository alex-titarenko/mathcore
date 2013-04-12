using System;


namespace TAlex.MathCore.NumericalAnalysis.EquationSolvers
{
    /// <summary>
    /// Represents the abstract base class for classes implementing
    /// algorithms for finding roots of nonlinear equations
    /// of a real variable for a given initial guess for the root.
    /// </summary>
    public abstract class InitialGuessEquationSolver : EquationSolver
    {
        #region Fields

        private double _x0;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the initial value for the algorithm.
        /// </summary>
        public double InitialGuess
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
        /// Initializes a new instance of the InitialGuessEquationSolver class.
        /// </summary>
        protected InitialGuessEquationSolver()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the InitialGuessEquationSolver class
        /// with the specified target function and initial guess for the root.
        /// </summary>
        /// <param name="function">A delegate that specifies the target function.</param>
        /// <param name="initialGuess">The initial guess for the root.</param>
        protected InitialGuessEquationSolver(Func<double, double> function, double initialGuess)
            : base(function)
        {
            _x0 = initialGuess;
        }

        /// <summary>
        /// Initializes a new instance of the InitialGuessEquationSolver class
        /// with the specified target function, initial guess for the root and tolerance.
        /// </summary>
        /// <param name="function">A delegate that specifies the target function.</param>
        /// <param name="initialGuess">The initial guess for the root.</param>
        /// <param name="tolerance">The tolerance used in the convergence test.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// tolerance must be non negative.
        /// </exception>
        protected InitialGuessEquationSolver(Func<double, double> function, double initialGuess, double tolerance)
            : base(function, tolerance)
        {
            _x0 = initialGuess;
        }

        #endregion
    }
}
