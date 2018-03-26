using System;


namespace TAlex.MathCore.NumericalAnalysis
{
    /// <summary>
    /// Represents numerical differentiation of the first, second, third and fourth orders.
    /// </summary>
    public static class NumericalDerivation
    {
        #region Fields

        private const int ntab = 50;                        // Sets maximum size of tableau.
        private const double con = 1.4;                     // Stepsize is decreased by CON at each iteration.
        private const double con2 = con * con;
        private const double big = double.MaxValue;
        private const double safe = 2.0;                    // Return when error is SAFE worse than the best so far.

        private static Complex[,] a = new Complex[ntab, ntab];

        private const double _tol = 1E-6;

        #endregion

        #region Methods

        /// <summary>
        /// Returns the value of the central derivative of the first order.
        /// </summary>
        /// <param name="function">A target complex function.</param>
        /// <param name="c">A point at which the derivative is calculated.</param>
        /// <returns>Numerical approximation of the value of the derivative of function at point c.</returns>
        public static Complex FirstDerivative(Func<Complex, Complex> function, Complex c)
        {
            double err;
            double h = 0.01 + 1E-16;
            Complex result = RidersDerivation(function, CentralFirstDerivative3Points, c, h, out err);

            return result;
        }

        /// <summary>
        /// Returns the value of the central derivative of the second order.
        /// </summary>
        /// <param name="function">A target complex function.</param>
        /// <param name="c">A point at which the derivative is calculated.</param>
        /// <returns>Numerical approximation of the value of the derivative of function at point c.</returns>
        public static Complex SecondDerivative(Func<Complex, Complex> function, Complex c)
        {
            double err;
            double h = 0.01 + 1E-16;
            Complex result = RidersDerivation(function, CentralSecondDerivative3Points, c, h, out err);

            return result;
        }

        /// <summary>
        /// Returns the value of the central derivative of the third order.
        /// </summary>
        /// <param name="function">A target complex function.</param>
        /// <param name="c">A point at which the derivative is calculated.</param>
        /// <returns>Numerical approximation of the value of the derivative of function at point c.</returns>
        public static Complex ThirdDerivative(Func<Complex, Complex> function, Complex c)
        {
            double err;
            double h = 0.01 + 1E-16;
            Complex result = RidersDerivation(function, CentralThirdDerivative3Points, c, h, out err);

            return result;
        }

        /// <summary>
        /// Returns the value of the central derivative of the fourth order.
        /// </summary>
        /// <param name="function">A target complex function.</param>
        /// <param name="c">A point at which the derivative is calculated.</param>
        /// <returns>Numerical approximation of the value of the derivative of function at point c.</returns>
        public static Complex FourthDerivative(Func<Complex, Complex> function, Complex c)
        {
            double err;
            double h = 0.1 + 1E-16;
            Complex result = RidersDerivation(function, CentralFourthDerivative5Points, c, h, out err);

            return result;
        }


        /// <summary>
        /// Returns the derivative of a complex function at a point x by Ridders' method of polynomial
        /// extrapolation. The value h is input as an estimated initial stepsize; it need not be small, but
        /// rather should be an increment in x over which func changes substantially. An estimate of the
        /// error in the derivative is returned as err.
        /// </summary>
        /// <param name="function">A target complex function.</param>
        /// <param name="difference">A complex function represents the difference quotient.</param>
        /// <param name="x">A point at which the derivative is calculated.</param>
        /// <param name="h">An estimated initial stepsize.</param>
        /// <param name="err">An estimate of the error.</param>
        /// <returns>Numerical approximation of the value of the derivative of function at point x.</returns>
        private static Complex RidersDerivation(Func<Complex, Complex> function, DifferenceQuotient difference, Complex x, double h, out double err)
        {
            if (Complex.IsNaN(function(x)) || Complex.IsInfinity(function(x)))
            {
                err = double.MaxValue;
                return double.NaN;
            }

            int i, j;
            double errt, fac, hh;
            Complex ans = Complex.Zero;

            if (h == 0.0)
                throw new ArgumentException("h must be nonzero.");

            hh = h;
            a[0, 0] = difference(function, x, hh);
            err = big;

            for (i = 1; i < ntab; i++)
            {
                // Successive columns in the Neville tableau will go to smaller stepsizes and higher orders of
                // extrapolation.

                hh /= con;
                a[0, i] = difference(function, x, hh);      // Try new, smaller stepsize.
                fac = con2;

                // Compute extrapolations of various orders, requiring no new function evaluations.
                for (j = 1; j <= i; j++)
                {
                    a[j, i] = (a[j - 1, i] * fac - a[j - 1, i - 1]) / (fac - 1.0);
                    fac = con2 * fac;
                    errt = Math.Max(Complex.Abs(a[j, i] - a[j - 1, i]), Complex.Abs(a[j, i] - a[j - 1, i - 1]));

                    // The error strategy is to compare each new extrapolation to one order lower, both
                    // at the present stepsize and the previous one.

                    // If error is decreased, save the improved answer.
                    if (errt <= err)
                    {
                        err = errt;
                        ans = a[j, i];
                    }
                }

                // If higher order is worse by a significant factor SAFE, then quit early.
                if (Complex.Abs(a[i, i] - a[i - 1, i - 1]) >= safe * err && err <= _tol * Complex.Abs(ans))
                {
                    return ans;
                }
            }

            throw new NotConvergenceException("Calculation does not converge to a solution.");
        }

        #region Differences

        private static Complex CentralFirstDerivative3Points(Func<Complex, Complex> func, Complex value, double h)
        {
            return (func(value + h) - func(value - h)) / (2.0 * h);
        }

        private static Complex CentralFirstDerivative5Points(Func<Complex, Complex> func, Complex value, double h)
        {
            return (-func(value + 2 * h) + 8 * func(value + h) - 8 * func(value - h) + func(value - 2 * h)) / (12 * h);
        }

        private static Complex CentralFirstDerivative7Points(Func<Complex, Complex> func, Complex value, double h)
        {
            return (-func(value - 3 * h) + 9 * func(value - 2 * h) - 45 * func(value - h) + 45 * func(value + h) - 9 * func(value + 2 * h) + func(value + 3 * h)) / (60 * h);
        }


        private static Complex CentralSecondDerivative3Points(Func<Complex, Complex> func, Complex value, double h)
        {
            return (func(value - h) - 2 * func(value) + func(value + h)) / (h * h);
        }

        private static Complex CentralSecondDerivative5Points(Func<Complex, Complex> func, Complex value, double h)
        {
            return (-func(value + 2 * h) + 16 * func(value + h) - 30 * func(value) + 16 * func(value - h) - func(value - 2 * h)) / (12 * h * h);
        }

        private static Complex CentralSecondDerivative7Points(Func<Complex, Complex> func, Complex value, double h)
        {
            return (2 * func(value - 3 * h) - 27 * func(value - 2 * h) + 270 * func(value - h) - 490 * func(value) + 270 * func(value + h) - 27 * func(value + 2 * h) + 2 * func(value + 3 * h)) / (180 * h * h);
        }


        private static Complex CentralThirdDerivative3Points(Func<Complex, Complex> func, Complex value, double h)
        {
            return (-func(value - 2 * h) + 2 * func(value - h) - 2 * func(value + h) + func(value + 2 * h)) / (2 * h * h * h);
        }


        private static Complex CentralFourthDerivative5Points(Func<Complex, Complex> func, Complex value, double h)
        {
            return (func(value + 2 * h) - 4 * func(value + h) + 6 * func(value) - 4 * func(value - h) + func(value - 2 * h)) / (h * h * h * h);
        }

        private static Complex CentralFourthDerivative7Points(Func<Complex, Complex> func, Complex value, double h)
        {
            return (-func(value - 3 * h) + 12 * func(value - 2 * h) - 39 * func(value - h) + 56 * func(value) - 39 * func(value + h) + 12 * func(value + 2 * h) - func(value + 3 * h)) / (6 * h * h * h * h);
        }

        #endregion

        #endregion

        #region Nested types

        /// <summary>
        /// A delegate to a function that represents the difference quotient.
        /// </summary>
        /// <param name="function">A target complex function.</param>
        /// <param name="c">A point at which the derivative is calculated.</param>
        /// <param name="h">A stepsize value.</param>
        /// <returns>Numerical approximation of the value of the derivative of function at point c.</returns>
        private delegate Complex DifferenceQuotient(Func<Complex, Complex> function, Complex c, double h);

        #endregion
    }
}
