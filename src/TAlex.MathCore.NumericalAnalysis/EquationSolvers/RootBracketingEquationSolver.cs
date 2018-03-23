using System;


namespace TAlex.MathCore.NumericalAnalysis.EquationSolvers
{
    /// <summary>
    /// Represents the abstract base class for classes implementing
    /// algorithms for finding roots of nonlinear equations
    /// of a real variable for a given interval of localization of the root.
    /// </summary>
    public abstract class RootBracketingEquationSolver : EquationSolver
    {
        #region Fields

        private double _a;

        private double _b;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the lower bound of the interval.
        /// </summary>
        public double LowerBound
        {
            get
            {
                return _a;
            }

            set
            {
                _a = value;
            }
        }

        /// <summary>
        /// Gets or sets the upper bound of the interval.
        /// </summary>
        public double UpperBound
        {
            get
            {
                return _b;
            }

            set
            {
                _b = value;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the RootBracketingEquationSolver class.
        /// </summary>
        protected RootBracketingEquationSolver()
            : base()
        {
            _a = double.NaN;
            _b = double.NaN;
        }

        /// <summary>
        /// Initializes a new instance of the RootBracketingEquationSolver class
        /// with the specified target function and bracketing interval.
        /// </summary>
        /// <param name="function">A delegate that specifies the target function.</param>
        /// <param name="lowerBound">The lower bound of the interval.</param>
        /// <param name="upperBound">The upper bound of the interval.</param>
        protected RootBracketingEquationSolver(Func<double, double> function, double lowerBound, double upperBound)
            : base(function)
        {
            _a = lowerBound;
            _b = upperBound;
        }

        /// <summary>
        /// Initializes a new instance of the RootBracketingEquationSolver class
        /// with the specified target function, bracketing interval and tolerance.
        /// </summary>
        /// <param name="function">A delegate that specifies the target function.</param>
        /// <param name="lowerBound">The lower bound of the interval.</param>
        /// <param name="upperBound">The upper bound of the interval.</param>
        /// <param name="tolerance">The tolerance used in the convergence test.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// tolerance must be non negative.
        /// </exception>
        protected RootBracketingEquationSolver(Func<double, double> function, double lowerBound, double upperBound, double tolerance)
            : base(function, tolerance)
        {
            _a = lowerBound;
            _b = upperBound;
        }

        #endregion
    }
}
