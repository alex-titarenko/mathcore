using System;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Collections.Generic;


namespace TAlex.MathCore.LinearAlgebra
{
    /// <summary>
    /// Represents a general complex matrix.
    /// </summary>
    public class CMatrix : IFormattable, IEnumerable<Complex>
    {
        #region Fields

        /// <summary>
        /// Providing data for the complex matrix.
        /// </summary>
        private Complex[,] _m;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the number of rows of the matrix.
        /// </summary>
        public int RowCount
        {
            get
            {
                return _m.GetLength(0);
            }
        }

        /// <summary>
        /// Gets the number of columns of the matrix.
        /// </summary>
        public int ColumnCount
        {
            get
            {
                return _m.GetLength(1);
            }
        }

        /// <summary>
        /// Gets or sets the one element of the matrix.
        /// </summary>
        /// <param name="row">A row index.</param>
        /// <param name="column">A column index.</param>
        public Complex this[long row, long column]
        {
            get
            {
                return _m[row, column];
            }

            set
            {
                _m[row, column] = value;
            }
        }

        /// <summary>
        /// Gets or sets the one element of the column vector.
        /// </summary>
        /// <param name="index">Element index.</param>
        public Complex this[long index]
        {
            get
            {
                if (IsVector) return _m[index, 0];
                else throw new MatrixSizeMismatchException();
            }

            set
            {
                if (IsVector) _m[index, 0] = value;
                else throw new MatrixSizeMismatchException();
            }
        }

        /// <summary>
        /// Gets the transpose of the matrix.
        /// </summary>
        public CMatrix Transpose
        {
            get
            {
                CMatrix result = new CMatrix(ColumnCount, RowCount);

                for (int i = 0; i < RowCount; i++)
                {
                    for (int j = 0; j < ColumnCount; j++)
                    {
                        result[j, i] = this[i, j];
                    }
                }

                return result;
            }
        }

        /// <summary>
        /// Gets the conjugate of the matrix.
        /// </summary>
        public CMatrix Conjugate
        {
            get
            {
                CMatrix m = new CMatrix(RowCount, ColumnCount);

                for (int i = 0; i < RowCount; i++)
                {
                    for (int j = 0; j < ColumnCount; j++)
                    {
                        m[i, j] = Complex.Conjugate(this[i, j]);
                    }
                }

                return m;
            }
        }

        /// <summary>
        /// Gets the conjugate transpose of the matrix.
        /// </summary>
        public CMatrix Adjoint
        {
            get
            {
                return Transpose.Conjugate;
            }
        }

        /// <summary>
        /// Gets number of elements of the matrix.
        /// </summary>
        public int Length
        {
            get { return _m.Length; }
        }

        /// <summary>
        /// Gets the maximum element of the matrix.
        /// </summary>
        public Complex Max
        {
            get
            {
                Complex max = _m[0, 0];

                for (int i = 0; i < RowCount; i++)
                {
                    for (int j = 0; j < ColumnCount; j++)
                        max = Complex.Max(max, _m[i, j]);
                }

                return max;
            }
        }

        /// <summary>
        /// Gets the minimum element of the matrix.
        /// </summary>
        public Complex Min
        {
            get
            {
                Complex min = _m[0, 0];

                for (int i = 0; i < RowCount; i++)
                {
                    for (int j = 0; j < ColumnCount; j++)
                        min = Complex.Min(min, _m[i, j]);
                }

                return min;
            }
        }

        /// <summary>
        /// Gets an array consisting of the real parts of elements of the matrix.
        /// </summary>
        public double[,] Real
        {
            get
            {
                double[,] m = new double[RowCount, ColumnCount];

                for (int i = 0; i < RowCount; i++)
                    for (int j = 0; j < ColumnCount; j++)
                        m[i, j] = this[i, j].Re;

                return m;
            }
        }

        /// <summary>
        /// Gets an array consisting of the imaginary parts of elements of the matrix.
        /// </summary>
        public double[,] Imag
        {
            get
            {
                double[,] m = new double[RowCount, ColumnCount];

                for (int i = 0; i < RowCount; i++)
                    for (int j = 0; j < ColumnCount; j++)
                        m[i, j] = this[i, j].Im;

                return m;
            }
        }

        /// <summary>
        /// Gets a value that indicating whether the matrix
        /// contains any elements that evaluate are infinity or not a number.
        /// </summary>
        public bool IsFinite
        {
            get
            {
                for (int i = 0; i < RowCount; i++)
                {
                    for (int j = 0; j < ColumnCount; j++)
                        if (Complex.IsInfinity(this[i, j]) || Complex.IsNaN(this[i, j]))
                            return false;
                }

                return true;
            }
        }

        /// <summary>
        /// Gets a value that indicates whether the matrix is real
        /// (the matrix consisting only of real entries).
        /// </summary>
        public bool IsReal
        {
            get
            {
                for (int i = 0; i < RowCount; i++)
                    for (int j = 0; j < ColumnCount; j++)
                        if (!this[i, j].IsReal) return false;

                return true;
            }
        }

        /// <summary>
        /// Gets a value that indicates whether the matrix is column vector.
        /// </summary>
        public bool IsVector
        {
            get
            {
                return (ColumnCount == 1 && RowCount >= 1);
            }
        }

        /// <summary>
        /// Gets a value that indicates whether the matrix is identity matrix.
        /// </summary>
        public bool IsIdentity
        {
            get
            {
                return (IsSquare && this == Identity(RowCount));
            }
        }

        /// <summary>
        /// Gets a value that indicates whether the matrix is square
        /// (the matrix with the same number of rows and columns).
        /// </summary>
        public bool IsSquare
        {
            get
            {
                return (RowCount == ColumnCount);
            }
        }

        /// <summary>
        /// Gets a value that indicates whether the matrix is diagonal
        /// (the square matrix in which the entries outside the main diagonal are all zero).
        /// </summary>
        public bool IsDiagonal
        {
            get
            {
                if (!IsSquare) return false;

                for (int i = 0; i < RowCount; i++)
                {
                    for (int j = 0; j < ColumnCount; j++)
                    {
                        if (i != j && this[i, j] != Complex.Zero)
                            return false;
                    }
                }

                return true;
            }
        }

        /// <summary>
        /// Gets a value that indicates whether the matrix is tridiagonal
        /// (the square matrix has nonzero elements only in the main diagonal,
        /// the first diagonal below this, and the first diagonal above the main diagonal).
        /// </summary>
        public bool IsTridiagonal
        {
            get
            {
                if (!IsSquare) return false;

                for (int i = 0; i < RowCount; i++)
                {
                    for (int j = 0; j < ColumnCount; j++)
                    {
                        if ((i != j && i + 1 != j && i - 1 != j) && this[i, j] != Complex.Zero)
                            return false;
                    }
                }

                return true;
            }
        }

        /// <summary>
        /// Gets a value that indicates whether the matrix consisting only of zeros and ones.
        /// </summary>
        public bool IsZeroOneMatrix
        {
            get
            {
                for (int i = 0; i < RowCount; i++)
                    for (int j = 0; j < ColumnCount; j++)
                        if (this[i, j] != Complex.Zero && this[i, j] != Complex.One)
                            return false;

                return true;
            }
        }

        /// <summary>
        /// Gets a value that indicates whether the matrix is permutation of the identity matrix.
        /// </summary>
        public bool IsPermutation
        {
            get
            {
                return (IsSquare && IsZeroOneMatrix && Multiply(this, Transpose) == Identity(RowCount));
            }
        }

        /// <summary>
        /// Gets a value that indicates whether the matrix is trapeze
        /// (the matrix where the entries either below or above the main diagonal are zero).
        /// </summary>
        public bool IsTrapeze
        {
            get
            {
                return (IsUpperTrapeze || IsLowerTrapeze);
            }
        }

        /// <summary>
        /// Gets a value that indicates whether the matrix is triangular
        /// (the square matrix where the entries either below or above the main diagonal are zero).
        /// </summary>
        public bool IsTriangular
        {
            get
            {
                return (IsLowerTriangular || IsUpperTriangular);
            }
        }

        /// <summary>
        /// Gets a value that indicates whether the matrix is upper triangular
        /// (the square matrix where the entries below the main diagonal are zero).
        /// </summary>
        public bool IsUpperTriangular
        {
            get
            {
                return (IsSquare && IsUpperTrapeze);
            }
        }

        /// <summary>
        /// Gets a value that indicates whether the matrix is lower triangular
        /// (the square matrix where the entries above the main diagonal are zero).
        /// </summary>
        public bool IsLowerTriangular
        {
            get
            {
                return (IsSquare && IsLowerTrapeze);
            }
        }

        /// <summary>
        /// Gets a value that indicates whether the matrix is upper trapeze
        /// (the matrix where the entries below the main diagonal are zero).
        /// </summary>
        public bool IsUpperTrapeze
        {
            get
            {
                for (int j = 0; j < ColumnCount; j++)
                    for (int i = j + 1; i < RowCount; i++)
                        if (this[i, j] != Complex.Zero) return false;

                return true;
            }
        }

        /// <summary>
        /// Gets a value that indicates whether the matrix is lower trapeze
        /// (the matrix where the entries above the main diagonal are zero).
        /// </summary>
        public bool IsLowerTrapeze
        {
            get
            {
                for (int i = 0; i < RowCount; i++)
                    for (int j = i + 1; j < ColumnCount; j++)
                        if (this[i, j] != Complex.Zero) return false;

                return true;
            }
        }

        /// <summary>
        /// Gets a value that indicates whether the matrix row or column consisting of zeros.
        /// </summary>
        public bool HasZeroRowOrColumn
        {
            get
            {
                for (int i = 0; i < RowCount; i++)
                    if (AbsRowSum(i) == 0) return true;

                for (int i = 0; i < ColumnCount; i++)
                    if (AbsColumnSum(i) == 0) return true;

                return false;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a complex matrix from another instance of a complex matrix.
        /// </summary>
        /// <param name="obj">A complex matrix.</param>
        public CMatrix(CMatrix obj)
        {
            _m = (Complex[,])obj._m.Clone();
        }

        /// <summary>
        /// Initializes a complex matrix from a two-dimensional complex array.
        /// </summary>
        /// <param name="m">Two-dimensional complex array.</param>
        public CMatrix(Complex[,] m)
        {
            _m = (Complex[,])m.Clone();
        }

        /// <summary>
        /// Initializes a complex matrix from a two-dimensional real array.
        /// </summary>
        /// <param name="m">Two-dimensional real array.</param>
        public CMatrix(double[,] m)
        {
            _m = new Complex[m.GetLength(0), m.GetLength(1)];

            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < ColumnCount; j++)
                    this[i, j] = m[i, j];
            }
        }

        /// <summary>
        /// Initializes a complex matrix.
        /// </summary>
        /// <param name="rows">The number of rows.</param>
        /// <param name="columns">The number of columns.</param>
        public CMatrix(int rows, int columns)
        {
            _m = new Complex[rows, columns];
        }

        /// <summary>
        /// Initializes a complex column vector.
        /// </summary>
        /// <param name="n">The number of elements.</param>
        public CMatrix(int n)
        {
            _m = new Complex[n, 1];
        }

        #endregion

        #region Methods

        #region Statics

        /// <summary>
        /// Returns the identity matrix of the specified size.
        /// </summary>
        /// <param name="n">A number of rows and columns.</param>
        /// <returns>The n by n identity matrix.</returns>
        public static CMatrix Identity(int n)
        {
            CMatrix m = new CMatrix(n, n);

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (i == j)
                        m[i, j] = Complex.One;
                }
            }

            return m;
        }

        /// <summary>
        /// Returns a column vector containing the main diagonal
        /// of the complex square matrix, or
        /// returns a complex square matrix containing on its main diagonal
        /// the elements of the column vector.
        /// </summary>
        /// <param name="m">A complex square matrix or column vector.</param>
        /// <returns>
        /// A column vector containing the main diagonal of the m,
        /// or a complex square matrix containing on its main diagonal the elements of the m.
        /// </returns>
        /// <exception cref="MatrixSizeMismatchException">
        /// The matrix m is not square or not column vector.
        /// </exception>
        public static CMatrix Diagonal(CMatrix m)
        {
            if (m.IsVector)
            {
                CMatrix result = new CMatrix(m.Length, m.Length);

                for (int i = 0; i < result.RowCount; i++)
                    result[i, i] = m[i];

                return result;
            }
            else if (m.IsSquare)
            {
                CMatrix result = new CMatrix(m.RowCount);

                for (int i = 0; i < m.RowCount; i++)
                    result[i] = m[i, i];

                return result;
            }
            else
                throw new MatrixSizeMismatchException("Matrix must be square or column vector.");
        }

        /// <summary>
        /// Returns a complex matrix formed by vertical concatenating of the matrices.
        /// </summary>
        /// <param name="values">An array of complex matrix instances.</param>
        /// <returns>complex matrix formed by vertical concatenating values.</returns>
        /// <exception cref="MatrixSizeMismatchException">
        /// The matrices of the array values have different number of columns.
        /// </exception>
        public static CMatrix StackConcat(params CMatrix[] values)
        {
            int cols = values[0].ColumnCount;
            int rows = 0;
            for (int i = 0; i < values.Length; i++)
                rows += values[i].RowCount;

            CMatrix result = new CMatrix(rows, cols);

            int row_idx = 0;
            for (int idx = 0; idx < values.Length; idx++)
            {
                if (cols != values[idx].ColumnCount)
                    throw new MatrixSizeMismatchException("The matrices have different number of columns.");

                for (int i = 0; i < values[idx].RowCount; i++)
                {
                    for (int j = 0; j < cols; j++)
                        result[row_idx, j] = values[idx][i, j];

                    row_idx++;
                }
            }

            return result;
        }

        /// <summary>
        /// Returns a complex matrix formed by horizontal concatenating of the matrices.
        /// </summary>
        /// <param name="values">An array of complex matrix instances.</param>
        /// <returns>complex matrix formed by horizontal concatenating values.</returns>
        /// <exception cref="MatrixSizeMismatchException">
        /// The matrices of the array values have different number of rows.
        /// </exception>
        public static CMatrix AugmentConcat(params CMatrix[] values)
        {
            int rows = values[0].RowCount;
            int cols = 0;
            for (int i = 0; i < values.Length; i++)
                cols += values[i].ColumnCount;

            CMatrix result = new CMatrix(rows, cols);

            int col_idx = 0;
            for (int idx = 0; idx < values.Length; idx++)
            {
                if (rows != values[idx].RowCount)
                    throw new MatrixSizeMismatchException("The matrices have different number of rows.");

                for (int j = 0; j < values[idx].ColumnCount; j++)
                {
                    for (int i = 0; i < rows; i++)
                        result[i, col_idx] = values[idx][i, j];

                    col_idx++;
                }
            }

            return result;
        }

        /// <summary>
        /// Adds two complex matrices.
        /// </summary>
        /// <param name="m1">A complex matrix (the first term).</param>
        /// <param name="m2">A complex matrix (the second term).</param>
        /// <returns>sum of complex matrices m1 and m2.</returns>
        /// <exception cref="MatrixSizeMismatchException">
        /// The matrix sizes of m1 and m2 do not match.
        /// </exception>
        public static CMatrix Add(CMatrix m1, CMatrix m2)
        {
            if (m1.RowCount != m2.RowCount || m1.ColumnCount != m2.ColumnCount)
                throw new MatrixSizeMismatchException("Matrix sizes do not match.");

            CMatrix m3 = new CMatrix(m1.RowCount, m1.ColumnCount);

            for (int i = 0; i < m1.RowCount; i++)
            {
                for (int j = 0; j < m1.ColumnCount; j++)
                    m3[i, j] = m1[i, j] + m2[i, j];
            }

            return m3;
        }

        /// <summary>
        /// Adds of complex matrix and scalar.
        /// </summary>
        /// <param name="m">A complex matrix (the first term).</param>
        /// <param name="number">A scalar value (the second term).</param>
        /// <returns>sum of m and number.</returns>
        public static CMatrix Add(CMatrix m, Complex number)
        {
            CMatrix result = new CMatrix(m.RowCount, m.ColumnCount);

            for (int i = 0; i < m.RowCount; i++)
            {
                for (int j = 0; j < m.ColumnCount; j++)
                    result[i, j] = m[i, j] + number;
            }

            return result;
        }

        /// <summary>
        /// Subtracts one complex matrix from another.
        /// </summary>
        /// <param name="m1">A complex matrix (the minuend).</param>
        /// <param name="m2">A complex matrix (the subtrahend).</param>
        /// <returns>The result of subtracting m2 from m1.</returns>
        /// <exception cref="MatrixSizeMismatchException">
        /// The matrix sizes of m1 and m2 do not match.
        /// </exception>
        public static CMatrix Subtract(CMatrix m1, CMatrix m2)
        {
            if (m1.RowCount != m2.RowCount || m1.ColumnCount != m2.ColumnCount)
                throw new MatrixSizeMismatchException("Matrix sizes do not match.");

            CMatrix m3 = new CMatrix(m1.RowCount, m1.ColumnCount);

            for (int i = 0; i < m1.RowCount; i++)
            {
                for (int j = 0; j < m1.ColumnCount; j++)
                    m3[i, j] = m1[i, j] - m2[i, j];
            }

            return m3;
        }

        /// <summary>
        /// Subtracts a scalar from a complex matrix.
        /// </summary>
        /// <param name="m">A complex matrix (the minuend).</param>
        /// <param name="number">A scalar value (the subtrahend).</param>
        /// <returns>The result of subtracting number from m.</returns>
        public static CMatrix Subtract(CMatrix m, Complex number)
        {
            CMatrix result = new CMatrix(m.RowCount, m.ColumnCount);

            for (int i = 0; i < m.RowCount; i++)
            {
                for (int j = 0; j < m.ColumnCount; j++)
                    result[i, j] = m[i, j] - number;
            }

            return result;
        }

        /// <summary>
        /// Subtracts a complex matrix from a scalar.
        /// </summary>
        /// <param name="number">A scalar value (the minuend).</param>
        /// <param name="m">A complex matrix (the subtrahend).</param>
        /// <returns>The result of subtracting m from number.</returns>
        public static CMatrix Subtract(Complex number, CMatrix m)
        {
            CMatrix result = new CMatrix(m.RowCount, m.ColumnCount);

            for (int i = 0; i < m.RowCount; i++)
            {
                for (int j = 0; j < m.ColumnCount; j++)
                    result[i, j] = number - m[i, j];
            }

            return result;
        }

        /// <summary>
        /// Multiplies two complex matrices.
        /// </summary>
        /// <remarks>
        /// The inner dimensions of the matrices must agree;
        /// first matrix must have as many columns as second matrix has rows.
        /// </remarks>
        /// <param name="m1">A complex matrix (the multiplicand).</param>
        /// <param name="m2">A complex matrix (the multiplier).</param>
        /// <returns>The product of two complex matrices m1 and m2.</returns>
        /// <exception cref="MatrixSizeMismatchException">
        /// The number of columns of the matrix m1 is not equal to the number of rows of the matrix m2.
        /// </exception>
        public static CMatrix Multiply(CMatrix m1, CMatrix m2)
        {
            if (m1.ColumnCount != m2.RowCount)
                throw new MatrixSizeMismatchException("The number of columns of the first matrix must equal the number of rows of the second matrix.");

            CMatrix m3 = new CMatrix(m1.RowCount, m2.ColumnCount);

            for (int i = 0; i < m1.RowCount; i++)
            {
                for (int j = 0; j < m2.ColumnCount; j++)
                {
                    for (int k = 0; k < m1.ColumnCount; k++)
                        m3[i, j] += m1[i, k] * m2[k, j];
                }
            }

            return m3;
        }

        /// <summary>
        /// Multiplies a complex matrix by a scalar.
        /// </summary>
        /// <param name="m">A complex matrix (the multiplicand).</param>
        /// <param name="number">A scalar value (the multiplier).</param>
        /// <returns>The product of m and number.</returns>
        public static CMatrix Multiply(CMatrix m, Complex number)
        {
            CMatrix result = new CMatrix(m.RowCount, m.ColumnCount);

            for (int i = 0; i < m.RowCount; i++)
            {
                for (int j = 0; j < m.ColumnCount; j++)
                    result[i, j] = m[i, j] * number;
            }

            return result;
        }

        /// <summary>
        /// Returns the vector cross product of two column vectors.
        /// </summary>
        /// <param name="v1">A three-element column vector (the multiplicand).</param>
        /// <param name="v2">A three-element column vector (the multiplier).</param>
        /// <returns>The vector cross product of v1 and v2.</returns>
        /// <exception cref="MatrixSizeMismatchException">
        /// The matrix v1 or v2 is not a three-element column vector.
        /// </exception>
        public static CMatrix CrossProduct(CMatrix v1, CMatrix v2)
        {
            if (v1.IsVector && v1.Length == 3 && v2.IsVector && v2.Length == 3)
            {
                CMatrix m3 = new CMatrix(3);
                m3[0] = v1[1] * v2[2] - v1[2] * v2[1];
                m3[1] = v1[2] * v2[0] - v1[0] * v2[2];
                m3[2] = v1[0] * v2[1] - v1[1] * v2[0];

                return m3;
            }
            else
                throw new MatrixSizeMismatchException("Arguments must be a three-element column vectors.");
        }

        /// <summary>
        /// Returns the vector dot product of two column vectors.
        /// </summary>
        /// <param name="v1">A column vector (the multiplicand).</param>
        /// <param name="v2">A column vector (the multiplier).</param>
        /// <returns>The vector dot product of v1 and v2.</returns>
        /// <exception cref="MatrixSizeMismatchException">
        /// The matrix v1 or v2 is not a vector, or have unequal length.
        /// </exception>
        public static Complex DotProduct(CMatrix v1, CMatrix v2)
        {
            if (!v1.IsVector || !v2.IsVector)
                throw new MatrixSizeMismatchException("Arguments need to be vectors.");

            if (v1.Length != v2.Length)
                throw new MatrixSizeMismatchException("Vectors must be of the same length.");

            Complex result = Complex.Zero;

            for (int i = 0; i < v1.Length; i++)
                result += v1[i] * v2[i];

            return result;

        }

        /// <summary>
        /// Divides two complex matrices.
        /// </summary>
        /// <param name="m1">A complex matrix (the dividend).</param>
        /// <param name="m2">A complex matrix (the divisor).</param>
        /// <returns>The result of dividing m1 by m2.</returns>
        /// <exception cref="MatrixSizeMismatchException">
        /// The matrix m2 is not square or the number of columns of the matrix m1
        /// is not equal to the number of rows of the matrix m2.
        /// </exception>
        /// <exception cref="System.DivideByZeroException">The matrix m2 is singular.</exception>
        public static CMatrix Divide(CMatrix m1, CMatrix m2)
        {
            return m1 * Inverse(m2);
        }

        /// <summary>
        /// Divides a complex matrix by a scalar.
        /// </summary>
        /// <param name="m">A complex matrix (the dividend).</param>
        /// <param name="number">A scalar value (the divisor).</param>
        /// <returns>The result of dividing m by number.</returns>
        public static CMatrix Divide(CMatrix m, Complex number)
        {
            CMatrix result = new CMatrix(m.RowCount, m.ColumnCount);

            for (int i = 0; i < m.RowCount; i++)
            {
                for (int j = 0; j < m.ColumnCount; j++)
                    result[i, j] = m[i, j] / number;
            }

            return result;
        }

        /// <summary>
        /// Divides a scalar by a complex matrix.
        /// </summary>
        /// <param name="number">A scalar value (the dividend).</param>
        /// <param name="m">A complex matrix (the divisor).</param>
        /// <returns>The result of dividing number by m.</returns>
        public static CMatrix Divide(Complex number, CMatrix m)
        {
            if (m.IsSquare)
            {
                return number * CMatrix.Inverse(m);
            }
            else
            {
                CMatrix result = new CMatrix(m.RowCount, m.ColumnCount);

                for (int i = 0; i < m.RowCount; i++)
                {
                    for (int j = 0; j < m.ColumnCount; j++)
                        result[i, j] = number / m[i, j];
                }

                return result;
            }
        }

        /// <summary>
        /// Returns a complex square matrix or column vector raised to integer power.
        /// </summary>
        /// <param name="m">A complex square matrix or column vector.</param>
        /// <param name="degree">An integer number that specifies a power.</param>
        /// <returns>The m raised to the power degree.</returns>
        /// <exception cref="MatrixSizeMismatchException">The matrix m is not square or not column vector.</exception>
        public static CMatrix Pow(CMatrix m, int degree)
        {
            if (m.IsSquare)
            {
                if (degree == 0)
                    return Identity(m.RowCount);

                if (degree < 0)
                {
                    degree = -degree;
                    m = Inverse(m);
                }

                CMatrix result = Identity(m.RowCount);

                while (degree != 0)
                {
                    if ((degree & 1) != 0)
                        result *= m;

                    m *= m;
                    degree >>= 1;
                }

                return result;
            }
            else if (m.IsVector)
            {
                CMatrix result = new CMatrix(m.Length);

                for (int i = 0; i < m.Length; i++)
                    result[i] = Complex.Pow(m[i], degree);

                return result;
            }
            else
            {
                throw new MatrixSizeMismatchException("The matrix must be square or column vector.");
            }
        }

        /// <summary>
        /// Returns a column vector raised to complex power.
        /// </summary>
        /// <param name="v">A column vector.</param>
        /// <param name="degree">A complex number that specifies a power.</param>
        /// <returns>The v raised to the power degree.</returns>
        /// <exception cref="MatrixSizeMismatchException">The matrix v is not column vector.</exception>
        public static CMatrix Pow(CMatrix v, Complex degree)
        {
            if (!v.IsVector)
                throw new MatrixSizeMismatchException("The matrix must be column vector.");

            CMatrix result = new CMatrix(v.Length);

            for (int i = 0; i < v.Length; i++)
                result[i] = Complex.Pow(v[i], degree);

            return result;
        }

        /// <summary>
        /// Returns the square root of a complex square matrix.
        /// </summary>
        /// <param name="m">A complex square matrix.</param>
        /// <returns>The square root of m.</returns>
        /// <exception cref="MatrixSizeMismatchException">The matrix m is not square.</exception>
        public static CMatrix Sqrt(CMatrix m)
        {
            if (!m.IsSquare)
                throw new MatrixSizeMismatchException("The matrix must be square.");

            // Calculate the eigenvalues and eigenvectors of matrix
            CEigenproblem eigen = new CEigenproblem(m, true);

            CMatrix D = eigen.D;
            for (int i = 0; i < D.RowCount; i++)
                D[i, i] = Complex.Sqrt(D[i, i]);

            CMatrix eigvecs = eigen.Eigenvectors;

            return eigvecs * D * Inverse(eigvecs);
        }

        /// <summary>
        /// Returns the exponential of a complex square matrix.
        /// </summary>
        /// <param name="m">A complex square matrix.</param>
        /// <returns>The exponential of m.</returns>
        /// <exception cref="MatrixSizeMismatchException">The matrix m is not square.</exception>
        public static CMatrix Exp(CMatrix m)
        {
            if (!m.IsSquare)
                throw new MatrixSizeMismatchException("The matrix must be square.");

            // Calculate the eigenvalues and eigenvectors of matrix
            CEigenproblem eigen = new CEigenproblem(m, true);

            CMatrix D = eigen.D;
            for (int i = 0; i < D.RowCount; i++)
                D[i, i] = Complex.Exp(D[i, i]);

            CMatrix eigvecs = eigen.Eigenvectors;

            return eigvecs * D * Inverse(eigvecs);
        }

        /// <summary>
        /// Returns the result of multiplying the complex matrix by negative one.
        /// </summary>
        /// <param name="m">A complex matrix.</param>
        /// <returns>The result of m multiplied by negative one.</returns>
        public static CMatrix Negate(CMatrix m)
        {
            CMatrix result = new CMatrix(m.RowCount, m.ColumnCount);

            for (int i = 0; i < m.RowCount; i++)
            {
                for (int j = 0; j < m.ColumnCount; j++)
                    result[i, j] = Complex.Negate(m[i, j]);
            }

            return result;
        }

        /// <summary>
        /// Returns the inverse matrix for a regular matrix.
        /// </summary>
        /// <param name="m">A complex square regular matrix.</param>
        /// <returns>Inverse matrix for m.</returns>
        /// <exception cref="MatrixSizeMismatchException">The matrix m is not square.</exception>
        /// <exception cref="System.DivideByZeroException">The matrix m is singular.</exception>
        public static CMatrix Inverse(CMatrix m)
        {
            if (!m.IsSquare)
                throw new MatrixSizeMismatchException("Cannot invert non-square matrix.");

            if (m.IsReal && m.IsOrthogonal())
                return m.Transpose;
            else if (m.IsUnitary())
                return m.Adjoint;

            int n = m.ColumnCount;

            if (m.IsDiagonal)
            {
                CMatrix d = Diagonal(m);

                for (int i = 0; i < n; i++)
                    d[i] = 1 / d[i];

                return Diagonal(d);
            }

            CMatrix[] LUP = LUPDecomposition(m);

            CMatrix P = LUP[0];

            CMatrix C = new CMatrix(n, n);
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (i > j) C[i, j] = LUP[1][i, j];
                    else C[i, j] = LUP[2][i, j];
                }
            }

            CMatrix X = new CMatrix(n, n);

            for(int k = n - 1; k >= 0; k--)
            {
                X[k, k] = Complex.One;

                for(int j = n - 1; j > k; j--)
                    X[k, k] -= C[k, j] * X[j, k];

                X[k, k] /= C[k, k];

                for(int i = k - 1; i >= 0; i--)
                {
                    for(int j = n - 1; j > i; j--)
                    {
                        X[i, k] -= C[i, j] * X[j, k];
                        X[k, i] -= C[j, i] * X[k, j];
                    }
                    X[i, k] /= C[i, i];
                }
            }

            return X * P;
        }

        /// <summary>
        /// Returns the Moore-Penrose inverse (pseudoinverse) of the matrix.
        /// </summary>
        /// <param name="m">A complex square matrix.</param>
        /// <returns>Pseudoinverse matrix for m.</returns>
        public static CMatrix PseudoInverse(CMatrix m)
        {
            return new CSVD(m).PseudoInverse();
        }

        /// <summary>
        /// Returns the solution of the linear system.
        /// </summary>
        /// <param name="a">A complex square matrix.</param>
        /// <param name="b">A complex column vector.</param>
        /// <returns>The column vector which is a solution of the equation a * x = b.</returns>
        /// <exception cref="MatrixSizeMismatchException">
        /// The matrix a is not square or the matrix b is not column vector
        /// or the length of column vector b is not equal to the size of matrix a.
        /// </exception>
        /// <exception cref="System.DivideByZeroException">The matrix a is singular.</exception>
        public static CMatrix Solve(CMatrix a, CMatrix b)
        {
            if (!a.IsSquare)
                throw new MatrixSizeMismatchException("Cannot uniquely solve non-square equation system.");

            CMatrix[] LUP = LUPDecomposition(a);
            int n = b.Length;

            // Solve Ly = Pb for y using forward substitution.
            CMatrix L = LUP[1];
            CMatrix y = LUP[0] * b;

            for (int j = 0; j < n - 1; j++)
            {
                y[j] /= L[j, j];

                for (int i = 1; i < n - j; i++)
                    y[j + i] -= y[j] * L[j + i, j];
            }

            y[n - 1] /= L[n - 1, n - 1];

            // Solve Ux = y for x using back substitution.
            CMatrix U = LUP[2];
            CMatrix x = new CMatrix(y);

            for (int j = n - 1; j > 0; j--)
            {
                x[j] /= U[j, j];

                for (int i = 0; i <= j - 1; i++)
                    x[i] -= x[j] * U[i, j];
            }

            x[0] /= U[0, 0];

            return x;
        }

        /// <summary>
        /// Returns trace of the complex square matrix.
        /// </summary>
        /// <param name="m">A complex square matrix.</param>
        /// <returns>The sum of diagonal elements of m.</returns>
        /// <exception cref="MatrixSizeMismatchException">The matrix m is not square.</exception>
        public static Complex Trace(CMatrix m)
        {
            if (!m.IsSquare)
                throw new MatrixSizeMismatchException("The matrix must be square.");

            Complex sum = Complex.Zero;

            for (int i = 0; i < m.RowCount; i++)
                sum += m[i, i];

            return sum;
        }

        /// <summary>
        /// Returns determinant of the complex square matrix.
        /// </summary>
        /// <param name="m">A complex square matrix.</param>
        /// <returns>The determinant of m.</returns>
        /// <exception cref="MatrixSizeMismatchException">The matrix m is not square.</exception>
        public static Complex Determ(CMatrix m)
        {
            if (!m.IsSquare)
                throw new MatrixSizeMismatchException("Cannot calc determinant of non-square matrix.");
            else if (m.ColumnCount == 1)
                return m[0, 0];
            else if (m.IsTriangular)
            {
                return m.DiagProduct();
            }
            else
            {
                try
                {
                    CMatrix[] LUP = LUPDecomposition(m);
                    return SignPermut(LUP[0]) * LUP[2].DiagProduct();
                }
                catch (DivideByZeroException)
                {
                    return Complex.Zero;
                }
            }
        }

        /// <summary>
        /// Returns the number of linearly independent rows.
        /// </summary>
        /// <param name="m">A complex matrix.</param>
        /// <returns>The number of linearly independent rows in m.</returns>
        public static int Rank(CMatrix m)
        {
            m = new CMatrix(m);

            int i = 0;
            int j = 0;

            while (i < m.RowCount && j < m.ColumnCount)
            {
                // Invariant: matrix minor in the columns 0..j-1
                // have been reduced to a stepped form, and the row
                // with the index i-1 contains a nonzero element
                // in the column with the number, smaller than j.

                // Search pivot element in the j-th column, starting with the i-th row
                int pivot = 0;
                double pivotValue = 0;

                for (int k = i; k < m.RowCount; k++)
                {
                    if (Complex.Abs(m[k, j]) > pivotValue)
                    {
                        pivot = k;
                        pivotValue = Complex.Abs(m[k, j]);
                    }
                }

                if (pivotValue <= 10E-12)
                {
                    for (int k = i; k < m.RowCount; k++)
                    {
                        m[k, j] = Complex.Zero;
                    }
                    ++j;      // Increase the column index
                    continue;
                }

                if (pivot != i)
                {
                    // Swap i-th and pivot-th row
                    for (int k = j; k < m.ColumnCount; k++)
                    {
                        Complex temp = m[i, k];
                        m[i, k] = m[pivot, k];
                        m[pivot, k] = -temp; // Changes sign row
                    }
                }

                // Assertion: Complex.Abs(m[i, k]) > eps

                // Nulling the j-th column, starting with the row i+1,
                // applying the elementary transformations of the second kind.
                for (int k = i + 1; k < m.RowCount; k++)
                {
                    Complex temp = -m[k, j] / m[i, j];

                    m[k, j] = Complex.Zero;
                    for (int l = j + 1; l < m.ColumnCount; l++)
                    {
                        m[k, l] += temp * m[i, l];
                    }
                }

                // Go to the next Minor
                ++i; ++j;
            }

            // Return the number of nonzero rows
            return i;
        }

        /// <summary>
        /// Returns the sign of the permutation matrix.
        /// (Which is 1 for an even number of swaps
        /// and -1 for an odd number of swaps).
        /// </summary>
        /// <param name="m">A permutation matrix.</param>
        /// <returns>The sign of the permutation matrix m.</returns>
        /// <exception cref="System.ArgumentException">The matrix m is not a permutation.</exception>
        public static int SignPermut(CMatrix m)
        {
            if (!m.IsPermutation)
                throw new ArgumentException("The matrix must be permutation.", "m");

            m = new CMatrix(m);
            int sgn = 1;

            for (int i = 0; i < m.RowCount - 1; i++)
            {
                if (m[i, i] != Complex.One)
                {
                    sgn *= -1;

                    int j = 0;
                    for (j = i + 1; j < m.RowCount && m[j, i] != Complex.One; j++) ;

                    m.SwapRows(i, j);
                }
            }

            return sgn;
        }

        /// <summary>
        /// Returns a vector whose elements are the eigenvalues of a complex square matrix.
        /// </summary>
        /// <param name="m">A complex square matrix.</param>
        /// <returns>The column vector whose elements are the roots of the characteristic equation.</returns>
        /// <exception cref="MatrixSizeMismatchException">The matrix m is not square.</exception>
        public static CMatrix Eigenvalues(CMatrix m)
        {
            CEigenproblem eigen = new CEigenproblem(m, false);
            return eigen.Eigenvalues;
        }

        /// <summary>
        /// Returns a matrix containing all normalized eigenvectors of the matrix.
        /// </summary>
        /// <param name="m">A complex square matrix.</param>
        /// <returns>The matrix containing all normalized eigenvectors of m.</returns>
        /// <exception cref="MatrixSizeMismatchException">The matrix m is not square.</exception>
        public static CMatrix Eigenvectors(CMatrix m)
        {
            CEigenproblem eigen = new CEigenproblem(m, true);
            return eigen.Eigenvectors;
        }

        /// <summary>
        /// Returns a vector whose elements are the singular values of a complex matrix.
        /// </summary>
        /// <param name="m">A complex matrix.</param>
        /// <returns>A vector whose elements are the singular values of m.</returns>
        public static CMatrix Singularvalues(CMatrix m)
        {
            return new CSVD(m).SingularValues;
        }

        /// <summary>
        /// Returns the characteristic polynomial of a complex square matrix.
        /// </summary>
        /// <param name="m">A complex square matrix.</param>
        /// <returns>The characteristic polynomial of the matrix m.</returns>
        /// <exception cref="MatrixSizeMismatchException">The matrix m is not square.</exception>
        public static CPolynomial CharacteristicPolynomial(CMatrix m)
        {
            if (!m.IsSquare)
                throw new MatrixSizeMismatchException("The matrix must be square.");

            int n = m.RowCount;

            CMatrix eig = Eigenvalues(m);

            Complex[] roots = new Complex[n];
            for (int i = 0; i < n; i++)
                roots[i] = eig[i];

            return CPolynomial.FromRoots(roots);
        }

        /// <summary>
        /// Returns Cholesky decomposition for hermitian positive definite matrix.
        /// </summary>
        /// <param name="m">A hermitian positive definite matrix.</param>
        /// <returns>The lower triangular matrix L.</returns>
        /// <exception cref="System.ArgumentException">The matrix m is not hermitian matrix.</exception>
        /// <exception cref="System.ArithmeticException">The matrix m is not positive definite.</exception>
        public static CMatrix CholeskyDecomposition(CMatrix m)
        {
            if (!m.IsHermitian())
                throw new ArgumentException("The matrix must be hermitian.");

            CMatrix L = new CMatrix(m.RowCount, m.ColumnCount);

            for (int i = 0; i < m.RowCount; i++)
            {
                for (int j = 0; j <= i; j++)
                {
                    Complex sum = 0;

                    for (int k = 0; k < j; k++)
                        sum += L[i, k] * Complex.Conjugate(L[j, k]);

                    if (i == j)
                        L[i, i] = Complex.Sqrt(m[i, i] - sum);
                    else
                        L[i, j] = 1.0 / L[j, j] * (m[i, j] - sum);
                }

                if (L[i, i].Re <= 0 || L[i, i].Im != 0)
                    throw new ArithmeticException("The matrix is not positive definite.");
            }

            return L;
        }

        /// <summary>
        /// Returns LU-decomposition with column pivoting for the complex square matrix.
        /// </summary>
        /// <param name="m">A complex square matrix.</param>
        /// <returns>The permutation matrix P, the lower and upper triangular matrices L, U.</returns>
        /// <exception cref="MatrixSizeMismatchException">The matrix m is not square.</exception>
        /// <exception cref="System.DivideByZeroException">The matrix m is singular.</exception>
        public static CMatrix[] LUPDecomposition(CMatrix m)
        {
            if (!m.IsSquare)
                throw new MatrixSizeMismatchException("Cannot perform LUP-decomposition of non-square matrix.");

            int n = m.RowCount;

            CMatrix c = new CMatrix(m);
            CMatrix p = Identity(n);

            for (int i = 0; i < n; i++)
            {
                // Search pivot
                double pivotValue = 0;
                int pivot = -1;
                for (int row = i; row < n; row++)
                {
                    if (Complex.Abs(c[row, i]) > pivotValue)
                    {
                        pivotValue = Complex.Abs(c[row, i]);
                        pivot = row;
                    }
                }
                if (pivotValue == 0)
                {
                    throw new DivideByZeroException("The matrix is singular.");
                }

                // 	Swap the i-th row
                p.SwapRows(pivot, i);
                c.SwapRows(pivot, i);

                for (int j = i + 1; j < n; j++)
                {
                    c[j, i] /= c[i, i];
                    for (int k = i + 1; k < n; k++)
                        c[j, k] -= c[j, i] * c[i, k];
                }
            }

            CMatrix l = new CMatrix(n, n);

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j <= i; j++)
                {
                    if (i == j) l[i, j] = 1;
                    else l[i, j] = c[i, j];
                }
            }

            CMatrix u = c.ExtractUpperTrapeze();

            return new CMatrix[] { p, l, u };
        }

        /// <summary>
        /// Returns QR-decomposition for the complex matrix.
        /// </summary>
        /// <param name="matrix">A complex matrix.</param>
        /// <returns>The unitary matrix Q and upper triangular matrix R.</returns>
        /// <exception cref="MatrixSizeMismatchException">The rows less than column.</exception>
        public static CMatrix[] QRDecomposition(CMatrix matrix)
        {
            int m = matrix.RowCount;
            int n = matrix.ColumnCount;

            if (m < n)
                throw new MatrixSizeMismatchException("The number of rows must be greater than or equal to the number of columns.");

            CMatrix a = new CMatrix(matrix);

            // Unitary matrix
            CMatrix q = Identity(m);

            int t = Math.Min(m - 1, n);

            for (int k = 0; k < t; k++)
            {
                CMatrix x = a.Submatrix(k, m - 1, k, k);
                CMatrix e = new CMatrix(x.Length); e[0] = 1;
                int sgn = (x[0].Re >= 0) ? 1 : -1;
                CMatrix u = x + sgn * FrobeniusNorm(x) * e;

                double norm = PNorm(u, 2);
                if (norm <= 10E-12) continue;

                CMatrix v = u / norm;

                Complex w = DotProduct(x.Conjugate, v) / DotProduct(v.Conjugate, x);

                CMatrix qi = Identity(m);

                for (int i = 0; i < v.Length; i++)
                {
                    for (int j = 0; j < v.Length; j++)
                    {
                        if (i == j) qi[k + i, k + j] = 1 - (1 + w) * v[i] * Complex.Conjugate(v[j]);
                        else qi[k + i, k + j] = -(1 + w) * v[i] * Complex.Conjugate(v[j]);
                    }
                }

                a = qi * a;
                q = q * qi.Adjoint;
            }

            // Upper triangular matrix (r = q^H * A)
            CMatrix r = new CMatrix(m, n);

            for (int i = 0; i < n; i++)
                for (int j = i; j < n; j++)
                    for (int k = 0; k < m; k++)
                        r[i, j] += Complex.ConjugateMultiply(q[k, i], matrix[k, j]);

            return new CMatrix[2] { q, r };
        }

        /// <summary>
        /// Returns Singular value decomposition for the complex matrix.
        /// </summary>
        /// <param name="m">A complex matrix.</param>
        /// <returns>
        /// The unitary matrix U, the diagonal matrix Σ with nonnegative real numbers on the diagonal
        /// and the unitary matrix V* denotes the conjugate transpose of V.
        /// </returns>
        public static CMatrix[] SingularValueDecomposition(CMatrix m)
        {
            CSVD svd = new CSVD(m);
            return new CMatrix[]{ svd.U, svd.S, svd.VH };
        }

        /// <summary>
        /// Returns the maximum absolute column sum norm
        /// (also known as taxi norm).
        /// </summary>
        /// <param name="m">A complex matrix or column vector.</param>
        /// <returns>The maximum absolute column sum norm of m.</returns>
        public static double OneNorm(CMatrix m)
        {
            double norm = 0;

            if (m.IsVector)
            {
                for (int j = 0; j < m.Length; j++)
                    norm += Complex.Abs(m[j]);
            }
            else
            {
                for (int j = 0; j < m.ColumnCount; j++)
                {
                    if (norm < m.AbsColumnSum(j))
                        norm = m.AbsColumnSum(j);
                }
            }

            return norm;
        }

        /// <summary>
        /// Returns the maximum absolute row sum norm
        /// (also known as max norm).
        /// </summary>
        /// <param name="m">A complex matrix or column vector.</param>
        /// <returns>The maximum absolute row sum norm of m.</returns>
        public static double InfinityNorm(CMatrix m)
        {
            double norm = 0;

            if (m.IsVector)
            {
                double max = 0;

                for (int i = 0; i < m.Length; i++)
                {
                    if (max < Complex.Abs(m[i]))
                        max = Complex.Abs(m[i]);
                }
                norm = max;
            }
            else
            {
                for (int i = 0; i < m.RowCount; i++)
                {
                    if (norm < m.AbsRowSum(i))
                        norm = m.AbsRowSum(i);
                }
            }

            return norm;
        }

        /// <summary>
        /// Returns the Frobenius norm of a complex matrix.
        /// </summary>
        /// <param name="m">A complex matrix.</param>
        /// <returns>The Frobenius norm of m.</returns>
        public static double FrobeniusNorm(CMatrix m)
        {
            double norm = 0;

            for (int i = 0; i < m.RowCount; i++)
            {
                for (int j = 0; j < m.ColumnCount; j++)
                {
                    norm += Complex.AbsSquared(m[i, j]);
                }
            }

            return Math.Sqrt(norm);
        }

        /// <summary>
        /// Returns the p-norm of a complex matrix.
        /// </summary>
        /// <param name="m">A complex matrix.</param>
        /// <param name="p">A real number.</param>
        /// <returns>
        /// The p-th root of the sum of the p-th powers
        /// of the absolute values of all elements of m.
        /// </returns>
        public static double PNorm(CMatrix m, double p)
        {
            if (p <= 0)
                throw new ArgumentException("Argument must be greater than zero.", "p");

            if (p == 1)
                return OneNorm(m);
            else if (Double.IsPositiveInfinity(p))
                return InfinityNorm(m);

            double norm = 0;

            for (int i = 0; i < m.RowCount; i++)
            {
                for (int j = 0; j < m.ColumnCount; j++)
                {
                    norm += Math.Pow(Complex.Abs(m[i, j]), p);
                }
            }
            return Math.Pow(norm, 1.0 / p);
        }

        /// <summary>
        /// Returns the determinant of some smaller square matrix,
        /// cut down from the matrix by removing one or more of its rows or columns.
        /// </summary>
        /// <param name="m">A complex square matrix.</param>
        /// <param name="row">An index row, which should be removed.</param>
        /// <param name="col">An index column, which should be removed.</param>
        /// <returns>
        /// determinant of some smaller square matrix,
        /// cut down from the m by removing one or more of its rows or columns.
        /// </returns>
        /// <exception cref="MatrixSizeMismatchException">The matrix m is not square.</exception>
        public static Complex Minor(CMatrix m, int row, int col)
        {
            if (!m.IsSquare)
                throw new MatrixSizeMismatchException("The matrix must be square.");

            CMatrix minor = new CMatrix(m.RowCount - 1, m.RowCount - 1);

            int row_idx = 0;
            int col_idx = 0;

            for (int i = 0; i < m.RowCount; i++)
            {
                col_idx = 0;

                for (int j = 0; j < m.ColumnCount; j++)
                {
                    if (i != row && j != col)
                    {
                        minor[row_idx, col_idx] = m[i, j];
                        col_idx++;
                    }
                }

                if (i != row) row_idx++;
            }

            return Determ(minor);
        }

        /// <summary>
        /// Returns the signed minor of the complex square matrix.
        /// </summary>
        /// <param name="m">A complex square matrix.</param>
        /// <param name="row">An index row, which should be removed.</param>
        /// <param name="col">An index column, which should be removed.</param>
        /// <returns>signed minor of m.</returns>
        /// <exception cref="MatrixSizeMismatchException">The matrix m is not square.</exception>
        public static Complex Cofactor(CMatrix m, int row, int col)
        {
            return Math.Pow(-1, row + col + 2) * Minor(m, row, col);
        }

        /// <summary>
        /// Returns the value indicating whether two instances of complex matrix are equal.
        /// </summary>
        /// <param name="m1">The first complex matrix to compare.</param>
        /// <param name="m2">The second complex matrix to compare.</param>
        /// <returns>True if the m1 and m2 parameters have the same value; otherwise, false.</returns>
        public static bool Equals(CMatrix m1, CMatrix m2)
        {
            if ((m1.RowCount != m2.RowCount) || (m1.ColumnCount != m2.ColumnCount))
                return false;

            for (int i = 0; i < m1.RowCount; i++)
            {
                for (int j = 0; j < m1.ColumnCount; j++)
                {
                    if (m1[i, j] != m2[i, j])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Returns the value indicating whether two instances
        /// of complex matrix are equal with the specified relative tolerance.
        /// </summary>
        /// <param name="m1">The first complex matrix to compare.</param>
        /// <param name="m2">The second complex matrix to compare.</param>
        /// <param name="TOL">The tolerance.</param>
        /// <returns>True if the m1 and m2 parameters have the same value with tolerance TOL; otherwise, false.</returns>
        public static bool FuzzyEquals(CMatrix m1, CMatrix m2, double relativeTolerance)
        {
            if ((m1.RowCount != m2.RowCount) || (m1.ColumnCount != m2.ColumnCount))
                return false;

            for (int i = 0; i < m1.RowCount; i++)
            {
                for (int j = 0; j < m1.ColumnCount; j++)
                {
                    if (!NumericUtil.FuzzyEquals(m1[i, j], m2[i, j], relativeTolerance))
                        return false;
                }
            }
            return true;
        }

        #endregion

        #region Dynamics

        /// <summary>
        /// Returns the specified row of the matrix.
        /// </summary>
        /// <param name="index">A row index.</param>
        /// <returns>The row with the specified index of this matrix.</returns>
        /// <exception cref="System.IndexOutOfRangeException">The index exceed matrix size.</exception>
        public CMatrix GetRow(int index)
        {
            if (index < 0 || index >= RowCount)
                throw new IndexOutOfRangeException("The index exceed matrix size.");

            CMatrix matrix = new CMatrix(1, ColumnCount);

            for (int i = 0; i < ColumnCount; i++)
                matrix[0, i] = _m[index, i];

            return matrix;
        }

        /// <summary>
        /// Returns the specified column of the matrix.
        /// </summary>
        /// <param name="index">A column index.</param>
        /// <returns>The column with the specified index of this matrix.</returns>
        /// <exception cref="System.IndexOutOfRangeException">The index exceed matrix size.</exception>
        public CMatrix GetColumn(int index)
        {
            if (index < 0 || index >= ColumnCount)
                throw new IndexOutOfRangeException("The index exceed matrix size.");

            CMatrix matrix = new CMatrix(RowCount, 1);

            for (int i = 0; i < RowCount; i++)
                matrix[i] = _m[i, index];

            return matrix;
        }

        /// <summary>
        /// Sets all elements of the specified row of
        /// the matrix using the elements of row vector.
        /// </summary>
        /// <param name="index">A row index.</param>
        /// <param name="row">A row vector.</param>
        /// <exception cref="System.IndexOutOfRangeException">The index exceed matrix size.</exception>
        /// <exception cref="MatrixSizeMismatchException">
        /// The matrix row is not row vector or
        /// its length is not equal to the number of columns of the matrix.
        /// </exception>
        public void SetRow(int index, CMatrix row)
        {
            if (index < 0 || index >= RowCount)
                throw new IndexOutOfRangeException("The index exceed matrix size.");

            if ((row.ColumnCount != ColumnCount) || (row.RowCount != 1))
                throw new MatrixSizeMismatchException("Size of the matrix is not correctly.");

            for (int i = 0; i < ColumnCount; i++)
                this[index, i] = row[0, i];
        }

        /// <summary>
        /// Sets all elements of the specified column of
        /// the matrix using the elements of column vector.
        /// </summary>
        /// <param name="index">A column index.</param>
        /// <param name="column">A column vector.</param>
        /// <exception cref="System.IndexOutOfRangeException">The index exceed matrix size.</exception>
        /// <exception cref="MatrixSizeMismatchException">
        /// The matrix column is not column vector or
        /// its length is not equal to the number of rows of the matrix.
        /// </exception>
        public void SetColumn(int index, CMatrix column)
        {
            if (index < 0 || index >= ColumnCount)
                throw new IndexOutOfRangeException("The index exceed matrix size.");

            if ((column.RowCount != RowCount) || (column.ColumnCount != 1))
                throw new MatrixSizeMismatchException("Size of the matrix is not correctly.");

            for (int i = 0; i < RowCount; i++)
                this[i, index] = column[i];
        }

        /// <summary>
        /// Returns the matrix without the specified one row.
        /// </summary>
        /// <param name="index">A row index.</param>
        /// <returns>The matrix without the one row with the specified index.</returns>
        /// <exception cref="System.IndexOutOfRangeException">The index exceed matrix size.</exception>
        public CMatrix RemoveRow(int index)
        {
            if (index < 0 || index >= RowCount)
                throw new IndexOutOfRangeException("The index exceed matrix size.");

            if (RowCount <= 1)
                throw new ArgumentException();

            CMatrix m = new CMatrix(RowCount - 1, ColumnCount);

            for (int i = 0, row_idx = 0; i < RowCount; i++)
            {
                if (i != index)
                {
                    for (int j = 0; j < ColumnCount; j++)
                    {
                        m[row_idx, j] = this[i, j];
                    }

                    row_idx++;
                }
            }

            return m;
        }

        /// <summary>
        /// Returns the matrix without the specified one column.
        /// </summary>
        /// <param name="index">A column index.</param>
        /// <returns>matrix without the one column with the specified index.</returns>
        /// <exception cref="System.IndexOutOfRangeException">The index exceed matrix size.</exception>
        public CMatrix RemoveColumn(int index)
        {
            if (index < 0 || index >= ColumnCount)
                throw new IndexOutOfRangeException("The index exceed matrix size.");

            if (ColumnCount <= 1)
                throw new ArgumentException();

            CMatrix m = new CMatrix(RowCount, ColumnCount - 1);

            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0, col_idx = 0; j < ColumnCount; j++)
                {
                    if (j != index)
                    {
                        m[i, col_idx] = this[i, j];
                        col_idx++;
                    }
                }
            }

            return m;
        }

        /// <summary>
        /// Returns a matrix with the row inserted in the specified position.
        /// </summary>
        /// <param name="index">A row index.</param>
        /// <param name="row">A row vector.</param>
        /// <returns>matrix with row inserted in the position index.</returns>
        /// <exception cref="System.IndexOutOfRangeException">The index exceed matrix size.</exception>
        /// /// <exception cref="MatrixSizeMismatchException">
        /// The matrix row is not row vector or
        /// its length is not equal to the number of columns of the matrix.
        /// </exception>
        public CMatrix InsertRow(int index, CMatrix row)
        {
            if (index < 0 || index > RowCount)
                throw new IndexOutOfRangeException("The index exceed matrix size.");

            if ((row.ColumnCount != ColumnCount) || (row.RowCount != 1))
                throw new MatrixSizeMismatchException("Size of the matrix is not correctly.");

            CMatrix m = new CMatrix(RowCount + 1, ColumnCount);

            int row_idx = 0;
            for (int j = 0; j < RowCount + 1; j++)
            {
                if (j != index)
                {
                    for (int k = 0; k < ColumnCount; k++)
                        m[j, k] = _m[row_idx, k];

                    row_idx++;
                }
                else
                {
                    for (int k = 0; k < ColumnCount; k++)
                        m[j, k] = row[0, k];
                }
            }

            return m;
        }

        /// <summary>
        /// Returns a matrix with the column inserted in the specified position.
        /// </summary>
        /// <param name="index">A column index.</param>
        /// <param name="column">A column vector.</param>
        /// <returns>matrix with column inserted in the position index.</returns>
        /// <exception cref="System.IndexOutOfRangeException">The index exceed matrix size.</exception>
        /// <exception cref="MatrixSizeMismatchException">
        /// The matrix column is not column vector or
        /// its length is not equal to the number of rows of the matrix.
        /// </exception>
        public CMatrix InsertColumn(int index, CMatrix column)
        {
            if (index < 0 || index > ColumnCount)
                throw new IndexOutOfRangeException("The index exceed matrix size.");

            if ((column.RowCount != RowCount) || (column.ColumnCount != 1))
                throw new MatrixSizeMismatchException("Size of the matrix is not correctly.");

            CMatrix m = new CMatrix(RowCount, ColumnCount + 1);

            int col_idx = 0;
            for (int j = 0; j < ColumnCount + 1; j++)
            {
                if (j != index)
                {
                    for (int k = 0; k < RowCount; k++)
                        m[k, j] = _m[k, col_idx];

                    col_idx++;
                }
                else
                {
                    for (int k = 0; k < ColumnCount; k++)
                        m[k, j] = column[k, 0];
                }
            }

            return m;
        }

        /// <summary>
        /// Returns a matrix with spaced rows in the specified order.
        /// </summary>
        /// <param name="pivots">An array of integers containing the row placements.</param>
        /// <returns>The matrix with spaced rows in the pivots order.</returns>
        /// <exception cref="MatrixSizeMismatchException">Array size does not match.</exception>
        /// <exception cref="System.IndexOutOfRangeException">The indices out of range.</exception>
        public CMatrix ReorderRows(int[] pivots)
        {
            if (pivots.Length != RowCount)
                throw new MatrixSizeMismatchException("Array size does not match.");

            CMatrix m = new CMatrix(RowCount, ColumnCount);

            for (int rowIdx = 0; rowIdx < RowCount; rowIdx++)
            {
                for (int colIdx = 0; colIdx < ColumnCount; colIdx++)
                    m[rowIdx, colIdx] = this[pivots[rowIdx], colIdx];
            }

            return m;
        }

        /// <summary>
        /// Returns a matrix with spaced columns in the specified order.
        /// </summary>
        /// <param name="pivots">An array of integers containing the column placements.</param>
        /// <returns>The matrix with spaced columns in the pivots order.</returns>
        /// <exception cref="MatrixSizeMismatchException">Array size does not match.</exception>
        /// <exception cref="System.IndexOutOfRangeException">The indices out of range.</exception>
        public CMatrix ReorderColumns(int[] pivots)
        {
            if (pivots.Length != ColumnCount)
                throw new MatrixSizeMismatchException("Array size does not match.");

            CMatrix m = new CMatrix(RowCount, ColumnCount);

            for (int colIdx = 0; colIdx < ColumnCount; colIdx++)
            {
                for (int rowIdx = 0; rowIdx < RowCount; rowIdx++)
                    m[rowIdx, colIdx] = this[rowIdx, pivots[colIdx]];
            }

            return m;
        }

        /// <summary>
        /// Returns the specified submatrix.
        /// </summary>
        /// <param name="startRow">A start row index.</param>
        /// <param name="endRow">An end row index.</param>
        /// <param name="startCol">A start column index.</param>
        /// <param name="endCol">An end column index.</param>
        /// <returns>The specified submatrix.</returns>
        /// <exception cref="System.IndexOutOfRangeException">The indices out of range.</exception>
        public CMatrix Submatrix(int startRow, int endRow, int startCol, int endCol)
        {
            if (startRow < 0 || endRow < 0 || startCol < 0 || endCol < 0 ||
                startRow >= RowCount || endRow >= RowCount || startCol >= ColumnCount || endCol >= ColumnCount)
                throw new IndexOutOfRangeException("The indices out of range.");

            CMatrix result = new CMatrix(Math.Abs(endRow - startRow) + 1, Math.Abs(endCol - startCol) + 1);

            int signr = (endRow - startRow >= 0) ? 1 : -1;
            int signc = (endCol - startCol >= 0) ? 1 : -1;

            for (int i = 0; i < result.RowCount; i++)
            {
                for (int j = 0; j < result.ColumnCount; j++)
                {
                    result[i, j] = this[startRow + signr * i, startCol + signc * j];
                }
            }

            return result;
        }

        /// <summary>
        /// Returns lower trapeze matrix of the complex matrix.
        /// </summary>
        /// <returns>The lower trapeze matrix of this complex matrix.</returns>
        public CMatrix ExtractLowerTrapeze()
        {
            CMatrix result = new CMatrix(RowCount, ColumnCount);

            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j <= i; j++)
                {
                    result[i, j] = this[i, j];
                }
            }

            return result;
        }

        /// <summary>
        /// Returns upper trapeze matrix of the complex matrix.
        /// </summary>
        /// <returns>The upper trapeze matrix of this complex matrix.</returns>
        public CMatrix ExtractUpperTrapeze()
        {
            CMatrix result = new CMatrix(RowCount, ColumnCount);

            for (int i = 0; i < RowCount; i++)
            {
                for (int j = i; j < ColumnCount; j++)
                {
                    result[i, j] = this[i, j];
                }
            }

            return result;
        }

        /// <summary>
        /// Returns a complex matrix which is flipped vertically.
        /// </summary>
        /// <returns>A complex matrix which is flipped vertically of this matrix.</returns>
        public CMatrix VerticalFlip()
        {
            CMatrix result = new CMatrix(RowCount, ColumnCount);

            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < ColumnCount; j++)
                    result[i, j] = this[i, ColumnCount - 1 - j];
            }

            return result;
        }

        /// <summary>
        /// Returns a complex matrix which is flipped horizontally.
        /// </summary>
        /// <returns>A complex matrix which is flipped horizontally of this matrix.</returns>
        public CMatrix HorizontalFlip()
        {
            CMatrix result = new CMatrix(RowCount, ColumnCount);

            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < ColumnCount; j++)
                    result[i, j] = this[RowCount - 1 - i, j];
            }

            return result;
        }

        /// <summary>
        /// Returns the sum of the elements of a specified column.
        /// </summary>
        /// <param name="index">A column index.</param>
        /// <returns>The sum of the elements of a column with the specified index.</returns>
        /// <exception cref="System.IndexOutOfRangeException">The index out of range.</exception>
        public Complex ColumnSum(int index)
        {
            if (index < 0 || index >= ColumnCount)
                throw new IndexOutOfRangeException("The index out of range.");

            Complex result = Complex.Zero;

            for (int i = 0; i < RowCount; i++)
                result += this[i, index];

            return result;
        }

        /// <summary>
        /// Returns the sum of the absolute values of the elements of a specified column.
        /// </summary>
        /// <param name="index">A column index.</param>
        /// <returns>The sum of the absolute values of the elements of a column with the specified index.</returns>
        /// <exception cref="System.IndexOutOfRangeException">The index out of range.</exception>
        public double AbsColumnSum(int index)
        {
            if (index < 0 || index >= ColumnCount)
                throw new IndexOutOfRangeException("The index out of range.");

            double result = 0;

            for (int i = 0; i < RowCount; i++)
                result += Complex.Abs(this[i, index]);

            return result;
        }

        /// <summary>
        /// Returns the sum of the elements of a specified row.
        /// </summary>
        /// <param name="index">A row index.</param>
        /// <returns>The sum of the elements of a row with the specified index.</returns>
        /// <exception cref="System.IndexOutOfRangeException">The index out of range.</exception>
        public Complex RowSum(int index)
        {
            if (index < 0 || index >= RowCount)
                throw new IndexOutOfRangeException("The index out of range.");

            Complex result = Complex.Zero;

            for (int j = 0; j < ColumnCount; j++)
                result += this[index, j];

            return result;
        }

        /// <summary>
        /// Returns the sum of the absolute values of the elements of a specified row.
        /// </summary>
        /// <param name="index">A row index.</param>
        /// <returns>The sum of the absolute values of the elements of a row with the specified index.</returns>
        /// <exception cref="System.IndexOutOfRangeException">The index out of range.</exception>
        public double AbsRowSum(int index)
        {
            if (index < 0 || index >= RowCount)
                throw new IndexOutOfRangeException("The index out of range.");

            double result = 0;

            for (int j = 0; j < ColumnCount; j++)
            {
                result += Complex.Abs(this[index, j]);
            }

            return result;
        }

        /// <summary>
        /// Returns product of main diagonal elements.
        /// </summary>
        /// <returns>The product of diagonal elements of this matrix.</returns>
        public Complex DiagProduct()
        {
            Complex result = Complex.One;
            int n = Math.Min(RowCount, ColumnCount);

            for (int i = 0; i < n; i++)
            {
                result *= this[i, i];
            }

            return result;
        }

        /// <summary>
        /// Returns a complex matrix whose elements are the result of
        /// applying the specified function to the elements of this matrix.
        /// </summary>
        /// <param name="func">
        /// A delegate to a function that takes one complex parameter and returns a complex.
        /// </param>
        /// <returns>
        /// A complex matrix whose elements are the result of
        /// applying the specified function func to the elements of this matrix.
        /// </returns>
        public CMatrix ApplyFunction(Func<Complex, Complex> func)
        {
            CMatrix m = new CMatrix(RowCount, ColumnCount);

            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < ColumnCount; j++)
                    m[i, j] = func(this[i, j]);
            }

            return m;
        }

        /// <summary>
        /// Returns a complex matrix whose elements are the result of
        /// applying the specified function to the elements of this matrix.
        /// </summary>
        /// <param name="func">
        /// A delegate to a function that takes one complex and two integer parameters and returns a complex.
        /// </param>
        /// <returns>
        /// A complex matrix whose elements are the result of
        /// applying the specified function func to the elements of this matrix.
        /// </returns>
        public CMatrix ApplyFunction(Func<Complex, int, int, Complex> func)
        {
            CMatrix m = new CMatrix(RowCount, ColumnCount);

            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < ColumnCount; j++)
                    m[i, j] = func(this[i, j], i, j);
            }

            return m;
        }

        /// <summary>
        /// Returns the complex matrix with the new number of rows and columns.
        /// </summary>
        /// <param name="rows">New number of rows.</param>
        /// <param name="cols">New number of columns.</param>
        /// <returns>This instance with the new number of rows and columns.</returns>
        public CMatrix Resize(int rows, int cols)
        {
            if (RowCount != rows || ColumnCount != cols)
            {
                CMatrix m = new CMatrix(rows, cols);

                int nRows = Math.Min(RowCount, rows);
                int nCols = Math.Min(ColumnCount, cols);

                for (int i = 0; i < nRows; i++)
                {
                    for (int j = 0; j < nCols; j++)
                        m[i, j] = this[i, j];
                }

                return m;
            }

            return new CMatrix(this);
        }

        /// <summary>
        /// Swaps rows at specified indices.
        /// </summary>
        /// <param name="first">An index of first row.</param>
        /// <param name="second">An index of second row.</param>
        /// <exception cref="System.IndexOutOfRangeException">The indices out of range.</exception>
        public void SwapRows(int first, int second)
        {
            if (first < 0 || first >= RowCount || second < 0 || second >= RowCount)
                throw new IndexOutOfRangeException("Indices must be positive and less number of rows.");

            if (first != second)
            {
                for (int i = 0; i < ColumnCount; i++)
                {
                    Complex temp = this[first, i];
                    this[first, i] = _m[second, i];
                    this[second, i] = temp;
                }
            }
        }

        /// <summary>
        /// Swaps columns at specified indices.
        /// </summary>
        /// <param name="first">An index of first column.</param>
        /// <param name="second">An index of second column.</param>
        /// <exception cref="System.IndexOutOfRangeException">The indices out of range.</exception>
        public void SwapColumns(int first, int second)
        {
            if (first < 0 || first >= ColumnCount || second < 0 || second >= ColumnCount)
                throw new IndexOutOfRangeException("Indices must be positive and less number of columns.");

            if (first != second)
            {
                for (int i = 0; i < RowCount; i++)
                {
                    Complex temp = this[i, first];
                    this[i, first] = this[i, second];
                    this[i, second] = temp;
                }
            }
        }

        /// <summary>
        /// Returns a value that indicates whether the matrix is symmetric
        /// (the square matrix, that is equal to its transpose).
        /// </summary>
        /// <returns>true if the matrix is symmetric; otherwise false;</returns>
        public bool IsSymmetric()
        {
            if (!IsSquare) return false;

            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < ColumnCount; j++)
                {
                    if (this[i, j] != this[j, i])
                        return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Returns a value that indicates whether the matrix is symmetric
        /// (the square matrix, that is equal to its transpose).
        /// </summary>
        /// <param name="relativeTolerance">A real number that represents a relative tolerance.</param>
        /// <returns>true if the matrix is symmetric; otherwise false;</returns>
        public bool IsSymmetric(double relativeTolerance)
        {
            if (!IsSquare) return false;

            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < ColumnCount; j++)
                {
                    if (!NumericUtil.FuzzyEquals(this[i, j], this[j, i], relativeTolerance))
                        return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Returns a value that indicates whether the matrix is normal
        /// (the matrix is normal if it commutes with its conjugate transpose).
        /// </summary>
        /// <returns>true if the matrix is normal; otherwise false;</returns>
        public bool IsNormal()
        {
            return CMatrix.Equals(this * Adjoint, Adjoint * this);
        }

        /// <summary>
        /// Returns a value that indicates whether the matrix is normal
        /// (the matrix is normal if it commutes with its conjugate transpose).
        /// </summary>
        /// <param name="relativeTolerance">A real number that represents a relative tolerance.</param>
        /// <returns>true if the matrix is normal; otherwise false;</returns>
        public bool IsNormal(double relativeTolerance)
        {
            return CMatrix.FuzzyEquals(this * Adjoint, Adjoint * this, relativeTolerance);
        }

        /// <summary>
        /// Returns a value that indicates whether the matrix is hermitian
        /// (the square matrix with complex entries which is equal to its own conjugate transpose).
        /// </summary>
        /// <returns>true if the matrix is hermitian; otherwise false;</returns>
        public bool IsHermitian()
        {
            return (IsSquare && CMatrix.Equals(Adjoint, this));
        }

        /// <summary>
        /// Returns a value that indicates whether the matrix is hermitian
        /// (the square matrix with complex entries which is equal to its own conjugate transpose).
        /// </summary>
        /// <param name="relativeTolerance">A real number that represents a relative tolerance.</param>
        /// <returns>true if the matrix is hermitian; otherwise false;</returns>
        public bool IsHermitian(double relativeTolerance)
        {
            return (IsSquare && CMatrix.FuzzyEquals(Adjoint, this, relativeTolerance));
        }

        /// <summary>
        /// Returns a value that indicates whether the matrix is orthogonal
        /// (the transposition matrix is equal to its inverse matrix).
        /// </summary>
        /// <returns>true if the matrix is orthogonal; otherwise false;</returns>
        public bool IsOrthogonal()
        {
            return IsSquare && CMatrix.Equals(this * Transpose, Identity(RowCount));
        }

        /// <summary>
        /// Returns a value that indicates whether the matrix is orthogonal
        /// (the transposition matrix is equal to its inverse matrix).
        /// </summary>
        /// <param name="relativeTolerance">A real number that represents a relative tolerance.</param>
        /// <returns>true if the matrix is orthogonal; otherwise false;</returns>
        public bool IsOrthogonal(double relativeTolerance)
        {
            return IsSquare && CMatrix.FuzzyEquals(this * Transpose, Identity(RowCount), relativeTolerance);
        }

        /// <summary>
        /// Returns a value that indicates whether the matrix is unitary
        /// (the conjugate transposition matrix is equal to its inverse matrix).
        /// </summary>
        /// <returns>true if the matrix is unitary; otherwise false;</returns>
        public bool IsUnitary()
        {
            return (IsSquare && Equals(Adjoint * this, Identity(RowCount)));
        }

        /// <summary>
        /// Returns a value that indicates whether the matrix is unitary
        /// (the conjugate transposition matrix is equal to its inverse matrix).
        /// </summary>
        /// <param name="relativeTolerance">A real number that represents a relative tolerance.</param>
        /// <returns>true if the matrix is unitary; otherwise false;</returns>
        public bool IsUnitary(double relativeTolerance)
        {
            return (IsSquare && FuzzyEquals(Adjoint * this, Identity(RowCount), relativeTolerance));
        }

        /// <summary>
        /// Returns a value that indicates whether the matrix is singular
        /// (the matrix that is not invertible).
        /// </summary>
        /// <returns>true if the matrix is singular; otherwise false;</returns>
        public bool IsSingular()
        {
            return IsSquare && (Determ(this) == Complex.Zero);
        }

        /// <summary>
        /// Returns a value that indicates whether the matrix is singular
        /// (the matrix that is not invertible).
        /// </summary>
        /// <param name="TOL">A real number that represents a tolerance.</param>
        /// <returns>true if the matrix is singular; otherwise false;</returns>
        public bool IsSingular(double TOL)
        {
            return IsSquare && NumericUtil.FuzzyEquals(Determ(this), Complex.Zero, TOL);
        }

        /// <summary>
        /// Returns a value that indicates whether the matrix is involutary
        /// (the matrix that is its own inverse).
        /// </summary>
        /// <returns>true if the matrix is involutary; otherwise false;</returns>
        public bool IsInvolutary()
        {
            return (IsSquare && CMatrix.Equals(this * this, Identity(RowCount)));
        }

        /// <summary>
        /// Returns a value that indicates whether the matrix is involutary
        /// (the matrix that is its own inverse).
        /// </summary>
        /// <param name="relativeTolerance">A real number that represents a relative tolerance.</param>
        /// <returns>true if the matrix is involutary; otherwise false;</returns>
        public bool IsInvolutary(double relativeTolerance)
        {
            return (IsSquare && CMatrix.FuzzyEquals(this * this, Identity(RowCount), relativeTolerance));
        }

        /// <summary>
        /// Copies the elements of the complex matrix to a new complex array.
        /// </summary>
        /// <returns>An complex array containing copies of the elements of the CMatrix.</returns>
        public Complex[,] ToArray()
        {
            return (Complex[,])_m.Clone();
        }

        public Complex[] To1DimArray()
        {
            Complex[] result = new Complex[RowCount * ColumnCount];

            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < ColumnCount; j++)
                {
                    result[j * RowCount + i] = this[i, j];
                }
            }
            return result;
        }

        /// <summary>
        /// Creates a shallow copy of the complex matrix.
        /// </summary>
        /// <returns>A shallow copy of the CMatrix.</returns>
        public Object Clone()
        {
            return new CMatrix(_m);
        }

        /// <summary>
        /// Converts the complex matrix to its equivalent string representation.
        /// </summary>
        /// <returns>The string representation of the value of this instance.</returns>
        public override string ToString()
        {
            return ToString(null, NumberFormatInfo.CurrentInfo);
        }

        /// <summary>
        /// Converts the complex matrix to its equivalent string representation.
        /// </summary>
        /// <param name="format">A numeric format string.</param>
        /// <returns>The string representation of the value of this instance as specified by format.</returns>
        /// <exception cref="System.FormatException">Format is invalid.</exception>
        public string ToString(string format)
        {
            return ToString(format, NumberFormatInfo.CurrentInfo);
        }

        /// <summary>
        /// Converts the complex matrix to its equivalent string representation.
        /// </summary>
        /// <param name="format">A numeric format string.</param>
        /// <param name="provider">An <see cref="System.IFormatProvider"/> that supplies culture-specific formatting information.</param>
        /// <returns>The string representation of the value of this instance as specified by format and provider.</returns>
        /// <exception cref="System.FormatException">Format is invalid.</exception>
        public string ToString(string format, IFormatProvider provider)
        {
            return ToString(format, provider, ", ", "; ");
        }

        /// <summary>
        /// Converts the complex matrix to its equivalent string representation.
        /// </summary>
        /// <param name="format">A numeric format string.</param>
        /// <param name="provider">An System.IFormatProvider that supplies culture-specific formatting information.</param>
        /// <param name="elementSeparator">A string that will be used to separate matrix elements.</param>
        /// <param name="rowSeparator">A string that will be used to separate matrix rows.</param>
        /// <returns>The string representation of the value of this instance as specified by format and provider.</returns>
        /// <exception cref="System.FormatException">Format is invalid.</exception>
        public string ToString(string format, IFormatProvider provider, string elementSeparator, string rowSeparator)
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("{");

            for (int i = 0; i < RowCount; i++)
            {
                if (i > 0) builder.Append(rowSeparator);
                for (int j = 0; j < ColumnCount; j++)
                {
                    if (j > 0) builder.Append(elementSeparator);
                    builder.Append(_m[i, j].ToString(format, provider));
                }
            }
            builder.Append("}");

            return builder.ToString();
        }

        /// <summary>
        /// Returns a value indicating whether this instance is equal
        /// to a specified object.
        /// </summary>
        /// <param name="obj">An object to compare with this instance.</param>
        /// <returns>
        /// True if obj is an instance of CMatrix and equals the value of this instance;
        /// otherwise, false.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (!(obj is CMatrix))
                return false;

            return Equals(this, (CMatrix)obj);
        }

        /// <summary>
        /// Returns a value indicating whether this instance is equal to a specified
        /// complex matrix.
        /// </summary>
        /// <param name="obj">A CMatrix object to compare to this instance.</param>
        /// <returns>True if obj is equal to this instance; otherwise, false.</returns>
        public bool Equals(CMatrix obj)
        {
            return Equals(this, obj);
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>A 32-bit signed integer hash code.</returns>
        public override int GetHashCode()
        {
            int hashCode = 0;

            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < ColumnCount; j++)
                {
                    hashCode ^= this[i, j].GetHashCode();
                }
            }

            return hashCode;
        }

        #endregion

        #endregion

        #region Operators

        /// <summary>
        /// Returns a value indicating whether
        /// two instances of complex matrix are equal.
        /// </summary>
        /// <param name="m1">The first complex matrix to compare.</param>
        /// <param name="m2">The second complex matrix to compare.</param>
        /// <returns>True if the m1 and m2 parameters have the same value; otherwise, false.</returns>
        public static bool operator ==(CMatrix m1, CMatrix m2)
        {
            return Equals(m1, m2);
        }

        /// <summary>
        /// Returns a value indicating whether
        /// two instances of complex matrix are not equal.
        /// </summary>
        /// <param name="m1">The first complex matrix to compare.</param>
        /// <param name="m2">The second complex matrix to compare.</param>
        /// <returns>True if m1 and m2 are not equal; otherwise, false.</returns>
        public static bool operator !=(CMatrix m1, CMatrix m2)
        {
            return !Equals(m1, m2);
        }

        /// <summary>
        /// Returns the value of the complex matrix operand
        /// (the sign of the operand is unchanged).
        /// </summary>
        /// <param name="m">A complex matrix.</param>
        /// <returns>The value of the operand, m.</returns>
        public static CMatrix operator +(CMatrix m)
        {
            return new CMatrix(m);
        }

        /// <summary>
        /// Negates of all elements of the complex matrix.
        /// </summary>
        /// <param name="m">A complex matrix.</param>
        /// <returns>The result of m multiplied by negative one.</returns>
        public static CMatrix operator -(CMatrix m)
        {
            return Negate(m);
        }

        /// <summary>
        /// Adds of complex matrix and scalar.
        /// </summary>
        /// <param name="m">A complex matrix (the first term).</param>
        /// <param name="number">A scalar value (the second term).</param>
        /// <returns>The sum of m and number.</returns>
        public static CMatrix operator +(CMatrix m, Complex number)
        {
            return Add(m, number);
        }

        /// <summary>
        /// Adds of scalar and complex matrix.
        /// </summary>
        /// <param name="number">A scalar value (the first term).</param>
        /// <param name="m">A complex matrix (the second term).</param>
        /// <returns>The sum of number and m.</returns>
        public static CMatrix operator +(Complex number, CMatrix m)
        {
            return Add(m, number);
        }

        /// <summary>
        /// Adds two complex matrices.
        /// </summary>
        /// <param name="m1">A complex matrix (the first term).</param>
        /// <param name="m2">A complex matrix (the second term).</param>
        /// <returns>The sum of complex matrices m1 and m2.</returns>
        public static CMatrix operator +(CMatrix m1, CMatrix m2)
        {
            return Add(m1, m2);
        }

        /// <summary>
        /// Subtracts a scalar from a complex matrix.
        /// </summary>
        /// <param name="m">A complex matrix (the minuend).</param>
        /// <param name="number">A scalar value (the subtrahend).</param>
        /// <returns>The result of subtracting number from m.</returns>
        public static CMatrix operator -(CMatrix m, Complex number)
        {
            return Subtract(m, number);
        }

        /// <summary>
        /// Subtracts a complex matrix from a scalar.
        /// </summary>
        /// <param name="number">A scalar value (the minuend).</param>
        /// <param name="m">A complex matrix (the subtrahend).</param>
        /// <returns>The result of subtracting m from number.</returns>
        public static CMatrix operator -(Complex number, CMatrix m)
        {
            return Subtract(number, m);
        }

        /// <summary>
        /// Subtracts one complex matrix from another.
        /// </summary>
        /// <param name="m1">A complex matrix (the minuend).</param>
        /// <param name="m2">A complex matrix (the subtrahend).</param>
        /// <returns>The result of subtracting m2 from m1.</returns>
        public static CMatrix operator -(CMatrix m1, CMatrix m2)
        {
            return Subtract(m1, m2);
        }

        /// <summary>
        /// Multiplies a complex matrix by a scalar.
        /// </summary>
        /// <param name="m">A complex matrix (the multiplicand).</param>
        /// <param name="number">A scalar value (the multiplier).</param>
        /// <returns>The product of m and number.</returns>
        public static CMatrix operator *(CMatrix m, Complex number)
        {
            return Multiply(m, number);
        }

        /// <summary>
        /// Multiplies a scalar by a complex matrix.
        /// </summary>
        /// <param name="number">A scalar value (the multiplicand).</param>
        /// <param name="m">A complex matrix (the multiplier).</param>
        /// <returns>The product of number and m.</returns>
        public static CMatrix operator *(Complex number, CMatrix m)
        {
            return Multiply(m, number);
        }

        /// <summary>
        /// Multiplies two complex matrices.
        /// </summary>
        /// <param name="m1">A complex matrix (the multiplicand).</param>
        /// <param name="m2">A complex matrix (the multiplier).</param>
        /// <returns>The product of two complex matrices m1 and m2.</returns>
        public static CMatrix operator *(CMatrix m1, CMatrix m2)
        {
            return Multiply(m1, m2);
        }

        /// <summary>
        /// Divides a complex matrix by a scalar.
        /// </summary>
        /// <param name="m">A complex matrix (the dividend).</param>
        /// <param name="number">A scalar value (the divisor).</param>
        /// <returns>The result of dividing m by number.</returns>
        public static CMatrix operator /(CMatrix m, Complex number)
        {
            return Divide(m, number);
        }

        /// <summary>
        /// Divides a scalar by a complex matrix.
        /// </summary>
        /// <param name="number">A scalar value (the dividend).</param>
        /// <param name="m">A complex matrix (the divisor).</param>
        /// <returns>The result of dividing number by m.</returns>
        public static CMatrix operator /(Complex number, CMatrix m)
        {
            return Divide(number, m);
        }

        /// <summary>
        /// Divides two complex matrices.
        /// </summary>
        /// <param name="m1">A complex matrix (the dividend).</param>
        /// <param name="m2">A complex matrix (the divisor).</param>
        /// <returns>The result of dividing m1 by m2.</returns>
        public static CMatrix operator /(CMatrix m1, CMatrix m2)
        {
            return Divide(m1, m2);
        }

        /// <summary>
        /// Converts a complex matrix to a two-dimensional complex array.
        /// </summary>
        /// <param name="matrix">A complex matrix.</param>
        /// <returns>A two-dimensional complex array that represents the converted complex matrix.</returns>
        public static implicit operator Complex[,](CMatrix matrix)
        {
            return (Complex[,])matrix._m.Clone();
        }

        /// <summary>
        /// Converts a one-dimensional complex array to a complex matrix.
        /// </summary>
        /// <param name="array">One-dimensional complex array.</param>
        /// <returns>A complex matrix that represents the converted one-dimensional complex array.</returns>
        public static implicit operator CMatrix(Complex[] array)
        {
            var m = new CMatrix(array.Length, 1);

            for (int i = 0; i < array.Length; i++)
            {
                m[i, 0] = array[i];
            }

            return m;
        }

        /// <summary>
        /// Converts a two-dimensional complex array to a complex matrix.
        /// </summary>
        /// <param name="array">Two-dimensional complex array.</param>
        /// <returns>A complex matrix that represents the converted two-dimensional complex array.</returns>
        public static implicit operator CMatrix(Complex[,] array)
        {
            return new CMatrix(array);
        }

        /// <summary>
        /// Converts a one-dimensional real array to a complex matrix.
        /// </summary>
        /// <param name="array">One-dimensional real array.</param>
        /// <returns>A complex matrix that represents the converted one-dimensional real array.</returns>
        public static implicit operator CMatrix(double[] array)
        {
            var m = new CMatrix(array.Length, 1);

            for (int i = 0; i < array.Length; i++)
            {
                m[i, 0] = array[i];
            }

            return m;
        }

        /// <summary>
        /// Converts a two-dimensional real array to a complex matrix.
        /// </summary>
        /// <param name="array">Two-dimensional real array.</param>
        /// <returns>A complex matrix that represents the converted two-dimensional real array.</returns>
        public static implicit operator CMatrix(double[,] array)
        {
            return new CMatrix(array);
        }

        #endregion


        #region IEnumerable<Complex> Members

        IEnumerator<Complex> IEnumerable<Complex>.GetEnumerator()
        {
            return new CMatrixEnumerator(this);
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return new CMatrixEnumerator(this);
        }
        
        #endregion
    }

    internal class CMatrixEnumerator : IEnumerator<Complex>
    {
        private int _currRow;
        private int _currCol;
        private CMatrix _matrix;

        public CMatrixEnumerator(CMatrix matrix)
        {
            _matrix = matrix;
            Reset();
        }


        #region IEnumerator<Complex> Members

        public Complex Current
        {
            get
            {
                return _matrix[_currRow, _currCol];
            }
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
        }

        #endregion

        #region IEnumerator Members

        object System.Collections.IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        public bool MoveNext()
        {
            if (_currCol >= _matrix.ColumnCount - 1 && _currRow >= _matrix.RowCount - 1)
            {
                return false;
            }

            if (_currCol >= _matrix.ColumnCount - 1)
            {
                _currRow++;
                _currCol = 0;
            }
            else
            {
                _currCol++;
            }
            return true;
        }

        public void Reset()
        {
            _currRow = 0;
            _currCol = -1;
        }

        #endregion
    }
}