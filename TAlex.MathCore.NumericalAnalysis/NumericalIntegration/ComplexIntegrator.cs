using System;


namespace TAlex.MathCore.NumericalAnalysis.NumericalIntegration
{
    /// <summary>
    /// Represents the abstract base class for classes implementing algorithms of numerical integration.
    /// </summary>
    public abstract class ComplexIntegrator
    {
        #region Fields

        private double _lowerBound;

        private double _upperBound;

        private Function1Complex _integrand;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the complex function to integrate.
        /// </summary>
        public Function1Complex Integrand
        {
            get
            {
                return _integrand;
            }

            set
            {
                _integrand = value;
            }
        }

        /// <summary>
        /// Gets or sets the lower integration limit.
        /// </summary>
        public double LowerBound
        {
            get
            {
                return _lowerBound;
            }

            set
            {
                _lowerBound = value;
            }
        }

        /// <summary>
        /// Gets or sets the upper integration limit.
        /// </summary>
        public double UpperBound
        {
            get
            {
                return _upperBound;
            }

            set
            {
                _upperBound = value;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the ComplexIntegrator class.
        /// </summary>
        protected ComplexIntegrator()
        {
        }

        /// <summary>
        /// Initializes a new instance of the ComplexIntegrator class.
        /// </summary>
        /// <param name="integrand">A complex function to integrate of one variable.</param>
        /// <param name="lowerBound">The lower integration limit.</param>
        /// <param name="upperBound">The upper integration limit.</param>
        protected ComplexIntegrator(Function1Complex integrand, double lowerBound, double upperBound)
        {
            _integrand = integrand;
            _lowerBound = lowerBound;
            _upperBound = upperBound;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns the numerical value of the definite integral complex function of one variable.
        /// </summary>
        /// <returns>Approximate value of the definite integral.</returns>
        public abstract Complex Integrate();

        /// <summary>
        /// Returns the numerical value of the definite integral complex function of one variable.
        /// </summary>
        /// <param name="lowerBound">The lower integration limit.</param>
        /// <param name="upperBound">The upper integration limit.</param>
        /// <returns>Approximate value of the definite integral.</returns>
        public Complex Integrate(double lowerBound, double upperBound)
        {
            _lowerBound = lowerBound;
            _upperBound = upperBound;

            return Integrate();
        }

        /// <summary>
        /// Returns the numerical value of the definite integral complex function of one variable.
        /// </summary>
        /// <param name="integrand">A complex function to integrate of one variable.</param>
        /// <param name="lowerBound">The lower integration limit.</param>
        /// <param name="upperBound">The upper integration limit.</param>
        /// <returns>Approximate value of the definite integral.</returns>
        public Complex Integrate(Function1Complex integrand, double lowerBound, double upperBound)
        {
            _lowerBound = lowerBound;
            _upperBound = upperBound;

            _integrand = integrand;

            return Integrate();
        }

        #endregion
    }
}
