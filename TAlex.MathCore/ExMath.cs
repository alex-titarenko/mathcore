using System;


namespace TAlex.MathCore
{
    /// <summary>
    /// Provides mathematical constants and methods for
    /// evaluating elementary and common mathematical functions.
    /// </summary>
    /// <remarks>
    /// Represents the extension of the base <see cref="System.Math"/> class.
    /// </remarks>
    public static class ExMath
    {
        #region Fields

        /// <summary>
        /// Represents pi, the ratio of the circumference of a circle to its diameter.
        /// </summary>
        public const double Pi = 3.1415926535897931;

        /// <summary>
        /// Represents the twice the value of pi.
        /// </summary>
        public const double TwoPi = 6.2831853071795862;

        /// <summary>
        /// Represents half the value of pi.
        /// </summary>
        public const double PiOverTwo = 1.5707963267948966;

        /// <summary>
        /// Represents pi squared.
        /// </summary>
        public const double PiSquared = 9.869604401089358;

        /// <summary>
        /// Represents the square root of pi.
        /// </summary>
        public const double SqrtPi = 1.7724538509055161;

        /// <summary>
        /// Represents e, the natural logarithmic base.
        /// </summary>
        public const double E = 2.7182818284590451;

        /// <summary>
        /// Represents the natural logarithm of 2.
        /// </summary>
        public const double Log2 = 0.69314718055994529;

        /// <summary>
        /// Represents the Golden Ratio.
        /// </summary>
        public const double GoldenRatio = 1.6180339887498949;

        /// <summary>
        /// Represents the Euler–Mascheroni constant.
        /// </summary>
        public const double EulersConstant = 0.57721566490153287;

        /// <summary>
        /// Represents the Catalan's constant G
        /// </summary>
        public const double CatalansConstant = 0.91596559417721901;

        /// <summary>
        /// Represents the square root of 2. Pythagoras' constant.
        /// </summary>
        public const double Sqrt2 = 1.4142135623730952;

        /// <summary>
        /// Represents the square root of 3. Theodorus' constant
        /// </summary>
        public const double Sqrt3 = 1.7320508075688772;

        #endregion

        #region Methods

        /// <summary>
        /// Returns an integer number raised to an integer power.
        /// </summary>
        /// <param name="l">A 64-bit integer number to be raised to a power.</param>
        /// <param name="exponent">An integer number that specifies a power.</param>
        /// <returns>The 64-bit integer number l raised to the power exponent.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// The value of exponent is less than zero.
        /// </exception>
        public static long Pow(long l, int exponent)
        {
            if (exponent == 0)
                return 1L;

            if (exponent < 0)
                throw new ArgumentOutOfRangeException("exponent", "The value of the exponent must be non negative.");

            long result = 1L;

            while (exponent != 0)
            {
                if ((exponent & 1) != 0)
                    result *= l;

                l *= l;
                exponent >>= 1;
            }

            return result;
        }

        /// <summary>
        /// Returns a real number raised to an integer power.
        /// </summary>
        /// <param name="d">A real number to be raised to a power.</param>
        /// <param name="exponent">An integer number that specifies a power.</param>
        /// <returns>The real number d raised to the power exponent.</returns>
        public static double Pow(double d, int exponent)
        {
            if (exponent == 0)
                return 1;

            if (exponent < 0)
            {
                exponent = -exponent;
                d = 1.0 / d;
            }

            double result = 1;

            while (exponent != 0)
            {
                if ((exponent & 1) != 0)
                    result *= d;

                d *= d;
                exponent >>= 1;
            }

            return result;
        }

        /// <summary>
        /// Returns the minus one raised to an integer power.
        /// </summary>
        /// <param name="exponent">An integer number that specifies a power.</param>
        /// <returns>The minus one raised to the power exponent.</returns>
        public static int MinusOnePow(int exponent)
        {
            return ((exponent & 1) == 0) ? 1 : -1;
        }

        /// <summary>
        /// Returns the integer part of a real number.
        /// </summary>
        /// <param name="d">A real number.</param>
        /// <returns>The integer part of d.</returns>
        public static double IntPart(double d)
        {
            return Math.Floor(d);
        }

        /// <summary>
        /// Returns the integer part of a complex number.
        /// </summary>
        /// <param name="c">A complex number.</param>
        /// <returns>The integer part of c.</returns>
        public static Complex IntPart(Complex c)
        {
            return Complex.Floor(c);
        }

        /// <summary>
        /// Returns the fractional part of a real number.
        /// </summary>
        /// <param name="d">A real number.</param>
        /// <returns>The fractional part of d.</returns>
        public static double FracPart(double d)
        {
            return d - Math.Floor(d);
        }

        /// <summary>
        /// Returns the fractional part of a complex number.
        /// </summary>
        /// <param name="c">A complex number.</param>
        /// <returns>The fractional part of c.</returns>
        public static Complex FracPart(Complex c)
        {
            return c - Complex.Floor(c);
        }

        /// <summary>
        /// Returns a value that indicates whether a real number is an 16-bit integer.
        /// </summary>
        /// <param name="value">A real number.</param>
        /// <returns>true if the number value is an 16-bit integer; otherwise false.</returns>
        public static bool IsInt16(double value)
        {
            return (value == (short)value);
        }

        /// <summary>
        /// Returns a value that indicates whether a real number is an 32-bit integer.
        /// </summary>
        /// <param name="value">A real number.</param>
        /// <returns>true if the number value is an 32-bit integer; otherwise false.</returns>
        public static bool IsInt32(double value)
        {
            return (value == (int)value);
        }

        /// <summary>
        /// Returns a value that indicates whether a real number is an 64-bit integer.
        /// </summary>
        /// <param name="value">A real number.</param>
        /// <returns>true if the number value is an 64-bit integer; otherwise false.</returns>
        public static bool IsInt64(double value)
        {
            return (value == (long)value);
        }

        /// <summary>
        /// Returns a value that indicates whether a real number is finite.
        /// </summary>
        /// <param name="value">A real number.</param>
        /// <returns>true if value is not infinite and not NaN; otherwise false.</returns>
        public static bool IsFinite(double value)
        {
            return !(Double.IsNaN(value) || Double.IsInfinity(value));
        }

        /// <summary>
        /// Returns the length of the hypotenuse of a right-angled
        /// triangle with sides of specified length.
        /// </summary>
        /// <param name="a">The length of the first side.</param>
        /// <param name="b">The length of the second side.</param>
        /// <returns>The length of the hypotenuse.</returns>
        public static double Hypot(double a, double b)
        {
            double r;

            if (Math.Abs(a) > Math.Abs(b))
            {
                r = b / a;
                r = Math.Abs(a) * Math.Sqrt(1 + r * r);
            }
            else if (b != 0)
            {
                r = a / b;
                r = Math.Abs(b) * Math.Sqrt(1 + r * r);
            }
            else
            {
                r = 0.0;
            }

            return r;
        }

        /// <summary>
        /// Returns the value of the angle in degrees converted from radians.
        /// </summary>
        /// <param name="radian">An angle, measured in radians.</param>
        /// <returns>The value of the angle in degrees.</returns>
        public static double ToDegrees(double radian)
        {
            return 180.0 / Math.PI * radian;
        }

        /// <summary>
        /// Returns the value of the angle in radians converted from degrees.
        /// </summary>
        /// <param name="degree">An angle, measured in degree.</param>
        /// <returns>The value of the angle in radians.</returns>
        public static double ToRadians(double degree)
        {
            return degree / 180.0 * Math.PI;
        }

        /// <summary>
        /// Returns the sine of a real number.
        /// </summary>
        /// <param name="d">A real number.</param>
        /// <returns>The sine of d.</returns>
        public static double Sin(double d)
        {
            return Math.Sin(d);
        }

        /// <summary>
        /// Returns the cosine of a real number.
        /// </summary>
        /// <param name="d">A real number.</param>
        /// <returns>The cosine of d.</returns>
        public static double Cos(double d)
        {
            return Math.Cos(d);
        }

        /// <summary>
        /// Returns the tangent of a real number.
        /// </summary>
        /// <param name="d">A real number.</param>
        /// <returns>The tangent of d.</returns>
        public static double Tan(double d)
        {
            return Math.Tan(d);
        }

        /// <summary>
        /// Returns the cotangent of a real number.
        /// </summary>
        /// <param name="d">A real number.</param>
        /// <returns>The cotangent of d.</returns>
        public static double Cot(double d)
        {
            return 1.0 / Math.Tan(d);
        }

        /// <summary>
        /// Returns the secant of a real number.
        /// </summary>
        /// <param name="d">A real number.</param>
        /// <returns>The secant of d.</returns>
        public static double Sec(double d)
        {
            return 1.0 / Math.Cos(d);
        }

        /// <summary>
        /// Returns the cosecant of a real number.
        /// </summary>
        /// <param name="d">A real number.</param>
        /// <returns>The cosecant of d.</returns>
        public static double Csc(double d)
        {
            return 1.0 / Math.Sin(d);
        }

        /// <summary>
        /// Returns the inverse sine of a real number.
        /// </summary>
        /// <param name="d">A real number.</param>
        /// <returns>The inverse sine of d.</returns>
        public static double Asin(double d)
        {
            return Math.Asin(d);
        }

        /// <summary>
        /// Returns the inverse cosine of a real number.
        /// </summary>
        /// <param name="d">A real number.</param>
        /// <returns>The inverse cosine of d.</returns>
        public static double Acos(double d)
        {
            return Math.Acos(d);
        }

        /// <summary>
        /// Returns the inverse tangent of a real number.
        /// </summary>
        /// <param name="d">A real number.</param>
        /// <returns>The inverse tangent of d.</returns>
        public static double Atan(double d)
        {
            return Math.Atan(d);
        }

        /// <summary>
        /// Returns the inverse cotangent of a real number.
        /// </summary>
        /// <param name="d">A real number.</param>
        /// <returns>The inverse cotangent of d.</returns>
        public static double Acot(double d)
        {
            return Math.PI / 2 - Math.Atan(d);
        }

        /// <summary>
        /// Returns the inverse secant of a real number.
        /// </summary>
        /// <param name="d">A real number.</param>
        /// <returns>The inverse secant of d.</returns>
        public static double Asec(double d)
        {
            return Math.Acos(1.0 / d);
        }

        /// <summary>
        /// Returns the inverse cosecant of a real number.
        /// </summary>
        /// <param name="d">A real number.</param>
        /// <returns>The inverse cosecant of d.</returns>
        public static double Acsc(double d)
        {
            return Math.Asin(1.0 / d);
        }

        /// <summary>
        /// Returns the versine of a real number.
        /// </summary>
        /// <param name="d">A real number.</param>
        /// <returns>The versine of d.</returns>
        public static double Vers(double d)
        {
            return 1 - Math.Cos(d);
        }

        /// <summary>
        /// Returns the coversine of a real number.
        /// </summary>
        /// <param name="d">A real number.</param>
        /// <returns>The coversine of d.</returns>
        public static double Cvs(double d)
        {
            return 1 - Math.Sin(d);
        }

        /// <summary>
        /// Returns the haversine of a real number.
        /// </summary>
        /// <param name="d">A real number.</param>
        /// <returns>The haversine of d.</returns>
        public static double Hav(double d)
        {
            return (1 - Math.Cos(d)) / 2;
        }

        /// <summary>
        /// Returns the exsecant of a real number.
        /// </summary>
        /// <param name="d">A real number.</param>
        /// <returns>The exsecant of d.</returns>
        public static double Exsec(double d)
        {
            return 1 / Math.Cos(d) - 1;
        }

        /// <summary>
        /// Returns the excosecant of a real number.
        /// </summary>
        /// <param name="d">A real number.</param>
        /// <returns>The excosecant of d.</returns>
        public static double Excsc(double d)
        {
            return 1 / Math.Sin(d) - 1;
        }

        /// <summary>
        /// Returns the inverse haversine of a real number.
        /// </summary>
        /// <param name="d">A real number.</param>
        /// <returns>The inverse haversine of d.</returns>
        public static double Ahav(double d)
        {
            return 2 * Math.Asin(Math.Sqrt(d));
        }

        /// <summary>
        /// Returns the sine cardinal of a real number.
        /// </summary>
        /// <param name="d">A real number.</param>
        /// <returns>The sine cardinal of d.</returns>
        public static double Sinc(double d)
        {
            if (d == 0.0)
                return 1.0;
            else
                return Math.Sin(d) / d;
        }

        /// <summary>
        /// Returns the tangent cardinal of a real number.
        /// </summary>
        /// <param name="d">A real number.</param>
        /// <returns>The tangent cardinal of d.</returns>
        public static double Tanc(double d)
        {
            if (d == 0.0)
                return 1.0;
            else
                return Math.Tan(d) / d;
        }

        /// <summary>
        /// Returns the hyperbolic sine of a real number.
        /// </summary>
        /// <param name="d">A real number.</param>
        /// <returns>The hyperbolic sine of d.</returns>
        public static double Sinh(double d)
        {
            return Math.Sinh(d);
        }

        /// <summary>
        /// Returns the hyperbolic cosine of a real number.
        /// </summary>
        /// <param name="d">A real number.</param>
        /// <returns>The hyperbolic cosine of d.</returns>
        public static double Cosh(double d)
        {
            return Math.Cosh(d);
        }

        /// <summary>
        /// Returns the hyperbolic tangent of a real number.
        /// </summary>
        /// <param name="d">A real number.</param>
        /// <returns>The hyperbolic tangent of d.</returns>
        public static double Tanh(double d)
        {
            return Math.Tanh(d);
        }

        /// <summary>
        /// Returns the hyperbolic cotangent of a real number.
        /// </summary>
        /// <param name="d">A real number.</param>
        /// <returns>The hyperbolic cotangent of d.</returns>
        public static double Coth(double d)
        {
            return 1.0 / Math.Tanh(d);
        }

        /// <summary>
        /// Returns the hyperbolic secant of a real number.
        /// </summary>
        /// <param name="d">A real number.</param>
        /// <returns>The hyperbolic secant of d.</returns>
        public static double Sech(double d)
        {
            return 1.0 / Math.Cosh(d);
        }

        /// <summary>
        /// Returns the hyperbolic cosecant of a real number.
        /// </summary>
        /// <param name="d">A real number.</param>
        /// <returns>The hyperbolic cosecant of d.</returns>
        public static double Csch(double d)
        {
            return 1.0 / Math.Sinh(d);
        }

        /// <summary>
        /// Returns the inverse hyperbolic sine of a real number.
        /// </summary>
        /// <param name="d">A real number.</param>
        /// <returns>The inverse hyperbolic sine of d.</returns>
        public static double Asinh(double d)
        {
            return Math.Log(d + Math.Sqrt(d * d + 1));
        }

        /// <summary>
        /// Returns the inverse hyperbolic cosine of a real number.
        /// </summary>
        /// <param name="d">A real number.</param>
        /// <returns>The inverse hyperbolic cosine of d.</returns>
        public static double Acosh(double d)
        {
            return Math.Log(d + Math.Sqrt(d * d - 1));
        }

        /// <summary>
        /// Returns the inverse hyperbolic tangent of a real number.
        /// </summary>
        /// <param name="d">A real number.</param>
        /// <returns>The inverse hyperbolic tangent of d.</returns>
        public static double Atanh(double d)
        {
            return Math.Log((1.0 / d + 1) / (1.0 / d - 1)) / 2;
        }

        /// <summary>
        /// Returns the inverse hyperbolic cotangent of a real number.
        /// </summary>
        /// <param name="d">A real number.</param>
        /// <returns>The inverse hyperbolic cotangent of d.</returns>
        public static double Acoth(double d)
        {
            return Math.Log((d + 1) / (d - 1)) / 2;
        }

        /// <summary>
        /// Returns the inverse hyperbolic secant of a real number.
        /// </summary>
        /// <param name="d">A real number.</param>
        /// <returns>The inverse hyperbolic secant of d.</returns>
        public static double Asech(double d)
        {
            return Math.Log((Math.Sqrt(1 - d * d) + 1) / d);
        }

        /// <summary>
        /// Returns the inverse hyperbolic cosecant of a real number.
        /// </summary>
        /// <param name="d">A real number.</param>
        /// <returns>The inverse hyperbolic cosecant of d.</returns>
        public static double Acsch(double d)
        {
            return Math.Log((Math.Sign(d) * Math.Sqrt(d * d + 1) + 1) / d);
        }

        /// <summary>
        /// Returns the hyperbolic sine cardinal of a real number.
        /// </summary>
        /// <param name="d">A real number.</param>
        /// <returns>The hyperbolic sine cardinal of d.</returns>
        public static double Sinhc(double d)
        {
            if (d == 0.0)
                return 1.0;
            else
                return Math.Sinh(d) / d;
        }

        /// <summary>
        /// Returns the hyperbolic tangent cardinal of a real number.
        /// </summary>
        /// <param name="d">A real number.</param>
        /// <returns>The hyperbolic tangent cardinal of d.</returns>
        public static double Tanhc(double d)
        {
            if (d == 0.0)
                return 1.0;
            else
                return Math.Tanh(d) / d;
        }

        /// <summary>
        /// Calculates the integral part of a specified real number.
        /// </summary>
        /// <param name="d">A number to truncate.</param>
        /// <returns>The integral part of d; that is, the number that remains after any fractional digits have been discarded.</returns>
        public static double Truncate(double d)
        {
            return (d > 0) ? Math.Floor(d) : Math.Ceiling(d);
        }

        #endregion
    }
}
