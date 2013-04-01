using System;


namespace TAlex.MathCore.NumericalAnalysis.Interpolation
{
    /// <summary>
    /// Represents the method of Newton polynomial interpolation.
    /// </summary>
    public class NewtonPolynomialInterpolator : Interpolator
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the NewtonPolynomialInterpolator class.
        /// </summary>
        /// <param name="xValues">An array of real numbers containing the abscissas of the interpolation nodes.</param>
        /// <param name="yValues">An array of real numbers containing the ordinates of the interpolation nodes.</param>
        /// <exception cref="System.ArgumentException">
        /// The length of the array xValues does not match the length of the array yValues.
        /// </exception>
        public NewtonPolynomialInterpolator(double[] xValues, double[] yValues)
            : base(xValues, yValues)
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns the interpolated value at specified value.
        /// </summary>
        /// <param name="x">A real number.</param>
        /// <returns>The interpolated value at x.</returns>
        public override double Interpolate(double x)
        {
            double m = 1.0;

            double N = yValues[0];

            for (int i = 1; i < n; i++)
            {
                double C = 0.0;
                for (int j = 0; j <= i; j++)
                {
                    double denom = 1.0;
                    for (int k = 0; k <= i; k++)
                    {
                        if (k != j)
                            denom *= (xValues[j] - xValues[k]);
                    }
                    C += yValues[j] / denom;
                }

                m *= (x - xValues[i - 1]);
                N += C * m;
            }

            return N;
        }

        #endregion
    }
}
