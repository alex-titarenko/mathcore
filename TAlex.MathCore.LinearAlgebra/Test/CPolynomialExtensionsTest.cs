using FluentAssertions;
using NUnit.Framework;
using System;
using TAlex.MathCore;
using TAlex.MathCore.LinearAlgebra;
using TAlex.MathCore.LinearAlgebra.Test.Helpers;


namespace TAlex.MathCore.LinearAlgebra.Test
{
    [TestFixture]
    public class CPolynomialExtensionsTest
    {
        private RandomGenerator _rand = new RandomGenerator();


        [Test]
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

                    //assert
                    Complex.Abs(p).Should().BeLessThan(TOL);
                }
            }

            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
