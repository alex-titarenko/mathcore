// <copyright file="Normal.cs" company="Math.NET">
// Math.NET Numerics, part of the Math.NET Project
// http://numerics.mathdotnet.com
// http://github.com/mathnet/mathnet-numerics
// http://mathnetnumerics.codeplex.com
//
// Copyright (c) 2009-2013 Math.NET
//
// Permission is hereby granted, free of charge, to any person
// obtaining a copy of this software and associated documentation
// files (the "Software"), to deal in the Software without
// restriction, including without limitation the rights to use,
// copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following
// conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
// </copyright>

using System;
using System.Collections.Generic;
using MathNet.Numerics.Properties;
using MathNet.Numerics.Random;

namespace MathNet.Numerics.Distributions
{
    /// <summary>
    /// Continuous Univariate Normal distribution, also known as Gaussian distribution.
    /// For details about this distribution, see
    /// <a href="http://en.wikipedia.org/wiki/Normal_distribution">Wikipedia - Normal distribution</a>.
    /// </summary>
    public class Normal : IContinuousDistribution
    {
        System.Random _random;

        readonly double _mean;
        readonly double _stdDev;

        /// <summary>
        /// Initializes a new instance of the Normal class. This is a normal distribution with mean 0.0
        /// and standard deviation 1.0. The distribution will
        /// be initialized with the default <seealso cref="System.Random"/> random number generator.
        /// </summary>
        public Normal()
            : this(0.0, 1.0)
        {
        }

        /// <summary>
        /// Initializes a new instance of the Normal class. This is a normal distribution with mean 0.0
        /// and standard deviation 1.0. The distribution will
        /// be initialized with the default <seealso cref="System.Random"/> random number generator.
        /// </summary>
        /// <param name="randomSource">The random number generator which is used to draw random samples.</param>
        public Normal(System.Random randomSource)
            : this(0.0, 1.0, randomSource)
        {
        }

        /// <summary>
        /// Initializes a new instance of the Normal class with a particular mean and standard deviation. The distribution will
        /// be initialized with the default <seealso cref="System.Random"/> random number generator.
        /// </summary>
        /// <param name="mean">The mean (μ) of the normal distribution.</param>
        /// <param name="stddev">The standard deviation (σ) of the normal distribution. Range: σ ≥ 0.</param>
        public Normal(double mean, double stddev)
        {
            if (!IsValidParameterSet(mean, stddev))
            {
                throw new ArgumentException(Resources.InvalidDistributionParameters);
            }

            _random = SystemRandomSource.Default;
            _mean = mean;
            _stdDev = stddev;
        }

        /// <summary>
        /// Initializes a new instance of the Normal class with a particular mean and standard deviation. The distribution will
        /// be initialized with the default <seealso cref="System.Random"/> random number generator.
        /// </summary>
        /// <param name="mean">The mean (μ) of the normal distribution.</param>
        /// <param name="stddev">The standard deviation (σ) of the normal distribution. Range: σ ≥ 0.</param>
        /// <param name="randomSource">The random number generator which is used to draw random samples.</param>
        public Normal(double mean, double stddev, System.Random randomSource)
        {
            if (!IsValidParameterSet(mean, stddev))
            {
                throw new ArgumentException(Resources.InvalidDistributionParameters);
            }

            _random = randomSource ?? SystemRandomSource.Default;
            _mean = mean;
            _stdDev = stddev;
        }

        /// <summary>
        /// Constructs a normal distribution from a mean and standard deviation.
        /// </summary>
        /// <param name="mean">The mean (μ) of the normal distribution.</param>
        /// <param name="stddev">The standard deviation (σ) of the normal distribution. Range: σ ≥ 0.</param>
        /// <param name="randomSource">The random number generator which is used to draw random samples. Optional, can be null.</param>
        /// <returns>a normal distribution.</returns>
        public static Normal WithMeanStdDev(double mean, double stddev, System.Random randomSource = null)
        {
            return new Normal(mean, stddev, randomSource);
        }

        /// <summary>
        /// Constructs a normal distribution from a mean and variance.
        /// </summary>
        /// <param name="mean">The mean (μ) of the normal distribution.</param>
        /// <param name="var">The variance (σ^2) of the normal distribution.</param>
        /// <param name="randomSource">The random number generator which is used to draw random samples. Optional, can be null.</param>
        /// <returns>A normal distribution.</returns>
        public static Normal WithMeanVariance(double mean, double var, System.Random randomSource = null)
        {
            return new Normal(mean, Math.Sqrt(var), randomSource);
        }

        /// <summary>
        /// Constructs a normal distribution from a mean and precision.
        /// </summary>
        /// <param name="mean">The mean (μ) of the normal distribution.</param>
        /// <param name="precision">The precision of the normal distribution.</param>
        /// <param name="randomSource">The random number generator which is used to draw random samples. Optional, can be null.</param>
        /// <returns>A normal distribution.</returns>
        public static Normal WithMeanPrecision(double mean, double precision, System.Random randomSource = null)
        {
            return new Normal(mean, 1.0/Math.Sqrt(precision), randomSource);
        }

        /// <summary>
        /// A string representation of the distribution.
        /// </summary>
        /// <returns>a string representation of the distribution.</returns>
        public override string ToString()
        {
            return "Normal(μ = " + _mean + ", σ = " + _stdDev + ")";
        }

        /// <summary>
        /// Tests whether the provided values are valid parameters for this distribution.
        /// </summary>
        /// <param name="mean">The mean (μ) of the normal distribution.</param>
        /// <param name="stddev">The standard deviation (σ) of the normal distribution. Range: σ ≥ 0.</param>
        public static bool IsValidParameterSet(double mean, double stddev)
        {
            return stddev >= 0.0 && !double.IsNaN(mean);
        }

        /// <summary>
        /// Gets or sets the mean (μ) of the normal distribution.
        /// </summary>
        public double Mean
        {
            get { return _mean; }
        }

        /// <summary>
        /// Gets or sets the standard deviation (σ) of the normal distribution. Range: σ ≥ 0.
        /// </summary>
        public double StdDev
        {
            get { return _stdDev; }
        }

        /// <summary>
        /// Gets or sets the variance of the normal distribution.
        /// </summary>
        public double Variance
        {
            get { return _stdDev*_stdDev; }
        }

        /// <summary>
        /// Gets or sets the precision of the normal distribution.
        /// </summary>
        public double Precision
        {
            get { return 1.0/(_stdDev*_stdDev); }
        }

        /// <summary>
        /// Gets or sets the random number generator which is used to draw random samples.
        /// </summary>
        public System.Random RandomSource
        {
            get { return _random; }
            set { _random = value ?? SystemRandomSource.Default; }
        }

        /// <summary>
        /// Gets the entropy of the normal distribution.
        /// </summary>
        public double Entropy
        {
            get { return Math.Log(_stdDev) + Constants.LogSqrt2PiE; }
        }

        /// <summary>
        /// Gets the skewness of the normal distribution.
        /// </summary>
        public double Skewness
        {
            get { return 0.0; }
        }

        /// <summary>
        /// Gets the mode of the normal distribution.
        /// </summary>
        public double Mode
        {
            get { return _mean; }
        }

        /// <summary>
        /// Gets the median of the normal distribution.
        /// </summary>
        public double Median
        {
            get { return _mean; }
        }

        /// <summary>
        /// Gets the minimum of the normal distribution.
        /// </summary>
        public double Minimum
        {
            get { return double.NegativeInfinity; }
        }

        /// <summary>
        /// Gets the maximum of the normal distribution.
        /// </summary>
        public double Maximum
        {
            get { return double.PositiveInfinity; }
        }

        /// <summary>
        /// Computes the probability density of the distribution (PDF) at x, i.e. ∂P(X ≤ x)/∂x.
        /// </summary>
        /// <param name="x">The location at which to compute the density.</param>
        /// <returns>the density at <paramref name="x"/>.</returns>
        /// <seealso cref="PDF"/>
        public double Density(double x)
        {
            var d = (x - _mean)/_stdDev;
            return Math.Exp(-0.5*d*d)/(Constants.Sqrt2Pi*_stdDev);
        }

        /// <summary>
        /// Computes the log probability density of the distribution (lnPDF) at x, i.e. ln(∂P(X ≤ x)/∂x).
        /// </summary>
        /// <param name="x">The location at which to compute the log density.</param>
        /// <returns>the log density at <paramref name="x"/>.</returns>
        /// <seealso cref="PDFLn"/>
        public double DensityLn(double x)
        {
            var d = (x - _mean)/_stdDev;
            return (-0.5*d*d) - Math.Log(_stdDev) - Constants.LogSqrt2Pi;
        }

        /// <summary>
        /// Generates a sample from the normal distribution using the <i>Box-Muller</i> algorithm.
        /// </summary>
        /// <returns>a sample from the distribution.</returns>
        public double Sample()
        {
            return SampleUnchecked(_random, _mean, _stdDev);
        }

        /// <summary>
        /// Generates a sequence of samples from the normal distribution using the <i>Box-Muller</i> algorithm.
        /// </summary>
        /// <returns>a sequence of samples from the distribution.</returns>
        public IEnumerable<double> Samples()
        {
            return SamplesUnchecked(_random, _mean, _stdDev);
        }

        internal static double SampleUnchecked(System.Random rnd, double mean, double stddev)
        {
            double x, y;
            while (!PolarTransform(rnd.NextDouble(), rnd.NextDouble(), out x, out y))
            {
            }

            return mean + (stddev*x);
        }

        internal static IEnumerable<double> SamplesUnchecked(System.Random rnd, double mean, double stddev)
        {
            while (true)
            {
                double x, y;
                if (!PolarTransform(rnd.NextDouble(), rnd.NextDouble(), out x, out y))
                {
                    continue;
                }

                yield return mean + (stddev*x);
                yield return mean + (stddev*y);
            }
        }

        static bool PolarTransform(double a, double b, out double x, out double y)
        {
            var v1 = (2.0*a) - 1.0;
            var v2 = (2.0*b) - 1.0;
            var r = (v1*v1) + (v2*v2);
            if (r >= 1.0 || r == 0.0)
            {
                x = 0;
                y = 0;
                return false;
            }

            var fac = Math.Sqrt(-2.0*Math.Log(r)/r);
            x = v1*fac;
            y = v2*fac;
            return true;
        }

        /// <summary>
        /// Computes the probability density of the distribution (PDF) at x, i.e. ∂P(X ≤ x)/∂x.
        /// </summary>
        /// <param name="mean">The mean (μ) of the normal distribution.</param>
        /// <param name="stddev">The standard deviation (σ) of the normal distribution. Range: σ ≥ 0.</param>
        /// <param name="x">The location at which to compute the density.</param>
        /// <returns>the density at <paramref name="x"/>.</returns>
        /// <seealso cref="Density"/>
        /// <remarks>MATLAB: normpdf</remarks>
        public static double PDF(double mean, double stddev, double x)
        {
            if (stddev < 0.0)
            {
                throw new ArgumentException(Resources.InvalidDistributionParameters);
            }

            var d = (x - mean)/stddev;
            return Math.Exp(-0.5*d*d)/(Constants.Sqrt2Pi*stddev);
        }

        /// <summary>
        /// Computes the log probability density of the distribution (lnPDF) at x, i.e. ln(∂P(X ≤ x)/∂x).
        /// </summary>
        /// <param name="mean">The mean (μ) of the normal distribution.</param>
        /// <param name="stddev">The standard deviation (σ) of the normal distribution. Range: σ ≥ 0.</param>
        /// <param name="x">The location at which to compute the density.</param>
        /// <returns>the log density at <paramref name="x"/>.</returns>
        /// <seealso cref="DensityLn"/>
        public static double PDFLn(double mean, double stddev, double x)
        {
            if (stddev < 0.0)
            {
                throw new ArgumentException(Resources.InvalidDistributionParameters);
            }

            var d = (x - mean)/stddev;
            return (-0.5*d*d) - Math.Log(stddev) - Constants.LogSqrt2Pi;
        }

        /// <summary>
        /// Generates a sample from the normal distribution using the <i>Box-Muller</i> algorithm.
        /// </summary>
        /// <param name="rnd">The random number generator to use.</param>
        /// <param name="mean">The mean (μ) of the normal distribution.</param>
        /// <param name="stddev">The standard deviation (σ) of the normal distribution. Range: σ ≥ 0.</param>
        /// <returns>a sample from the distribution.</returns>
        public static double Sample(System.Random rnd, double mean, double stddev)
        {
            if (stddev < 0.0)
            {
                throw new ArgumentException(Resources.InvalidDistributionParameters);
            }

            return SampleUnchecked(rnd, mean, stddev);
        }

        /// <summary>
        /// Generates a sequence of samples from the normal distribution using the <i>Box-Muller</i> algorithm.
        /// </summary>
        /// <param name="rnd">The random number generator to use.</param>
        /// <param name="mean">The mean (μ) of the normal distribution.</param>
        /// <param name="stddev">The standard deviation (σ) of the normal distribution. Range: σ ≥ 0.</param>
        /// <returns>a sequence of samples from the distribution.</returns>
        public static IEnumerable<double> Samples(System.Random rnd, double mean, double stddev)
        {
            if (stddev < 0.0)
            {
                throw new ArgumentException(Resources.InvalidDistributionParameters);
            }

            return SamplesUnchecked(rnd, mean, stddev);
        }

        /// <summary>
        /// Generates a sample from the normal distribution using the <i>Box-Muller</i> algorithm.
        /// </summary>
        /// <param name="mean">The mean (μ) of the normal distribution.</param>
        /// <param name="stddev">The standard deviation (σ) of the normal distribution. Range: σ ≥ 0.</param>
        /// <returns>a sample from the distribution.</returns>
        public static double Sample(double mean, double stddev)
        {
            if (stddev < 0.0)
            {
                throw new ArgumentException(Resources.InvalidDistributionParameters);
            }

            return SampleUnchecked(SystemRandomSource.Default, mean, stddev);
        }
    }
}
