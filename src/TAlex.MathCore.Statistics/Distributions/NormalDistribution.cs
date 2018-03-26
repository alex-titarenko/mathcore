using System;


namespace TAlex.MathCore.Statistics.Distributions
{
    /// <summary>
    /// Represents the normal distribution.
    /// </summary>
    public class NormalDistribution : Distribution
    {
        #region Fields

        private double _mean;
        private double _stdev;

        /// <summary>
        /// Represents the standard normal distribution.
        /// </summary>
        public static readonly NormalDistribution Standard = new NormalDistribution(0.0, 1.0);

        #endregion

        #region Properties

        /// <summary>
        /// Gets the mean of the distribution.
        /// </summary>
        public override double Mean
        {
            get
            {
                return _mean;
            }
        }

        /// <summary>
        /// Gets the variance of the distribution.
        /// </summary>
        public override double Variance
        {
            get
            {
                return _stdev * _stdev;
            }
        }

        /// <summary>
        /// Gets the standard deviation of the distribution.
        /// </summary>
        public override double StandardDeviation
        {
            get
            {
                return _stdev;
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
                return 0.0;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the NormalDistribution class
        /// with mean is equal to zero and standard deviation is equal to one.
        /// </summary>
        public NormalDistribution() : this(0.0, 1.0)
        {
        }

        /// <summary>
        /// Initializes a new instance of the NormalDistribution class
        /// with specified mean and standard deviation.
        /// </summary>
        /// <param name="mean">The mean of the distribution.</param>
        /// <param name="standardDeviation">The standard deviation of the distribution.</param>
        /// <exception cref="System.ArgumentException">
        /// standardDeviation is less than or equal to zero.
        /// </exception>
        public NormalDistribution(double mean, double standardDeviation)
        {
            if (standardDeviation <= 0.0)
            {
                throw new ArgumentException("Standard deviation must be greater than zero.");
            }

            _mean = mean;
            _stdev = standardDeviation;
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
            double var = _stdev * _stdev;
            return (1.0 / Math.Sqrt(ExMath.TwoPi * var)) * Math.Exp(-ExMath.Pow(x - _mean, 2) / (2 * var));
        }

        /// <summary>
        /// Returns the value of the cumulative distribution function for the specified value.
        /// </summary>
        /// <param name="x">A real number.</param>
        /// <returns>The value of the cumulative distribution function for x.</returns>
        public override double CumulativeDistributionFunction(double x)
        {
            double t = (x - _mean) / (ExMath.Sqrt2 * _stdev);
            return 0.5 * (1.0 + SpecialFunctions.ProbabilityIntegrals.Erf(t));
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
            double x, y, s;

            do
            {
                x = random.NextDouble() * 2.0 - 1.0;
                y = random.NextDouble() * 2.0 - 1.0;
                s = x * x + y * y;
            } while (s >= 1.0 || s == 0.0);

            double z = x * Math.Sqrt(-2.0 * Math.Log(s) / s);

            return _mean + z * _stdev;
        }

        #endregion
    }
}
