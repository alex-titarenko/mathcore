using System;
using System.Linq;


namespace TAlex.MathCore.LinearAlgebra
{
    /// <summary>
    /// Computes the eigenvalues and the left and/or right eigenvectors of a square complex matrix.
    /// </summary>
    public class CEigenproblem
    {
        #region Fields

        private bool _succeded;

        private Complex[] _vals;

        private CMatrix _leftvecs;

        private CMatrix _rightvecs;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the square matrix containing the main diagonal the eigenvalues.
        /// </summary>
        public CMatrix D
        {
            get
            {
                return CMatrix.Diagonal(_vals);
            }
        }

        /// <summary>
        /// Gets the complex array containing all eigenvalues.
        /// </summary>
        public Complex[] Eigenvalues
        {
            get
            {
                return (Complex[])_vals.Clone();
            }
        }

        /// <summary>
        /// Gets the matrix containing in its columns the normalized right eigenvectors.
        /// </summary>
        public CMatrix Eigenvectors
        {
            get
            {
                return new CMatrix(_rightvecs);
            }
        }

        /// <summary>
        /// Gets the matrix containing in its columns the normalized left eigenvectors.
        /// </summary>
        public CMatrix LeftEigenvectors
        {
            get
            {
                return new CMatrix(_leftvecs);
            }
        }

        /// <summary>
        /// Gets a value that indicating whether the eigenvalues and eigenvectors has been calculated successfully.
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
        /// Initializes a new instance of the CEigenproblem class, computing
        /// the eigenvalues and the right eigenvectors of a square complex matrix.
        /// </summary>
        /// <param name="matrix">A complex square matrix.</param>
        public CEigenproblem(CMatrix matrix) : this(matrix, true, false)
        {
        }

        /// <summary>
        /// Initializes a new instance of the CEigenproblem class, computing
        /// the eigenvalues and optionally the right eigenvectors of a square complex matrix.
        /// </summary>
        /// <param name="matrix">A complex square matrix.</param>
        /// <param name="rightEigenvectors">A value that indicating whether the right eigenvectors will be computed.</param>
        public CEigenproblem(CMatrix matrix, bool rightEigenvectors) : this(matrix, rightEigenvectors, false)
        {
        }

        /// <summary>
        /// Initializes a new instance of the CEigenproblem class, computing
        /// the eigenvalues and optionally the right and/or the left eigenvectors of a square complex matrix.
        /// </summary>
        /// <param name="matrix">A complex square matrix.</param>
        /// <param name="rightEigenvectors">A value that indicating whether the right eigenvectors will be computed.</param>
        /// <param name="leftEigenvectors">A value that indicating whether the left eigenvectors will be computed.</param>
        public CEigenproblem(CMatrix matrix, bool rightEigenvectors, bool leftEigenvectors)
        {
            if (!matrix.IsSquare)
                throw new MatrixSizeMismatchException("The matrix must be square.");

            
            var m = new MathNet.Numerics.LinearAlgebra.Complex.DenseMatrix(matrix.RowCount, matrix.ColumnCount);
            for (int i = 0; i < m.RowCount; i++)
            {
                for (int j = 0; j < m.ColumnCount; j++)
                {
                    m.Storage[i, j] = new MathNet.Numerics.Complex(matrix[i, j].Re, matrix[i, j].Im);
                }
            }
            var evd = m.Evd();

            _vals = evd.EigenValues.ToArray().Select(x => new Complex(x.Real, x.Imaginary)).ToArray();

            _rightvecs = new CMatrix(matrix.RowCount, matrix.ColumnCount);
            var vecs = evd.EigenVectors;

            for (int i = 0; i < m.RowCount; i++)
            {
                for (int j = 0; j < m.ColumnCount; j++)
                {
                    _rightvecs[i, j] = new Complex(vecs.At(i, j).Real, vecs.At(i, j).Imaginary);
                }
            }

            //string jobvl, jobvr;
            //int n = matrix.RowCount;
            //int lda = Math.Max(1, n);
            //int ldvl, ldvr;
            //int ilo, ihi;
            //double abnrm;
            //int lwork = n * 3;
            //int info;

            //complex16[] a = new complex16[lda * n];
            //complex16[] w = new complex16[n];
            //complex16[] vl, vr;
            //double[] scale = new double[n];
            //double[] rconde = new double[n];
            //double[] rcondv = new double[n];
            //complex16[] work = new complex16[Math.Max(1, lwork)];
            //double[] rwork = new double[2 * n];

            //if (rightEigenvectors)
            //{
            //    jobvr = "V";
            //    ldvr = Math.Max(1, n);

            //    complex16[] temp = new complex16[ldvr * n];
            //    vr = temp;
            //}
            //else
            //{
            //    jobvr = "N";
            //    ldvr = 1;
            //    vr = null;
            //}

            //if (leftEigenvectors)
            //{
            //    jobvl = "V";
            //    ldvl = Math.Max(1, n);

            //    complex16[] temp = new complex16[ldvl * n];
            //    vl = temp;
            //}
            //else
            //{
            //    jobvl = "N";
            //    ldvl = 1;
            //    vl = null;
            //}

            //for (int i = 0; i < n; i++)
            //{
            //    for (int j = 0; j < n; j++)
            //    {
            //        Complex temp = matrix[i, j];
            //        a[i + j * n] = new complex16(temp.Re, temp.Im);
            //    }
            //}
            
            //lapack.zgeevx("B", jobvl, jobvr, "N", n, a, lda, w, vl, ldvl, vr, ldvr, out ilo, out ihi, scale,
            //    out abnrm, rconde, rcondv, work, lwork, rwork, out info);

            //if (info == 0) _succeded = true;
            //else _succeded = false;

            //_vals = new Complex[n];

            //for (int i = 0; i < n; i++)
            //    _vals[i] = new Complex(w[i].r, w[i].i);

            //if (rightEigenvectors)
            //{
            //    _rightvecs = new CMatrix(n, n);

            //    for (int i = 0; i < n; i++)
            //    {
            //        for (int j = 0; j < n; j++)
            //        {
            //            complex16 temp = vr[i + j * n];
            //            _rightvecs[i, j] = new Complex(temp.r, temp.i);
            //        }
            //    }
            //}

            //if (leftEigenvectors)
            //{
            //    _leftvecs = new CMatrix(n, n);

            //    for (int i = 0; i < n; i++)
            //    {
            //        for (int j = 0; j < n; j++)
            //        {
            //            complex16 temp = vl[i + j * n];
            //            _leftvecs[i, j] = new Complex(temp.r, temp.i);
            //        }
            //    }
            //}
        }

        #endregion
    }
}
