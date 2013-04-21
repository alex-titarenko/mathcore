using System;
using System.Linq;
using System.Collections.Generic;


namespace TAlex.MathCore.Statistics
{
    /// <summary>
    /// Represents various functions of mathematical statistics.
    /// </summary>
    public static class MathStats
    {
        #region Methods

        /// <summary>
        /// Returns the median of the elements of an array.
        /// </summary>
        /// <param name="v">An array of real numbers.</param>
        /// <returns>The median of the elements of v.</returns>
        public static double Median(IEnumerable<double> v)
        {
            List<double> ordered_v = new List<double>(v);
            ordered_v.Sort();

            if ((ordered_v.Count & 1) != 0)
                return ordered_v[ordered_v.Count / 2];
            else
                return (ordered_v[ordered_v.Count / 2 - 1] + ordered_v[ordered_v.Count / 2]) / 2.0;
        }

        /// <summary>
        /// Returns the arithmetic mean of the elements of an array.
        /// </summary>
        /// <param name="v">An array of real numbers.</param>
        /// <returns>The arithmetic mean of the elements of v.</returns>
        public static double Mean(IEnumerable<double> v)
        {
            int count = 0;
            double sum = 0.0;

            foreach (double item in v)
            {
                sum += item;
                count++;
            }

            return sum / count;
        }

        /// <summary>
        /// Returns the arithmetic mean of the elements of an array.
        /// </summary>
        /// <param name="v">An array of complex numbers.</param>
        /// <returns>The arithmetic mean of the elements of v.</returns>
        public static Complex Mean(IEnumerable<Complex> v)
        {
            int count = 0;
            Complex sum = Complex.Zero;

            foreach (Complex item in v)
            {
                sum += item;
                count++;
            }
            return sum / count;
        }

        /// <summary>
        /// Returns the geometric mean of the elements of an array of positive real numbers.
        /// </summary>
        /// <param name="v">An array of positive real numbers.</param>
        /// <returns>The geometric mean of the elements of v.</returns>
        /// <exception cref="System.ArgumentException">
        /// The array v contains a negative numbers.
        /// </exception>
        public static double GeometricMean(ICollection<double> v)
        {
            double mult = 1.0;

            foreach (double item in v)
            {
                if (item < 0.0)
                    throw new ArgumentException("The geometric means accepts only positive real numbers.");

                mult *= item;
            }

            return Math.Pow(mult, 1.0 / v.Count);
        }

        /// <summary>
        /// Returns the harmonic mean of the elements of an array of positive real numbers.
        /// </summary>
        /// <param name="v">An array of positive real numbers.</param>
        /// <returns>The harmonic mean of the elements of v.</returns>
        /// <exception cref="System.ArgumentException">
        /// The array v contains a negative numbers.
        /// </exception>
        public static double HarmonicMean(ICollection<double> v)
        {
            double sum = 0.0;

            foreach (double item in v)
            {
                if (item < 0.0)
                    throw new ArgumentException("The harmonic means accepts only positive real numbers.");

                sum += (1.0 / item);
            }

            return (double)v.Count / sum;
        }

        /// <summary>
        /// Returns the value from the elements of an array that occurs most often.
        /// </summary>
        /// <param name="v">An array of real numbers.</param>
        /// <returns>The value from the elements of v that occurs most often.</returns>
        /// <exception cref="System.ArgumentException">
        /// The value of mode of the elements of v is not unique.
        /// </exception>
        public static double Mode(IEnumerable<double> v)
        {
            List<double> elems = new List<double>(v);
            elems.Sort();

            double result = elems[0];
            int maxCount = 0;
            int repeats = 0;

            int i = 0;

            while (i < elems.Count)
            {
                int count = 1;
                double value = elems[i++];

                while (i < elems.Count && value == elems[i])
                {
                    count++;
                    i++;
                }

                if (count == maxCount)
                {
                    repeats++;
                }
                else if (count > maxCount)
                {
                    maxCount = count;
                    repeats = 0;
                    result = elems[i - 1];
                }
            }

            if (repeats > 0)
                throw new ArgumentException("The value of mode is not unique.");

            return result;
        }

        /// <summary>
        /// Returns the value from the elements of an array that occurs most often.
        /// </summary>
        /// <param name="v">An array of complex numbers.</param>
        /// <returns>The value from the elements of v that occurs most often.</returns>
        /// <exception cref="System.ArgumentException">
        /// The value of mode of the elements of v is not unique.
        /// </exception>
        public static Complex Mode(IEnumerable<Complex> v)
        {
            List<Complex> elems = new List<Complex>(v);
            elems.Sort(new ComplexComparer());

            Complex result = elems[0];
            int maxCount = 0;
            int repeats = 0;

            int i = 0;

            while (i < elems.Count)
            {
                int count = 1;
                Complex value = elems[i++];

                while (i < elems.Count && value == elems[i])
                {
                    count++;
                    i++;
                }

                if (count == maxCount)
                {
                    repeats++;
                }
                else if (count > maxCount)
                {
                    maxCount = count;
                    repeats = 0;
                    result = elems[i - 1];
                }
            }

            if (repeats > 0)
                throw new ArgumentException("The value of mode is not unique.");

            return result;
        }

        /// <summary>
        /// Returns the sample variance of the elements of an array.
        /// </summary>
        /// <param name="v">An array of real numbers.</param>
        /// <returns>The sample variance of the elements of v.</returns>
        public static double SampleVariance(IEnumerable<double> v)
        {
            int count = 0;
            double sum = 0.0;
            double mean = Mean(v);

            foreach (double item in v)
            {
                sum += ExMath.Pow(item - mean, 2);
                count++;
            }

            if (count == 1) return 0.0;
            return sum / (count - 1);
        }

        /// <summary>
        /// Returns the sample variance of the elements of an array.
        /// </summary>
        /// <param name="v">An array of complex numbers.</param>
        /// <returns>The sample variance of the elements of v.</returns>
        public static double SampleVariance(IEnumerable<Complex> v)
        {
            int count = 0;
            double sum = 0.0;
            Complex mean = Mean(v);

            foreach (Complex item in v)
            {
                sum += Complex.AbsSquared(item - mean);
                count++;
            }

            if (count == 1) return 0.0;
            return sum / (count - 1);
        }

        /// <summary>
        /// Returns the square root of the sample variance of the elements of an array.
        /// </summary>
        /// <param name="v">An array of real numbers.</param>
        /// <returns>The square root of the sample variance of the elements of v.</returns>
        public static double SampleStandardDeviation(IEnumerable<double> v)
        {
            return Math.Sqrt(SampleVariance(v));
        }

        /// <summary>
        /// Returns the square root of the sample variance of the elements of an array.
        /// </summary>
        /// <param name="v">An array of complex numbers.</param>
        /// <returns>The square root of the sample variance of the elements of v.</returns>
        public static double SampleStandardDeviation(IEnumerable<Complex> v)
        {
            return Math.Sqrt(SampleVariance(v));
        }

        /// <summary>
        /// Returns the sample skewness of the elements of an array.
        /// </summary>
        /// <param name="v">An array of real numbers.</param>
        /// <returns>The sample skewness of the elements of v.</returns>
        public static double SampleSkewness(IEnumerable<double> v)
        {
            double mean = Mean(v);

            int count = 0;
            double sumcube = 0.0;
            double sumsq = 0.0;

            foreach (double item in v)
            {
                double c = item - mean;
                double sq = c * c;
                sumcube += sq * c;
                sumsq += sq;
                count++;
            }

            if (count < 3) return double.NaN;

            double Stdev = Math.Sqrt(sumsq / (count - 1));
            return (count * sumcube) / ((count - 1.0) * (count - 2.0) * ExMath.Pow(Stdev, 3));
        }

        /// <summary>
        /// Returns the sample skewness of the elements of an array.
        /// </summary>
        /// <param name="v">An array of complex numbers.</param>
        /// <returns>The sample skewness of the elements of v.</returns>
        public static Complex SampleSkewness(IEnumerable<Complex> v)
        {
            Complex mean = Mean(v);

            int count = 0;
            Complex sumcube = Complex.Zero;
            Complex sumsq = Complex.Zero;

            foreach (Complex item in v)
            {
                Complex c = item - mean;
                Complex sq = c * c;
                sumcube += sq * c;
                sumsq += sq;
                count++;
            }

            if (count < 3) return Complex.NaN;

            Complex Stdev = Complex.Sqrt(sumsq / (count - 1));
            return (count * sumcube) / ((count - 1.0) * (count - 2.0) * Complex.Pow(Stdev, 3));
        }

        /// <summary>
        /// Returns the sample kurtosis of the elements of an array.
        /// </summary>
        /// <param name="v">An array of real numbers.</param>
        /// <returns>The sample kurtosis of the elements of v.</returns>
        public static double SampleKurtosis(IEnumerable<double> v)
        {
            double mean = Mean(v);

            int count = 0;
            double num = 0.0;
            double sumsq = 0.0;

            foreach (double item in v)
            {
                double c = item - mean;
                double sq = c * c;
                num += sq * sq;
                sumsq += sq;
                count++;
            }

            if (count < 4) return double.NaN;

            double Stdev4 = ExMath.Pow(sumsq / (count - 1), 2);
            return (count * (count + 1.0) / ((count - 1.0) * (count - 2.0) * (count - 3.0))) * (num / Stdev4) -
                3 * ((count - 1.0) * (count - 1.0)) / ((count - 2.0) * (count - 3.0));
        }

        /// <summary>
        /// Returns the sample kurtosis of the elements of an array.
        /// </summary>
        /// <param name="v">An array of complex numbers.</param>
        /// <returns>The sample kurtosis of the elements of v.</returns>
        public static Complex SampleKurtosis(IEnumerable<Complex> v)
        {
            Complex mean = Mean(v);

            int count = 0;
            Complex num = Complex.Zero;
            Complex sumsq = Complex.Zero;

            foreach (Complex item in v)
            {
                Complex c = item - mean;
                Complex sq = c * c;
                num += sq * sq;
                sumsq += sq;
                count++;
            }

            if (count < 4) return Complex.NaN;

            Complex Stdev4 = Complex.Pow(sumsq / (count - 1), 2);
            return (count * (count + 1.0) / ((count - 1.0) * (count - 2.0) * (count - 3.0))) * (num / Stdev4) -
                3 * ((count - 1.0) * (count - 1.0)) / ((count - 2.0) * (count - 3.0));            
        }

        /// <summary>
        /// Returns the sample covariance of two sets of values.
        /// </summary>
        /// <param name="a">An array of real numbers.</param>
        /// <param name="b">An array of real numbers.</param>
        /// <returns>The sample covariance of the elements of a and b.</returns>
        /// <exception cref="System.ArgumentException">
        /// The length of the array a does not match the length of the array b.
        /// </exception>
        public static double SampleCovariance(IList<double> a, IList<double> b)
        {
            if (a.Count != b.Count)
                throw new ArgumentException("The lengths of the two arrays do not match.");

            double mean_a = Mean(a);
            double mean_b = Mean(b);

            double sum = 0.0;

            for (int i = 0; i < a.Count; i++)
            {
                sum += (a[i] - mean_a) * (b[i] - mean_b);
            }

            return sum / (a.Count - 1);
        }

        /// <summary>
        /// Returns the sample covariance of two sets of values.
        /// </summary>
        /// <param name="a">An array of complex numbers.</param>
        /// <param name="b">An array of complex numbers.</param>
        /// <returns>The sample covariance of the elements of a and b.</returns>
        /// <exception cref="System.ArgumentException">
        /// The length of the array a does not match the length of the array b.
        /// </exception>
        public static Complex SampleCovariance(IList<Complex> a, IList<Complex> b)
        {
            if (a.Count != b.Count)
                throw new ArgumentException("The lengths of the two arrays do not match.");

            Complex mean_a = Mean(a);
            Complex mean_b = Mean(b);

            Complex sum = Complex.Zero;

            for (int i = 0; i < a.Count; i++)
            {
                sum += Complex.ConjugateMultiply(b[i] - mean_b, a[i] - mean_a);
            }

            return sum / (a.Count - 1);
        }

        /// <summary>
        /// Returns the population variance of the elements of an array.
        /// </summary>
        /// <param name="v">An array of real numbers.</param>
        /// <returns>The population variance of the elements of v.</returns>
        public static double PopulationVariance(IEnumerable<double> v)
        {
            int count = 0;
            double sum = 0.0;
            double mean = Mean(v);

            foreach (double item in v)
            {
                sum += ExMath.Pow(item - mean, 2);
                count++;
            }

            return sum / count;
        }

        /// <summary>
        /// Returns the population variance of the elements of an array.
        /// </summary>
        /// <param name="v">An array of complex numbers.</param>
        /// <returns>The population variance of the elements of v.</returns>
        public static double PopulationVariance(IEnumerable<Complex> v)
        {
            int count = 0;
            double sum = 0.0;
            Complex mean = Mean(v);

            foreach (Complex item in v)
            {
                sum += Complex.AbsSquared(item - mean);
                count++;
            }

            return sum / count;
        }

        /// <summary>
        /// Returns the square root of the population variance of the elements of an array.
        /// </summary>
        /// <param name="v">An array of real numbers.</param>
        /// <returns>The square root of the population variance of the elements of v.</returns>
        public static double PopulationStandardDeviation(IEnumerable<double> v)
        {
            return Math.Sqrt(PopulationVariance(v));
        }

        /// <summary>
        /// Returns the square root of the population variance of the elements of an array.
        /// </summary>
        /// <param name="v">An array of complex numbers.</param>
        /// <returns>The square root of the population variance of the elements of v.</returns>
        public static double PopulationStandardDeviation(IEnumerable<Complex> v)
        {
            return Math.Sqrt(PopulationVariance(v));
        }

        /// <summary>
        /// Returns the population skewness of the elements of an array.
        /// </summary>
        /// <param name="v">An array of real numbers.</param>
        /// <returns>The population skewness of the elements of v.</returns>
        public static double PopulationSkewness(IEnumerable<double> v)
        {
            double mean = Mean(v);

            int count = 0;
            double sumcube = 0.0;
            double sumsq = 0.0;

            foreach (double item in v)
            {
                double c = item - mean;
                double sq = c * c;
                sumcube += sq * c;
                sumsq += sq;
                count++;
            }

            if (count < 3) return double.NaN;

            double stdev = Math.Sqrt(sumsq / count);
            return (sumcube / count) / ExMath.Pow(stdev, 3);
        }

        /// <summary>
        /// Returns the population skewness of the elements of an array.
        /// </summary>
        /// <param name="v">An array of complex numbers.</param>
        /// <returns>The population skewness of the elements of v.</returns>
        public static Complex PopulationSkewness(IEnumerable<Complex> v)
        {
            Complex mean = Mean(v);

            int count = 0;
            Complex sumcube = Complex.Zero;
            Complex sumsq = Complex.Zero;

            foreach (Complex item in v)
            {
                Complex c = item - mean;
                Complex sq = c * c;
                sumcube += sq * c;
                sumsq += sq;
                count++;
            }

            if (count < 3) return Complex.NaN;
            Complex stdev = Complex.Sqrt(sumsq / count);
            return (sumcube / count) / Complex.Pow(stdev, 3);
        }

        /// <summary>
        /// Returns the population kurtosis of the elements of an array.
        /// </summary>
        /// <param name="v">An array of real numbers.</param>
        /// <returns>The population kurtosis of the elements of v.</returns>
        public static double PopulationKurtosis(IEnumerable<double> v)
        {
            double mean = Mean(v);

            int count = 0;
            double num = 0.0;
            double sumsq = 0.0;

            foreach (double item in v)
            {
                double c = item - mean;
                double sq = c * c;
                num += sq * sq;
                sumsq += sq;
                count++;
            }

            if (count < 4) return double.NaN;

            double Stdev4 = ExMath.Pow(sumsq / count, 2);
            return (num / count) / Stdev4 - 3;
        }

        /// <summary>
        /// Returns the population kurtosis of the elements of an array.
        /// </summary>
        /// <param name="v">An array of complex numbers.</param>
        /// <returns>The population kurtosis of the elements of v.</returns>
        public static Complex PopulationKurtosis(IEnumerable<Complex> v)
        {
            Complex mean = Mean(v);

            int count = 0;
            Complex num = Complex.Zero;
            Complex sumsq = 0.0;

            foreach (Complex item in v)
            {
                Complex c = item - mean;
                Complex sq = c * c;
                num += sq * sq;
                sumsq += sq;
                count++;
            }

            if (count < 4) return Complex.NaN;

            Complex Stdev4 = Complex.Pow(sumsq / count, 2);
            return (num / count) / Stdev4 - 3;
        }

        /// <summary>
        /// Returns the moment of a specific order of the elements of an array.
        /// </summary>
        /// <param name="v">An array of real numbers.</param>
        /// <param name="k">The order of the moment.</param>
        /// <returns>The moment of k-order of the elements of v.</returns>
        /// <exception cref="System.ArgumentException">The value of k less than one.</exception>
        public static double PopulationMoment(ICollection<double> v, int k)
        {
            if (k < 1)
            {
                throw new ArgumentException("The order of the moment must be greater than zero.");
            }

            if (v.Count == 0)
            {
                return double.NaN;
            }

            double sum = 0.0;

            foreach (double item in v)
            {
                sum += ExMath.Pow(item, k);
            }

            return sum / v.Count;
        }

        /// <summary>
        /// Returns the moment of a specific order of the elements of an array.
        /// </summary>
        /// <param name="v">An array of complex numbers.</param>
        /// <param name="k">The order of the moment.</param>
        /// <returns>The moment of k-order of the elements of v.</returns>
        /// <exception cref="System.ArgumentException">The value of k less than one.</exception>
        public static Complex PopulationMoment(ICollection<Complex> v, int k)
        {
            if (k < 1)
            {
                throw new ArgumentException("The order of the moment must be greater than zero.");
            }

            if (v.Count == 0)
            {
                return Complex.NaN;
            }

            Complex sum = 0.0;

            foreach (Complex item in v)
            {
                sum += Complex.Pow(item, k);
            }

            return sum / v.Count;
        }

        /// <summary>
        /// Returns the central moment of a specific order of the elements of an array.
        /// </summary>
        /// <param name="v">An array of real numbers.</param>
        /// <param name="k">The order of the central moment.</param>
        /// <returns>The central moment of k-order of the elements of v.</returns>
        /// <exception cref="System.ArgumentException">The value of k less than one.</exception>
        public static double PopulationCentralMoment(ICollection<double> v, int k)
        {
            if (k < 1)
            {
                throw new ArgumentException("The order of the central moment must be greater than zero.");
            }

            if (v.Count == 0)
            {
                return double.NaN;
            }

            if (k == 1)
            {
                return 0.0;
            }

            double mean = Mean(v);
            double sum = 0.0;

            foreach (double item in v)
            {
                sum += ExMath.Pow(item - mean, k);
            }

            return sum / v.Count;
        }

        /// <summary>
        /// Returns the central moment of a specific order of the elements of an array.
        /// </summary>
        /// <param name="v">An array of complex numbers.</param>
        /// <param name="k">The order of the central moment.</param>
        /// <returns>The central moment of k-order of the elements of v.</returns>
        /// <exception cref="System.ArgumentException">The value of k less than one.</exception>
        public static Complex PopulationCentralMoment(ICollection<Complex> v, int k)
        {
            if (k < 1)
            {
                throw new ArgumentException("The order of the central moment must be greater than zero.");
            }

            if (v.Count == 0)
            {
                return Complex.NaN;
            }

            if (k == 1)
            {
                return Complex.Zero;
            }

            Complex mean = Mean(v);
            Complex sum = 0.0;

            foreach (Complex item in v)
            {
                sum += Complex.Pow(item - mean, k);
            }

            return sum / v.Count;
        }

        /// <summary>
        /// Returns the population covariance of two sets of values.
        /// </summary>
        /// <param name="a">An array of real numbers.</param>
        /// <param name="b">An array of real numbers.</param>
        /// <returns>The population covariance of the elements of a and b.</returns>
        /// <exception cref="System.ArgumentException">
        /// The length of the array a does not match the length of the array b.
        /// </exception>
        public static double PopulationCovariance(IList<double> a, IList<double> b)
        {
            if (a.Count != b.Count)
                throw new ArgumentException("The lengths of the two arrays do not match.");

            double mean_a = Mean(a);
            double mean_b = Mean(b);

            double sum = 0.0;

            for (int i = 0; i < a.Count; i++)
            {
                sum += (a[i] - mean_a) * (b[i] - mean_b);
            }

            return sum / a.Count;
        }

        /// <summary>
        /// Returns the population covariance of two sets of values.
        /// </summary>
        /// <param name="a">An array of complex numbers.</param>
        /// <param name="b">An array of complex numbers.</param>
        /// <returns>The population covariance of the elements of a and b.</returns>
        /// <exception cref="System.ArgumentException">
        /// The length of the array a does not match the length of the array b.
        /// </exception>
        public static Complex PopulationCovariance(IList<Complex> a, IList<Complex> b)
        {
            if (a.Count != b.Count)
                throw new ArgumentException("The lengths of the two arrays do not match.");

            Complex mean_a = Mean(a);
            Complex mean_b = Mean(b);

            Complex sum = Complex.Zero;

            for (int i = 0; i < a.Count; i++)
            {
                sum += Complex.ConjugateMultiply(b[i] - mean_b, a[i] - mean_a);
            }

            return sum / a.Count;
        }

        /// <summary>
        /// Returns the Pearson correlation coefficient of two sets of values.
        /// </summary>
        /// <param name="a">An array of real numbers.</param>
        /// <param name="b">An array of real numbers.</param>
        /// <returns>The Pearson correlation coefficient of the elements of a and b.</returns>
        /// <exception cref="System.ArgumentException">
        /// The length of the array a does not match the length of the array b.
        /// </exception>
        public static double Correlation(IList<double> a, IList<double> b)
        {
            if (a.Count != b.Count)
                throw new ArgumentException("The lengths of the two arrays do not match.");

            double mean_a = Mean(a);
            double mean_b = Mean(b);

            double sum = 0.0;
            double sum1 = 0.0;
            double sum2 = 0.0;

            for (int i = 0; i < a.Count; i++)
            {
                double c1 = a[i] - mean_a;
                double c2 = b[i] - mean_b;

                sum += c1 * c2;
                sum1 += c1 * c1;
                sum2 += c2 * c2;
            }

            return sum / Math.Sqrt(sum1 * sum2);
        }

        /// <summary>
        /// Returns the Pearson correlation coefficient of two sets of values.
        /// </summary>
        /// <param name="a">An array of complex numbers.</param>
        /// <param name="b">An array of complex numbers.</param>
        /// <returns>The Pearson correlation coefficient of the elements of a and b.</returns>
        /// <exception cref="System.ArgumentException">
        /// The length of the array a does not match the length of the array b.
        /// </exception>
        public static Complex Correlation(IList<Complex> a, IList<Complex> b)
        {
            if (a.Count != b.Count)
                throw new ArgumentException("The lengths of the two arrays do not match.");

            Complex mean_a = Mean(a);
            Complex mean_b = Mean(b);

            Complex sum = Complex.Zero;
            double sum1 = 0.0;
            double sum2 = 0.0;

            for (int i = 0; i < a.Count; i++)
            {
                Complex c1 = a[i] - mean_a;
                Complex c2 = b[i] - mean_b;

                sum += Complex.ConjugateMultiply(c2, c1);
                sum1 += Complex.AbsSquared(c1);
                sum2 += Complex.AbsSquared(c2);
            }

            return sum / Math.Sqrt(sum1 * sum2);
        }

        /// <summary>
        /// Returns the range of the elements of an array.
        /// </summary>
        /// <param name="v">An array of real numbers.</param>
        /// <returns>The range of the elements of v.</returns>
        public static double Range(IList<double> v)
        {
            if (v.Count == 0)
                return 0.0;

            double min = v[0];
            double max = v[0];

            for (int i = 1; i < v.Count; i++)
            {
                if (min > v[i]) min = v[i];
                else if (max < v[i]) max = v[i];
            }

            return max - min;
        }

        /// <summary>
        /// Returns the sum of the elements of an array.
        /// </summary>
        /// <param name="v">An array of real numbers.</param>
        /// <returns>The sum of the elements of v.</returns>
        public static double Sum(IEnumerable<double> v)
        {
            double sum = 0.0;

            foreach (double item in v)
            {
                sum += item;
            }

            return sum;
        }

        /// <summary>
        /// Returns the sum of the elements of an array.
        /// </summary>
        /// <param name="v">An array of complex numbers.</param>
        /// <returns>The sum of the elements of v.</returns>
        public static Complex Sum(IEnumerable<Complex> v)
        {
            Complex sum = Complex.Zero;

            foreach (Complex item in v)
            {
                sum += item;
            }

            return sum;
        }

        /// <summary>
        /// Returns the sum of the squares of the elements of an array.
        /// </summary>
        /// <param name="v">An array of real numbers.</param>
        /// <returns>The sum of the squares of the elements of v.</returns>
        public static double SumOfSquares(IEnumerable<double> v)
        {
            double sum = 0.0;

            foreach (double item in v)
            {
                sum += item * item;
            }

            return sum;
        }

        /// <summary>
        /// Returns the sum of the squares of the elements of an array.
        /// </summary>
        /// <param name="v">An array of complex numbers.</param>
        /// <returns>The sum of the squares of the elements of v.</returns>
        public static Complex SumOfSquares(IEnumerable<Complex> v)
        {
            Complex sum = Complex.Zero;

            foreach (Complex item in v)
            {
                sum += item * item;
            }

            return sum;
        }

        /// <summary>
        /// Returns the product of the elements of an array.
        /// </summary>
        /// <param name="v">An array of real numbers.</param>
        /// <returns>The product of the elements of v.</returns>
        public static double Product(IEnumerable<double> v)
        {
            if (v.Any())
            {
                double product = 1.0;

                foreach (double item in v)
                {
                    product *= item;
                }
                return product;
            }
            else
            {
                return 0.0;
            }
        }

        /// <summary>
        /// Returns the product of the elements of an array.
        /// </summary>
        /// <param name="v">An array of complex numbers.</param>
        /// <returns>The product of the elements of v.</returns>
        public static Complex Product(IEnumerable<Complex> v)
        {
            if (v.Any())
            {
                Complex product = Complex.One;

                foreach (Complex item in v)
                {
                    product *= item;
                }
                return product;
            }
            else
            {
                return Complex.Zero;
            }
        }

        /// <summary>
        /// Returns the maximum value of the elements of an array.
        /// </summary>
        /// <param name="v">An array of real numbers.</param>
        /// <returns>The maximum value of the elements of v.</returns>
        public static double Max(IList<double> v)
        {
            if (v.Count == 0)
                return 0.0;

            double max = v[0];

            for (int i = 1; i < v.Count; i++)
            {
                if (max < v[i])
                {
                    max = v[i];
                }
            }

            return max;
        }

        /// <summary>
        /// Returns the maximum value of the elements of an array.
        /// </summary>
        /// <param name="v">An array of complex numbers.</param>
        /// <returns>The maximum value of the elements of v.</returns>
        public static Complex Max(IList<Complex> v)
        {
            if (v.Count == 0)
                return Complex.Zero;

            Complex max = v[0];

            for (int i = 1; i < v.Count; i++)
            {
                max = Complex.Max(max, v[i]);
            }

            return max;
        }

        /// <summary>
        /// Returns the minimum value of the elements of an array.
        /// </summary>
        /// <param name="v">An array of real numbers.</param>
        /// <returns>The minimum value of the elements of v.</returns>
        public static double Min(IList<double> v)
        {
            if (v.Count == 0)
                return 0.0;

            double min = v[0];

            for (int i = 1; i < v.Count; i++)
            {
                if (min > v[i])
                {
                    min = v[i];
                }
            }

            return min;
        }

        /// <summary>
        /// Returns the minimum value of the elements of an array.
        /// </summary>
        /// <param name="v">An array of complex numbers.</param>
        /// <returns>The minimum value of the elements of v.</returns>
        public static Complex Min(IList<Complex> v)
        {
            if (v.Count == 0)
                return Complex.Zero;

            Complex min = v[0];

            for (int i = 1; i < v.Count; i++)
            {
                min = Complex.Min(min, v[i]);
            }

            return min;
        }

        /// <summary>
        /// Returns an array containing the frequencies of occurrence of values in specified intervals.
        /// </summary>
        /// <param name="v">An array of real numbers.</param>
        /// <returns>The array containing the frequencies of occurrence of the values of v.</returns>
        public static double[] Histogram(double[] v)
        {
            int intervals = (int)Math.Round(1 + Math.Log(v.Length, 2));
            return Histogram(v, intervals);
        }

        /// <summary>
        /// Returns an array containing the frequencies of occurrence of values in specified intervals.
        /// </summary>
        /// <param name="v">An array of real numbers.</param>
        /// <param name="intervals">An integer number representing intervals count.</param>
        /// <returns>The array containing the frequencies of occurrence of the values of v in intervals.</returns>
        public static double[] Histogram(double[] v, int intervals)
        {
            v = (double[])v.Clone();
            Array.Sort(v);

            int n = v.Length;
            double min = v[0];
            double max = v[n - 1];

            int k = intervals;
            double h = (max - min) / k;

            double[] frequencies = new double[k];

            int j = 0;
            for (int i = 0; i < k; i++)
            {
                int count = 0;
                double endp = min + (i + 1) * h;

                while (j < n && v[j] <= endp)
                {
                    count++;
                    j++;
                }

                frequencies[i] = count / (double)n;
            }

            return frequencies;
        }

        /// <summary>
        /// Returns an array containing the frequencies of occurrence of values in specified intervals.
        /// </summary>
        /// <param name="v">An array of real numbers.</param>
        /// <param name="intervals">A real array containing interval endpoints in ascending order.</param>
        /// <returns>The array containing the frequencies of occurrence of the values of v in intervals.</returns>
        /// <exception cref="System.ArgumentException">
        /// The elements of intervals are not sorted in ascending order.
        /// </exception>
        public static double[] Histogram(double[] v, double[] intervals)
        {
            v = (double[])v.Clone();
            Array.Sort(v);

            int n = v.Length;

            int k = intervals.Length - 1;
            double[] frequencies = new double[k];

            int j = 0;

            while (j < n && v[j] < intervals[0])
            {
                j++;
            }

            for (int i = 0; i < k; i++)
            {
                if (intervals[i + 1] <= intervals[i])
                {
                    throw new ArgumentException("The interval endpoints must be in ascending order.");
                }

                int count = 0;
                double endp = intervals[i + 1];

                while (j < n && v[j] < endp)
                {
                    count++;
                    j++;
                }

                frequencies[i] = count / (double)n;
            }

            while (j < n && v[j] == intervals[k])
            {
                frequencies[k - 1] += 1.0 / n;
                j++;
            }

            return frequencies;
        }

        #endregion
    }
}
