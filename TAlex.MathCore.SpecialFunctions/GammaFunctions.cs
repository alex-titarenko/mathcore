using System;

namespace TAlex.MathCore.SpecialFunctions
{
    /// <summary>
    /// 
    /// </summary>
    public static class GammaFunctions
    {
        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static double Beta(double a, double b)
        {
            return Math.Exp((LogGamma(a) + LogGamma(b)) - LogGamma(a + b));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="d">A real number.</param>
        /// <returns></returns>
        public static double Gamma(double d)
        {
            return Math.Exp(LogGamma(d));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="c">A complex number.</param>
        /// <returns></returns>
        public static Complex Gamma(Complex c)
        {
            if (c.IsReal)
            {
                return Gamma(c.Re);
            }

            return Complex.Exp(LogGamma(c));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="d">A real number.</param>
        /// <returns></returns>
        public static double LogGamma(double d)
        {
            // Internal arithmetic will be done in double precision, a nicety that you can omit if five-figure
            // accuracy is good enough.
            double x, y, tmp, ser;

            double[] cof = new double[] {
                76.18009172947146, -86.50532032941677,
                24.01409824083091, -1.231739572450155,
                0.1208650973866179E-2, -0.5395239384953E-5};

            int j;
            y = x = d;
            tmp = x + 5.5;
            tmp -= (x + 0.5) * Math.Log(tmp);
            ser = 1.000000000190015;

            for (j = 0; j <= 5; j++)
                ser += cof[j] / ++y;

            return -tmp + Math.Log(2.5066282746310005 * ser / x);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="c">A complex number.</param>
        /// <returns></returns>
        public static Complex LogGamma(Complex c)
        {
            // Internal arithmetic will be done in double precision, a nicety that you can omit if five-figure
            // accuracy is good enough.
            Complex x, y, tmp, ser;

            double[] cof = new double[] {
                76.18009172947146, -86.50532032941677,
                24.01409824083091, -1.231739572450155,
                0.1208650973866179E-2, -0.5395239384953E-5};

            int j;
            y = x = c;
            tmp = x + 5.5;
            tmp -= (x + 0.5) * Complex.Log(tmp);
            ser = 1.000000000190015;

            for (j = 0; j <= 5; j++)
            {
                y = y + 1;
                ser += cof[j] / y;
            }

            return -tmp + Complex.Log(2.5066282746310005 * ser / x);
        }

        #endregion
    }
}
