using System;


namespace TAlex.MathCore.Statistics.Distributions
{
    /// <summary>
    /// Represents the exponential distribution.
    /// </summary>
    public class ExponentialDistribution : Distribution
    {
        #region Fields

        private double _rate;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the mean of the distribution.
        /// </summary>
        public override double Mean
        {
            get
            {
                return 1.0 / _rate;
            }
        }

        /// <summary>
        /// Gets the variance of the distribution.
        /// </summary>
        public override double Variance
        {
            get
            {
                return 1.0 / (_rate * _rate);
            }
        }

        /// <summary>
        /// Gets the skewness of the distribution.
        /// </summary>
        public override double Skewness
        {
            get
            {
                return 2.0;
            }
        }

        /// <summary>
        /// Gets the kurtosis of the distribution.
        /// </summary>
        public override double Kurtosis
        {
            get
            {
                return 6.0;
            }
        }

        /// <summary>
        /// Gets the rate parameter of the distribution.
        /// </summary>
        public double Rate
        {
            get
            {
                return _rate;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the ExponentialDistribution class
        /// with the specified rate parameter.
        /// </summary>
        /// <param name="rate">The rate parameter of the distribution.</param>
        /// <exception cref="System.ArgumentException">
        /// rate is less than or equal to zero.
        /// </exception>
        public ExponentialDistribution(double rate)
        {
            if (rate <= 0.0)
            {
                throw new ArgumentException("The rate parameter must be greater than zero.");
            }

            _rate = rate;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns the value of the probability density function for the specified value.
        /// </summary>
        /// <param name="x">A real number within the domain of the distribution.</param>
        /// <returns>The value of the probability density function for x.</returns>
        public override double ProbabilityDensityFunction(double x)
        {
            if (x < 0.0)
            {
                return 0.0;
            }

            return _rate * Math.Exp(-_rate * x);
        }

        /// <summary>
        /// Returns the value of the cumulative distribution function for the specified value.
        /// </summary>
        /// <param name="x">A real number.</param>
        /// <returns>The value of the cumulative distribution function for x.</returns>
        public override double CumulativeDistributionFunction(double x)
        {
            if (x <= 0.0)
            {
                return 0.0;
            }

            return 1.0 - Math.Exp(-_rate * x);
        }

        /// <summary>
        /// Returns a random variable from the distribution.
        /// </summary>
        /// <param name="random">
        /// A <see cref="T:System.Random"/> object used to generate the random variable.
        /// </param>
        /// <returns>The random variable from the distribution.</returns>
        public override double GetRandomVariable(Random random)
        {
            return -Math.Log(1.0 - random.NextDouble()) / _rate;
        }

        #endregion
    }
}
