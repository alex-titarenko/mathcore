using System;
using System.Text;
using System.Globalization;
using System.Xml.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.Text.RegularExpressions;


namespace TAlex.MathCore
{
    /// <summary>
    /// Represents a complex number.
    /// </summary>
    public struct Complex : IEquatable<Complex>, IFormattable, IXmlSerializable
    {
        #region Fields

        private static readonly string _reAttrName = "Re";
        private static readonly string _imAttrName = "Im";
        internal static readonly string ComplexPattern = @"(?<num>[-+]?[ \t]*[0-9.,]+[ij]?)(?<num>[ \t]*[-+][ \t]*[0-9.,]+[ij]?)*";
        private static readonly string _complexFullPattern = String.Format(@"^{0}$", ComplexPattern);
        private static readonly Regex _complexRegex = new Regex(_complexFullPattern);

        /// <summary>
        /// The real part of the complex number.
        /// </summary>
        private double _real;

        /// <summary>
        /// The imaginary part of the complex number.
        /// </summary>
        private double _imag;

        /// <summary>
        /// Represents the complex number zero.
        /// </summary>
        public static readonly Complex Zero = Complex.FromRealImaginary(0, 0);

        /// <summary>
        /// Represents the imaginary unit.
        /// </summary>
        public static readonly Complex I = Complex.FromRealImaginary(0, 1);

        /// <summary>
        /// Represents the complex number one.
        /// </summary>
        public static readonly Complex One = Complex.FromRealImaginary(1, 0);

        /// <summary>
        /// Represents a complex value that is not a number (NaN).
        /// </summary>
        public static readonly Complex NaN = Complex.FromRealImaginary(double.NaN, double.NaN);

        /// <summary>
        /// Represents complex positive infinity.
        /// </summary>
        public static readonly Complex Infinity = Complex.FromRealImaginary(double.PositiveInfinity, double.PositiveInfinity);

        #endregion

        #region Properties

        /// <summary>
        /// Gets the real part of the complex number.
        /// </summary>
        public double Re
        {
            get
            {
                return _real;
            }
        }

        /// <summary>
        /// Gets the imaginary part of the complex number.
        /// </summary>
        public double Im
        {
            get
            {
                return _imag;
            }
        }

        /// <summary>
        /// Gets the modulus of the complex number.
        /// </summary>
        public double Modulus
        {
            get
            {
                return Abs(this);
            }
        }

        /// <summary>
        /// Gets the argument of the complex number.
        /// </summary>
        public double Argument
        {
            get
            {
                return Arg(this);
            }
        }

        /// <summary>
        /// Gets a value that indicates whether the complex number is equal to zero.
        /// </summary>
        public bool IsZero
        {
            get
            {
                return ((_real == 0.0) && (_imag == 0.0));
            }
        }

        /// <summary>
        /// Gets a value that indicates whether the imaginary part is equal to zero.
        /// </summary>
        public bool IsReal
        {
            get
            {
                return _imag == 0;
            }
        }

        /// <summary>
        /// Gets a value that indicates whether the real part is equal to zero.
        /// </summary>
        public bool IsImaginary
        {
            get
            {
                return _real == 0;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a complex number from the real number.
        /// </summary>
        /// <param name="real">The real part of the complex number.</param>
        public Complex(double real)
        {
            _real = real;
            _imag = 0.0;
        }

        /// <summary>
        /// Initializes a complex number.
        /// </summary>
        /// <param name="real">The real part of the complex number.</param>
        /// <param name="imaginary">The imaginary part of the complex number.</param>
        public Complex(double real, double imaginary)
        {
            _real = real;
            _imag = imaginary;
        }

        #endregion

        #region Methods

        #region Statics

        /// <summary>
        /// Creates a complex number from the real number.
        /// </summary>
        /// <param name="real">The real part of the complex number.</param>
        /// <returns>A new instance of a complex number.</returns>
        public static Complex FromReal(double real)
        {
            Complex c;
            c._real = real;
            c._imag = 0.0;

            return c;
        }

        /// <summary>
        /// Creates a complex number from real and imaginary coords.
        /// </summary>
        /// <param name="real">The real part of the complex number.</param>
        /// <param name="imaginary">The imaginary part of the complex number.</param>
        /// <returns>A new instance of a complex number.</returns>
        public static Complex FromRealImaginary(double real, double imaginary)
        {
            Complex c;
            c._real = real;
            c._imag = imaginary;

            return c;
        }

        /// <summary>
        /// Creates a complex number from polar coordinates.
        /// </summary>
        /// <param name="modulus">The modulus of complex number.</param>
        /// <param name="argument">The argument of complex number.</param>
        /// <returns>A new instance of a complex number.</returns>
        public static Complex FromPolarCoordinates(double modulus, double argument)
        {
            Complex c;
            c._real = modulus * Math.Cos(argument);
            c._imag = modulus * Math.Sin(argument);

            return c;
        }

        /// <summary>
        /// Returns the absolute value of a complex number.
        /// </summary>
        /// <param name="c">A complex number.</param>
        /// <returns>The absolute value of c.</returns>
        public static double Abs(Complex c)
        {
            if (double.IsInfinity(c._real) || double.IsInfinity(c._imag))
                return double.PositiveInfinity;

            double real = Math.Abs(c._real);
            double imag = Math.Abs(c._imag);

            if (real > imag)
            {
                double temp1 = imag / real;
                return (real * Math.Sqrt(1.0 + (temp1 * temp1)));
            }

            if (imag == 0.0)
            {
                return real;
            }

            double temp2 = real / imag;
            return (imag * Math.Sqrt(1.0 + (temp2 * temp2)));
        }

        /// <summary>
        /// Returns the absolute value squared of a complex number.
        /// </summary>
        /// <param name="c">A complex number.</param>
        /// <returns>The absolute value squared of c.</returns>
        public static double AbsSquared(Complex c)
        {
            return c._real * c._real + c._imag * c._imag;
        }

        /// <summary>
        /// Returns the argument of a complex number.
        /// </summary>
        /// <param name="c">A complex number.</param>
        /// <returns>The argument of c.</returns>
        public static double Arg(Complex c)
        {
            if (c == Zero)
                return double.NaN;
            else
                return Math.Atan2(c._imag, c._real);
        }

        /// <summary>
        /// Returns the inverse value of a complex number.
        /// </summary>
        /// <param name="c">A complex number.</param>
        /// <returns>The inverse value of c.</returns>
        public static Complex Inverse(Complex c)
        {
            Complex complex;
            complex._real = c._real / (c._real * c._real + c._imag * c._imag);
            complex._imag = -c._imag / (c._real * c._real + c._imag * c._imag);

            return complex;
        }

        /// <summary>
        /// Returns the result of multiplying a complex number by negative one.
        /// </summary>
        /// <param name="c">A complex number.</param>
        /// <returns>The complex number with the value of c, but the opposite sign.</returns>
        public static Complex Negate(Complex c)
        {
            Complex complex;
            complex._real = (c._real != 0.0) ? -c._real : 0.0;
            complex._imag = (c._imag != 0.0) ? -c._imag : 0.0;

            return complex;
        }

        /// <summary>
        /// Returns conjugate the complex number.
        /// </summary>
        /// <param name="c">A complex number.</param>
        /// <returns>The conjugate of c.</returns>
        public static Complex Conjugate(Complex c)
        {
            Complex complex;
            complex._real = c._real;
            complex._imag = (c._imag != 0.0) ? -c._imag : 0.0;

            return complex;
        }

        /// <summary>
        /// Returns the sign of a complex number.
        /// </summary>
        /// <param name="c">A complex number.</param>
        /// <returns>A number indicating the sign of c.</returns>
        public static int Sign(Complex c)
        {
            if (c == Zero)
                return 0;
            else if (c._real > 0.0 || (c._real == 0.0 && c._imag > 0.0))
                return 1;
            else
                return -1;
        }

        /// <summary>
        /// Returns the smaller of two complex numbers.
        /// </summary>
        /// <param name="c1">The first complex number to compare.</param>
        /// <param name="c2">The second complex number to compare.</param>
        /// <returns>The smaller of two complex numbers.</returns>
        public static Complex Min(Complex c1, Complex c2)
        {
            Complex c;
            c._real = Math.Min(c1._real, c2._real);
            c._imag = Math.Min(c1._imag, c2._imag);

            return c;
        }

        /// <summary>
        /// Returns the larger of two complex numbers.
        /// </summary>
        /// <param name="c1">The first complex number to compare.</param>
        /// <param name="c2">The second complex number to compare.</param>
        /// <returns>The larger of two complex numbers.</returns>
        public static Complex Max(Complex c1, Complex c2)
        {
            Complex c;
            c._real = Math.Max(c1._real, c2._real);
            c._imag = Math.Max(c1._imag, c2._imag);

            return c;
        }

        /// <summary>
        /// Adds two complex numbers.
        /// </summary>
        /// <param name="c1">A complex number (the first term).</param>
        /// <param name="c2">A complex number (the second term).</param>
        /// <returns>The sum of complex numbers.</returns>
        public static Complex Add(Complex c1, Complex c2)
        {
            return c1 + c2;
        }

        /// <summary>
        /// Subtracts one a complex number from another.
        /// </summary>
        /// <param name="c1">A complex number (the minuend).</param>
        /// <param name="c2"> A complex number(the subtrahend).</param>
        /// <returns>The result of subtracting c2 from c1.</returns>
        public static Complex Subtract(Complex c1, Complex c2)
        {
            return c1 - c2;
        }

        /// <summary>
        /// Multiplies two complex numbers.
        /// </summary>
        /// <param name="c1">A complex number (the multiplicand).</param>
        /// <param name="c2">A complex number (the multiplier).</param>
        /// <returns>The product of c1 and c2.</returns>
        public static Complex Multiply(Complex c1, Complex c2)
        {
            return c1 * c2;
        }

        /// <summary>
        /// Multiplies one the conjugate of a complex number and another.
        /// </summary>
        /// <param name="c1">A complex number (the multiplicand).</param>
        /// <param name="c2">A complex number (the multiplier).</param>
        /// <returns>The product of conjugate of c1 and c2.</returns>
        public static Complex ConjugateMultiply(Complex c1, Complex c2)
        {
            Complex c;
            c._real = c1._real * c2._real + c1._imag * c2._imag;
            c._imag = c1._real * c2._imag - c1._imag * c2._real;

            return c;
        }

        /// <summary>
        /// Divides two complex numbers.
        /// </summary>
        /// <param name="c1">A complex number (the dividend).</param>
        /// <param name="c2">A complex number (the divisor).</param>
        /// <returns>The result of dividing c1 by c2.</returns>
        public static Complex Divide(Complex c1, Complex c2)
        {
            return c1 / c2;
        }

        /// <summary>
        /// Returns the square root of a real number.
        /// </summary>
        /// <param name="d">A real number.</param>
        /// <returns>The square root of d.</returns>
        public static Complex Sqrt(double d)
        {
            if (d >= 0.0)
                return Complex.FromReal(Math.Sqrt(d));
            else
                return Complex.FromRealImaginary(0, Math.Sqrt(-d));
        }

        /// <summary>
        /// Returns the square root of a complex number.
        /// </summary>
        /// <param name="c">A complex number.</param>
        /// <returns>The square root of c.</returns>
        public static Complex Sqrt(Complex c)
        {
            double zr = c._real;
            double zi = c._imag;
            double mag = Abs(c);

            Complex complex;

            if (mag == 0.0)
            {
                complex._real = complex._imag = 0.0;
            }
            else if (zr > 0)
            {
                complex._real = Math.Sqrt(0.5 * (mag + zr));
                complex._imag = zi / complex._real / 2;
            }
            else
            {
                complex._imag = Math.Sqrt(0.5 * (mag - zr));

                if (zi < 0)
                    complex._imag = -complex._imag;

                complex._real = zi / complex._imag / 2;
            }

            return complex;
        }

        /// <summary>
        /// Returns a complex number raised to an integer power.
        /// </summary>
        /// <param name="c">A complex number to be raised to a power.</param>
        /// <param name="exponent">An integer number that specifies a power.</param>
        /// <returns>The complex number c raised to the power exponent.</returns>
        public static Complex Pow(Complex c, int exponent)
        {
            if (exponent == 0)
                return Complex.One;

            if (exponent < 0)
            {
                exponent = -exponent;
                c = 1.0 / c;
            }

            Complex result = 1;

            while (exponent != 0)
            {
                if ((exponent & 1) != 0)
                    result *= c;

                c *= c;
                exponent >>= 1;
            }

            return result;
        }

        /// <summary>
        /// Returns a complex number raised to a real power.
        /// </summary>
        /// <param name="c">A complex number to be raised to a power.</param>
        /// <param name="exponent">A real number that specifies a power.</param>
        /// <returns>The complex number c raised to the power exponent.</returns>
        public static Complex Pow(Complex c, double exponent)
        {
            double rn = Math.Pow(Abs(c), exponent);
            double arg = Math.Atan2(c._imag, c._real);

            Complex complex;
            complex._real = rn * Math.Cos(arg * exponent);
            complex._imag = rn * Math.Sin(arg * exponent);

            return complex;
        }

        /// <summary>
        /// Returns a complex number raised to a complex power.
        /// </summary>
        /// <param name="c">A complex number to be raised to a power.</param>
        /// <param name="exponent">A complex number that specifies a power.</param>
        /// <returns>The complex number c raised to the power exponent.</returns>
        public static Complex Pow(Complex c, Complex exponent)
        {
            if (exponent.IsReal)
                return Pow(c, exponent._real);
            else
                return Exp(exponent * Log(c));
        }

        /// <summary>
        /// Returns the sine of a complex number.
        /// </summary>
        /// <param name="c">A complex number.</param>
        /// <returns>The sine of c.</returns>
        public static Complex Sin(Complex c)
        {
            if (c._imag == 0.0)
            {
                return Math.Sin(c._real);
            }

            Complex complex;
            complex._real = Math.Sin(c._real) * Math.Cosh(c._imag);
            complex._imag = Math.Cos(c._real) * Math.Sinh(c._imag);

            return complex;
        }

        /// <summary>
        /// Returns the cosine of a complex number.
        /// </summary>
        /// <param name="c">A complex number.</param>
        /// <returns>The cosine of c.</returns>
        public static Complex Cos(Complex c)
        {
            if (c._imag == 0.0)
            {
                return Math.Cos(c._real);
            }

            Complex complex;
            complex._real = Math.Cos(c._real) * Math.Cosh(c._imag);
            complex._imag = -Math.Sin(c._real) * Math.Sinh(c._imag);

            return complex;
        }

        /// <summary>
        /// Returns the tangent of a complex number.
        /// </summary>
        /// <param name="c">A complex number.</param>
        /// <returns>The tangent of c.</returns>
        public static Complex Tan(Complex c)
        {
            if (c._imag == 0.0)
            {
                return Math.Tan(c._real);
            }

            return Sin(c) / Cos(c);
        }

        /// <summary>
        /// Returns the cotangent of a complex number.
        /// </summary>
        /// <param name="c">A complex number.</param>
        /// <returns>The cotangent of c.</returns>
        public static Complex Cot(Complex c)
        {
            return Cos(c) / Sin(c);
        }

        /// <summary>
        /// Returns the secant of a complex number.
        /// </summary>
        /// <param name="c">A complex number.</param>
        /// <returns>The secant of c.</returns>
        public static Complex Sec(Complex c)
        {
            return 2.0 / (Exp(I * c) + Exp(-I * c));
        }

        /// <summary>
        /// Returns the cosecant of a complex number.
        /// </summary>
        /// <param name="c">A complex number.</param>
        /// <returns>The cosecant of c.</returns>
        public static Complex Csc(Complex c)
        {
            return (2.0 * I) / (Exp(I * c) - Exp(-I * c));
        }

        /// <summary>
        /// Returns the inverse sine of a complex number.
        /// </summary>
        /// <param name="c">A complex number.</param>
        /// <returns>The inverse sine of c.</returns>
        public static Complex Asin(Complex c)
        {
            if (c._imag == 0.0 && Math.Abs(c._real) <= 1.0)
            {
                return Math.Asin(c._real);
            }

            return -I * Log(I * c + Sqrt(1.0 - c * c));
        }

        /// <summary>
        /// Returns the inverse cosine of a complex number.
        /// </summary>
        /// <param name="c">A complex number.</param>
        /// <returns>The inverse cosine of c.</returns>
        public static Complex Acos(Complex c)
        {
            if (c._imag == 0.0 && Math.Abs(c._real) <= 1.0)
            {
                return Math.Acos(c._real);
            }

            return -I * Log(c + I * Sqrt(1.0 - c * c));
        }

        /// <summary>
        /// Returns the inverse tangent of a complex number.
        /// </summary>
        /// <param name="c">A complex number.</param>
        /// <returns>The inverse tangent of c.</returns>
        public static Complex Atan(Complex c)
        {
            if (c._imag == 0.0)
            {
                return Math.Atan(c._real);
            }

            return I / 2.0 * Log((I + c) / (I - c));
        }

        /// <summary>
        /// Returns the inverse cotangent of a complex number.
        /// </summary>
        /// <param name="c">A complex number.</param>
        /// <returns>The inverse cotangent of c.</returns>
        public static Complex Acot(Complex c)
        {
            return I / 2.0 * (Log((c - I) / c) - Log((c + I) / c));
        }

        /// <summary>
        /// Returns the inverse secant of a complex number.
        /// </summary>
        /// <param name="c">A complex number.</param>
        /// <returns>The inverse secant of c.</returns>
        public static Complex Asec(Complex c)
        {
            return Acos(1.0 / c);
        }

        /// <summary>
        /// Returns the inverse cosecant of a complex number.
        /// </summary>
        /// <param name="c">A complex number.</param>
        /// <returns>The inverse cosecant of c.</returns>
        public static Complex Acsc(Complex c)
        {
            return Asin(1.0 / c);
        }

        /// <summary>
        /// Returns the versine of a complex number.
        /// </summary>
        /// <param name="c">A complex number.</param>
        /// <returns>The versine of c.</returns>
        public static Complex Vers(Complex c)
        {
            return 1.0 - Cos(c);
        }

        /// <summary>
        /// Returns the coversine of a complex number.
        /// </summary>
        /// <param name="c">A complex number.</param>
        /// <returns>The coversine of c.</returns>
        public static Complex Cvs(Complex c)
        {
            return 1.0 - Sin(c);
        }

        /// <summary>
        /// Returns the haversine of a complex number.
        /// </summary>
        /// <param name="c">A complex number.</param>
        /// <returns>The haversine of c.</returns>
        public static Complex Hav(Complex c)
        {
            return (1.0 - Cos(c)) / 2.0;
        }

        /// <summary>
        /// Returns the exsecant of a complex number.
        /// </summary>
        /// <param name="c">A complex number.</param>
        /// <returns>The exsecant of c.</returns>
        public static Complex Exsec(Complex c)
        {
            return Sec(c) - 1.0;
        }

        /// <summary>
        /// Returns the excosecant of a complex number.
        /// </summary>
        /// <param name="c">A complex number.</param>
        /// <returns>The excosecant of c.</returns>
        public static Complex Excsc(Complex c)
        {
            return Csc(c) - 1.0;
        }

        /// <summary>
        /// Returns the inverse haversine of a complex number.
        /// </summary>
        /// <param name="c">A complex number.</param>
        /// <returns>The inverse haversine of c.</returns>
        public static Complex Ahav(Complex c)
        {
            return 2.0 * Asin(Sqrt(c));
        }

        /// <summary>
        /// Returns the sine cardinal of a complex number.
        /// </summary>
        /// <param name="c">A complex number.</param>
        /// <returns>The sine cardinal of c.</returns>
        public static Complex Sinc(Complex c)
        {
            if (c == Zero)
                return One;
            else
                return Sin(c) / c;
        }

        /// <summary>
        /// Returns the tangent cardinal of a complex number.
        /// </summary>
        /// <param name="c">A complex number.</param>
        /// <returns>The tangent cardinal of c.</returns>
        public static Complex Tanc(Complex c)
        {
            if (c == Zero)
                return One;
            else
                return Tan(c) / c;
        }

        /// <summary>
        /// Returns the hyperbolic sine of a complex number.
        /// </summary>
        /// <param name="c">A complex number.</param>
        /// <returns>The hyperbolic sine of c.</returns>
        public static Complex Sinh(Complex c)
        {
            if (c._imag == 0.0)
            {
                return Math.Sinh(c._real);
            }

            Complex complex;
            complex._real = Math.Sinh(c._real) * Math.Cos(c._imag);
            complex._imag = Math.Cosh(c._real) * Math.Sin(c._imag);

            return complex;
        }

        /// <summary>
        /// Returns the hyperbolic cosine of a complex number.
        /// </summary>
        /// <param name="c">A complex number.</param>
        /// <returns>The hyperbolic cosine of c.</returns>
        public static Complex Cosh(Complex c)
        {
            if (c._imag == 0.0)
            {
                return Math.Cosh(c._real);
            }

            Complex complex;
            complex._real = Math.Cosh(c._real) * Math.Cos(c._imag);
            complex._imag = Math.Sinh(c._real) * Math.Sin(c._imag);

            return complex;
        }

        /// <summary>
        /// Returns the hyperbolic tangent of a complex number.
        /// </summary>
        /// <param name="c">A complex number.</param>
        /// <returns>The hyperbolic tangent of c.</returns>
        public static Complex Tanh(Complex c)
        {
            if (c._imag == 0.0)
            {
                return Math.Tanh(c._real);
            }

            return (Exp(2.0 * c) - 1.0) / (Exp(2.0 * c) + 1.0);
        }

        /// <summary>
        /// Returns the hyperbolic cotangent of a complex number.
        /// </summary>
        /// <param name="c">A complex number.</param>
        /// <returns>The hyperbolic cotangent of c.</returns>
        public static Complex Coth(Complex c)
        {
            return (Exp(2.0 * c) + 1.0) / (Exp(2.0 * c) - 1.0);
        }

        /// <summary>
        /// Returns the hyperbolic secant of a complex number.
        /// </summary>
        /// <param name="c">A complex number.</param>
        /// <returns>The hyperbolic secant of c.</returns>
        public static Complex Sech(Complex c)
        {
            return Inverse(Cosh(c));
        }

        /// <summary>
        /// Returns the hyperbolic cosecant of a complex number.
        /// </summary>
        /// <param name="c">A complex number.</param>
        /// <returns>The hyperbolic cosecant of c.</returns>
        public static Complex Csch(Complex c)
        {
            return Inverse(Sinh(c));
        }

        /// <summary>
        /// Returns the inverse hyperbolic sine of a complex number.
        /// </summary>
        /// <param name="c">A complex number.</param>
        /// <returns>The inverse hyperbolic sine of c.</returns>
        public static Complex Asinh(Complex c)
        {
            return Log(c + Sqrt(1.0 + c * c));
        }

        /// <summary>
        /// Returns the inverse hyperbolic cosine of a complex number.
        /// </summary>
        /// <param name="c">A complex number.</param>
        /// <returns>The inverse hyperbolic cosine of c.</returns>
        public static Complex Acosh(Complex c)
        {
            return Log(c + Sqrt(c + 1.0) * Sqrt(c - 1.0));
        }

        /// <summary>
        /// Returns the inverse hyperbolic tangent of a complex number.
        /// </summary>
        /// <param name="c">A complex number.</param>
        /// <returns>The inverse hyperbolic tangent of c.</returns>
        public static Complex Atanh(Complex c)
        {
            return (Log(1.0 + c) - Log(1.0 - c)) / 2.0;
        }

        /// <summary>
        /// Returns the inverse hyperbolic cotangent of a complex number.
        /// </summary>
        /// <param name="c">A complex number.</param>
        /// <returns>The inverse hyperbolic cotangent of c.</returns>
        public static Complex Acoth(Complex c)
        {
            Complex invc = 1.0 / c;
            return (Log(1.0 + invc) - Log(1.0 - invc)) / 2.0;
        }

        /// <summary>
        /// Returns the inverse hyperbolic secant of a complex number.
        /// </summary>
        /// <param name="c">A complex number.</param>
        /// <returns>The inverse hyperbolic secant of c.</returns>
        public static Complex Asech(Complex c)
        {
            Complex invc = 1.0 / c;
            return Log(Sqrt(invc - 1.0) * Sqrt(invc + 1.0) + invc);
        }

        /// <summary>
        /// Returns the inverse hyperbolic cosecant of a complex number.
        /// </summary>
        /// <param name="c">A complex number.</param>
        /// <returns>The inverse hyperbolic cosecant of c.</returns>
        public static Complex Acsch(Complex c)
        {
            return Log(Sqrt(1.0 + 1.0 / (c * c)) + 1.0 / c);
        }

        /// <summary>
        /// Returns the hyperbolic sine cardinal of a complex number.
        /// </summary>
        /// <param name="c">A complex number.</param>
        /// <returns>The hyperbolic sine cardinal of c.</returns>
        public static Complex Sinhc(Complex c)
        {
            if (c == Complex.Zero)
                return One;
            else
                return Sinh(c) / c;
        }

        /// <summary>
        /// Returns the hyperbolic tangent cardinal of a complex number.
        /// </summary>
        /// <param name="c">A complex number.</param>
        /// <returns>The hyperbolic tangent cardinal of c.</returns>
        public static Complex Tanhc(Complex c)
        {
            if (c == Complex.Zero)
                return One;
            else
                return Tanh(c) / c;
        }

        /// <summary>
        /// Returns e raised to the complex power.
        /// </summary>
        /// <param name="c">A complex number that specifies a power.</param>
        /// <returns>The number e raised to the power c.</returns>
        public static Complex Exp(Complex c)
        {
            Complex complex;
            complex._real = Math.Exp(c._real) * Math.Cos(c._imag);
            complex._imag = Math.Exp(c._real) * Math.Sin(c._imag);

            return complex;
        }

        /// <summary>
        /// Returns the natural logarithm of a complex number.
        /// </summary>
        /// <param name="c">A complex number.</param>
        /// <returns>The natural logarithm of c.</returns>
        public static Complex Log(Complex c)
        {
            Complex complex;
            complex._real = Math.Log(Abs(c));
            complex._imag = Math.Atan2(c._imag, c._real);

            return complex;
        }

        /// <summary>
        /// Returns the logarithm of a complex number in a specified base.
        /// </summary>
        /// <param name="c">A complex number.</param>
        /// <param name="newBase">The base of the logarithm.</param>
        /// <returns>The logarithm of c in base newBase.</returns>
        /// <exception cref="System.ArgumentException">newBase is negative.</exception>
        public static Complex Log(Complex c, double newBase)
        {
            if (newBase < 0)
                throw new ArgumentException("The base must be positive value.");

            return Log(c) / Math.Log(newBase);
        }

        /// <summary>
        /// Returns the base 10 logarithm of a complex number.
        /// </summary>
        /// <param name="c">A complex number.</param>
        /// <returns>The base 10 logarithm of c.</returns>
        public static Complex Log10(Complex c)
        {
            return Log(c) / Math.Log(10);
        }

        /// <summary>
        /// Returns the largest complex integer less than or equal to the specified complex number.
        /// </summary>
        /// <param name="c">A complex number.</param>
        /// <returns>The largest complex integer less than or equal to c.</returns>
        public static Complex Floor(Complex c)
        {
            Complex complex;
            complex._real = Math.Floor(c._real);
            complex._imag = Math.Floor(c._imag);

            return complex;
        }

        /// <summary>
        /// Returns the smallest complex integer greater than or equal to the specified complex number.
        /// </summary>
        /// <param name="c">A complex number.</param>
        /// <returns>The smallest complex integer greater than or equal to c.</returns>
        public static Complex Ceiling(Complex c)
        {
            Complex complex;
            complex._real = Math.Ceiling(c._real);
            complex._imag = Math.Ceiling(c._imag);

            return complex;
        }

        /// <summary>
        /// Rounds a complex value to the nearest integer.
        /// </summary>
        /// <param name="c">A complex number to be rounded.</param>
        /// <returns>
        /// The complex integer nearest c. If the fractional component of c is halfway between
        /// two integers, one of which is even and the other odd, then the even number
        /// is returned.
        /// </returns>
        public static Complex Round(Complex c)
        {
            Complex complex;
            complex._real = Math.Round(c._real);
            complex._imag = Math.Round(c._imag);

            return complex;
        }

        /// <summary>
        /// Rounds a complex value to a specified complex number of fractional digits.
        /// </summary>
        /// <param name="c">A complex number to be rounded.</param>
        /// <param name="digits">The number of fractional digits in the return value.</param>
        /// <returns>
        /// The complex number nearest to value that contains
        /// a number of fractional digits equal to digits.
        /// </returns>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// digits is less than 0 or greater than 15.
        /// </exception>
        public static Complex Round(Complex c, int digits)
        {
            Complex complex;
            complex._real = Math.Round(c._real, digits);
            complex._imag = Math.Round(c._imag, digits);

            return complex;
        }

        /// <summary>
        /// Returns the integral part of a specified complex number.
        /// </summary>
        /// <param name="c">A complex number to truncate.</param>
        /// <returns>
        /// The integral part of c; that is, the number that remains after any fractional
        /// digits have been discarded.
        /// </returns>
        public static Complex Truncate(Complex c)
        {
            return new Complex
            {
                _real = (c.Re > 0) ? Math.Floor(c.Re) : Math.Ceiling(c.Re),
                _imag = (c.Im > 0) ? Math.Floor(c.Im) : Math.Ceiling(c.Im)
            };
        }

        /// <summary>
        /// Returns a value that indicates whether the complex number evaluates to an infinity value.
        /// </summary>
        /// <param name="c">A complex number.</param>
        /// <returns>
        /// true if the real or imaginary part evaluates to positive or negative infinity;
        /// otherwise false.
        /// </returns>
        public static bool IsInfinity(Complex c)
        {
            if (double.IsInfinity(c._real) || double.IsInfinity(c._imag))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Returns a value that indicates whether the complex number evaluates to a value that is not a number.
        /// </summary>
        /// <param name="c">A complex number.</param>
        /// <returns>
        /// true if the real or imaginary part evaluates to System.Double.NaN;
        /// otherwise false.
        /// </returns>
        public static bool IsNaN(Complex c)
        {
            if (double.IsNaN(c._real) || double.IsNaN(c._imag))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Converts string representation into the equivalent of a complex number.
        /// </summary>
        /// <param name="s">A string containing a complex number to convert.</param>
        /// <returns>A complex number equivalent to the numeric value or symbol specified in s.</returns>
        /// <exception cref="System.ArgumentNullException">The string s is null.</exception>
        /// <exception cref="System.ArgumentException">The string s is empty string.</exception>
        /// <exception cref="System.FormatException">The string s is not a number in a valid format.</exception>
        public static Complex Parse(string s)
        {
            return Parse(s, NumberFormatInfo.CurrentInfo);
        }

        /// <summary>
        /// Converts string representation of a number
        /// in a specified culture-specific format to its complex number equivalent.
        /// </summary>
        /// <param name="s">A string containing a complex number to convert.</param>
        /// <param name="provider">An System.IFormatProvider that supplies culture-specific formatting information about s.</param>
        /// <returns>A complex number equivalent to the numeric value or symbol specified in s.</returns>
        /// <exception cref="System.ArgumentNullException">The string s is null.</exception>
        /// <exception cref="System.ArgumentException">The string s is empty string.</exception>
        /// <exception cref="System.FormatException">The string s is not a number in a valid format.</exception>
        public static Complex Parse(string s, IFormatProvider provider)
        {
            if (s == null) throw new ArgumentNullException(Properties.Resources.EXC_STRING_NOT_NULL);

            string str = s.Trim();
            if (String.IsNullOrEmpty(str)) throw new ArgumentException(Properties.Resources.EXC_STRING_NOT_EMPTY);

            Match match = _complexRegex.Match(str);
            if (!match.Success) throw new FormatException(Properties.Resources.EXC_INVALID_FORMAT_COMPLEX);

            Complex result = Complex.Zero;
            foreach (Capture capture in match.Groups["num"].Captures)
            {
                string term = capture.Value.Replace(" ", String.Empty).Replace("\t", String.Empty);

                if (term.EndsWith("i") || term.EndsWith("j"))
                    result += Complex.FromRealImaginary(0, double.Parse(term.Substring(0, term.Length - 1), provider));
                else
                    result += double.Parse(term, provider);
            }

            return result;
        }

        #endregion

        #region Dynamics

        /// <summary>
        /// Converts the complex value of this instance to its equivalent string representation.
        /// </summary>
        /// <returns>The string representation of the value of this instance.</returns>
        public override string ToString()
        {
            return ToString(null, NumberFormatInfo.CurrentInfo);
        }

        /// <summary>
        /// Converts the complex value of this instance to its equivalent string representation.
        /// </summary>
        /// <param name="provider">An System.IFormatProvider that supplies culture-specific formatting information.</param>
        /// <returns>The string representation of the value of this instance as specified by provider.</returns>
        public string ToString(IFormatProvider provider)
        {
            return ToString(null, provider);
        }

        /// <summary>
        /// Converts the complex value of this instance to its equivalent string representation.
        /// </summary>
        /// <param name="format">A numeric format string.</param>
        /// <returns>The string representation of the value of this instance as specified by format.</returns>
        /// <exception cref="System.FormatException">The format is invalid.</exception>
        public string ToString(string format)
        {
            return ToString(format, NumberFormatInfo.CurrentInfo);
        }

        /// <summary>
        /// Returns a value indicating whether this instance is equal
        /// to a specified object.
        /// </summary>
        /// <param name="obj">An object to compare with this instance.</param>
        /// <returns>
        /// True if obj is an instance of Complex and equals the value of this instance;
        /// otherwise, false.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (!(obj is Complex)) return false;
            return this == (Complex)obj;
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>A 32-bit signed integer hash code.</returns>
        public override int GetHashCode()
        {
            int hashCode = _real.GetHashCode();

            if (_imag != 0.0)
                hashCode = hashCode ^ (_imag.GetHashCode() << 4);

            return hashCode;
        }

        #endregion

        #endregion

        #region Operators

        /// <summary>
        /// Returns a value indicating whether
        /// two instances of complex number are equal.
        /// </summary>
        /// <param name="c1">The first complex number to compare.</param>
        /// <param name="c2">The second complex number to compare.</param>
        /// <returns>True if the c1 and c2 parameters have the same value; otherwise, false.</returns>
        public static bool operator ==(Complex c1, Complex c2)
        {
            return (c1._real == c2._real && c1._imag == c2._imag);
        }

        /// <summary>
        /// Returns a value indicating whether
        /// two instances of complex number are not equal.
        /// </summary>
        /// <param name="c1">The first complex number to compare.</param>
        /// <param name="c2">The second complex number to compare.</param>
        /// <returns>True if c1 and c2 are not equal; otherwise, false.</returns>
        public static bool operator !=(Complex c1, Complex c2)
        {
            return !(c1 == c2);
        }

        /// <summary>
        /// Returns the value of the complex number operand
        /// (the sign of the operand is unchanged).
        /// </summary>
        /// <param name="c">A complex number.</param>
        /// <returns>The value of the operand, c.</returns>
        public static Complex operator +(Complex c)
        {
            return c;
        }

        /// <summary>
        /// Negates the value of the complex number operand.
        /// </summary>
        /// <param name="c">A complex number.</param>
        /// <returns>The result of c multiplied by negative one.</returns>
        public static Complex operator -(Complex c)
        {
            Complex complex;
            complex._real = (c._real != 0) ? -c._real : 0;
            complex._imag = (c._imag != 0) ? -c._imag : 0;

            return complex;
        }

        /// <summary>
        /// Adds two complex numbers.
        /// </summary>
        /// <param name="c1">A complex number (the first term).</param>
        /// <param name="c2">A complex number (the second term).</param>
        /// <returns>The sum of complex numbers.</returns>
        public static Complex operator +(Complex c1, Complex c2)
        {
            Complex c;
            c._real = c1._real + c2._real;
            c._imag = c1._imag + c2._imag;

            return c;
        }

        /// <summary>
        /// Subtracts one complex number from another.
        /// </summary>
        /// <param name="c1">A complex number (the minuend).</param>
        /// <param name="c2">A complex number (the subtrahend).</param>
        /// <returns>The result of subtracting c2 from c1.</returns>
        public static Complex operator -(Complex c1, Complex c2)
        {
            Complex c;
            c._real = c1._real - c2._real;
            c._imag = c1._imag - c2._imag;

            return c;
        }

        /// <summary>
        /// Multiplies two complex numbers.
        /// </summary>
        /// <param name="c1">A complex number (the multiplicand).</param>
        /// <param name="c2">A complex number (the multiplier).</param>
        /// <returns>The product of c1 and c2.</returns>
        public static Complex operator *(Complex c1, Complex c2)
        {
            Complex c;
            c._real = c1._real * c2._real - c1._imag * c2._imag;
            c._imag = c1._real * c2._imag + c1._imag * c2._real;

            return c;
        }

        /// <summary>
        /// Divides two complex numbers.
        /// </summary>
        /// <param name="c1">A complex number (the dividend).</param>
        /// <param name="c2">A complex number (the divisor).</param>
        /// <returns>The result of dividing c1 by c2.</returns>
        public static Complex operator /(Complex c1, Complex c2)
        {
            double ar = c1._real, ai = c1._imag;
            double br = c2._real, bi = c2._imag;

            Complex complex;

            if (Math.Abs(bi) < Math.Abs(br))
            {
                double denom = br + (bi * (bi / br));

                complex._real = (ar + (ai * (bi / br))) / denom;
                complex._imag = (ai - (ar * (bi / br))) / denom;
            }
            else
            {
                double denom = bi + (br * (br / bi));

                complex._real = (ai + (ar * (br / bi))) / denom;
                complex._imag = (-ar + (ai * (br / bi))) / denom;
            }

            return complex;
        }

        /// <summary>
        /// Converts a 16-bit signed integer to a complex number.
        /// </summary>
        /// <param name="value">A 16-bit signed integer.</param>
        /// <returns>A complex number that represents the converted 16-bit signed integer.</returns>
        public static implicit operator Complex(short value)
        {
            Complex c;
            c._real = value;
            c._imag = 0.0;

            return c;
        }

        /// <summary>
        /// Converts a 32-bit signed integer to a complex number.
        /// </summary>
        /// <param name="value">A 32-bit signed integer.</param>
        /// <returns>A complex number that represents the converted 32-bit signed integer.</returns>
        public static implicit operator Complex(int value)
        {
            Complex c;
            c._real = value;
            c._imag = 0.0;

            return c;
        }

        /// <summary>
        /// Converts a 64-bit signed integer to a complex number.
        /// </summary>
        /// <param name="value">A 64-bit signed integer.</param>
        /// <returns>A complex number that represents the converted 64-bit signed integer.</returns>
        public static implicit operator Complex(long value)
        {
            Complex c;
            c._real = value;
            c._imag = 0.0;

            return c;
        }

        /// <summary>
        /// Converts a single-precision floating-point number to a complex number.
        /// </summary>
        /// <param name="value">A single-precision floating-point number.</param>
        /// <returns>A complex number that represents the converted single-precision floating-point number.</returns>
        public static implicit operator Complex(float value)
        {
            Complex c;
            c._real = value;
            c._imag = 0.0;

            return c;
        }

        /// <summary>
        /// Converts a double-precision floating-point number to a complex number.
        /// </summary>
        /// <param name="value">A double-precision floating-point number.</param>
        /// <returns>A complex number that represents the converted double-precision floating-point number.</returns>
        public static implicit operator Complex(double value)
        {
            Complex c;
            c._real = value;
            c._imag = 0.0;

            return c;
        }

        /// <summary>
        /// Converts a complex number to a 16-bit signed integer.
        /// </summary>
        /// <param name="value">A complex number.</param>
        /// <returns>A 16-bit signed integer that represents the converted complex number.</returns>
        public static explicit operator short(Complex value)
        {
            return (short)value._real;
        }

        /// <summary>
        /// Converts a complex number to a 32-bit signed integer.
        /// </summary>
        /// <param name="value">A complex number.</param>
        /// <returns>A 32-bit signed integer that represents the converted complex number.</returns>
        public static explicit operator int(Complex value)
        {
            return (int)value._real;
        }

        /// <summary>
        /// Converts a complex number to a 64-bit signed integer.
        /// </summary>
        /// <param name="value">A complex number.</param>
        /// <returns>A 64-bit signed integer that represents the converted complex number.</returns>
        public static explicit operator long(Complex value)
        {
            return (long)value._real;
        }

        /// <summary>
        /// Converts a complex number to a single-precision floating-point number.
        /// </summary>
        /// <param name="value">A complex number.</param>
        /// <returns>A single-precision floating-point number that represents the converted complex number.</returns>
        public static explicit operator float(Complex value)
        {
            return (float)value._real;
        }

        /// <summary>
        /// Converts a complex number to a double-precision floating-point number.
        /// </summary>
        /// <param name="value">A complex number.</param>
        /// <returns>A double-precision floating-point number that represents the converted complex number.</returns>
        public static explicit operator double(Complex value)
        {
            return value._real;
        }

        #endregion


        #region IEquatable<Complex> Members

        /// <summary>
        /// Returns a value indicating whether this instance is equal
        /// to a specified complex number.
        /// </summary>
        /// <param name="obj">A Complex object to compare to this instance.</param>
        /// <returns>True if obj is equal to this instance; otherwise, false.</returns>
        public bool Equals(Complex obj)
        {
            return this == obj;
        }

        #endregion

        #region IFormattable Members

        /// <summary>
        /// Converts the complex value of this instance to its equivalent string representation.
        /// </summary>
        /// <param name="format">A numeric format string.</param>
        /// <param name="provider">An System.IFormatProvider that supplies culture-specific formatting information.</param>
        /// <returns>The string representation of the value of this instance as specified by format and provider.</returns>
        /// <exception cref="System.FormatException">The format is invalid.</exception>
        public string ToString(string format, IFormatProvider provider)
        {
            NumberFormatInfo formatInfo = NumberFormatInfo.GetInstance(provider);

            string real = _real.ToString(format, provider);
            string imag = Math.Abs(_imag).ToString(format, provider);

            if (this == Zero) return "0";
            if (IsReal) return real;
            else if (IsImaginary && _imag < 0)
                return String.Format("{0}{1}i", formatInfo.NegativeSign, imag);
            else if (IsImaginary && _imag >= 0)
                return String.Format("{0}i", imag);
            else if (_imag < 0)
                return String.Format("{0} {1} {2}i", real, formatInfo.NegativeSign, imag);
            else
                return String.Format("{0} {1} {2}i", real, formatInfo.PositiveSign, imag);
        }

        #endregion

        #region IXmlSerializable Members

        XmlSchema IXmlSerializable.GetSchema()
        {
            throw new NotImplementedException();
        }

        void IXmlSerializable.ReadXml(XmlReader reader)
        {
            _real = double.Parse(reader.GetAttribute(_reAttrName), CultureInfo.InvariantCulture);
            _imag = double.Parse(reader.GetAttribute(_imAttrName), CultureInfo.InvariantCulture);
        }

        void IXmlSerializable.WriteXml(XmlWriter writer)
        {
            writer.WriteAttributeString(_reAttrName, Re.ToString(CultureInfo.InvariantCulture));
            writer.WriteAttributeString(_imAttrName, Im.ToString(CultureInfo.InvariantCulture));
        }

        #endregion
    }
}
