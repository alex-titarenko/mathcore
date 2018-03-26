using FluentAssertions;
using NUnit.Framework;


namespace TAlex.MathCore.LinearAlgebra.Tests
{
    [TestFixture]
    public class CEigenproblemTests
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
        public void EigenSystemTest()
        {
            //arrange
            int size = _m.RowCount;
            double TOL = 10E-13;
            CMatrix identity = CMatrix.Identity(size);

            //action
            CEigenproblem eigen = new CEigenproblem(_m, true);
            CMatrix eigenVals = eigen.Eigenvalues;
            CMatrix eigenVecs = eigen.Eigenvectors;

            //assert
            for (int i = 0; i < size; i++)
            {
                CMatrix v = (_m - eigenVals[i] * identity) * eigenVecs.GetColumn(i);
                
                foreach (Complex elem in v)
                {
                    NumericUtil.FuzzyEquals(Complex.Abs(elem), 0, TOL).Should().BeTrue();
                }
            }
        }
    }
}
