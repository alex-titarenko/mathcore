using System;


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
            int m = matrix.RowCount;
            int n = matrix.ColumnCount;

            _m = matrix.RowCount;
            _n = matrix.ColumnCount;

            int min = Math.Min(m, n);
            int max = Math.Max(m, n);

            string jobz;
            int lda = Math.Max(1, m);
            int ldu = m;
            int ldvt = n;
            int lwork;
            int info;

            complex16[] u, vt;

            if (singularValuesOnly)
            {
                jobz = "N";
                lwork = (min * 2) + max;
                u = null;
                vt = null;
            }
            else
            {
                jobz = "A";
                lwork = ((min + 2) * min) + max;
                complex16[] temp1 = new complex16[m * m];
                complex16[] temp2 = new complex16[n * n];
                u = temp1;
                vt = temp2;
            }

            complex16[] a = new complex16[lda * n];
            double[] s = new double[Math.Min(m, n)];
            
            complex16[] work = new complex16[Math.Max(1, lwork)];
            double[] rwork = new double[(((min * 5) + 7) * min)];
            int[] iwork = new int[8 * Math.Min(m, n)];

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Complex temp = matrix[i, j];
                    a[i + j * m] = new complex16(temp.Re, temp.Im);
                }
            }

            lapack.zgesdd(jobz, &m, &n, a, &lda, s, u, &ldu, vt, &ldvt, work, &lwork, rwork, iwork, out info);

            if (info == 0) _succeded = true;
            else _succeded = false;

            _s = new double[Math.Min(m, n)];

            for (int i = 0; i < _s.Length; i++)
                _s[i] = s[i];

            if (!singularValuesOnly)
            {
                _u = new CMatrix(m, m);

                for (int i = 0; i < m; i++)
                {
                    for (int j = 0; j < m; j++)
                    {
                        complex16 temp = u[i + j * m];
                        _u[i, j] = new Complex(temp.r, temp.i);
                    }
                }

                _vt = new CMatrix(n, n);

                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        complex16 temp = vt[i + j * n];
                        _vt[i, j] = new Complex(temp.r, temp.i);
                    }
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
