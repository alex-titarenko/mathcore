using System;
using System.Linq;


namespace TAlex.MathCore.LinearAlgebra
{
    /// <summary>
    /// Represents the Singular Value Decomposition of a general complex matrix.
    /// </summary>
    public class CSVD
    {
        #region Fields

        private bool _succeded;

        private double[] _s;

        private CMatrix _u;

        private CMatrix _vt;

        private int _m;

        private int _n;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the matrix containing the main diagonal singular values.
        /// </summary>
        public CMatrix S
        {
            get
            {
                CMatrix m = new CMatrix(_m, _n);

                for (int i = 0; i < _s.Length; i++)
                    m[i, i] = _s[i];

                return m;
            }
        }

        /// <summary>
        /// Gets the unitary matrix U, containing the left singular vectors.
        /// </summary>
        public CMatrix U
        {
            get
            {
                return new CMatrix(_u);
            }
        }

        /// <summary>
        /// Gets the conjugate transpose of the unitary matrix V, containing the right singular vectors.
        /// </summary>
        public CMatrix VH
        {
            get
            {
                return new CMatrix(_vt);
            }
        }

        /// <summary>
        /// Gets the singular value decomposition
        /// (the matrix formed by the horizontal concatenation of matrices U, S, VH).
        /// </summary>
        public CMatrix SVD
        {
            get
            {
                int m = _u.RowCount;
                int n = _vt.ColumnCount;
                
                CMatrix svd = new CMatrix(Math.Max(m, n), m + n * 2);

                for (int i = 0; i < m; i++)
                {
                    for (int j = 0; j < m; j++)
                    {
                        svd[i, j] = _u[i, j];
                    }
                }

                for (int i = 0; i < _s.Length; i++)
                {
                    svd[i, i + m] = _s[i];
                }

                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        svd[i, j + m + n] = _vt[i, j];
                    }
                }

                return svd;
            }
        }

        /// <summary>
        /// Gets the real array containing all singular values.
        /// </summary>
        public double[] SingularValues
        {
            get
            {
                return (double[])_s.Clone();
            }
        }

        /// <summary>
        /// Gets a value that indicating whether the singular value decomposition has been calculated successfully.
        /// </summary>
        public bool Succeded
        {
            get
            {
                return _succeded;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the CSVD class,
        /// computing the Singular Value Decomposition of a general complex matrix.
        /// </summary>
        /// <param name="matrix">A general complex matrix.</param>
        public CSVD(CMatrix matrix) : this(matrix, false)
        {
        }

        /// <summary>
        /// Initializes a new instance of the CSVD class,
        /// computing the Singular Value Decomposition of a general complex matrix,
        /// with the possibility of computing only the singular values.
        /// </summary>
        /// <param name="matrix">A general complex matrix.</param>
        /// <param name="singularValuesOnly">A value that indicating whether only the singular values will be computed.</param>
        public CSVD(CMatrix matrix, bool singularValuesOnly)
        {
            _m = matrix.RowCount;
            _n = matrix.ColumnCount;

            var m = new MathNet.Numerics.LinearAlgebra.Complex.DenseMatrix(matrix.RowCount, matrix.ColumnCount);
            for (int i = 0; i < m.RowCount; i++)
            {
                for (int j = 0; j < m.ColumnCount; j++)
                {
                    m.Storage[i, j] = new MathNet.Numerics.Complex(matrix[i, j].Re, matrix[i, j].Im);
                }
            }
            var svd = m.Svd();

            _s = svd.S.ToArray().Select(x => x.Real).ToArray();
            
            _u = new CMatrix(m.RowCount, m.RowCount);
            
            var u = svd.U;
            for (int i = 0; i < m.RowCount; i++)
            {
                for (int j = 0; j < m.RowCount; j++)
                {
                    _u[i, j] = new Complex(u.At(i, j).Real, u.At(i, j).Imaginary);
                }
            }

            _vt = new CMatrix(m.ColumnCount, m.ColumnCount);
            var vt = svd.VT;
            for (int i = 0; i < m.ColumnCount; i++)
            {
                for (int j = 0; j < m.ColumnCount; j++)
                {
                    _vt[i, j] = new Complex(vt.At(i, j).Real, vt.At(i, j).Imaginary);
                }
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns the two norm.
        /// </summary>
        /// <returns>The maximum singular value.</returns>
        public double Norm2()
        {
            return _s[0];
        }

        /// <summary>
        /// Returns the two norm condition number.
        /// </summary>
        /// <returns>Ratio of maximum to minimum singular value.</returns>
        public double Condition()
        {
            return _s[0] / _s[_s.Length - 1];
        }

        /// <summary>
        /// Returns the effective numerical matrix rank.
        /// </summary>
        /// <returns>Number of nonnegligible singular values.</returns>
        public int Rank()
        {
            double tol = Math.Max(_m, _n) * _s[0] * Machine.Epsilon;

            int rank = 0;
            for (int i = 0; i < _s.Length; i++)
            {
                if (_s[i] > tol)
                    rank++;
            }

            return rank;
        }

        /// <summary>
        /// Returns the Moore-Penrose inverse (pseudoinverse) matrix.
        /// </summary>
        /// <returns>The generalized inverse matrix.</returns>
        public CMatrix PseudoInverse()
        {
            double tol = Math.Max(_m, _n) * _s[0] * Machine.Epsilon;

            CMatrix s = new CMatrix(_n, _m);

            for (int i = 0; i < _s.Length; i++)
            {
                if (_s[i] > tol)
                    s[i, i] = 1.0 / _s[i];
            }

            return _vt.Adjoint * s * _u.Adjoint;
        }

        #endregion
    }
}
