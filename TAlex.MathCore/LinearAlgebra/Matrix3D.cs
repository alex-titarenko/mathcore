using System;


namespace TAlex.MathCore.LinearAlgebra
{
    /// <summary>
    /// Represents a 3x3 matrix used for transformations in 3-D space.
    /// </summary>
    public struct Matrix3D
    {
        #region Properties

        /// <summary>
        /// Gets or sets the value of the first row and first column of the matrix.
        /// </summary>
        public double M11 { get; set; }

        /// <summary>
        /// Gets or sets the value of the first row and second column of the matrix.
        /// </summary>
        public double M12 { get; set; }

        /// <summary>
        /// Gets or sets the value of the first row and third column of the matrix.
        /// </summary>
        public double M13 { get; set; }

        /// <summary>
        /// Gets or sets the value of the second row and first column of the matrix.
        /// </summary>
        public double M21 { get; set; }
        
        /// <summary>
        /// Gets or sets the value of the second row and second column of the matrix.
        /// </summary>
        public double M22 { get; set; }

        /// <summary>
        /// Gets or sets the value of the second row and third column of the matrix.
        /// </summary>
        public double M23 { get; set; }

        /// <summary>
        /// Gets or sets the value of the third row and first column of the matrix.
        /// </summary>
        public double M31 { get; set; }

        /// <summary>
        /// Gets or sets the value of the third row and second column of the matrix.
        /// </summary>
        public double M32 { get; set; }

        /// <summary>
        /// Gets or sets the value of the third row and third column of the matrix.
        /// </summary>
        public double M33 { get; set; }

        /// <summary>
        /// Gets the identity matrix.
        /// </summary>
        public static Matrix3D Identity
        {
            get 
            {
                return new Matrix3D(1, 0, 0, 0, 1, 0, 0, 0, 1);
            }
        }
            
        /// <summary>
        /// Gets the orthogonal projection matrix.
        /// </summary>
        public static Matrix3D OrthogonalProjectionMatrix
        {
            get
            {
                return new Matrix3D(1, 0, 0, 0, 1, 0, 0, 0, 0);
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor that sets matrix's initial values.
        /// </summary>
        /// <param name="m11">Value of the (1,1) field of the new matrix.</param>
        /// <param name="m12">Value of the (1,2) field of the new matrix.</param>
        /// <param name="m13">Value of the (1,3) field of the new matrix.</param>
        /// <param name="m21">Value of the (2,1) field of the new matrix.</param>
        /// <param name="m22">Value of the (2,2) field of the new matrix.</param>
        /// <param name="m23">Value of the (2,3) field of the new matrix.</param>
        /// <param name="m31">Value of the (3,1) field of the new matrix.</param>
        /// <param name="m32">Value of the (3,2) field of the new matrix.</param>
        /// <param name="m33">Value of the (3,3) field of the new matrix.</param>
        public Matrix3D(double m11, double m12, double m13, double m21, double m22, double m23, double m31, double m32, double m33)
            : this()
        {
            M11 = m11;
            M12 = m12;
            M13 = m13;

            M21 = m21;
            M22 = m22;
            M23 = m23;

            M31 = m31;
            M32 = m32;
            M33 = m33;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Multiplies matrix by scalar.
        /// </summary>
        /// <param name="m">The matrix to multiply.</param>
        /// <param name="scalar">The scalar value to multiply.</param>
        /// <returns>the result of multiplication.</returns>
        public static Matrix3D Multiply(Matrix3D m, double scalar)
        {
            Matrix3D result = new Matrix3D();
            result.M11 = m.M11 * scalar;
            result.M12 = m.M12 * scalar;
            result.M13 = m.M13 * scalar;

            result.M21 = m.M21 * scalar;
            result.M22 = m.M22 * scalar;
            result.M23 = m.M23 * scalar;

            result.M31 = m.M31 * scalar;
            result.M32 = m.M32 * scalar;
            result.M33 = m.M33 * scalar;

            return result;
        }

        /// <summary>
        /// Multiplies matrix by vector.
        /// </summary>
        /// <param name="m">The matrix to multiply.</param>
        /// <param name="v">The vector by which the matrix is multiplied.</param>
        /// <returns>the result of multiplication.</returns>
        public static Vector3D Multiply(Matrix3D m, Vector3D v)
        {
            Vector3D result = new Vector3D();

            result.X = m.M11 * v.X + m.M12 * v.Y + m.M13 * v.Z;
            result.Y = m.M21 * v.X + m.M22 * v.Y + m.M23 * v.Z;
            result.Z = m.M31 * v.X + m.M32 * v.Y + m.M33 * v.Z;

            return result;
        }

        /// <summary>
        /// Multiplies the specified matrices.
        /// </summary>
        /// <param name="m1">The matrix to multiply.</param>
        /// <param name="m2">The matrix by which the first matrix is multiplied.</param>
        /// <returns>the result of multiplication.</returns>
        public static Matrix3D Multiply(Matrix3D m1, Matrix3D m2)
        {
            Matrix3D result = new Matrix3D();

            result.M11 = m1.M11 * m2.M11 + m1.M12 * m2.M21 + m1.M13 * m2.M31;
            result.M12 = m1.M11 * m2.M12 + m1.M12 * m2.M22 + m1.M13 * m2.M32;
            result.M13 = m1.M11 * m2.M13 + m1.M12 * m2.M23 + m1.M13 * m2.M33;

            result.M21 = m1.M21 * m2.M11 + m1.M22 * m2.M21 + m1.M23 * m2.M31;
            result.M22 = m1.M21 * m2.M12 + m1.M22 * m2.M22 + m1.M23 * m2.M32;
            result.M23 = m1.M21 * m2.M13 + m1.M22 * m2.M23 + m1.M23 * m2.M33;

            result.M31 = m1.M31 * m2.M11 + m1.M32 * m2.M21 + m1.M33 * m2.M31;
            result.M32 = m1.M31 * m2.M12 + m1.M32 * m2.M22 + m1.M33 * m2.M32;
            result.M33 = m1.M31 * m2.M13 + m1.M32 * m2.M23 + m1.M33 * m2.M33;

            return result;
        }

        /// <summary>
        /// Returns scale matrix by scale factor.
        /// </summary>
        /// <param name="scale">Scale factor.</param>
        /// <returns>scale matrix.</returns>
        public static Matrix3D ScaleMatrix(double scale)
        {
            return ScaleMatrix(scale, scale, scale);
        }

        /// <summary>
        /// Returns scale matrix by coresponding values of x, y, z factors.
        /// </summary>
        /// <param name="scaleX">Scale factor by axis X.</param>
        /// <param name="scaleY">Scale factor by axis Y.</param>
        /// <param name="scaleZ">Scale factor by axis Z.</param>
        /// <returns>scale matrix.</returns>
        public static Matrix3D ScaleMatrix(double scaleX, double scaleY, double scaleZ)
        {
            return new Matrix3D(scaleX, 0, 0, 0, scaleY, 0, 0, 0, scaleZ);
        }

        /// <summary>
        /// Returns rotation matrix by coresponding values of alpha, double beta, double gamma angles.
        /// </summary>
        /// <param name="alpha">Angle of rotation by axis X.</param>
        /// <param name="beta">Angle of rotation by axis Y.</param>
        /// <param name="gamma">Angle of rotation by axis Z.</param>
        /// <returns>rotation matrix.</returns>
        public static Matrix3D RotationMatrix(double alpha, double beta, double gamma)
        {
            return RotationMatrixX(alpha) * RotationMatrixY(beta) * RotationMatrixZ(gamma);
        }

        /// <summary>
        /// Returns rotation matrix by axis X.
        /// </summary>
        /// <param name="angle">Angle of rotation by axis X.</param>
        /// <returns>rotation matrix.</returns>
        public static Matrix3D RotationMatrixX(double angle)
        {
            Matrix3D m = new Matrix3D();

            m.M11 = 1;
            m.M22 = Math.Cos(angle);
            m.M23 = -Math.Sin(angle);
            m.M32 = Math.Sin(angle);
            m.M33 = Math.Cos(angle);

            return m;
        }

        /// <summary>
        /// Returns rotation matrix by axis Y.
        /// </summary>
        /// <param name="angle">Angle of rotation by axis Y.</param>
        /// <returns>rotation matrix.</returns>
        public static Matrix3D RotationMatrixY(double angle)
        {
            Matrix3D m = new Matrix3D();

            m.M11 = Math.Cos(angle);
            m.M13 = Math.Sin(angle);
            m.M22 = 1;
            m.M31 = -Math.Sin(angle);
            m.M33 = Math.Cos(angle);

            return m;
        }

        /// <summary>
        /// Returns rotation matrix by axis Z.
        /// </summary>
        /// <param name="angle">Angle of rotation by axis Z.</param>
        /// <returns>rotation matrix.</returns>
        public static Matrix3D RotationMatrixZ(double angle)
        {
            Matrix3D m = new Matrix3D();

            m.M11 = Math.Cos(angle);
            m.M12 = -Math.Sin(angle);
            m.M21 = Math.Sin(angle);
            m.M22 = Math.Cos(angle);
            m.M33 = 1;

            return m;
        }

        /// <summary>
        /// Returns transformation matrix which is combination of scale and rotation matrices.
        /// </summary>
        /// <param name="scale">Scale factor.</param>
        /// <param name="alpha">Angle of rotation by axis X.</param>
        /// <param name="beta">Angle of rotation by axis Y.</param>
        /// <param name="gamma">Angle of rotation by axis Z.</param>
        /// <returns>transformation matrix.</returns>
        public static Matrix3D TransformMatrix(double scale, double alpha, double beta, double gamma)
        {
            return ScaleMatrix(scale) * RotationMatrix(alpha, beta, gamma);
        }

        /// <summary>
        /// Returns transformation matrix which is combination of scale and rotation matrices.
        /// </summary>
        /// <param name="scaleX">Scale factor by axis X.</param>
        /// <param name="scaleY">Scale factor by axis Y.</param>
        /// <param name="scaleZ">Scale factor by axis Z.</param>
        /// <param name="alpha">Angle of rotation by axis X.</param>
        /// <param name="beta">Angle of rotation by axis Y.</param>
        /// <param name="gamma">Angle of rotation by axis Z.</param>
        /// <returns>transformation matrix.</returns>
        public static Matrix3D TransformMatrix(double scaleX, double scaleY, double scaleZ, double alpha, double beta, double gamma)
        {
            return ScaleMatrix(scaleX, scaleY, scaleZ) * RotationMatrix(alpha, beta, gamma);
        }

        /// <summary>
        /// Transforms the specified vector by this matrix.
        /// </summary>
        /// <param name="v">The vector for transformation.</param>
        /// <returns>result of transforming vector by this Matrix3D.</returns>
        public Vector3D Transform(Vector3D v)
        {
            return this * v;
        }

        /// <summary>
        /// Transforms to orthogonal projection the specified vector by this matrix.
        /// </summary>
        /// <param name="v">The vector for transformation.</param>
        /// <returns>result of transforming vector by this Matrix3D.</returns>
        public Point TransformToOrthogonalProjection(Vector3D v)
        {
            Vector3D c = Transform(v);
            Vector3D b = OrthogonalProjectionMatrix * c;

            return new Point(b.X, b.Y);
        }

        #endregion

        #region Operators

        /// <summary>
        /// Multiplies matrix by scalar.
        /// </summary>
        /// <param name="m">The matrix to multiply.</param>
        /// <param name="scalar">The scalar value to multiply.</param>
        /// <returns>the result of multiplication.</returns>
        public static Matrix3D operator *(Matrix3D m, double scalar)
        {
            return Multiply(m, scalar);
        }

        /// <summary>
        /// Multiplies scalar by matrix.
        /// </summary>
        /// <param name="scalar">The scalar value to multiply.</param>
        /// <param name="m">The matrix to multiply.</param>
        /// <returns>the result of multiplication.</returns>
        public static Matrix3D operator *(double scalar, Matrix3D m)
        {
            return Multiply(m, scalar);
        }

        /// <summary>
        /// Multiplies matrix by vector.
        /// </summary>
        /// <param name="m">The matrix to multiply.</param>
        /// <param name="v">The vector by which the matrix is multiplied.</param>
        /// <returns>the result of multiplication.</returns>
        public static Vector3D operator *(Matrix3D m, Vector3D v)
        {
            return Multiply(m, v);
        }

        /// <summary>
        /// Multiplies the specified matrices.
        /// </summary>
        /// <param name="m1">The matrix to multiply.</param>
        /// <param name="m2">The matrix by which the first matrix is multiplied.</param>
        /// <returns>the result of multiplication.</returns>
        public static Matrix3D operator *(Matrix3D m1, Matrix3D m2)
        {
            return Multiply(m1, m2);
        }

        #endregion
    }
}
