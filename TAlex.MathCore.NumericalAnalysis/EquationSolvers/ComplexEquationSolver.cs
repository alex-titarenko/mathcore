using System;


namespace TAlex.MathCore.NumericalAnalysis.EquationSolvers
{
    /// <summary>
    /// Represents the abstract base class for classes implementing
    /// algorithms for finding roots of nonlinear equations of a complex variable.
    /// </summary>
    public abstract class ComplexEquationSolver
    {
        #region Fields

        private Function1Complex _func;

        private double _tol = 1E-9;

        private int _maxIters = 100;

        private int _itersNeeded = -1;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the target function.
        /// </summary>
        public Function1Complex Function
        {
            get
            {
                return _func;
            }

            set
            {
                _func = value;
            }
        }

        /// <summary>
        /// Gets or sets the tolerance used in the convergence test.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">
        /// Tolerance must be non negative.
        /// </exception>
        public double Tolerance
        {
            get
            {
                return _tol;
            }

            set
            {
                if (value < 0.0)
                    throw new InvalidOperationException("The value of tolerance must be non negative.");

                _tol = value;
            }
        }

        /// <summary>
        /// Gets or sets the maximum number of iterations.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">
        /// MaxIterations must be greater than zero.
        /// </exception>
        public int MaxIterations
        {
            get
            {
                return _maxIters;
            }

            set
            {
                if (value < 1)
                    throw new InvalidOperationException("The maximum number of iterations must be greater than zero.");

                _maxIters = value;
            }
        }

        /// <summary>
        /// Gets the number of iterations needed for the algorithm to achieve the desired accuracy.
        /// </summary>
        public int IterationsNeeded
        {
            get
            {
                return _itersNeeded;
            }

            internal set
            {
                _itersNeeded = value;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the ComplexEquationSolver class.
        /// </summary>
        protected ComplexEquationSolver()
        {
        }

        /// <summary>
        /// Initializes a new instance of the ComplexEquationSolver class with the specified target function.
        /// </summary>
        /// <param name="function">A delegate that specifies the target function.</param>
        protected ComplexEquationSolver(Function1Complex function)
        {
            _func = function;
        }

        /// <summary>
        /// Initializes a new instance of the ComplexEquationSolver class with the specified target function and tolerance.
        /// </summary>
        /// <param name="function">A delegate that specifies the target function.</param>
        /// <param name="tolerance">The tolerance used in the convergence test.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// tolerance must be non negative.
        /// </exception>
        protected ComplexEquationSolver(Function1Complex function, double tolerance)
        {
            if (tolerance < 0.0)
                throw new ArgumentOutOfRangeException("The value of tolerance must be non negative.");

            _func = function;
            _tol = tolerance;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns the best approximation to the root of the nonlinear equation.
        /// </summary>
        /// <returns>The best approximation to the root.</returns>
        public abstract Complex Solve();

        #endregion
    }
}
