using System;
using System.Collections;
using System.Collections.Generic;


namespace TAlex.MathCore
{
    /// <summary>
    /// Represents a complex number comparison operation.
    /// </summary>
    public class ComplexComparer : IComparer, IComparer<Complex>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the ComplexComparer class.
        /// </summary>
        public ComplexComparer()
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Compares two objects and returns an indication of their relative sort order.
        /// </summary>
        /// <param name="x">An object to compare to y.</param>
        /// <param name="y">An object to compare to x.</param>
        /// <returns>
        /// Value Meaning Less than zero x is less than y. -or- x is null.
        /// Zero x is equal to y. Greater than zero x is greater than y. -or- y is null. 
        /// </returns>
        /// <exception cref="System.ArgumentException">Neither x nor y is a Complex object.</exception>
        public int Compare(object x, object y)
        {
            if (x == y) return 0;
            if (x == null) return -1;
            if (y == null) return 1;

            Complex c1 = (Complex)x;
            Complex c2 = (Complex)y;

            return Compare(c1, c2);
        }

        /// <summary>
        /// Compares two complex numbers and returns an indication of their relative sort order.
        /// </summary>
        /// <param name="c1">A complex number to compare to c2.</param>
        /// <param name="c2">A complex number to compare to c1.</param>
        /// <returns>
        /// Value Meaning Less than zero c1 is less than c2.
        /// Zero c1 is equal to c2. Greater than zero c1 is greater than c2.
        /// </returns>
        public int Compare(Complex c1, Complex c2)
        {
            if (Math.Abs(c1.Re - c2.Re) == Complex.Zero)
                return c1.Im.CompareTo(c2.Im);
            else
                return c1.Re.CompareTo(c2.Re);
        }

        #endregion
    }
}
