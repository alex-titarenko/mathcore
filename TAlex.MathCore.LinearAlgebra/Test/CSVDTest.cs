using FluentAssertions;
using NUnit.Framework;
using System;
using TAlex.MathCore.LinearAlgebra;
using TAlex.MathCore.LinearAlgebra.Test.Helpers;


namespace TAlex.MathCore.LinearAlgebra.Test
{
    [TestFixture]
    public class CSVDTest
    {
        private RandomGenerator _rand = new RandomGenerator();


        [Test]
        public void SingularValueDecompositionTest()
        {
            int rowCount = 10;
            int columnCount = 10;
            int n = 5000;
            double TOL = 10E-11;

            CMatrix m = new CMatrix(rowCount, columnCount);

            for (int idx = 0; idx < n; idx++)
            {
                _rand.Fill(m, -1000, 1000, 3);

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
        }
    }
}
