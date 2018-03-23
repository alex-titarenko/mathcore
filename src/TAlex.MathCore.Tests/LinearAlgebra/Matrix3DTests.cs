using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using TAlex.MathCore.LinearAlgebra;


namespace TAlex.MathCore.Tests.LinearAlgebra
{
    [TestFixture]
    public class Matrix3DTests
    {
        #region Multiply[Matrix, Scalar]

        [Test]
        public void MultiplyMatrixScalar_MatrixAndScalar_Matrix()
        {
            //arrange
            var m = new Matrix3D(2, 3, 1, 5, 4, -2, -3, 10, 11);
            const double scalar = 2;
            var expected = new Matrix3D(4, 6, 2, 10, 8, -4, -6, 20, 22);

            //action
            var actual = Matrix3D.Multiply(m, scalar);

            //assert
            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region Multiply[Matrix, Vector]

        [Test]
        public void MultiplyVectorMatrix_VectorAndMatrix_Vector()
        {
            //arrange
            var m = new Matrix3D(2, 1, 3, 3, 3, 2, 4, 1, 2);
            var v = new Vector3D(1, 2, 3);
            var expected = new Vector3D(13, 15, 12);

            //action
            var actual = Matrix3D.Multiply(m, v);

            //assert
            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region Multiply[Matrix, Matrix]

        [Test]
        public void MultiplyMatrixMatrix_TwoMatrices_Matrix()
        {
            //arrange
            var m1 = new Matrix3D(1, 2, 3, 4, 5, 6, 7, 8, 9);
            var m2 = new Matrix3D(9, 8, 7, 6, 5, 4, 3, 2, 1);
            var expected = new Matrix3D(30, 24, 18, 84, 69, 54, 138, 114, 90);

            //action
            var actual = Matrix3D.Multiply(m1, m2);

            //assert
            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region TransformToOrthogonalProjection

        [Test]
        public void TransformToOrthogonalProjection_Vector_Point()
        {
            //arrange
            var transformationMatrix = new Matrix3D(2, 3, 5, 1, 1, 18, 3, 0, 2);
            var v = new Vector3D(2, 3, 5);
            var expected = new Point(38, 95);

            //action
            var actual = transformationMatrix.TransformToOrthogonalProjection(v);

            //assert
            Assert.AreEqual(expected, actual);
        }

        #endregion
    }
}
