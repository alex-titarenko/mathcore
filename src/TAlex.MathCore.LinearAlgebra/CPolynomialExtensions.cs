using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAlex.MathCore.LinearAlgebra
{
    public static class CPolynomialExtensions
    {
        /// <summary>
        /// Returns the value of the complex polynomial evaluated at a specified value.
        /// </summary>
        /// <param name="poly">The source.</param>
        /// <param name="value">A complex square matrix.</param>
        /// <returns>The evaluated value of the complex polynomial.</returns>
        /// <exception cref="MatrixSizeMismatchException">The matrix value is not square.</exception>
        public static CMatrix Evaluate(this CPolynomial poly, CMatrix value)
        {
            if (!value.IsSquare)
                throw new MatrixSizeMismatchException("The matrix must be square.");

            int len = poly.Degree + 1;
            int n = value.RowCount;

            CMatrix m = CMatrix.Identity(n);
            CMatrix result = new CMatrix(n, n);

            for (int i = 0; i < len; i++)
            {
                result += poly[i] * m;
                m *= value;
            }

            return result;
        }

        /// <summary>
        /// Returns a vector of approximate values of roots of a complex polynomial.
        /// </summary>
        /// <param name="poly">A complex polynomial.</param>
        /// <returns>The approximate values of roots of poly.</returns>
        /// <exception cref="System.ArgumentException">
        /// Number of elements in poly is less than 2 or more than 99.
        /// </exception>
        public static Complex[] Roots(this CPolynomial poly)
        {
            return CompanionMatrixRootsFinding(poly);
        }

        /// <summary>
        /// Returns a vector of approximate values of roots of a complex polynomial by companion matrix method.
        /// </summary>
        /// <param name="poly">A complex polynomial.</param>
        /// <returns>The approximate values of roots of poly.</returns>
        /// <exception cref="System.ArgumentException">
        /// Number of elements in coeffs is less than 2 or more than 99.
        /// </exception>
        public static Complex[] CompanionMatrixRootsFinding(this CPolynomial poly)
        {
            // Remove zero elements standing at the end.
            int lidx = 0;
            int ridx = poly.Length - 1;
            while (ridx >= 0 && poly[ridx] == Complex.Zero) ridx--;
            while (lidx < poly.Length && poly[lidx] == Complex.Zero) lidx++;
            int length = ridx + 1;

            if (length < 2 || length > 99)
                throw new ArgumentException("Number of coefficients must be between 1 and 100.");

            int rootsCount = length - 1;
            Complex[] roots = new Complex[rootsCount];

            int n = ridx - lidx;
            CMatrix companionMatrix = new CMatrix(n, n);

            for (int i = 1; i < n; i++)
                companionMatrix[i, i - 1] = Complex.One;

            for (int i = 0; i < n; i++)
                companionMatrix[i, n - 1] = -poly[lidx + i] / poly[ridx];

            CMatrix eigenvals = CMatrix.Eigenvalues(companionMatrix);

            for (int i = 0; i < n; i++)
                roots[i] = eigenvals[i];

            Array.Sort<Complex>(roots, new ComplexComparer());
            return roots;
        }
    }
}
