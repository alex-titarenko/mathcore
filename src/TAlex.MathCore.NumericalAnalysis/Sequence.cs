using System;

namespace TAlex.MathCore.NumericalAnalysis
{
    public static class Sequence
    {
        #region Fields

        private const int _maxIters = 100;

        #endregion

        #region Methods

        public static Complex Summation(Func<Complex, Complex> term, int m, int n)
        {
            Complex sum = Complex.Zero;

            for (int i = m; i <= n; i++)
            {
                sum += term(i);
            }

            return sum;
        }

        public static Complex InfiniteSummation(Func<Complex, Complex> term, int m, double relativeTolerance)
        {
            double tolsq = relativeTolerance * relativeTolerance;

            Complex sum = Complex.Zero;
            Complex termValue;

            for (int i = m; i <= _maxIters; i++)
            {
                termValue = term(i);
                sum += termValue;

                if (Complex.AbsSquared(termValue / sum) <= tolsq)
                    return sum;
            }

            throw new NotConvergenceException();
        }

        public static Complex Product(Func<Complex, Complex> term, int m, int n)
        {
            Complex product = 1;

            for (int i = m; i <= n; i++)
            {
                product *= term(i);
            }

            return product;
        }

        public static Complex InfiniteProduct(Func<Complex, Complex> term, int m, double relativeTolerance)
        {
            double tolsq = relativeTolerance * relativeTolerance;

            Complex product = Complex.One;
            Complex termValue;

            for (int i = m; i < _maxIters; i++)
            {
                termValue = term(i);
                product *= termValue;

                if (Complex.AbsSquared(termValue / product) <= tolsq)
                    return product;
            }

            throw new NotConvergenceException();
        }

        #endregion
    }
}
