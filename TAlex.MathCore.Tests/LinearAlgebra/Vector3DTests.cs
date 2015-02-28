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
    }
}
