using System;


namespace TAlex.MathCore.LinearAlgebra
{
    /// <summary>
    /// Represents a point in 2-D space.
    /// </summary>
    public struct Point
    {
        #region Properties

        /// <summary>
        /// Gets or sets X component of the point.
        /// </summary>
        public double X { get; set; }

        /// <summary>
        /// Gets or sets Y component of the point.
        /// </summary>
        public double Y { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of a point.
        /// </summary>
        /// <param name="x">X component of the point.</param>
        /// <param name="y">Y component of the point.</param>
        public Point(double x, double y)
            : this()
        {
            X = x;
            Y = y;
        }

        #endregion
    }
}
