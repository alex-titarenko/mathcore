using System;


namespace TAlex.MathCore.SpecialFunctions
{
    /// <summary>
    /// Represents various functions of number theory.
    /// </summary>
    public static class NumberTheory
    {
        #region Methods

        /// <summary>
        /// Returns the greatest common divisor of two integer numbers.
        /// </summary>
        /// <param name="a">An 32-bit integer number.</param>
        /// <param name="b">An 32-bit integer number.</param>
        /// <returns>The greatest common divisor of a and b.</returns>
        public static int GCD(int a, int b)
        {
            while (b != 0)
            {
                int temp = a % b;
                a = b;
                b = temp;
            }

            return Math.Abs(a);
        }

        /// <summary>
        /// Returns the greatest common divisor of two integer numbers.
        /// </summary>
        /// <param name="a">An 64-bit integer number.</param>
        /// <param name="b">An 64-bit integer number.</param>
        /// <returns>The greatest common divisor of a and b.</returns>
        public static long GCD(long a, long b)
        {
            while (b != 0L)
            {
                long temp = a % b;
                a = b;
                b = temp;
            }

            return Math.Abs(a);
        }

        /// <summary>
        /// Returns the greatest common divisor of an array of integer numbers.
        /// </summary>
        /// <param name="numbers">An array of 32-bit integer numbers.</param>
        /// <returns>The greatest common divisor of numbers.</returns>
        public static int GCD(params int[] numbers)
        {
            int result = numbers[0];

            for (int i = 1; i < numbers.Length; i++)
            {
                result = GCD(result, numbers[i]);
            }

            return result;
        }

        /// <summary>
        /// Returns the greatest common divisor of an array of integer numbers.
        /// </summary>
        /// <param name="numbers">An array of 64-bit integer numbers.</param>
        /// <returns>The greatest common divisor of numbers.</returns>
        public static long GCD(params long[] numbers)
        {
            long result = numbers[0];

            for (int i = 1; i < numbers.Length; i++)
            {
                result = GCD(result, numbers[i]);
            }

            return result;
        }

        /// <summary>
        /// Returns the least common multiple of two integer numbers.
        /// </summary>
        /// <param name="a">An 32-bit integer number.</param>
        /// <param name="b">An 32-bit integer number.</param>
        /// <returns>The least common multiple of a and b.</returns>
        public static int LCM(int a, int b)
        {
            return (a * b) / GCD(a, b);
        }

        /// <summary>
        /// Returns the least common multiple of two integer numbers.
        /// </summary>
        /// <param name="a">An 64-bit integer number.</param>
        /// <param name="b">An 64-bit integer number.</param>
        /// <returns>The least common multiple of a and b.</returns>
        public static long LCM(long a, long b)
        {
            return (a * b) / GCD(a, b);
        }

        /// <summary>
        /// Returns the least common multiple of an array of integer numbers.
        /// </summary>
        /// <param name="numbers">An array of 32-bit integer numbers.</param>
        /// <returns>The least common multiple of numbers.</returns>
        public static int LCM(params int[] numbers)
        {
            int result = numbers[0];

            for (int i = 1; i < numbers.Length; i++)
            {
                result = LCM(result, numbers[i]);
            }

            return result;
        }

        /// <summary>
        /// Returns the least common multiple of an array of integer numbers.
        /// </summary>
        /// <param name="numbers">An array of 64-bit integer numbers.</param>
        /// <returns>The least common multiple of numbers.</returns>
        public static long LCM(params long[] numbers)
        {
            long result = numbers[0];

            for (int i = 1; i < numbers.Length; i++)
            {
                result = LCM(result, numbers[i]);
            }

            return result;
        }

        /// <summary>
        /// Returns a value that indicates whether an integer number is even.
        /// </summary>
        /// <param name="n">A 16-bit integer number.</param>
        /// <returns>true if the number n is even; false otherwise.</returns>
        public static bool IsEven(short n)
        {
            return ((n & 1) == 0);
        }

        /// <summary>
        /// Returns a value that indicates whether an integer number is even.
        /// </summary>
        /// <param name="n">A 32-bit integer number.</param>
        /// <returns>true if the number n is even; false otherwise.</returns>
        public static bool IsEven(int n)
        {
            return ((n & 1) == 0);
        }

        /// <summary>
        /// Returns a value that indicates whether an integer number is even.
        /// </summary>
        /// <param name="n">A 64-bit integer number.</param>
        /// <returns>true if the number n is even; false otherwise.</returns>
        public static bool IsEven(long n)
        {
            return ((n & 1L) == 0L);
        }

        /// <summary>
        /// Returns a value that indicates whether an integer number is odd.
        /// </summary>
        /// <param name="n">A 16-bit integer number.</param>
        /// <returns>true if the number n is odd; false otherwise.</returns>
        public static bool IsOdd(short n)
        {
            return ((n & 1) != 0);
        }

        /// <summary>
        /// Returns a value that indicates whether an integer number is odd.
        /// </summary>
        /// <param name="n">A 32-bit integer number.</param>
        /// <returns>true if the number n is odd; false otherwise.</returns>
        public static bool IsOdd(int n)
        {
            return ((n & 1) != 0);
        }

        /// <summary>
        /// Returns a value that indicates whether an integer number is odd.
        /// </summary>
        /// <param name="n">A 64-bit integer number.</param>
        /// <returns>true if the number n is odd; false otherwise.</returns>
        public static bool IsOdd(long n)
        {
            return ((n & 1L) != 0L);
        }

        #endregion
    }
}
