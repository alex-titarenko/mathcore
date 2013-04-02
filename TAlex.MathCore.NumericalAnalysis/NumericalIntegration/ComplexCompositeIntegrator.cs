using System;


namespace TAlex.MathCore.NumericalAnalysis.NumericalIntegration
{
    /// <summary>
    /// Represents the abstract base class for classes implementing algorithms of numerical composite integration.
    /// </summary>
    public abstract class ComplexCompositeIntegrator : ComplexIntegrator
    {
        #region Field

        private double _tolerance;
        private int _maxIterations;
        private int _iterationsNeeded;

        #endregion

        #region Properties

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
                return _tolerance;
            }

            set
            {
                if (value < 0.0)
                    throw new InvalidOperationException("The value of tolerance must be non negative.");

                _tolerance = value;
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
                return _maxIterations;
            }

            set
            {
                if (value < 1)
                    throw new InvalidOperationException("The maximum number of iterations must be greater than zero.");

                _maxIterations = value;
            }
        }

        /// <summary>
        /// Gets the number of iterations needed for the algorithm to achieve the desired accuracy.
        /// </summary>
        public int IterationsNeeded
        {
            get
            {
                return _iterationsNeeded;
            }

            protected set
            {
                _iterationsNeeded = value;
            }
        }

        #endregion
    }
}
