using System;


namespace TAlex.MathCore.Statistics.Distributions
{
    /// <summary>
    /// Represents the uniform distribution.
    /// </summary>
    public class UniformDistribution : Distribution
    {
        #region Fields

        private double _a;
        private double _b;

        /// <summary>
        /// Represents the standard uniform distribution.
        /// </summary>
        public static readonly UniformDistribution Standard = new UniformDistribution(0.0, 1.0);

        #endregion

        #region Properties

        /// <summary>
        /// Gets the mean of the distribution.
        /// </summary>
        public override double Mean
        {
            get
            {
                return (_a + _b) / 2.0;
            }
        }

        /// <summary>
        /// Gets the variance of the distribution.
        /// </summary>
        public override double Variance
        {
            get
            {
                double c = _b - _a;
                return (c * c) / 12.0;
            }
        }

        /// <summary>
        /// Gets the skewness of the distribution.
        /// </summary>
        public override double Skewness
        {
            get
            {
                return 0.0;
            }
        }

        /// <summary>
        /// Gets the kurtosis of the distribution.
        /// </summary>
        public override double Kurtosis
        {
            get
            {
                return -1.2;
            }
        }

        /// <summary>
        /// Gets the smallest value of the distribution.
        /// </summary>
        public double LowerBound
        {
            get
            {
                return _a;
            }
        }

        /// <summary>
        /// Gets the largest value of the distribution.
        /// </summary>
        public double UpperBound
        {
            get
            {
                return _b;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the UniformDistribution class
        /// with the unit interval.
        /// </summary>
        public UniformDistribution() : this(0.0, 1.0)
        {
        }

        /// <summary>
        /// Initializes a new instance of the UniformDistribution class
        /// with the specified interval.
        /// </summary>
        /// <param name="lowerBound">The lower bound of the interval.</param>
        /// <param name="upperBound">The upper bound of the interval.</param>
        /// <exception cref="System.ArgumentException">
        /// lowerBound greater than or equal upperBound.
        /// </exception>
        public UniformDistribution(double lowerBound, double upperBound)
        {
            if (lowerBound >= upperBound)
            {
                throw new ArgumentException("The lower bound should be less than the upper bound.");
            }

            _a = lowerBound;
            _b = upperBound;
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
            if (x >= _a && x <= _b)
                return 1.0 / (_b - _a);
            else
                return 0.0;
        }

        /// <summary>
        /// Returns the value of the cumulative distribution function for the specified value.
        /// </summary>
        /// <param name="x">A real number.</param>
        /// <returns>The value of the cumulative distribution function for x.</returns>
        public override double CumulativeDistributionFunction(double x)
        {
            if (x <= _a)
                return 0.0;
            else if (x < _b)
                return (x - _a) / (_b - _a);
            else
                return 1.0;
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
            return _a + random.NextDouble() * (_b - _a);
        }

        /// <summary>
        /// Returns a random variable from an uniform distribution
        /// on the interval with the lower bound zero and the specified upper bound.
        /// </summary>
        /// <param name="random">
        /// A <see cref="T:System.Random"/> object used to generate the random variable.
        /// </param>
        /// <param name="upperBound">The upper bound of the interval.</param>
        /// <returns>
        /// The random variable from an uniform distribution
        /// on the interval with the lower bound zero and the specified upper bound.
        /// </returns>
        public static double GetRandomVariable(Random random, double upperBound)
        {
            return (random.NextDouble() * upperBound);
        }

        /// <summary>
        /// Returns a random variable from an uniform distribution
        /// on the specified interval.
        /// </summary>
        /// <param name="random">
        /// A <see cref="T:System.Random"/> object used to generate the random variable.
        /// </param>
        /// <param name="lowerBound">The lower bound of the interval.</param>
        /// <param name="upperBound">The upper bound of the interval.</param>
        /// <returns>
        /// The random variable from an uniform distribution
        /// on the specified interval.
        /// </returns>
        /// <exception cref="System.ArgumentException">
        /// lowerBound greater than or equal upperBound.
        /// </exception>
        public static double GetRandomVariable(Random random, double lowerBound, double upperBound)
        {
            if (lowerBound >= upperBound)
            {
                throw new ArgumentException("The lower bound should be less than the upper bound.");
            }

            return lowerBound + random.NextDouble() * (upperBound - lowerBound);
        }

        #endregion
    }
}
