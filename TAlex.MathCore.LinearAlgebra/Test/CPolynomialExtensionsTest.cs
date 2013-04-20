using FluentAssertions;
using NUnit.Framework;
using System;
using TAlex.MathCore;
using TAlex.MathCore.LinearAlgebra;


namespace TAlex.MathCore.LinearAlgebra.Test
{
    [TestFixture]
    public class CPolynomialExtensionsTest
    {
        [Test]
        public void CompanionMatrixRootsFindingTest()
        {
            //arrange
            double TOL = 10E-10;
            CPolynomial poly = new CPolynomial(new Complex[]
            {
                new Complex(122, 14),
                new Complex(0, 2.2),
                new Complex(14.22, -18.44),
                new Complex(1130, 0),
                new Complex(14, 14),
                new Complex(18, 0.2)
            });

            //action
            Complex[] roots = poly.CompanionMatrixRootsFinding();

            //assert
            foreach (Complex root in roots)
            {
                Complex p = poly.Evaluate(root);
                Complex.Abs(p).Should().BeLessThan(TOL);
            }
        }
    }
}
