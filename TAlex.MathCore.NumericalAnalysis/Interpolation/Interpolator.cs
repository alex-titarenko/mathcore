using System;
using System.Collections.Generic;


namespace TAlex.MathCore.NumericalAnalysis.Interpolation
{
    /// <summary>
    /// Represents the abstract base class for classes implementing algorithms of interpolation.
    /// </summary>
    public abstract class Interpolator
    {
        #region Fields

        /// <summary>
        /// Represents the number of interpolation nodes.
        /// </summary>
        protected readonly int n;

        /// <summary>
        /// Represents the abscissas of the interpolation nodes.
        /// </summary>
        protected IList<double> xValues;

        /// <summary>
        /// Represents the ordinates of the interpolation nodes.
        /// </summary>
        protected IList<double> yValues;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the array of real numbers containing the abscissas of the interpolation nodes.
        /// </summary>
        public IList<double> XValues
        {
            get
            {
                return xValues;
            }
        }

        /// <summary>
        /// Gets the array of real numbers containing the ordinates of the interpolation nodes.
        /// </summary>
        public IList<double> YValues
        {
            get
            {
                return yValues;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Interpolator class.
        /// </summary>
        /// <param name="xValues">An array of real numbers containing the abscissas of the interpolation nodes.</param>
        /// <param name="yValues">An array of real numbers containing the ordinates of the interpolation nodes.</param>
        /// <exception cref="System.ArgumentException">
        /// The length of the array xValues does not match the length of the array yValues.
        /// </exception>
        public Interpolator(IList<double> xValues, IList<double> yValues)
        {
            if (xValues.Count != yValues.Count)
                throw new ArgumentException("The lengths of the two arrays do not match.");

            this.xValues = xValues;
            this.yValues = yValues;
            n = xValues.Count;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns the interpolated value at specified value.
        /// </summary>
        /// <param name="x">A real number.</param>
        /// <returns>The interpolated value at x.</returns>
        public abstract double Interpolate(double x);

        #endregion
    }
}
