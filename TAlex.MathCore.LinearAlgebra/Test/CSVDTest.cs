using FluentAssertions;
using NUnit.Framework;
using System;
using TAlex.MathCore.LinearAlgebra;


namespace TAlex.MathCore.LinearAlgebra.Test
{
    [TestFixture]
    public class CSVDTest
    {
        [Test]
        public void SingularValueDecompositionTest()
        {
            //arrange
            const double TOL = 10E-14;
            CMatrix m = new CMatrix(new Complex[,]
            {
                {new Complex(12, -3), new Complex(-51, 0), -Complex.I, new Complex(2, -8)},
                {new Complex(6, 0), new Complex(-2, 167), new Complex(-68, 0), new Complex(0, 5.3)},
                {new Complex(-4, 0), new Complex(24, 0), new Complex(0, -41), new Complex(2, 8.8)},
                {new Complex(15, 0), new Complex(24, 20), new Complex(-2.5, -41), new Complex(0, 0)}
            });

            //action
            CSVD svd = new CSVD(m);
            CMatrix u = svd.U;
            CMatrix s = svd.S;
            CMatrix vh = svd.VH;

            //assert
            u.IsUnitary(TOL).Should().BeTrue("U is not unitary");
            vh.IsUnitary(TOL).Should().BeTrue("VH is not unitary");
            CMatrix.FuzzyEquals(m, u * s * vh, TOL).Should().BeTrue();
        }

        [Test]
        public void PseudoInverseMatrixTest()
        {
            //arrange
            const double TOL = 10E-14;
            CMatrix m = new CMatrix(new Complex[,]
            {
                {new Complex(12, -3), new Complex(-51, 0), -Complex.I, new Complex(2, -8)},
                {new Complex(6, 0), new Complex(-2, 167), new Complex(-68, 0), new Complex(0, 5.3)},
                {new Complex(-4, 0), new Complex(24, 0), new Complex(0, -41), new Complex(2, 8.8)},
                {new Complex(15, 0), new Complex(24, 20), new Complex(-2.5, -41), new Complex(0, 0)}
            });

            //action
            CMatrix pinv = new CSVD(m).PseudoInverse();

            //assert
            CMatrix.FuzzyEquals(pinv, CMatrix.Inverse(m.Transpose * m) * m.Transpose, TOL).Should().BeTrue();
        }

        [Test]
        public void PseudoInverseMatrixTest2()
        {
            //arrange
            const double TOL = 10E-14;
            CMatrix m = new CMatrix(new Complex[,]
            {
                { 1, 1, 1, 1 },
                { 5, 7, 7, 9 }
            });

            CMatrix expected = new CMatrix(new Complex[,]
            {
                { 2, -0.25 },
                { 0.25, 0 },
                { 0.25, 0 },
                { -1.5, 0.25 }
            });

            //action
            CMatrix actual = new CSVD(m).PseudoInverse();

            //assert
            CMatrix.FuzzyEquals(actual, expected, TOL).Should().BeTrue();
        }
    }
}
