using System;


namespace TAlex.MathCore.SpecialFunctions
{
    /// <summary>
    /// Contains methods for evaluating exponential integrals.
    /// </summary>
    public static class ExponentialIntegrals
    {
        #region Fields

        /// <summary>
        /// Maximum absolute value of the argument for which applies a Taylor series expansion.
        /// </summary>
        private const double _taylorUpperLimit = 20.0;

        /// <summary>
        /// Maximum number of terms in the Taylor series.
        /// </summary>
        private const int _maxTaylorTerms = 60;

        /// <summary>
        /// Maximum number of terms in the asymptotic series.
        /// </summary>
        private const int _maxAsymptTerms = 10;

        /// <summary>
        /// Denominators in the series for sine integral Si(x).
        /// The sequence of the form: (2 * n - 1) * (2 * n - 1)!
        /// </summary>
        private static readonly double[] _A061079 = new double[_maxTaylorTerms + 1];

        /// <summary>
        /// Denominators in the series for cosine integral Ci(x).
        /// The sequence of the form: (2 * n) * (2 * n)!
        /// </summary>
        private static readonly double[] _A062779 = new double[_maxTaylorTerms + 1];

        /// <summary>
        /// Denominators in the series for exponential integral Ei(x).
        /// The sequence of the form: n * n!
        /// </summary>
        private static readonly double[] _A001563 = new double[_maxTaylorTerms + 1];

        #endregion

        #region Constructors

        static ExponentialIntegrals()
        {
            // Calculation denominators in the series for sine integral Si(x).
            for (int n = 1; n <= _maxTaylorTerms; n++)
            {
                _A061079[n] = (2 * n - 1) * Combinatorics.Factorial(2 * n - 1);
            }

            // Calculation denominators in the series for cosine integral Ci(x).
            for (int n = 1; n <= _maxTaylorTerms; n++)
            {
                _A062779[n] = (2 * n) * Combinatorics.Factorial(2 * n);
            }

            // Calculation denominators in the series for exponential integral Ei(x).
            for (int n = 1; n <= _maxTaylorTerms; n++)
            {
                _A001563[n] = n * Combinatorics.Factorial(n);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns the sine integral of a real number.
        /// </summary>
        /// <param name="x">A real number.</param>
        /// <returns>The sine integral of x.</returns>
        [Obsolete("Does not provide accurate results for some arguments.")]
        public static double SinIntegral(double x)
        {
            if (x < 0.0)
            {
                return -SinIntegral(-x);
            }

            if (Math.Abs(x) <= _taylorUpperLimit)
            {
                // Taylor expansion

                double result = 0.0;
                double term;

                double xsq = x * x;
                double xn = x;

                for (int n = 1; n <= _maxTaylorTerms; n++)
                {
                    term = ExMath.MinusOnePow(n - 1) * xn / _A061079[n];
                    result += term;

                    xn *= xsq;

                    if (Math.Abs(term / result) <= Machine.Epsilon)
                        break;
                }

                return result;
            }
            else
            {
                // Asymptotic expansion (for large argument)

                double f, g;
                AuxiliaryFunctions(x, out f, out g);

                return ExMath.PiOverTwo - f * Math.Cos(x) - g * Math.Sin(x);
            }
        }

        /// <summary>
        /// Returns the sine integral of a complex number.
        /// </summary>
        /// <param name="c">A complex number.</param>
        /// <returns>The sine integral of c.</returns>
        [Obsolete("Does not provide accurate results for some arguments.")]
        public static Complex SinIntegral(Complex c)
        {
            if (c.IsReal)
            {
                return SinIntegral(c.Re);
            }
            
            if (Complex.Sign(c) < 0)
            {
                return -SinIntegral(-c);
            }

            if (Complex.Abs(c) <= _taylorUpperLimit)
            {
                // Taylor expansion

                Complex result = Complex.Zero;
                Complex term;

                Complex csq = c * c;
                Complex cn = c;

                for (int n = 1; n <= _maxTaylorTerms; n++)
                {
                    term = ExMath.MinusOnePow(n - 1) * cn / _A061079[n];
                    result += term;

                    cn *= csq;

                    if (Complex.AbsSquared(term / result) <= Machine.Epsilon * Machine.Epsilon)
                        break;
                }

                return result;
            }
            else
            {
                // Asymptotic expansion (for large argument)

                Complex f, g;
                AuxiliaryFunctions(c, out f, out g);

                if (Math.Abs(c.Re) <= 1E-1)
                    return -f * Complex.Cos(c) - g * Complex.Sin(c);
                else
                    return ExMath.PiOverTwo - f * Complex.Cos(c) - g * Complex.Sin(c);
            }
        }

        /// <summary>
        /// Returns the cosine integral of a real number.
        /// </summary>
        /// <param name="x">A real number.</param>
        /// <returns>The cosine integral of x.</returns>
        [Obsolete("Does not provide accurate results for some arguments.")]
        public static double CosIntegral(double x)
        {
            if (x < 0.0)
            {
                return CosIntegral(-x) + Math.Log(x) - Math.Log(-x);
            }

            if (Math.Abs(x) <= _taylorUpperLimit)
            {
                // Taylor expansion

                double result = 0.0;
                double term;

                double xsq = -x * x;
                double xn = xsq;

                for (int n = 1; n <= _maxTaylorTerms; n++)
                {
                    term = xn / _A062779[n];
                    result += term;

                    xn *= xsq;

                    if (Math.Abs(term / result) <= Machine.Epsilon)
                        break;
                }

                return ExMath.EulersConstant + Math.Log(x) + result;
            }
            else
            {
                // Asymptotic expansion (for large argument)

                double f, g;
                AuxiliaryFunctions(x, out f, out g);

                return f * Math.Sin(x) - g * Math.Cos(x);
            }
        }

        /// <summary>
        /// Returns the cosine integral of a complex number.
        /// </summary>
        /// <param name="c">A complex number.</param>
        /// <returns>The cosine integral of c.</returns>
        [Obsolete("Does not provide accurate results for some arguments.")]
        public static Complex CosIntegral(Complex c)
        {
            if (c.IsReal && c.Re >= 0.0)
            {
                return CosIntegral(c.Re);
            }

            if (Complex.Sign(c) < 0)
            {
                return CosIntegral(-c) + Complex.Log(c) - Complex.Log(-c);
            }

            if (Complex.Abs(c) <= _taylorUpperLimit)
            {
                // Taylor expansion

                Complex result = Complex.Zero;
                Complex term;

                Complex csq = -c * c;
                Complex cn = csq;

                for (int n = 1; n <= _maxTaylorTerms; n++)
                {
                    term = cn / _A062779[n];
                    result += term;

                    cn *= csq;

                    if (Complex.AbsSquared(term / result) <= Machine.Epsilon * Machine.Epsilon)
                        break;
                }

                return ExMath.EulersConstant + Complex.Log(c) + result;
            }
            else
            {
                // Asymptotic expansion (for large argument)

                Complex f, g;
                AuxiliaryFunctions(c, out f, out g);

                if (Math.Abs(c.Re) <= 1E-1)
                    return Math.Sign(c.Im) * Complex.I * ExMath.PiOverTwo + f * Complex.Sin(c) - g * Complex.Cos(c);
                else
                    return f * Complex.Sin(c) - g * Complex.Cos(c);
            }
        }

        /// <summary>
        /// Returns the hyperbolic sine integral of a real number.
        /// </summary>
        /// <param name="x">A real number.</param>
        /// <returns>The hyperbolic sine integral of x.</returns>
        [Obsolete("Does not provide accurate results for some arguments.")]
        public static double SinhIntegral(double x)
        {
            return SinIntegral(new Complex(0.0, x)).Im;
        }

        /// <summary>
        /// Returns the hyperbolic sine integral of a complex number.
        /// </summary>
        /// <param name="c">A complex number.</param>
        /// <returns>The hyperbolic sine integral of c.</returns>
        [Obsolete("Does not provide accurate results for some arguments.")]
        public static Complex SinhIntegral(Complex c)
        {
            return -Complex.I * SinIntegral(Complex.I * c);
        }

        /// <summary>
        /// Returns the hyperbolic cosine integral of a real number.
        /// </summary>
        /// <param name="x">A real number.</param>
        /// <returns>The hyperbolic cosine integral of x.</returns>
        [Obsolete("Does not provide accurate results for some arguments.")]
        public static double CoshIntegral(double x)
        {
            return (CosIntegral(new Complex(0.0, x)) + Math.Log(x) - Complex.Log(new Complex(0.0, x))).Re;
        }

        /// <summary>
        /// Returns the hyperbolic cosine integral of a complex number.
        /// </summary>
        /// <param name="c">A complex number.</param>
        /// <returns>The hyperbolic cosine integral of c.</returns>
        [Obsolete("Does not provide accurate results for some arguments.")]
        public static Complex CoshIntegral(Complex c)
        {
            return CosIntegral(Complex.I * c) + Complex.Log(c) - Complex.Log(Complex.I * c);
        }

        /// <summary>
        /// Returns the exponential integral of a real number.
        /// </summary>
        /// <param name="x">A real number.</param>
        /// <returns>The exponential integral of x.</returns>
        [Obsolete("Does not provide accurate results for some arguments.")]
        public static double ExpIntegral(double x)
        {
            double origin = -5.0;

            if (Math.Abs(x + origin) <= _taylorUpperLimit)
            {
                // Taylor expansion

                double result = 0.0;
                double term;

                double xn = x;

                for (int n = 1; n <= _maxTaylorTerms; n++)
                {
                    term = xn / _A001563[n];
                    result += term;

                    xn *= x;

                    if (Math.Abs(term / result) <= Machine.Epsilon)
                        break;
                }

                return Math.Log(Math.Abs(x)) + result + ExMath.EulersConstant;
            }
            else
            {
                // Asymptotic expansion (for large argument)

                double xn = 1.0;
                double result = 0.0;

                for (int k = 0; k < _maxAsymptTerms; k++)
                {
                    result += Combinatorics.Factorial(k) / xn;
                    xn *= x;
                }

                return Math.Exp(x) / x * result;
            }
        }

        /// <summary>
        /// Returns the exponential integral of a complex number.
        /// </summary>
        /// <param name="c">A complex number.</param>
        /// <returns>The exponential integral of c.</returns>
        [Obsolete("Does not provide accurate results for some arguments.")]
        public static Complex ExpIntegral(Complex c)
        {
            if (c.IsReal)
            {
                return ExpIntegral(c.Re);
            }

            Complex origin = -5.0;

            if (Complex.Abs(c + origin) <= _taylorUpperLimit)
            {
                // Taylor expansion

                Complex result = Complex.Zero;
                Complex term;

                Complex cn = c;

                for (int n = 1; n <= _maxTaylorTerms; n++)
                {
                    term = cn / _A001563[n];
                    result += term;

                    cn *= c;

                    if (Complex.AbsSquared(term / result) <= Machine.Epsilon * Machine.Epsilon)
                        break;
                }

                if (Math.Abs(Complex.Arg(c) - Math.PI) <= 1E-15)
                    return Complex.Log(c) + result + ExMath.EulersConstant - Math.PI * Complex.I;
                else
                    return Complex.Log(c) + result + ExMath.EulersConstant;
            }
            else
            {
                // Asymptotic expansion (for large argument)

                Complex cn = Complex.One;
                Complex result = Complex.Zero;

                for (int k = 0; k < _maxAsymptTerms; k++)
                {
                    result += Combinatorics.Factorial(k) / cn;
                    cn *= c;
                }

                return Complex.Exp(c) / c * result + Math.PI * Complex.I * Math.Sign(c.Im);
            }
        }

        /// <summary>
        /// Returns the logarithmic integral of a real number.
        /// </summary>
        /// <param name="x">A real number.</param>
        /// <returns>The logarithmic integral of x.</returns>
        [Obsolete("Does not provide accurate results for some arguments.")]
        public static double LogIntegral(double x)
        {
            return ExpIntegral(Math.Log(x));
        }

        /// <summary>
        /// Returns the logarithmic integral of a complex number.
        /// </summary>
        /// <param name="c">A complex number.</param>
        /// <returns>The logarithmic integral of c.</returns>
        [Obsolete("Does not provide accurate results for some arguments.")]
        public static Complex LogIntegral(Complex c)
        {
            return ExpIntegral(Complex.Log(c));
        }

        private static void AuxiliaryFunctions(double x, out double f, out double g)
        {
            const int n = 2 * (_maxAsymptTerms - 1);

            double P = 0.0;
            double Q = 0.0;

            int sgn = 1;
            double xsq = x * x;
            double den = 1.0;

            for (int i = 0; i <= n; i += 2)
            {
                P += sgn * Combinatorics.Factorial(i) / den;
                Q += sgn * Combinatorics.Factorial(i + 1) / den;

                den *= xsq;
                sgn *= -1;
            }

            f = P / x;
            g = Q / xsq;
        }

        private static void AuxiliaryFunctions(Complex c, out Complex f, out Complex g)
        {
            const int n = 2 * (_maxAsymptTerms - 1);

            Complex P = Complex.Zero;
            Complex Q = Complex.Zero;

            int sgn = 1;
            Complex csq = c * c;
            Complex den = Complex.One;

            for (int i = 0; i <= n; i += 2)
            {
                P += sgn * Combinatorics.Factorial(i) / den;
                Q += sgn * Combinatorics.Factorial(i + 1) / den;

                den *= csq;
                sgn *= -1;
            }

            f = P / c;
            g = Q / csq;
        }

        #endregion
    }
}