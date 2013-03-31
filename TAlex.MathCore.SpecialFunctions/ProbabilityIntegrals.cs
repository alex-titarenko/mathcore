using System;


namespace TAlex.MathCore.SpecialFunctions
{
    /// <summary>
    /// Contains methods for evaluating the error function and related functions.
    /// </summary>
    public static class ProbabilityIntegrals
    {
        #region Fields

        private const double _intval1 = 0.46875;
        private const double _intval2 = 4.0;

        private const double _maxArgVal = 26.5583093100796414;

        /// <summary>
        /// Coefficients for approximation to erf in first interval.
        /// </summary>
        private static readonly double[] erf_p = new double[] {
            3.209377589138469472562E03,
            3.774852376853020208137E02,
            1.138641541510501556495E02,
            3.161123743870565596947E00,
            1.857777061846031526730E-01
        };

        private static readonly double[] erf_q = new double[] {
            2.844236833439170622273E03,
            1.282616526077372275645E03,
            2.440246379344441733056E02,
            2.360129095234412093499E01,
            1.0E00
        };

        /// <summary>
        /// Coefficients for approximation to erfc in second interval.
        /// </summary>
        private static readonly double[] erfc_p1 = new double[] {
            1.23033935479799725272E03,
            2.05107837782607146532E03,
            1.71204761263407058314E03,
            8.81952221241769090411E02,
            2.98635138197400131132E02,
            6.61191906371416294775E01,
            8.88314979438837594118E00,
            5.64188496988670089180E-01,
            2.15311535474403846343E-08
        };

        private static readonly double[] erfc_q1 = new double[] {
            1.23033935480374942043E03,
            3.43936767414372163696E03,
            4.36261909014324715820E03,
            3.29079923573345962678E03,
            1.62138957456669018874E03,
            5.37181101862009857509E02,
            1.17693950891312499305E02,
            1.57449261107098347253E01,
            1.0E00
        };

        /// <summary>
        /// Coefficients for approximation to erfc in third interval.
        /// </summary>
        private static readonly double[] erfc_p2 = new double[] {
            -6.58749161529837803157E-04,
            -1.60837851487422766278E-02,
            -1.25781726111229246204E-01,
            -3.60344899949804439429E-01,
            -3.05326634961232344035E-01,
            -1.63153871373020978498E-02
        };

        private static readonly double[] erfc_q2 = new double[] {
            2.33520497626869185443E-03,
            6.05183413124413191178E-02,
            5.27905102951428412248E-01,
            1.87295284992346047209E00,
            2.56852019228982242072E00,
            1.0E00
        };

        #endregion

        #region Methods

        /// <summary>
        /// Returns the value of error function for the specified argument.
        /// </summary>
        /// <param name="x">A real number.</param>
        /// <returns>The value of the error function for x.</returns>
        public static double Erf(double x)
        {
            if (Math.Abs(x) > _maxArgVal)
            {
                return (x > 0.0) ? 1.0 : -1.0;
            }

            if (Math.Abs(x) <= _intval1)
            {
                return x * RationalFunc(erf_p, erf_q, x * x);
            }

            return 1.0 - Erfc(x);
        }

        /// <summary>
        /// Returns the value of error function for the specified argument.
        /// </summary>
        /// <param name="x">A complex number.</param>
        /// <returns>The value of the error function for x.</returns>
        /// <remarks>
        /// Erf(z) ~ 1 - GammaRegularized(1/2, z^2)
        /// </remarks>
        public static Complex Erf(Complex x)
        {
            if (x.IsReal)
            {
                return Erf(x.Re);
            }

            if (Complex.Abs(x) <= _intval1)
            {
                return x * RationalFunc(erf_p, erf_q, x * x);
            }

            return 1.0 - Erfc(x);
        }

        /// <summary>
        /// Returns the value of two-argument error function for the specified arguments.
        /// </summary>
        /// <param name="x0">The first complex number.</param>
        /// <param name="x1">The second complex number.</param>
        /// <returns>The value of two-argument error function for x0 and x1.</returns>
        public static double Erf(double x0, double x1)
        {
            return Erf(x1) - Erf(x0);
        }

        /// <summary>
        /// Returns the value of two-argument error function for the specified arguments.
        /// </summary>
        /// <param name="x0">The first complex number.</param>
        /// <param name="x1">The second complex number.</param>
        /// <returns>The value of two-argument error function for x0 and x1.</returns>
        public static Complex Erf(Complex x0, Complex x1)
        {
            return Erf(x1) - Erf(x0);
        }

        /// <summary>
        /// Returns the value of complementary error function for the specified argument.
        /// </summary>
        /// <param name="x">A real number.</param>
        /// <returns>The value of the complementary error function for x.</returns>
        public static double Erfc(double x)
        {
            double absx = Math.Abs(x);

            if (absx > _maxArgVal)
            {
                return (x > 0.0) ? 0.0 : 2.0;
            }

            if (absx <= _intval1)
            {
                return 1.0 - Erf(x);
            }

            if (x < 0.0)
            {
                return 2.0 - Erfc(-x);
            }

            if (absx <= _intval2)
            {
                return Math.Exp(-(x * x)) * RationalFunc(erfc_p1, erfc_q1, x);
            }
            else
            {
                double xsq = x * x;
                double invxsq = 1.0 / xsq;
                double R = RationalFunc(erfc_p2, erfc_q2, invxsq);
                double t = 1.0 / ExMath.SqrtPi + invxsq * R;
                return Math.Exp(-xsq) / x * (t - Math.Truncate(t));
            }
        }

        /// <summary>
        /// Returns the value of complementary error function for the specified argument.
        /// </summary>
        /// <param name="x">A complex number.</param>
        /// <returns>The value of the complementary error function for x.</returns>
        public static Complex Erfc(Complex x)
        {
            if (x.IsReal)
            {
                return Erfc(x.Re);
            }

            double absx = Complex.Abs(x);

            if (absx <= _intval1)
            {
                return 1.0 - Erf(x);
            }

            if (Complex.Sign(x) < 0)
            {
                return 2.0 - Erfc(-x);
            }

            if (absx <= _intval2)
            {
                Complex result = Complex.Exp(-(x * x)) * RationalFunc(erfc_p1, erfc_q1, x);

                if (x.IsImaginary)
                    return new Complex(1.0, result.Im);
                else
                    return result;
            }
            else
            {
                Complex xsq = x * x;
                Complex invxsq = Complex.Inverse(xsq);
                Complex R = RationalFunc(erfc_p2, erfc_q2, invxsq);
                Complex t = 1.0 / ExMath.SqrtPi + invxsq * R;

                Complex result = Complex.Exp(-xsq) / x * (t - Complex.Truncate(t));

                if (x.IsImaginary && Math.Abs(x.Im) < 6.447290721054759554)
                    return new Complex(1.0, result.Im);
                else
                    return result;
            }
        }

        private static double RationalFunc(double[] numerator, double[] denominator, double value)
        {
            double numval = 0.0;

            for (int i = numerator.Length - 1; i >= 0; i--)
            {
                numval = numval * value + numerator[i];
            }

            double denval = 0.0;

            for (int i = denominator.Length - 1; i >= 0; i--)
            {
                denval = denval * value + denominator[i];
            }

            return numval / denval;
        }

        private static Complex RationalFunc(double[] numerator, double[] denominator, Complex value)
        {
            Complex numval = Complex.Zero;

            for (int i = numerator.Length - 1; i >= 0; i--)
            {
                numval = numval * value + numerator[i];
            }

            Complex denval = Complex.Zero;

            for (int i = denominator.Length - 1; i >= 0; i--)
            {
                denval = denval * value + denominator[i];
            }

            return numval / denval;
        }

        #endregion
    }
}