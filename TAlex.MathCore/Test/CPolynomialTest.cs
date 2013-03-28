using System;
using TAlex.MathCore;
using NUnit.Framework;
using FluentAssertions;


namespace TAlex.MathCore.Test
{
    [TestFixture]
    public class CPolynomialTest
    {
        [Test]
        public void FromRootsTest()
        {
            //arrange
            double TOL = 10E-10;
            Complex[] roots = new Complex[] { 3, 0, 158.3, 13, 8 };

            //action
            CPolynomial poly = CPolynomial.FromRoots(roots);

            //assert
            for (int i = 0; i < roots.Length; i++)
            {
                Complex p = poly.Evaluate(roots[i]);
                Complex.Abs(p).Should().BeLessThan(TOL);
            }
        }

        [Test]
        public void FirstDerivativeTest()
        {
            //arrange
            CPolynomial target = new CPolynomial(new Complex[] {5, 8, -14, 3, 6, -22});
            Complex value = 3;
            Complex expected = -8257;
            
            //action
            Complex actual = target.FirstDerivative(value);

            //assert
            actual.Should().Be(expected);
        }

        [Test]
        public void FirstDerivativeTest_Analytical()
        {
            //arrange
            CPolynomial target = new CPolynomial(new Complex[] { 5, 8, -14, 3, 6, -22 });
            CPolynomial expected = new CPolynomial(new Complex[] {8, -28, 9, 24, -110});

            //action
            CPolynomial actual = target.FirstDerivative();

            //assert
            actual.Should().Be(expected);
        }

        [Test]
        public void SecondDerivativeTest()
        {
            //arrange
            CPolynomial target = new CPolynomial(new Complex[] { 5, 8, -14, 3, 6, -22 });
            Complex c = 3;
            Complex expected = -11206;

            //action
            Complex actual = target.SecondDerivative(c);

            //assert
            actual.Should().Be(expected);
        }

        [Test]
        public void SecondDerivativeTest_Analytical()
        {
            //arrange
            CPolynomial target = new CPolynomial(new Complex[] { 5, 8, -14, 3, 6, -22 });
            CPolynomial expected = new CPolynomial(new Complex[] { -28, 18, 72, -440 });

            //action
            CPolynomial actual = target.SecondDerivative();

            //assert
            actual.Should().Be(expected);
        }

        [Test]
        public void NthDerivativeTest()
        {
            //arrange
            CPolynomial target = new CPolynomial(new Complex[] { 5, 8, -14, 3, 6, -22 });
            Complex c = 3;
            Complex expected = -11430;

            //action
            Complex actual = target.NthDerivative(3, c);

            //assert
            actual.Should().Be(expected);
        }

        [Test]
        public void NthDerivativeTest_Analytical()
        {
            //arrange
            CPolynomial target = new CPolynomial(new Complex[] { 5, 8, -14, 3, 6, -22 });
            CPolynomial expected = new CPolynomial(new Complex[] { 18, 144, -1320 });

            //action
            CPolynomial actual = target.NthDerivative(3);

            //assert
            actual.Should().Be(expected);
        }

        [Test]
        public void AntiderivativeTest()
        {
            //arrange
            CPolynomial target = new CPolynomial(new Complex[] { 3, 8, 16 });
            CPolynomial expected = new CPolynomial(new Complex[] { 0, 3, 4, 16.0 / 3 });

            //action
            CPolynomial actual = target.Antiderivative();

            //assert
            actual.Should().Be(expected);
        }

        [Test]
        public void DivBinomTest()
        {
            //arrange
            double TOL = 1E-10;
            CPolynomial f = new CPolynomial(new Complex[] {3, 0.2, new Complex(3, 8.2), -0.16, new Complex(2.3, 2), 0, 18.3466, 2000});
            Complex c = 346.34645;
            Complex r, rtest;
            
            //action
            CPolynomial q = CPolynomial.DivBinom(f, c, out r);

            f[0] -= r;
            CPolynomial qtest = CPolynomial.DivBinom(f, c, out rtest);

            //assert
            Complex.Abs(rtest).Should().BeLessOrEqualTo(TOL);
            q.Should().Be(qtest);
        }

        [Test]
        public void DivTest()
        {
            //arrange
            double TOL = 1E-12;

            CPolynomial f = new CPolynomial(new Complex[] { new Complex(3467.2, 456), new Complex(2.225, -0.0123), new Complex(46.2 ,4.24), new Complex(2, 2), new Complex(12.8, 16.3), new Complex(0, 0), new Complex(22, 347) });
            CPolynomial g = new CPolynomial(new Complex[] { new Complex(3, 8.5), new Complex(11, -0.5), new Complex(0, 1), new Complex(1, 2), new Complex(346, 4.365) });
            CPolynomial zero = new CPolynomial(1);

            CPolynomial r;
            CPolynomial q = CPolynomial.Divide(f, g, out r);
            CPolynomial rtest = new CPolynomial(1);
            
            //action
            CPolynomial qtest = CPolynomial.Divide(f - r, g, out rtest);

            //assert
            for (int j = 0; j < rtest.Length; j++)
            {
                Complex.Abs(rtest[j]).Should().BeLessOrEqualTo(TOL);
            }
            qtest.Should().Be(q);
        }

        [Test]
        public void LaguerreRootsFindingTest()
        {
            int size = 5;
            int n = 10000;
            double TOL = 10E-7;

            CPolynomial poly = new CPolynomial(size);

            for (int idx = 0; idx < n; idx++)
            {
                //_rand.Fill(poly, -100, 100, 1);

                Complex[] roots = CPolynomial.LaguerreRootsFinding(poly);

                for (int i = 0; i < roots.Length; i++)
                {
                    Complex p = poly.Evaluate(roots[i]);

                    if (Complex.Abs(p) >= TOL)
                        Assert.Fail("P(root): {0}", p);
                }
            }

            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [Test]
        public void InterpolatingPolynomialTest()
        {
            //arrange
            double TOL = 1E-6;
            double[] xValues = new double[] { 1, 2.5, 6, 7.9, 18 };
            double[] yValues = new double[] { 8.6, 0, -2, 9, 33 };

            //action
            CPolynomial poly = CPolynomial.InterpolatingPolynomial(xValues, yValues);

            //assert
            for (int j = 0; j < xValues.Length; j++)
            {
                Complex val = poly.Evaluate(xValues[j]);
                NumericUtil.FuzzyEquals(yValues[j], val, TOL).Should().BeTrue();
            }
        }
    }
}
