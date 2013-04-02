using System;


namespace TAlex.MathCore.NumericalAnalysis.NumericalIntegration
{
    /// <summary>
    /// Represents the method of Romberg of numerical integration.
    /// </summary>
    public class ComplexRombergIntegrator : ComplexCompositeIntegrator
    {
        #region Fields

        private Complex[,] R;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the ComplexRombergIntegrator class.
        /// </summary>
        public ComplexRombergIntegrator()
        {
            MaxIterations = 20;
            Tolerance = 1E-15;
            R = new Complex[MaxIterations, MaxIterations];
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns the numerical value of the definite integral complex function of one variable.
        /// </summary>
        /// <returns>Approximate value of the definite integral.</returns>
        /// <exception cref="NotConvergenceException">
        /// The algorithm does not converged for a certain number of iterations.
        /// </exception>
        public override Complex Integrate(Function1Complex integrand, double lowerBound, double upperBound)
        {
            if (lowerBound == upperBound)
            {
                return Complex.Zero;
            }

            double tol = Tolerance;

            // Testing the limits to infinity
            if (double.IsInfinity(lowerBound) || double.IsInfinity(upperBound))
            {
                throw new NotConvergenceException("The limits of integration can not be infinite.");
            }

            Complex fa = integrand(lowerBound);
            Complex fb = integrand(upperBound);

            // Testing the endpoints to singularity
            if (Complex.IsInfinity(fa) || Complex.IsNaN(fa) || Complex.IsInfinity(fb) || Complex.IsNaN(fb))
            {
                throw new NotConvergenceException("Calculation does not converge to a solution.");
            }


            R[0, 0] = 0.5 * (upperBound - lowerBound) * (integrand(lowerBound) + integrand(upperBound));

            int n;
            for (n = 1; n < MaxIterations; n++)
            {
                double h = (upperBound - lowerBound) / Math.Pow(2.0, n);

                Complex sum = Complex.Zero;
                for (int k = 1; k <= Math.Pow(2, n - 1); k++)
                    sum += integrand(lowerBound + (2 * k - 1) * h);

                R[n, 0] = 0.5 * R[n - 1, 0] + h * sum;

                for (int m = 1; m <= n; m++)
                    R[n, m] = R[n, m - 1] + (R[n, m - 1] - R[n - 1, m - 1]) / (Math.Pow(4, m) - 1);

                double relativeError = Complex.Abs(R[n, n - 1] - R[n, n]);

                if (relativeError < tol)
                {
                    IterationsNeeded = n;
                    return R[n, n];
                }
            }

            throw new NotConvergenceException("Calculation does not converge to a solution.");
        }

        #endregion
    }
}
