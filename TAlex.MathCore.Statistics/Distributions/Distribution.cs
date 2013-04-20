using System;
using System.Collections.Generic;


namespace TAlex.MathCore.Statistics.Distributions
{
    /// <summary>
    /// Represents the abstract base class for statistical distributions.
    /// </summary>
    public abstract class Distribution
    {
        #region Properties

        /// <summary>
        /// Gets the mean of the distribution.
        /// </summary>
        public abstract double Mean { get; }

        /// <summary>
        /// Gets the variance of the distribution.
        /// </summary>
        public abstract double Variance { get; }

        /// <summary>
        /// Gets the standard deviation of the distribution.
        /// </summary>
        public virtual double StandardDeviation
        {
            get
            {
                return Math.Sqrt(Variance);
            }
        }

        /// <summary>
        /// Gets the skewness of the distribution.
        /// </summary>
        public abstract double Skewness { get; }

        /// <summary>
        /// Gets the kurtosis of the distribution.
        /// </summary>
        public abstract double Kurtosis { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Returns the value of the probability density function for the specified value.
        /// </summary>
        /// <param name="x">A real number within the domain of the distribution.</param>
        /// <returns>The value of the probability density function for x.</returns>
        public abstract double ProbabilityDensityFunction(double x);

        /// <summary>
        /// Returns the value of the cumulative distribution function for the specified value.
        /// </summary>
        /// <param name="x">A real number.</param>
        /// <returns>The value of the cumulative distribution function for x.</returns>
        public abstract double CumulativeDistributionFunction(double x);

        /// <summary>
        /// Returns the probability that a random variable taken from the distribution
        /// lies within the specified interval.
        /// </summary>
        /// <param name="minValue">The lower bound of the interval.</param>
        /// <param name="maxValue">The upper bound of the interval.</param>
        /// <returns>
        /// The probability that a random variable taken from the distribution
        /// lies between minValue and maxValue.
        /// </returns>
        public double Probability(double minValue, double maxValue)
        {
            if (maxValue <= minValue)
            {
                return 0.0;
            }

            return (CumulativeDistributionFunction(maxValue) - CumulativeDistributionFunction(minValue));
        }

        /// <summary>
        /// Returns a random variable from the distribution.
        /// </summary>
        /// <param name="random">
        /// A <see cref="T:System.Random"/> object used to generate the random variable.
        /// </param>
        /// <returns>The random variable from the distribution.</returns>
        public abstract double GetRandomVariable(Random random);

        /// <summary>
        /// Fills an array of real numbers with random variables.
        /// </summary>
        /// <param name="random">
        /// A <see cref="T:System.Random"/> object used to generate the random variables.
        /// </param>
        /// <param name="v">An array of real numbers.</param>
        public void GetRandomVariables(Random random, IList<double> v)
        {
            for (int i = 0; i < v.Count; i++)
            {
                v[i] = GetRandomVariable(random);
            }
        }

        #endregion
    }
}