using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TAlex.MathCore;
using TAlex.MathCore.LinearAlgebra;
using TAlex.MathCore.LinearAlgebra.Test.Helpers;


namespace TAlex.MathCore.LinearAlgebra.Test
{    
    /// <summary>
    /// This is a test class for CMatrix and is intended
    /// to contain all CMatrix Unit Tests
    ///</summary>
    [TestClass()]
    public class CMatrixTest
    {
        #region Fields

        private TestContext testContextInstance;
        private RandomGenerator _rand = new RandomGenerator();

        #endregion

        #region Properties

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        ///A test for LUPDecomposition
        ///</summary>
        [TestMethod()]
        public void LUPDecompositionTest()
        {
            int size = 10;
            int n = 5000;
            double TOL = 10E-12;

            CMatrix m = new CMatrix(size, size);
            CMatrix identity = CMatrix.Identity(size);

            for (int idx = 0; idx < n; idx++)
            {
                _rand.Fill(m, -1000, 1000, 3);

                CMatrix[] LUP = CMatrix.LUPDecomposition(m);
                CMatrix P = LUP[0];
                CMatrix L = LUP[1];
                CMatrix U = LUP[2];

                if (!CMatrix.Equals(P * m, L * U, TOL))
                    Assert.Fail((P * m - L * U).ToString());
            }
        }

        /// <summary>
        ///A test for QRDecomposition
        ///</summary>
        [TestMethod()]
        public void QRDecompositionTest()
        {
            int rowCount = 10;
            int columnCount = 10;
            int n = 5000;
            double TOL = 10E-12;

            CMatrix m = new CMatrix(rowCount, columnCount);

            for (int idx = 0; idx < n; idx++)
            {
                _rand.Fill(m, -1000, 1000, 3);

                CMatrix[] QR = CMatrix.QRDecomposition(m);
                CMatrix Q = QR[0];
                CMatrix R = QR[1];

                if (!Q.IsUnitary)
                    Assert.Fail("Q is not unitary (iter: {0})", idx);

                if (!R.IsUpperTrapeze)
                    Assert.Fail("R is not upper trapeze (iter: {0})", R.ToString());

                if (!CMatrix.Equals(m, Q * R, TOL))
                    Assert.Fail((m - Q * R).ToString());
            }
        }

        /// <summary>
        ///A test for Div
        ///</summary>
        [TestMethod()]
        public void DivTest()
        {
            int size = 10;
            int n = 5000;
            double TOL = 10E-10;

            CMatrix a = new CMatrix(size, size);
            CMatrix b = new CMatrix(size, size);

            for (int idx = 0; idx < n; idx++)
            {
                _rand.Fill(a, -1000, 1000, 3);
                _rand.Fill(b, -1000, 1000, 3);

                CMatrix c = CMatrix.Divide(a, b);

                if (!CMatrix.Equals(c * b, a, TOL))
                    Assert.Fail();
            }
        }

        /// <summary>
        ///A test for Sqrt
        ///</summary>
        [TestMethod()]
        public void SqrtTest()
        {
            int size = 10;
            int n = 5000;
            double TOL = 10E-11;

            CMatrix m = new CMatrix(size, size);

            for (int idx = 0; idx < n; idx++)
            {
                _rand.Fill(m, -1000, 1000, 3);

                CMatrix sq = CMatrix.Sqrt(m);

                if (!CMatrix.Equals(sq * sq, m, TOL))
                    Assert.Fail();
            }
        }

        /// <summary>
        ///A test for Inverse
        ///</summary>
        [TestMethod()]
        public void InverseTest()
        {
            int size = 10;
            int n = 5000;
            double TOL = 10E-12;

            CMatrix m = new CMatrix(size, size);
            CMatrix identity = CMatrix.Identity(size);

            for (int idx = 0; idx < n; idx++)
            {
                _rand.Fill(m, -10000, 10000, 3);

                CMatrix inv_m = CMatrix.Inverse(m);

                if (!CMatrix.Equals(m * inv_m, identity, TOL))
                    Assert.Fail((m * inv_m).ToString());
            }
        }

        /// <summary>
        ///A test for Solve
        ///</summary>
        [TestMethod()]
        public void SolveTest()
        {
            int size = 10;
            int n = 10000;
            double TOL = 10E-10;

            CMatrix a = new CMatrix(size, size);
            CMatrix b = new CMatrix(size);

            for (int idx = 0; idx < n; idx++)
            {
                _rand.Fill(a, -1000, 1000, 3);
                _rand.Fill(b, -1000, 1000, 3);

                CMatrix x = CMatrix.Solve(a, b);

                if (!CMatrix.Equals(a * x, b, TOL))
                    Assert.Fail((a * x - b).ToString());
            }
        }

        /// <summary>
        ///A test for CharacteristicPolynomial
        ///</summary>
        [TestMethod()]
        public void CharacteristicPolynomialTest()
        {
            int size = 4;
            int n = 5000;
            double TOL = 10E-5;

            CMatrix m = new CMatrix(size, size);
            CMatrix zero = new CMatrix(size, size);

            for (int idx = 0; idx < n; idx++)
            {
                _rand.Fill(m, -100, 100, 0);

                CPolynomial poly = CMatrix.CharacteristicPolynomial(m);
                CMatrix test = poly.Evaluate(m);

                if (!CMatrix.Equals(test, zero, TOL))
                    Assert.Fail("m: {0}", test);
            }
        }

        #endregion
    }
}
