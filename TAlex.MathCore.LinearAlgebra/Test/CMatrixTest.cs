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
                CMatrix.FuzzyEquals(P * m, L * U, TOL).Should().BeTrue();
            }
        }

        [Test]
        public void QRDecompositionTest_Real()
        {
            //arrange
            double TOL = 10E-15;
            CMatrix m = new CMatrix(new Complex[,]
            {
                {12, -51, 4},
                {6, 167, -68},
                {-4, 24, -41}
            });

            //action
            CMatrix[] QR = CMatrix.QRDecomposition(m);
            CMatrix Q = QR[0];
            CMatrix R = QR[1];

            //assert
            Q.IsUnitary(TOL).Should().BeTrue("Q is not unitary");
            R.IsUpperTrapeze.Should().BeTrue("R is not upper trapeze");
            CMatrix.FuzzyEquals(m, Q * R, TOL).Should().BeTrue();
        }

        [Test]
        public void QRDecompositionTest_Complex()
        {
            //arrange
            double TOL = 10E-14;
            CMatrix m = new CMatrix(new Complex[,]
            {
                {new Complex(12, -3), new Complex(-51, 0), -Complex.I},
                {new Complex(6, 0), new Complex(-2, 167), new Complex(-68, 0)},
                {new Complex(-4, 0), new Complex(24, 0), new Complex(0, -41)}
            });

            //action
            CMatrix[] QR = CMatrix.QRDecomposition(m);
            CMatrix Q = QR[0];
            CMatrix R = QR[1];

            //assert
            Q.IsUnitary(TOL).Should().BeTrue("Q is not unitary");
            R.IsUpperTrapeze.Should().BeTrue("R is not upper trapeze");
            CMatrix.FuzzyEquals(m, Q * R, TOL).Should().BeTrue();
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
                CMatrix.FuzzyEquals(c * b, a, TOL).Should().BeTrue();
            }
        }

        [Test]
        public void SqrtTest()
        {
            //arrange
            double TOL = 10E-14;
            CMatrix m = new CMatrix(new Complex[,]
            {
                {new Complex(12, -3), new Complex(-51, 0), -Complex.I, new Complex(2, -8)},
                {new Complex(6, 0), new Complex(-2, 167), new Complex(-68, 0), new Complex(0, 5.3)},
                {new Complex(-4, 0), new Complex(24, 0), new Complex(0, -41), new Complex(2, 8.8)},
                {new Complex(15, 0), new Complex(24, 20), new Complex(-2.5, -41), new Complex(0, 0)}
            });

            //action
            CMatrix sq = CMatrix.Sqrt(m);

            //assert
            CMatrix.FuzzyEquals(sq * sq, m, TOL).Should().BeTrue();
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
                CMatrix.FuzzyEquals(m * inv_m, identity, TOL).Should().BeTrue();
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
                CMatrix.FuzzyEquals(a * x, b, TOL).Should().BeTrue();
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
                CMatrix.FuzzyEquals(test, zero, TOL).Should().BeTrue();
            }
        }
    }
}
