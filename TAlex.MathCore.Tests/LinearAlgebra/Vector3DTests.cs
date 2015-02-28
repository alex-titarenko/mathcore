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
    public class Vector3DTests
    {
        #region Length

        [Test]
        public void Length_Vector_LengthValue()
        {
            //arrange
            var target = new Vector3D(1, 2, 2);
            const double expected = 3;

            //action
            var actual = target.Length;

            //assert
            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region LengthSquared

        [Test]
        public void LengthSquared_Vector_LengthSquaredValue()
        {
            //arrange
            var target = new Vector3D(1, 2, 2);
            const double expected = 9;

            //action
            var actual = target.LengthSquared;

            //assert
            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region Add

        [Test]
        public void Add_TwoVectors_SumOfVectors()
        {
            //arrange
            var v1 = new Vector3D(1, 3, -5);
            var v2 = new Vector3D(2, 11, 1);
            var expected = new Vector3D(3, 14, -4);

            //action
            var actual = Vector3D.Add(v1, v2);

            //assert
            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region Subtract

        [Test]
        public void Subtract_TwoVectors_SubtractionOfVectors()
        {
            //arrange
            var v1 = new Vector3D(1, 3, -5);
            var v2 = new Vector3D(2, 11, 1);
            var expected = new Vector3D(-1, -8, -6);

            //action
            var actual = Vector3D.Subtract(v1, v2);

            //assert
            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region Multiply

        [Test]
        public void Multiply_VectorAndScalar_Vector()
        {
            //arrange
            var v = new Vector3D(2, 5, -3);
            const double scalar = 3;
            var expected = new Vector3D(6, 15, -9);

            //action
            var actual = Vector3D.Multiply(v, scalar);

            //assert
            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region CrossProduct

        [Test]
        public void CrossProduct_TwoVectors_Vector()
        {
            //arrange
            var v1 = new Vector3D(2, -3, 1);
            var v2 = new Vector3D(-2, 1, 1);
            var expected = new Vector3D(-4, -4, -4);

            //action
            var actual = Vector3D.CrossProduct(v1, v2);

            //assert
            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region DotProduct

        [Test]
        public void DotProduct_TwoVectors_Scalar()
        {
            //arrange
            var v1 = new Vector3D(1, 3, -5);
            var v2 = new Vector3D(4, -2, -1);
            const double expected = 3;

            //action
            var actual = Vector3D.DotProduct(v1, v2);

            //assert
            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region Normalize

        [Test]
        public void Normalize_NotNormalizedVector_NormalizeVector()
        {
            //arrange
            var v = new Vector3D(0, -4, 2);
            var expected = new Vector3D(0, -4 / Math.Sqrt(20), 2 / Math.Sqrt(20));

            //action
            v.Normalize();

            //assert
            Assert.AreEqual(expected, v);
        }

        [Test]
        public void Normalize_NormalizedVector_NormalizedVector()
        {
            //arrange
            var v = new Vector3D(0, 1, 0);
            var expected = v;

            //action
            v.Normalize();

            //assert
            Assert.AreEqual(expected, v);
        }

        #endregion
    }
}
