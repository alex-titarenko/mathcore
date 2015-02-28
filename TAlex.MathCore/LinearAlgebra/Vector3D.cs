using System;


namespace TAlex.MathCore.LinearAlgebra
{
    /// <summary>
    /// Represents a displacement in 3-D space.
    /// </summary>
    public struct Vector3D
    {
        #region Properties

        /// <summary>
        /// Gets or sets X component of the vector.
        /// </summary>
        public double X { get; set; }

        /// <summary>
        /// Gets or sets Y component of the vector.
        /// </summary>
        public double Y { get; set; }

        /// <summary>
        /// Gets or sets Z component of the vector.
        /// </summary>
        public double Z { get; set; }

        /// <summary>
        /// Gets the length of this vector.
        /// </summary>
        public double Length
        {
            get
            {
                return Math.Sqrt(LengthSquared);
            }
        }

        /// <summary>
        /// Gets the square of the length of this vector.
        /// </summary>
        public double LengthSquared
        {
            get
            {
                return X * X + Y * Y + Z * Z;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of a vector.
        /// </summary>
        /// <param name="x">X component of the vector.</param>
        /// <param name="y">Y component of the vector.</param>
        /// <param name="z">Z component of the vector.</param>
        public Vector3D(double x, double y, double z)
            : this()
        {
            X = x;
            Y = y;
            Z = z;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds two vectors.
        /// </summary>
        /// <param name="v1">The first vector to add.</param>
        /// <param name="v2">The second vector to add.</param>
        /// <returns>the sum of v1 and v2.</returns>
        public static Vector3D Add(Vector3D v1, Vector3D v2)
        {
            Vector3D v = new Vector3D();
            v.X = v1.X + v2.X;
            v.Y = v1.Y + v2.Y;
            v.Z = v1.Z + v2.Z;

            return v;
        }

        /// <summary>
        /// Subtracts a vector from a another vector.
        /// </summary>
        /// <param name="v1">The vector to be subtracted from.</param>
        /// <param name="v2">The vector to subtract from first vector.</param>
        /// <returns>the result of subtracting v2 from v1.</returns>
        public static Vector3D Subtract(Vector3D v1, Vector3D v2)
        {
            Vector3D v = new Vector3D();
            v.X = v1.X - v2.X;
            v.Y = v1.Y - v2.Y;
            v.Z = v1.Z - v2.Z;

            return v;
        }

        /// <summary>
        /// Multiplies the specified vector by the specified scalar.
        /// </summary>
        /// <param name="v">The vector to multiply.</param>
        /// <param name="scalar">The scalar to multiply.</param>
        /// <returns>the result of multiplying vector and scalar.</returns>
        public static Vector3D Multiply(Vector3D v, double scalar)
        {
            Vector3D result = new Vector3D();
            result.X = v.X * scalar;
            result.Y = v.Y * scalar;
            result.Z = v.Z * scalar;

            return result;
        }

        /// <summary>
        /// Calculates the cross product of two vectors.
        /// </summary>
        /// <param name="v1">The first vector to evaluate.</param>
        /// <param name="v2">The second vector to evaluate.</param>
        /// <returns>the cross product of vector1 and vector2.</returns>
        public static Vector3D CrossProduct(Vector3D v1, Vector3D v2)
        {
            Vector3D v = new Vector3D();
            v.X = v1.Y * v2.Z - v1.Z * v2.Y;
            v.Y = v1.Z * v2.X - v1.X * v2.Z;
            v.Z = v1.X * v2.Y - v1.Y * v2.X;

            return v;
        }

        /// <summary>
        /// Calculates the dot product of two vectors.
        /// </summary>
        /// <param name="v1">The first vector to evaluate.</param>
        /// <param name="v2">The second vector to evaluate.</param>
        /// <returns>the dot product of v1 and v2.</returns>
        public static double DotProduct(Vector3D v1, Vector3D v2)
        {
            return v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z;
        }

        /// <summary>
        /// Returns the string representation of the vector.
        /// </summary>
        /// <returns>the string representation of this vector.</returns>
        public override string ToString()
        {
            return String.Format("{{{0}; {1}; {2}}}", X, Y, Z);
        }

        /// <summary>
        /// Normalizes the specified vector.
        /// </summary>
        public void Normalize()
        {
            double length = Math.Sqrt(X * X + Y * Y + Z * Z);

            X /= length;
            Y /= length;
            Z /= length;
        }

        #endregion

        #region Operators

        /// <summary>
        /// Adds two vectors.
        /// </summary>
        /// <param name="v1">The first vector to add.</param>
        /// <param name="v2">The second vector to add.</param>
        /// <returns>the sum of v1 and v2.</returns>
        public static Vector3D operator +(Vector3D v1, Vector3D v2)
        {
            return Add(v1, v2);
        }

        /// <summary>
        /// Subtracts a vector from a another vector.
        /// </summary>
        /// <param name="v1">The vector to be subtracted from.</param>
        /// <param name="v2">The vector to subtract from first vector.</param>
        /// <returns>the result of subtracting v2 from v1.</returns>
        public static Vector3D operator -(Vector3D v1, Vector3D v2)
        {
            return Subtract(v1, v2);
        }

        /// <summary>
        /// Multiplies the specified vector by the specified scalar.
        /// </summary>
        /// <param name="v">The vector to multiply.</param>
        /// <param name="scalar">The scalar to multiply.</param>
        /// <returns>the result of multiplying vector and scalar.</returns>
        public static Vector3D operator *(Vector3D v, double scalar)
        {
            return Multiply(v, scalar);
        }

        /// <summary>
        /// Multiplies the specified scalar by the specified vector.
        /// </summary>
        /// <param name="scalar">The scalar to multiply.</param>
        /// <param name="v">The vector to multiply.</param>
        /// <returns>the result of multiplying scalar and vector.</returns>
        public static Vector3D operator *(double scalar, Vector3D v)
        {
            return Multiply(v, scalar);
        }

        #endregion
    }
}
