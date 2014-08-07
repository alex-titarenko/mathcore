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
        }

        #endregion
    }
}
