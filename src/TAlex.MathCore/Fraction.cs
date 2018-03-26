using System;
using System.Globalization;


namespace TAlex.MathCore
{
    /// <summary>
    /// Represents a rational number.
    /// </summary>
    public struct Fraction : IComparable, IFormattable, IComparable<Fraction>, IEquatable<Fraction>
    {
        #region Fields

        /// <summary>
        /// The numerator of the rational number.
        /// </summary>
        private long _num;

        /// <summary>
        /// The denominator of the rational number.
        /// </summary>
        private long _den;

        /// <summary>
        /// Represents the rational number zero.
        /// </summary>
        public static readonly Fraction Zero = new Fraction(0L);

        /// <summary>
        /// Represents the rational number one.
        /// </summary>
        public static readonly Fraction One = new Fraction(1L);

        /// <summary>
        /// Represents the rational number minus one.
        /// </summary>
        public static readonly Fraction MinusOne = new Fraction(-1L);

        /// <summary>
        /// Represents the largest possible value of a rational number.
        /// </summary>
        public static readonly Fraction MaxValue = new Fraction(long.MaxValue);

        /// <summary>
        /// Represents the smallest possible value of a rational number.
        /// </summary>
        public static readonly Fraction MinValue = new Fraction(long.MinValue);

        #endregion

        #region Properties

        /// <summary>
        /// Gets the numerator of the rational number.
        /// </summary>
        public long Numerator
        {
            get
            {
                return _num;
            }
        }

        /// <summary>
        /// Gets the denominator of the rational number.
        /// </summary>
        public long Denominator
        {
            get
            {
                return _den;
            }
        }

        /// <summary>
        /// Gets a value that indicates whether the rational number is equal to zero.
        /// </summary>
        public bool IsZero
        {
            get
            {
                return (_num == 0L);
            }
        }

        /// <summary>
        /// Gets a value that indicates whether the rational number is equal to one.
        /// </summary>
        public bool IsOne
        {
            get
            {
                return (_num == 1L && _den == 1L);
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a rational number from the specified value.
        /// </summary>
        /// <param name="value">The value of the rational number.</param>
        public Fraction(long value)
        {
            _num = value;
            _den = 1L;
        }

        /// <summary>
        /// Initializes a rational number from the numerator and denominator.
        /// </summary>
        /// <param name="numerator">The numerator of the rational number.</param>
        /// <param name="denominator">The denominator of the rational number.</param>
        /// <exception cref="System.DivideByZeroException">The value of denominator is equal to zero.</exception>
        public Fraction(long numerator, long denominator) :
            this(numerator, denominator, true)
        {
        }

        /// <summary>
        /// Initializes a rational number from the numerator and denominator
        /// with the possibility of normalizing.
        /// </summary>
        /// <param name="numerator">The numerator of the rational number.</param>
        /// <param name="denominator">The denominator of the rational number.</param>
        /// <param name="normalize">A value that indicates whether the rational number should be normalized.</param>
        /// <exception cref="System.DivideByZeroException">The value of denominator is equal to zero.</exception>
        private Fraction(long numerator, long denominator, bool normalize)
        {
            if (denominator == 0L)
                throw new DivideByZeroException("The denominator of the rational number must be non-zero value.");

            if (denominator < 0L)
            {
                _num = -numerator;
                _den = -denominator;
            }
            else
            {
                _num = numerator;
                _den = denominator;
            }

            if (normalize)
            {
                Normalize(ref _num, ref _den);
            }
        }

        #endregion

        #region Methods

        #region Statics

        /// <summary>
        /// Returns the absolute value of a rational number.
        /// </summary>
        /// <param name="frac">A rational number.</param>
        /// <returns>The absolute value of frac.</returns>
        public static Fraction Abs(Fraction frac)
        {
            return new Fraction(Math.Abs(frac._num), frac._den, false);
        }

        /// <summary>
        /// Adds two rational numbers.
        /// </summary>
        /// <param name="frac1">A rational number (the first term).</param>
        /// <param name="frac2">A rational number (the second term).</param>
        /// <returns>The sum of rational numbers.</returns>
        public static Fraction Add(Fraction frac1, Fraction frac2)
        {
            return frac1 + frac2;
        }

        /// <summary>
        /// Subtracts one rational number from another.
        /// </summary>
        /// <param name="frac1">A rational number (the minuend).</param>
        /// <param name="frac2">A rational number (the subtrahend).</param>
        /// <returns>The result of subtracting frac2 from frac1.</returns>
        public static Fraction Subtract(Fraction frac1, Fraction frac2)
        {
            return frac1 - frac2;
        }

        /// <summary>
        /// Multiplies two rational numbers.
        /// </summary>
        /// <param name="frac1">A rational number (the multiplicand).</param>
        /// <param name="frac2">A rational number (the multiplier).</param>
        /// <returns>The product of frac1 and frac2.</returns>
        public static Fraction Multiply(Fraction frac1, Fraction frac2)
        {
            return frac1 * frac2;
        }

        /// <summary>
        /// Divides two rational numbers.
        /// </summary>
        /// <param name="frac1">A rational number (the dividend).</param>
        /// <param name="frac2">A rational number (the divisor).</param>
        /// <returns>The result of dividing frac1 by frac2.</returns>
        /// <exception cref="System.DivideByZeroException">The value of frac2 is zero.</exception>
        public static Fraction Divide(Fraction frac1, Fraction frac2)
        {
            return frac1 / frac2;
        }

        /// <summary>
        /// Returns the inverse value of a rational number.
        /// </summary>
        /// <param name="frac">A rational number.</param>
        /// <returns>The inverse value of frac.</returns>
        /// <exception cref="System.DivideByZeroException">The value of frac is equal to zero.</exception>
        public static Fraction Inverse(Fraction frac)
        {
            if (frac.IsZero)
                throw new DivideByZeroException("Cannot invert rational number zero.");

            return new Fraction(frac._den, frac._num, false);
        }

        /// <summary>
        /// Returns the result of multiplying a rational number by negative one.
        /// </summary>
        /// <param name="frac">A rational number.</param>
        /// <returns>The rational number with the value of frac, but the opposite sign.</returns>
        public static Fraction Negate(Fraction frac)
        {
            return -frac;
        }

        /// <summary>
        /// Returns a rational number raised to an integer power.
        /// </summary>
        /// <param name="frac">A rational number to be raised to a power.</param>
        /// <param name="exponent">An integer number that specifies a power.</param>
        /// <returns>The rational number frac raised to the power exponent.</returns>
        /// <exception cref="System.DivideByZeroException">frac is zero and exponent is less than zero.</exception>
        public static Fraction Pow(Fraction frac, int exponent)
        {
            if (exponent == 0)
            {
                return One;
            }

            if (frac.IsZero)
            {
                if (exponent > 0)
                    return Zero;
                else
                    throw new DivideByZeroException();
            }

            if (exponent > 0)
            {
                return new Fraction(ExMath.Pow(frac._num, exponent), ExMath.Pow(frac._den, exponent), false);
            }

            return new Fraction(ExMath.Pow(frac._den, -exponent), ExMath.Pow(frac._num, -exponent), false);
        }

        /// <summary>
        /// Returns the smaller of two rational numbers.
        /// </summary>
        /// <param name="frac1">The first rational number to compare.</param>
        /// <param name="frac2">The second rational number to compare.</param>
        /// <returns>The smaller of two rational numbers.</returns>
        public static Fraction Min(Fraction frac1, Fraction frac2)
        {
            return (frac1 < frac2) ? frac1 : frac2;
        }

        /// <summary>
        /// Returns the larger of two rational numbers.
        /// </summary>
        /// <param name="frac1">The first rational number to compare.</param>
        /// <param name="frac2">The second rational number to compare.</param>
        /// <returns>The larger of two rational numbers.</returns>
        public static Fraction Max(Fraction frac1, Fraction frac2)
        {
            return (frac1 > frac2) ? frac1 : frac2;
        }

        /// <summary>
        /// Returns the largest rational integer less than or equal to the specified rational number.
        /// </summary>
        /// <param name="frac">A rational number.</param>
        /// <returns>The largest rational integer less than or equal to frac.</returns>
        public static Fraction Floor(Fraction frac)
        {
            if (frac._den == 1L)
                return frac;
            
            long quotient = frac._num / frac._den;

            if (frac._num <= 0)
                return new Fraction(quotient - 1L);
            else
                return new Fraction(quotient);
        }

        /// <summary>
        /// Returns the smallest rational integer greater than or equal to the specified rational number.
        /// </summary>
        /// <param name="frac">A rational number.</param>
        /// <returns>The smallest rational integer greater than or equal to frac.</returns>
        public static Fraction Ceiling(Fraction frac)
        {
            if (frac._den == 1L)
                return frac;

            long quotient = frac._num / frac._den;

            if (frac._num >= 0)
                return new Fraction(quotient + 1L);
            else
                return new Fraction(quotient);
        }

        /// <summary>
        /// Returns the integral part of a specified rational number.
        /// </summary>
        /// <param name="frac">A rational number to truncate.</param>
        /// <returns>
        /// The integral part of frac; that is, the number that remains after any fractional
        /// digits have been discarded.
        /// </returns>
        public static Fraction Truncate(Fraction frac)
        {
            if (frac._den == 1L)
                return frac;

            return new Fraction(frac._num / frac._den);
        }

        /// <summary>
        /// Converts string representation into the equivalent of a rational number.
        /// </summary>
        /// <param name="s">A string containing a rational number to convert.</param>
        /// <returns>rational number equivalent to the numeric value or symbol specified in s.</returns>
        /// <exception cref="System.ArgumentNullException">The string s is null.</exception>
        /// <exception cref="System.ArgumentException">The string s is empty string.</exception>
        /// <exception cref="System.FormatException">The string s is not a number in a valid format.</exception>
        public static Fraction Parse(string s)
        {
            return Parse(s, NumberFormatInfo.CurrentInfo);
        }

        /// <summary>
        /// Converts string representation of a number
        /// in a specified culture-specific format to its rational number equivalent.
        /// </summary>
        /// <param name="s">A string containing a rational number to convert.</param>
        /// <param name="provider">An <see cref="System.IFormatProvider"/> that supplies culture-specific formatting information about s.</param>
        /// <returns>rational number equivalent to the numeric value or symbol specified in s.</returns>
        /// <exception cref="System.ArgumentNullException">The string s is null.</exception>
        /// <exception cref="System.ArgumentException">The string s is empty string.</exception>
        /// <exception cref="System.FormatException">The string s is not a number in a valid format.</exception>
        public static Fraction Parse(string s, IFormatProvider provider)
        {
            if (s == null)
                throw new ArgumentNullException("String should not be null reference.");

            s = s.Trim();

            if (s.Length == 0)
                throw new ArgumentException("String should not be empty.");

            string[] fracParts = s.Split('/');

            if (fracParts.Length == 1)
                return new Fraction(long.Parse(fracParts[0], provider));
            else if (fracParts.Length == 2)
                return new Fraction(long.Parse(fracParts[0], provider), long.Parse(fracParts[1], provider));
            else
                throw new FormatException("Invalid format of the rational number.");
        }

        /// <summary>
        /// Normalizes two integer numbers.
        /// </summary>
        /// <param name="a">An 64-bit integer.</param>
        /// <param name="b">An 64-bit integer.</param>
        private static void Normalize(ref long a, ref long b)
        {
            if (a == 0L)
            {
                a = 0L;
                b = 1L;
            }
            else
            {
                long n = SpecialFunctions.NumberTheory.GCD(a, b);

                if (n != 1L)
                {
                    a = a / n;
                    b = b / n;
                }
            }
        }

        #endregion

        #region Dynamics

        /// <summary>
        /// Compares this instance to a specified rational number and returns
        /// an integer that indicates whether the value of this instance
        /// is less than, equal to, or greater than the value of the specified rational number.
        /// </summary>
        /// <param name="frac">A rational number to compare.</param>
        /// <returns>
        /// A signed number indicating the relative values of this instance and frac.
        /// Return value meaning:
        /// less than zero if this instance is less than frac;
        /// zero if this instance is equal to frac;
        /// greater than zero if this instance is greater than frac.
        /// </returns>
        public int CompareTo(Fraction frac)
        {
            return ((double)this).CompareTo((double)frac);
        }

        /// <summary>
        /// Compares this instance to a specified object and returns
        /// an integer that indicates whether the value of this instance
        /// is less than, equal to, or greater than the value of the specified object.
        /// </summary>
        /// <param name="obj">An object to compare.</param>
        /// <returns>
        /// A signed number indicating the relative values of this instance and obj.
        /// Return value meaning:
        /// less than zero if this instance is less than obj;
        /// zero if this instance is equal to obj;
        /// greater than zero if this instance is greater than obj, -or- obj is null.
        /// </returns>
        /// <exception cref="System.ArgumentException">obj is not a Fraction.</exception>
        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            if (!(obj is Fraction))
                throw new ArgumentException("The type of object must be Fraction.");

            return CompareTo((Fraction)obj);
        }

        /// <summary>
        /// Converts the rational value of this instance to its equivalent string representation.
        /// </summary>
        /// <returns>The string representation of the value of this instance.</returns>
        public override string ToString()
        {
            return ToString(null, NumberFormatInfo.CurrentInfo);
        }

        /// <summary>
        /// Converts the rational value of this instance to its equivalent string representation.
        /// </summary>
        /// <param name="provider">An System.IFormatProvider that supplies culture-specific formatting information.</param>
        /// <returns>The string representation of the value of this instance as specified by provider.</returns>
        public string ToString(IFormatProvider provider)
        {
            return ToString(null, provider);
        }

        /// <summary>
        /// Converts the rational value of this instance to its equivalent string representation.
        /// </summary>
        /// <param name="format">A numeric format string.</param>
        /// <returns>The string representation of the value of this instance as specified by format.</returns>
        /// <exception cref="System.FormatException">The format is invalid.</exception>
        public string ToString(string format)
        {
            return ToString(format, NumberFormatInfo.CurrentInfo);
        }

        /// <summary>
        /// Converts the rational value of this instance to its equivalent string representation.
        /// </summary>
        /// <param name="format">A numeric format string.</param>
        /// <param name="provider">An System.IFormatProvider that supplies culture-specific formatting information.</param>
        /// <returns>The string representation of the value of this instance as specified by format and provider.</returns>
        /// <exception cref="System.FormatException">The format is invalid.</exception>
        public string ToString(string format, IFormatProvider provider)
        {
            if (_den == 1L)
                return _num.ToString(format, provider);

            return _num.ToString(format, provider) + "/" + _den.ToString(format, provider);
        }

        /// <summary>
        /// Returns a value indicating whether this instance is equal
        /// to a specified object.
        /// </summary>
        /// <param name="obj">An object to compare with this instance.</param>
        /// <returns>
        /// True if obj is an instance of Fraction and equals the value of this instance;
        /// otherwise, false.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (!(obj is Fraction)) return false;
            return this == (Fraction)obj;
        }

        /// <summary>
        /// Returns a value indicating whether this instance is equal
        /// to a specified rational number.
        /// </summary>
        /// <param name="obj">A Fraction object to compare to this instance.</param>
        /// <returns>True if obj is equal to this instance; otherwise, false.</returns>
        public bool Equals(Fraction obj)
        {
            return (this == obj);
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>A 32-bit signed integer hash code.</returns>
        public override int GetHashCode()
        {
            int hashCode = _num.GetHashCode();
            return ((hashCode >> 12) + (hashCode << 18)) ^ _den.GetHashCode();
        }

        #endregion

        #endregion

        #region Operators

        /// <summary>
        /// Returns a value indicating whether a specified rational number is less than
        /// another specified rational number.
        /// </summary>
        /// <param name="frac1">The first rational number to compare.</param>
        /// <param name="frac2">The second rational number to compare.</param>
        /// <returns>True if frac1 is less than frac2; otherwise, false.</returns>
        public static bool operator <(Fraction frac1, Fraction frac2)
        {
            return (frac1.CompareTo(frac2) < 0);
        }

        /// <summary>
        /// Returns a value indicating whether a specified rational number is less than
        /// or equal to another specified rational number.
        /// </summary>
        /// <param name="frac1">The first rational number to compare.</param>
        /// <param name="frac2">The second rational number to compare.</param>
        /// <returns>True if frac1 is less than or equal to frac2; otherwise, false.</returns>
        public static bool operator <=(Fraction frac1, Fraction frac2)
        {
            return (frac1.CompareTo(frac2) <= 0);
        }

        /// <summary>
        /// Returns a value indicating whether a specified rational number is greater than
        /// another specified rational number.
        /// </summary>
        /// <param name="frac1">The first rational number to compare.</param>
        /// <param name="frac2">The second rational number to compare.</param>
        /// <returns>True if frac1 is greater than frac2; otherwise, false.</returns>
        public static bool operator >(Fraction frac1, Fraction frac2)
        {
            return (frac1.CompareTo(frac2) > 0);
        }

        /// <summary>
        /// Returns a value indicating whether a specified rational number is greater than
        /// or equal to another specified rational number.
        /// </summary>
        /// <param name="frac1">The first rational number to compare.</param>
        /// <param name="frac2">The second rational number to compare.</param>
        /// <returns>True if frac1 is greater than or equal to frac2; otherwise, false.</returns>
        public static bool operator >=(Fraction frac1, Fraction frac2)
        {
            return (frac1.CompareTo(frac2) >= 0);
        }

        /// <summary>
        /// Returns a value indicating whether two instances of rational number are equal.
        /// </summary>
        /// <param name="frac1">The first rational number to compare.</param>
        /// <param name="frac2">The second rational number to compare.</param>
        /// <returns>True if frac1 and frac2 are equal; otherwise, false.</returns>
        public static bool operator ==(Fraction frac1, Fraction frac2)
        {
            return (frac1._num == frac2._num && frac1._den == frac2._den);
        }

        /// <summary>
        /// Returns a value indicating whether two instances of rational number are not equal.
        /// </summary>
        /// <param name="frac1">The first rational number to compare.</param>
        /// <param name="frac2">The second rational number to compare.</param>
        /// <returns>True if frac1 and frac2 are not equal; otherwise, false.</returns>
        public static bool operator !=(Fraction frac1, Fraction frac2)
        {
            return (frac1._num != frac2._num || frac1._den != frac2._den);
        }

        /// <summary>
        /// Returns the value of the rational number operand
        /// (the sign of the operand is unchanged).
        /// </summary>
        /// <param name="frac">A rational number.</param>
        /// <returns>The value of the operand, frac.</returns>
        public static Fraction operator +(Fraction frac)
        {
            return frac;
        }

        /// <summary>
        /// Negates the value of the rational number operand.
        /// </summary>
        /// <param name="frac">A rational number.</param>
        /// <returns>The result of frac multiplied by negative one.</returns>
        public static Fraction operator -(Fraction frac)
        {
            return new Fraction(-frac._num, frac._den, false);
        }

        /// <summary>
        /// Adds two rational numbers.
        /// </summary>
        /// <param name="frac1">A rational number (the first term).</param>
        /// <param name="frac2">A rational number (the second term).</param>
        /// <returns>The sum of rational numbers.</returns>
        public static Fraction operator +(Fraction frac1, Fraction frac2)
        {
            if (frac1.IsZero) return frac2;
            if (frac2.IsZero) return frac1;

            long den1 = frac1._den;
            long den2 = frac2._den;

            if (den1 == 1L)
            {
                long num = (frac1._num * den2) + frac2._num;
                if (num == 0L) return Zero;

                return new Fraction(num, frac2._den, false);
            }

            if (den2 == 1L)
            {
                long num = frac1._num + (frac2._num * den1);
                if (num == 0L) return Zero;

                return new Fraction(num, frac1._den, false);
            }

            Normalize(ref den1, ref den2);

            long numerator = (frac1._num * den2) + (frac2._num * den1);
            if (numerator == 0L) return Zero;

            return new Fraction(numerator, den1 * frac2._den);
        }

        /// <summary>
        /// Subtracts one rational number from another.
        /// </summary>
        /// <param name="frac1">A rational number (the minuend).</param>
        /// <param name="frac2">A rational number (the subtrahend).</param>
        /// <returns>The result of subtracting frac2 from frac1.</returns>
        public static Fraction operator -(Fraction frac1, Fraction frac2)
        {
            if (frac1.IsZero) return -frac2;
            if (frac2.IsZero) return frac1;

            long den1 = frac1._den;
            long den2 = frac2._den;

            if (den1 == 1L)
            {
                long num = (frac1._num * den2) - frac2._num;
                if (num == 0L) return Zero;

                return new Fraction(num, frac2._den, false);
            }

            if (den2 == 1L)
            {
                long num = frac1._num - (frac2._num * den1);
                if (num == 0L) return Zero;

                return new Fraction(num, frac1._den, false);
            }

            Normalize(ref den1, ref den2);

            long numerator = (frac1._num * den2) - (frac2._num * den1);
            if (numerator == 0L) return Zero;

            return new Fraction(numerator, den1 * frac2._den);
        }

        /// <summary>
        /// Multiplies two rational numbers.
        /// </summary>
        /// <param name="frac1">A rational number (the multiplicand).</param>
        /// <param name="frac2">A rational number (the multiplier).</param>
        /// <returns>The product of frac1 and frac2.</returns>
        public static Fraction operator *(Fraction frac1, Fraction frac2)
        {
            if (frac1.IsZero || frac2.IsZero)
                return Zero;

            long num1 = frac1._num;
            long num2 = frac2._num;
            long den1 = frac1._den;
            long den2 = frac2._den;

            Normalize(ref den1, ref num2);
            Normalize(ref den2, ref num1);

            return new Fraction(num1 * num2, den1 * den2, false);
        }

        /// <summary>
        /// Divides two rational numbers.
        /// </summary>
        /// <param name="frac1">A rational number (the dividend).</param>
        /// <param name="frac2">A rational number (the divisor).</param>
        /// <returns>The result of dividing frac1 by frac2.</returns>
        /// <exception cref="System.DivideByZeroException">The value of frac2 is zero.</exception>
        public static Fraction operator /(Fraction frac1, Fraction frac2)
        {
            if (frac2.IsZero)
                throw new DivideByZeroException("The divisor should not be zero.");

            if (frac1.IsZero)
                return Zero;

            long num1 = frac1._num;
            long num2 = frac2._num;
            long den1 = frac1._den;
            long den2 = frac2._den;

            Normalize(ref den1, ref den2);
            Normalize(ref num2, ref num1);

            return new Fraction(num1 * den2, den1 * num2, false);
        }

        /// <summary>
        /// Converts a 16-bit signed integer to a rational number.
        /// </summary>
        /// <param name="value">A 16-bit signed integer.</param>
        /// <returns>A rational number that represents the converted 16-bit signed integer.</returns>
        public static implicit operator Fraction(short value)
        {
            return new Fraction(value);
        }

        /// <summary>
        /// Converts a 32-bit signed integer to a rational number.
        /// </summary>
        /// <param name="value">A 32-bit signed integer.</param>
        /// <returns>A rational number that represents the converted 32-bit signed integer.</returns>
        public static implicit operator Fraction(int value)
        {
            return new Fraction(value);
        }

        /// <summary>
        /// Converts a 64-bit signed integer to a rational number.
        /// </summary>
        /// <param name="value">A 64-bit signed integer.</param>
        /// <returns>A rational number that represents the converted 64-bit signed integer.</returns>
        public static implicit operator Fraction(long value)
        {
            return new Fraction(value);
        }

        /// <summary>
        /// Converts a rational number to a 16-bit signed integer.
        /// </summary>
        /// <param name="value">A rational number.</param>
        /// <returns>A 16-bit signed integer that represents the converted rational number.</returns>
        public static explicit operator short(Fraction value)
        {
            return (short)((int)value._num / (int)value._den);
        }

        /// <summary>
        /// Converts a rational number to a 32-bit signed integer.
        /// </summary>
        /// <param name="value">A rational number.</param>
        /// <returns>A 32-bit signed integer that represents the converted rational number.</returns>
        public static explicit operator int(Fraction value)
        {
            return (int)value._num / (int)value._den;
        }

        /// <summary>
        /// Converts a rational number to a 64-bit signed integer.
        /// </summary>
        /// <param name="value">A rational number.</param>
        /// <returns>A 64-bit signed integer that represents the converted rational number.</returns>
        public static explicit operator long(Fraction value)
        {
            return value._num / value._den;
        }

        /// <summary>
        /// Converts a rational number to a single-precision floating-point number.
        /// </summary>
        /// <param name="value">A rational number.</param>
        /// <returns>A single-precision floating-point number that represents the converted rational number.</returns>
        public static explicit operator float(Fraction value)
        {
            return (float)value._num / (float)value._den;
        }

        /// <summary>
        /// Converts a rational number to a double-precision floating-point number.
        /// </summary>
        /// <param name="value">A rational number.</param>
        /// <returns>A double-precision floating-point number that represents the converted rational number.</returns>
        public static explicit operator double(Fraction value)
        {
            return (double)value._num / (double)value._den;
        }

        /// <summary>
        /// Converts a rational number to a decimal number.
        /// </summary>
        /// <param name="value">A rational number.</param>
        /// <returns>A decimal number that represents the converted rational number.</returns>
        public static explicit operator decimal(Fraction value)
        {
            return (decimal)value._num / (decimal)value._den;
        }

        #endregion
    }
}
