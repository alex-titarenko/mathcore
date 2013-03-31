using System;


namespace TAlex.MathCore
{
    /// <summary>
    /// Represents various numerical utilities.
    /// </summary>
    public static class NumericUtil
    {
        #region Fields

        private const int MaxComplexZeroThreshold = 307;

        #endregion

        #region Methods

        /// <summary>
        /// Returns are much larger the real or imaginary part of a complex number.
        /// If the ratio of real and imaginary parts of complex number are not so large
        /// returns the initial value.
        /// </summary>
        /// <param name="value">A complex number.</param>
        /// <param name="complexThreshold">An integer representing the complex threshold.</param>
        /// <returns>
        /// Are much larger the real or imaginary part of the value.
        /// If the ratio of real and imaginary parts of the value are not so large
        /// returns the value.
        /// </returns>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// complexThreshold must be between 0 and 307.
        /// </exception>
        public static Complex ComplexThreshold(Complex value, int complexThreshold)
        {
            if (complexThreshold < 0 || complexThreshold > MaxComplexZeroThreshold)
                throw new ArgumentOutOfRangeException("complexThreshold", String.Format("Complex threshold must be between 0 and {0}.", MaxComplexZeroThreshold));

            if (value.IsReal || value.IsImaginary)
            {
                return value;
            }

            double d = Math.Pow(10, complexThreshold);

            double reAbs = Math.Abs(value.Re);
            double imAbs = Math.Abs(value.Im);

            if ((reAbs > imAbs) && (reAbs / imAbs > d))
            {
                return new Complex(value.Re, 0.0);
            }
            else if ((imAbs > reAbs) && (imAbs / reAbs > d))
            {
                return new Complex(0.0, value.Im);
            }

            return value;
        }

        /// <summary>
        /// Returns a zero value if the initial value is close to him.
        /// Otherwise, returns the initial value.
        /// </summary>
        /// <param name="value">A real number.</param>
        /// <param name="zeroThreshold">An integer representing the zero threshold.</param>
        /// <returns>
        /// A zero value if the value is close to him.
        /// Otherwise, returns the value.
        /// </returns>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// zeroThreshold must be between 0 and 307.
        /// </exception>
        public static double ZeroThreshold(double value, int zeroThreshold)
        {
            if (zeroThreshold < 0 || zeroThreshold > MaxComplexZeroThreshold)
                throw new ArgumentOutOfRangeException("zeroThreshold", String.Format("The zero threshold must be between 0 and {0}.", MaxComplexZeroThreshold));

            double d = Math.Pow(10, -zeroThreshold);
            return (Math.Abs(value) < d) ? 0.0 : value;
        }

        /// <summary>
        /// Returns a zero value if the initial value is close to him.
        /// Otherwise, returns the initial value.
        /// </summary>
        /// <param name="value">A complex number.</param>
        /// <param name="zeroThreshold">An integer representing the zero threshold.</param>
        /// <returns>
        /// A zero value if the value is close to him.
        /// Otherwise, returns the value.
        /// </returns>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// zeroThreshold must be between 0 and 307.
        /// </exception>
        public static Complex ZeroThreshold(Complex value, int zeroThreshold)
        {
            if (zeroThreshold < 0 || zeroThreshold > MaxComplexZeroThreshold)
                throw new ArgumentOutOfRangeException("zeroThreshold", String.Format("The zero threshold must be between 0 and {0}.", MaxComplexZeroThreshold));

            double d = Math.Pow(10, -zeroThreshold);

            double re = (Math.Abs(value.Re) < d) ? 0.0 : value.Re;
            double im = (Math.Abs(value.Im) < d) ? 0.0 : value.Im;

            return new Complex(re, im);
        }

        /// <summary>
        /// Applies the complex and zero threshold for a complex number and returns the result.
        /// </summary>
        /// <param name="value">A complex number.</param>
        /// <param name="complexThreshold">An integer representing the complex threshold.</param>
        /// <param name="zeroThreshold">An integer representing the zero threshold.</param>
        /// <returns>
        /// The result of applying a complex and zero threshold for the complex number value.
        /// </returns>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// complexThreshold and zeroThreshold must be between 0 and 307.
        /// </exception>
        public static Complex ComplexZeroThreshold(Complex value, int complexThreshold, int zeroThreshold)
        {
            return ZeroThreshold(ComplexThreshold(value, complexThreshold), zeroThreshold);
        }

        /// <summary>
        /// Returns a value that indicates whether the two double numbers are equal with the specified relative tolerance.
        /// </summary>
        /// <param name="value1">The first double value to fuzzy compare.</param>
        /// <param name="value2">The second double value to fuzzy compare.</param>
        /// <param name="relativeTolerance">A double value that represents the relative tolerance for compare.</param>
        /// <returns>true if the value1 and value2 are fuzzy equal with the specified relative tolerance; otherwise false.</returns>
        public static bool FuzzyEquals(double value1, double value2, double relativeTolerance)
        {
            if (value1 == value2) return true;

            if (value1 == 0 || value2 == 0)
            {
                return Math.Abs(value1 - value2) <= relativeTolerance;
            }

            return Math.Abs(value1 - value2) <= relativeTolerance * Math.Max(Math.Abs(value1), Math.Abs(value2));
        }

        /// <summary>
        /// Returns a value that indicates whether the two complex numbers are equal with the specified relative tolerance.
        /// </summary>
        /// <param name="value1">The first complex number to fuzzy compare.</param>
        /// <param name="value2">The second complex number to fuzzy compare.</param>
        /// <param name="relativeTolerance">A double value that represents the relative tolerance for compare.</param>
        /// <returns>true if the value1 and value2 are fuzzy equal with the specified relative tolerance; otherwise false.</returns>
        public static bool FuzzyEquals(Complex value1, Complex value2, double relativeTolerance)
        {
            if (value1 == value2) return true;

            if (value1.IsZero || value2.IsZero)
            {
                return Complex.Abs(value1 - value2) <= relativeTolerance;
            }

            return Complex.Abs(value1 - value2) <= relativeTolerance * Math.Max(Complex.Abs(value1), Complex.Abs(value2));
        }

        /// <summary>
        /// Returns whether or not two doubles are "close".  That is, whether or 
        /// not they are within epsilon of each other.  Note that this epsilon is proportional
        /// to the numbers themselves to that AreClose survives scalar multiplication.
        /// There are plenty of ways for this to return false even for numbers which
        /// are theoretically identical, so no code calling this should fail to work if this 
        /// returns false.
        /// </summary>
        /// <param name="value1">The first double value to compare.</param>
        /// <param name="value2">The second double value to compare.</param>
        /// <returns>result of the AreClose comparision.</returns>
        public static bool AreClose(double value1, double value2)
        {
            // in case they are Infinities (then epsilon check does not work)
            if (value1 == value2)
            {
                return true;
            }

            // This computes (|value1-value2| / (|value1| + |value2| + 10.0)) < DBL_EPSILON
            double eps = (Math.Abs(value1) + Math.Abs(value2) + 10.0) * Machine.Epsilon;
            double delta = value1 - value2;
            return (-eps < delta) && (eps > delta);
        }

        #endregion
    }
}
