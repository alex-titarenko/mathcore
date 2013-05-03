using System;


namespace TAlex.MathCore.SpecialFunctions
{
    /// <summary>
    /// Represents various combinatorial functions.
    /// </summary>
    public static class Combinatorics
    {
        #region Fields

        private const int _len = 171;

        private static readonly double[] _fact = new double[_len];

        #endregion

        #region Constructors

        static Combinatorics()
        {
            double result = 1.0;
            _fact[0] = 1.0;

            for (int n = 1; n < _len; n++)
            {
                result = result * n;
                _fact[n] = result;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns the factorial of a positive integer.
        /// </summary>
        /// <param name="n">A positive integer number.</param>
        /// <returns>The factorial of n.</returns>
        /// <exception cref="System.ArgumentException">
        /// n less than zero.
        /// </exception>
        public static double Factorial(int n)
        {
            if (n < 0)
            {
                throw new ArgumentException("The value of n must be nonnegative.");
            }

            if (n < _len)
            {
                return _fact[n];
            }
            else
            {
                return double.PositiveInfinity;
            }
        }

        /// <summary>
        /// Returns the number of ways of picking <paramref name="k"/>
        /// unordered outcomes from <paramref name="n"/> possibilities.
        /// </summary>
        /// <param name="n">An integer number.</param>
        /// <param name="k">An integer number.</param>
        /// <returns>
        /// The number of ways of picking <paramref name="k"/>
        /// unordered outcomes from <paramref name="n"/> possibilities.
        /// </returns>
        /// <exception cref="System.ArgumentException">
        /// n or k less that zero or n less that k.
        /// </exception>
        public static double Combinations(int n, int k)
        {
            if (n < 0 || k < 0)
                throw new ArgumentException("The values of n and k must be nonnegative.");

            if (n < k)
                throw new ArgumentException("The value of n must be greater that or equal to k.");

            return Factorial(n) / (Factorial(k) * Factorial(n - k));
        }

        /// <summary>
        /// Returns the number of ways of obtaining an ordered subset of
        /// <paramref name="k"/> elements from a set of <paramref name="n"/> elements.
        /// </summary>
        /// <param name="n">An integer number.</param>
        /// <param name="k">An integer number.</param>
        /// <returns>
        /// The number of ways of obtaining an ordered subset of
        /// <paramref name="k"/> elements from a set of <paramref name="n"/> elements.
        /// </returns>
        /// <exception cref="System.ArgumentException">
        /// n or k less that zero or n less that k.
        /// </exception>
        public static double Permutations(int n, int k)
        {
            if (n < 0 || k < 0)
                throw new ArgumentException("The values of n and k must be nonnegative.");

            if (n < k)
                throw new ArgumentException("The value of n must be greater that or equal to k.");

            return Factorial(n) / Factorial(n - k);
        }

        /// <summary>
        /// Returns the n-th Fibonacci number.
        /// </summary>
        /// <param name="n">The index of the number in the Fibonacci sequence.</param>
        /// <returns>The n-th Fibonacci number.</returns>
        /// <exception cref="System.OverflowException">n greater than 92.</exception>
        public static long Fibonacci(int n)
        {
            if (n < 0)
            {
                if (NumberTheory.IsEven(n))
                {
                    return -Fibonacci(-n);
                }

                return Fibonacci(-n);
            }

            if (n == 0)
            {
                return 0L;
            }

            if (n <= 2)
            {
                return 1L;
            }

            if (n > 92)
            {
                throw new OverflowException();
            }

            long F1 = 1L;
            long F2 = 2L;

            while (n-- > 3)
            {
                long F = F1 + F2;
                F1 = F2;
                F2 = F;
            }

            return F2;
        }

        #endregion
    }
}
