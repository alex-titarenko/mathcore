using FluentAssertions;
using NUnit.Framework;
using System;
using TAlex.MathCore;
using TAlex.MathCore.LinearAlgebra;
using TAlex.MathCore.LinearAlgebra.Test.Helpers;


namespace TAlex.MathCore.LinearAlgebra.Test
{    
    [TestFixture]
    public class CMatrixTest
    {
        private RandomGenerator _rand = new RandomGenerator();


        [Test]
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

                //action
                CMatrix[] LUP = CMatrix.LUPDecomposition(m);
                CMatrix P = LUP[0];
                CMatrix L = LUP[1];
                CMatrix U = LUP[2];

                //assert
                CMatrix.Equals(P * m, L * U, TOL).Should().BeTrue();
            }
        }

        [Test]
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

                //action
                CMatrix[] QR = CMatrix.QRDecomposition(m);
                CMatrix Q = QR[0];
                CMatrix R = QR[1];

                //assert
                Q.IsUnitary.Should().BeTrue("Q is not unitary");
                R.IsUpperTrapeze.Should().BeTrue("R is not upper trapeze");
                CMatrix.Equals(m, Q * R, TOL).Should().BeTrue();
            }
        }

        [Test]
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

                //action
                CMatrix c = CMatrix.Divide(a, b);

                //assert
                CMatrix.Equals(c * b, a, TOL).Should().BeTrue();
            }
        }

        [Test]
        public void SqrtTest()
        {
            int size = 10;
            int n = 5000;
            double TOL = 10E-11;

            CMatrix m = new CMatrix(size, size);

            for (int idx = 0; idx < n; idx++)
            {
                _rand.Fill(m, -1000, 1000, 3);

                //action
                CMatrix sq = CMatrix.Sqrt(m);

                //assert
                CMatrix.Equals(sq * sq, m, TOL).Should().BeTrue();
            }
        }

        [Test]
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

                //action
                CMatrix inv_m = CMatrix.Inverse(m);

                //assert
                CMatrix.Equals(m * inv_m, identity, TOL).Should().BeTrue();
            }
        }

        [Test]
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

                //action
                CMatrix x = CMatrix.Solve(a, b);

                //assert
                CMatrix.Equals(a * x, b, TOL).Should().BeTrue();
            }
        }

        [Test]
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

                //action
                CPolynomial poly = CMatrix.CharacteristicPolynomial(m);
                CMatrix test = poly.Evaluate(m);

                //assert
                CMatrix.Equals(test, zero, TOL).Should().BeTrue();
            }
        }
    }
}
