using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TAlex.MathCore.LinearAlgebra;
using TAlex.MathCore.LinearAlgebra.Test.Helpers;


namespace TAlex.MathCore.LinearAlgebra.Test
{
    /// <summary>
    /// This is a test class for CSVD and is intended
    /// to contain all CSVD Unit Tests
    ///</summary>
    [TestClass()]
    public class CSVDTest
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
        /// A test for SingularValueDecomposition
        ///</summary>
        [TestMethod()]
        public void SingularValueDecompositionTest()
        {
            int rowCount = 10;
            int columnCount = 10;
            int n = 5000;
            double TOL = 10E-11;

            CMatrix m = new CMatrix(rowCount, columnCount);

            for (int idx = 0; idx < n; idx++)
            {
                _rand.Fill(m, -1000, 1000, 3);

                CSVD svd = new CSVD(m);
                CMatrix u = svd.U;
                CMatrix s = svd.S;
                CMatrix vh = svd.VH;

                if (!u.IsUnitary)
                    Assert.Fail("U is not unitary (iter: {0})", idx);

                //if (!s.IsDiagonal || !s.IsReal)
                //    Assert.Fail("S is not diagonal or not real");

                if (!vh.IsUnitary)
                    Assert.Fail("VH is not unitary (iter: {0})", idx);

                if (!CMatrix.Equals(m, u * s * vh, TOL))
                    Assert.Fail((m - u * s * vh).ToString());
            }
        }

        #endregion
    }
}
