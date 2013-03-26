using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TAlex.MathCore.LinearAlgebra;
using TAlex.MathCore.LinearAlgebra.Test.Helpers;


namespace TAlex.MathCore.LinearAlgebra.Test
{
    /// <summary>
    ///This is a test class for CEigenproblem and is intended
    ///to contain all CEigenproblem Unit Tests
    ///</summary>
    [TestClass()]
    public class CEigenproblemTest
    {
        #region Fields

        private TestContext testContextInstance;
        private RandomGenerator _rand = new RandomGenerator();

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the test context which provides
        /// information about and functionality for the current test run.
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
        ///A test for EigenSystem
        ///</summary>
        [TestMethod()]
        public void EigenSystemTest()
        {
            int size = 10;
            int n = 5000;
            double TOL = 10E-11;

            CMatrix m = new CMatrix(size, size);
            CMatrix identity = CMatrix.Identity(size);

            for (int idx = 0; idx < n; idx++)
            {
                _rand.Fill(m, -1000, 1000, 3);
                CEigenproblem eigen = new CEigenproblem(m, true);
                CMatrix eigenVals = eigen.Eigenvalues;
                CMatrix eigenVecs = eigen.Eigenvectors;

                for (int i = 0; i < size; i++)
                {
                    CMatrix v = (m - eigenVals[i] * identity) * eigenVecs.GetColumn(i);

                    for (int j = 0; j < v.Length; j++)
                    {
                        if (Math.Abs(v[j].Re) >= TOL || Math.Abs(v[j].Im) >= TOL)
                            Assert.Fail("v: {0}", v);
                    }
                }
            }
        }

        /// <summary>
        ///A test for LeftEigenSystem
        ///</summary>
        [TestMethod()]
        public void LeftEigenSystemTest()
        {
            int size = 10;
            int n = 5000;
            double TOL = 10E-11;

            CMatrix m = new CMatrix(size, size);
            CMatrix identity = CMatrix.Identity(size);

            for (int idx = 0; idx < n; idx++)
            {
                _rand.Fill(m, -1000, 1000, 3);
                CEigenproblem eigen = new CEigenproblem(m, false, true);
                CMatrix eigenVals = eigen.Eigenvalues;
                CMatrix eigenVecs = eigen.LeftEigenvectors;

                for (int i = 0; i < size; i++)
                {
                    CMatrix eigenVec = eigenVecs.GetColumn(i).Adjoint;
                    CMatrix v = eigenVec * m - eigenVals[i] * eigenVec;

                    for (int j = 0; j < v.Length; j++)
                    {
                        if (Math.Abs(v[0, j].Re) >= TOL || Math.Abs(v[0, j].Im) >= TOL)
                            Assert.Fail("v: {0}", v);
                    }
                }
            }
        }

        #endregion
    }
}
