using FluentAssertions;
using NUnit.Framework;
using System;
using TAlex.MathCore;
using TAlex.MathCore.LinearAlgebra;


namespace TAlex.MathCore.LinearAlgebra.Test
{    
    [TestFixture]
    public class CMatrixTest
    {
        private CMatrix _m;

        [SetUp]
        public void SetUp()
        {
            _m = new CMatrix(new Complex[,]
            {
                {new Complex(12, -3), new Complex(-51, 0), -Complex.I, new Complex(2, -8)},
                {new Complex(6, 0), new Complex(-2, 167), new Complex(-68, 0), new Complex(0, 5.3)},
                {new Complex(-4, 0), new Complex(24, 0), new Complex(0, -41), new Complex(2, 8.8)},
                {new Complex(15, 0), new Complex(24, 20), new Complex(-2.5, -41), new Complex(0, 0)}
            });
        }


        [Test]
        public void LUPDecompositionTest()
        {
            //arrange
            double TOL = 10E-15;
            int size = _m.RowCount;
            CMatrix identity = CMatrix.Identity(size);

            //action
            CMatrix[] LUP = CMatrix.LUPDecomposition(_m);
            CMatrix P = LUP[0];
            CMatrix L = LUP[1];
            CMatrix U = LUP[2];

            //assert
            L.IsLowerTriangular.Should().BeTrue();
            U.IsUpperTriangular.Should().BeTrue();
            CMatrix.FuzzyEquals(P * _m, L * U, TOL).Should().BeTrue();
        }

        [Test]
        public void QRDecompositionTest()
        {
            //arrange
            double TOL = 10E-14;

            //action
            CMatrix[] QR = CMatrix.QRDecomposition(_m);
            CMatrix Q = QR[0];
            CMatrix R = QR[1];

            //assert
            Q.IsUnitary(TOL).Should().BeTrue("Q is not unitary");
            R.IsUpperTrapeze.Should().BeTrue("R is not upper trapeze");
            CMatrix.FuzzyEquals(_m, Q * R, TOL).Should().BeTrue();
        }

        [Test]
        public void DivTest()
        {
            //arrange
            double TOL = 10E-14;
            CMatrix m2 = new CMatrix(new Complex[,]
            {
                {new Complex(122, 5.8), new Complex(0, 0), new Complex(2, 24), new Complex(0, -8)},
                {new Complex(16, 0), new Complex(164, 167), new Complex(0.25, 0.4), new Complex(10, -5)},
                {new Complex(-44, 2.28), new Complex(2.4, 5.6), new Complex(0, 4.1), new Complex(-2, 0)},
                {new Complex(185, -14), new Complex(2.4, 2), new Complex(25.14, 39.5), new Complex(122, 122)}
            });

            //action
            CMatrix c = CMatrix.Divide(_m, m2);

            //assert
            CMatrix.FuzzyEquals(c * m2, _m, TOL).Should().BeTrue();
        }

        [Test]
        public void SqrtTest()
        {
            //arrange
            double TOL = 10E-14;

            //action
            CMatrix sq = CMatrix.Sqrt(_m);

            //assert
            CMatrix.FuzzyEquals(sq * sq, _m, TOL).Should().BeTrue();
        }

        [Test]
        public void InverseTest()
        {
            //arrange
            const double TOL = 10E-16;
            CMatrix identity = CMatrix.Identity(_m.RowCount);

            //action
            CMatrix inv_m = CMatrix.Inverse(_m);

            //assert
            CMatrix.FuzzyEquals(_m * inv_m, identity, TOL).Should().BeTrue();
        }

        [Test]
        public void SolveTest()
        {
            //assert
            double TOL = 10E-15;
            CMatrix b = new CMatrix(new Complex[]
            {
                new Complex(3, 5), new Complex(-8.5, 0), new Complex(0.5, -144), new Complex(0, 2)
            });

            //action
            CMatrix x = CMatrix.Solve(_m, b);

            //assert
            CMatrix.FuzzyEquals(_m * x, b, TOL).Should().BeTrue();
        }

        [Test]
        public void CharacteristicPolynomialTest()
        {
            //arrange
            double TOL = 10E-6;
            CMatrix zero = new CMatrix(_m.RowCount, _m.ColumnCount);

            //action
            CPolynomial poly = CMatrix.CharacteristicPolynomial(_m);
            CMatrix test = poly.Evaluate(_m);

            //assert
            CMatrix.FuzzyEquals(test, zero, TOL).Should().BeTrue();
        }
    }
}
