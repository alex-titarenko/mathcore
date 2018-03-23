using System;


namespace TAlex.MathCore
{
    /// <summary>
    /// Represents machine constants for the floating-point arithmetic.
    /// </summary>
    public static class Machine
    {
        #region Fields

        /// <summary>
        /// Represents the machine epsilon for double-precision floating-point numbers.
        /// Machine epsilon gives an upper bound on the relative error due to rounding in floating point arithmetic.
        /// </summary>
        public const double Epsilon = 2.2204460492503131E-16;

        /// <summary>
        /// Represents the square root of the machine epsilon for double-precision floating-point numbers.
        /// </summary>
        public const double SqrtEpsilon = 1.4901161193847656E-08;

        /// <summary>
        /// Represents the cube root of the machine epsilon for double-precision floating-point numbers.
        /// </summary>
        public const double CubeRootEpsilon = 6.0554544523933395E-06;

        /// <summary>
        /// Represents the natural logarithm of machine epsilon. 
        /// </summary>
        public const double LogEpsilon = -36.043653389117154;

        /// <summary>
        /// Represents the smallest positive double-precision floating-point number that is greater than zero.
        /// </summary>
        public const double MinDouble = 2.2250738585072014E-308;

        /// <summary>
        /// Represents the square root of MinDouble.
        /// </summary>
        public const double SqrtMinDouble = 1.4916681462400413E-154;

        /// <summary>
        /// Represents the natural logarithm of MinDouble.
        /// </summary>
        public const double LogMinDouble = -708.39641853226408;

        /// <summary>
        /// Represents the largest possible value of a double-precision floating-point number.
        /// </summary>
        public const double MaxDouble = 1.7976931348623157E+308;

        /// <summary>
        /// Represents the square root of MaxDouble.
        /// </summary>
        public const double SqrtMaxDouble = 1.3407807929942596E+154;

        /// <summary>
        /// Represents the natural logarithm of MaxDouble.
        /// </summary>
        public const double LogMaxDouble = 709.78271289338397;

        #endregion
    }
}
