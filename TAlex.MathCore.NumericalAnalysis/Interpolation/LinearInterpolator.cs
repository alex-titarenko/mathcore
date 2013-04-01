using System;


namespace TAlex.MathCore.NumericalAnalysis.Interpolation
{
    /// <summary>
    /// Represents the method of linear interpolation.
    /// </summary>
    public class LinearInterpolator : Interpolator
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the LinearInterpolator class.
        /// </summary>
        /// <param name="xValues">An array of real numbers containing the abscissas of the interpolation nodes.</param>
        /// <param name="yValues">An array of real numbers containing the ordinates of the interpolation nodes.</param>
        /// <exception cref="System.ArgumentException">
        /// The length of the array xValues does not match the length of the array yValues.
        /// </exception>
        /// <exception cref="System.ArgumentException">
        /// Elements of xValues are not sorted in ascending order.
        /// </exception>
        public LinearInterpolator(double[] xValues, double[] yValues)
            : base(xValues, yValues)
        {
            for (int i = 0; i < n - 1; i++)
            {
                if (xValues[i + 1] <= xValues[i])
                    throw new ArgumentException("The abscissas of interpolation nodes must be in ascending order.");
            }
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
            if (n == 1)
            {
                return yValues[0];
            }

            if (x <= xValues[0])
            {
                return ((x - xValues[0]) * yValues[1] + (xValues[1] - x) * yValues[0]) /
                    (xValues[1] - xValues[0]);
            }
            else if (x >= xValues[n - 1])
            {
                return ((x - xValues[n - 2]) * yValues[n - 1] + (xValues[n - 1] - x) * yValues[n - 2]) /
                    (xValues[n - 1] - xValues[n - 2]);
            }

            int idx = 0;

            for (int i = 0; i < n; i++)
            {
                if (x < xValues[i])
                {
                    idx = i;
                    break;
                }
            }

            return ((x - xValues[idx - 1]) * yValues[idx] + (xValues[idx] - x) * yValues[idx - 1]) /
                (xValues[idx] - xValues[idx - 1]);
        }

        #endregion
    }
}
