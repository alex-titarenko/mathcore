using System;
using TAlex.MathCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TAlex.MathCore.Test.Helpers;


namespace TAlex.MathCore.Test
{
    /// <summary>
    /// This is a test class for CPolynomialTest and is intended
    /// to contain all CPolynomialTest Unit Tests
    ///</summary>
    [TestClass()]
    public class CPolynomialTest
    {
        private RandomGenerator _rand = new RandomGenerator();


        /// <summary>
        ///A test for FirstDerivative
        ///</summary>
        [TestMethod()]
        public void FromRootsTest()
        {
            int size = 5;
            int n = 10000;
            double TOL = 10E-5;

            Complex[] roots = new Complex[size];

            for (int idx = 0; idx < n; idx++)
            {
                _rand.Fill(roots, -100, 100, 1);

                CPolynomial poly = CPolynomial.FromRoots(roots);

                for (int i = 0; i < roots.Length; i++)
                {
                    Complex p = poly.Evaluate(roots[i]);

                    if (Complex.Abs(p) >= TOL)
                        Assert.Fail("Polynom : {0}", p);
                }
            }
        }

        /// <summary>
        ///A test for FirstDerivative
        ///</summary>
        [TestMethod()]
        public void FirstDerivativeTest()
        {
            CPolynomial target = new CPolynomial(new Complex[] {5, 8, -14, 3, 6, -22});
            Complex value = 3;
            Complex expected = -8257;
            Complex actual = target.FirstDerivative(value);

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for FirstDerivative
        ///</summary>
        [TestMethod()]
        public void FirstDerivativeTest_Analytical()
        {
            CPolynomial target = new CPolynomial(new Complex[] { 5, 8, -14, 3, 6, -22 });
            CPolynomial expected = new CPolynomial(new Complex[] {8, -28, 9, 24, -110});

            CPolynomial actual = target.FirstDerivative();

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for SecondDerivative
        ///</summary>
        [TestMethod()]
        public void SecondDerivativeTest()
        {
            CPolynomial target = new CPolynomial(new Complex[] { 5, 8, -14, 3, 6, -22 });
            Complex c = 3;
            Complex expected = -11206;
            Complex actual = target.SecondDerivative(c);

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for SecondDerivative
        ///</summary>
        [TestMethod()]
        public void SecondDerivativeTest_Analytical()
        {
            CPolynomial target = new CPolynomial(new Complex[] { 5, 8, -14, 3, 6, -22 });
            CPolynomial expected = new CPolynomial(new Complex[] { -28, 18, 72, -440 });
            CPolynomial actual = target.SecondDerivative();

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for NthDerivative
        ///</summary>
        [TestMethod()]
        public void NthDerivativeTest()
        {
            CPolynomial target = new CPolynomial(new Complex[] { 5, 8, -14, 3, 6, -22 });
            Complex c = 3;
            Complex expected = -11430;
            Complex actual = target.NthDerivative(3, c);

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for NthDerivative
        ///</summary>
        [TestMethod()]
        public void NthDerivativeTest_Analytical()
        {
            CPolynomial target = new CPolynomial(new Complex[] { 5, 8, -14, 3, 6, -22 });
            CPolynomial expected = new CPolynomial(new Complex[] { 18, 144, -1320 });
            CPolynomial actual = target.NthDerivative(3);

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Antiderivative
        ///</summary>
        [TestMethod()]
        public void AntiderivativeTest()
        {
            CPolynomial target = new CPolynomial(new Complex[] { 3, 8, 16 });
            CPolynomial expected = new CPolynomial(new Complex[] { 0, 3, 4, 16.0 / 3 });
            CPolynomial actual = target.Antiderivative();

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for DivBinom
        ///</summary>
        [TestMethod()]
        public void DivBinomTest()
        {
            int size = 8;
            int n = 10000;
            double TOL = 1E-10;

            CPolynomial f = new CPolynomial(size);

            for (int i = 0; i < n; i++)
            {
                _rand.Fill(f, -1000, 1000, 5);
                Complex c = _rand.NextDouble(-1000, 1000, 5);

                Complex r;
                CPolynomial q = CPolynomial.DivBinom(f, c, out r);

                f[0] -= r;
                Complex rtest;
                CPolynomial qtest = CPolynomial.DivBinom(f, c, out rtest);

                if (Complex.Abs(rtest) > TOL)
                    Assert.Fail("remainder: {0}", rtest);

                if (q != qtest)
                    Assert.Fail("qtest: {0} | q: {1}", qtest, q);
            }
        }

        /// <summary>
        ///A test for Div
        ///</summary>
        [TestMethod()]
        public void DivTest()
        {
            int size1 = 7;
            int size2 = 5;
            int n = 10000;
            double TOL = 1E-5;

            CPolynomial f = new CPolynomial(size1);
            CPolynomial g = new CPolynomial(size2);
            CPolynomial zero = new CPolynomial(1);

            for (int i = 0; i < n; i++)
            {
                _rand.Fill(f, -1000, 1000, 5);
                _rand.Fill(g, -1000, 1000, 5);

                CPolynomial r;
                CPolynomial q = CPolynomial.Divide(f, g, out r);

                CPolynomial rtest = new CPolynomial(1);
                CPolynomial qtest = CPolynomial.Divide(f - r, g, out rtest);

                for (int j = 0; j < rtest.Length; j++)
                {
                    if (Complex.Abs(rtest[j]) > TOL)
                        Assert.Fail("remainder: {0}", rtest);
                }

                if (qtest != q)
                    Assert.Fail("qtest: {0} | q: {1}", qtest, q);
            }
        }

        /// <summary>
        ///A test for LaguerreRootsFinding
        ///</summary>
        [TestMethod()]
        public void LaguerreRootsFindingTest()
        {
            int size = 5;
            int n = 10000;
            double TOL = 10E-7;

            CPolynomial poly = new CPolynomial(size);

            for (int idx = 0; idx < n; idx++)
            {
                _rand.Fill(poly, -100, 100, 1);

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

        /// <summary>
        ///A test for InterpolatingPolynomial
        ///</summary>
        [TestMethod()]
        public void InterpolatingPolynomialTest()
        {
            int size = 5;
            int n = 10000;
            double TOL = 1E-6;

            double[] xValues = new double[size];
            double[] yValues = new double[size];

            for (int i = 0; i < n; i++)
            {
                xValues[0] = _rand.NextDouble(-100, 100, 5);
                yValues[0] = _rand.NextDouble(-100, 100, 5);

                for (int j = 1; j < size; j++)
                {
                    xValues[j] = xValues[j - 1] + _rand.NextDouble(1, 100, 5);
                    yValues[j] = _rand.NextDouble(-100, 100, 5);
                }

                CPolynomial poly = CPolynomial.InterpolatingPolynomial(xValues, yValues);

                for (int j = 0; j < size; j++)
                {
                    Complex val = poly.Evaluate(xValues[j]);

                    if (Complex.Abs(yValues[j] - val) >= TOL * Complex.Abs(val))
                        Assert.Fail("Error: {0}", yValues[j] - val);
                }
            }
        }
    }
}
