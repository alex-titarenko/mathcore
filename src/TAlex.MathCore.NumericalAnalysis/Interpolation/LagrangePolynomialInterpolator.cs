using System;


namespace TAlex.MathCore.NumericalAnalysis.Interpolation
{
    /// <summary>
    /// Represents the method of Lagrange polynomial interpolation.
    /// </summary>
    public class LagrangePolynomialInterpolator : Interpolator
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the LagrangePolynomialInterpolator class.
        /// </summary>
        /// <param name="xValues">An array of real numbers containing the abscissas of the interpolation nodes.</param>
        /// <param name="yValues">An array of real numbers containing the ordinates of the interpolation nodes.</param>
        /// <exception cref="System.ArgumentException">
        /// The length of the array xValues does not match the length of the array yValues.
        /// </exception>
        public LagrangePolynomialInterpolator(double[] xValues, double[] yValues)
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
            double L = 0.0;

            for (int i = 0; i < n; i++)
            {
                double l = 1.0;
                for (int j = 0; j < n; j++)
                {
                    if (j != i)
                        l *= (x - xValues[j]) / (xValues[i] - xValues[j]);
                }

                L += yValues[i] * l;
            }

            return L;
        }

        #endregion
    }
}
