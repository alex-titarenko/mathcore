using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TAlex.MathCore;
using TAlex.MathCore.LinearAlgebra;
using TAlex.MathCore.LinearAlgebra.Test.Helpers;


namespace TAlex.MathCore.LinearAlgebra.Test
{
    [TestClass()]
    public class CPolynomialExtensionsTest
    {
        private RandomGenerator _rand = new RandomGenerator();


        /// <summary>
        ///A test for CompanionMatrixRootsFinding
        ///</summary>
        [TestMethod()]
        public void CompanionMatrixRootsFindingTest()
        {
            int size = 5;
            int n = 10000;
            double TOL = 10E-7;

            CPolynomial poly = new CPolynomial(size);

            for (int idx = 0; idx < n; idx++)
            {
                _rand.Fill(poly, -100, 100, 1);
                
                Complex[] roots = poly.CompanionMatrixRootsFinding();

                for (int i = 0; i < roots.Length; i++)
                {
                    Complex p = poly.Evaluate(roots[i]);

                    if (Complex.Abs(p) >= TOL)
                        Assert.Fail("Polynom : {0}", p);
                }
            }

            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
