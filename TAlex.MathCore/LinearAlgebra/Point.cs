using System;


namespace TAlex.MathCore.LinearAlgebra
{
    public struct Point
    {
        #region Properties

        public double X { get; set; }
        public double Y { get; set; }

        #endregion

        #region Constructors

        public Point(double x, double y)
            : this()
        {
            X = x;
            Y = y;
        }

        #endregion
    }
}
