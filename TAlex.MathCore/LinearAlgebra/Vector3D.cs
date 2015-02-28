using System;


namespace TAlex.MathCore.LinearAlgebra
{
    public struct Vector3D
    {
        #region Properties

        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        #endregion

        #region Constructors

        public Vector3D(double x, double y, double z)
            : this()
        {
            X = x;
            Y = y;
            Z = z;
        }

        #endregion

        #region Methods

        public static Vector3D Add(Vector3D v1, Vector3D v2)
        {
            Vector3D v = new Vector3D();
            v.X = v1.X + v2.X;
            v.Y = v1.Y + v2.Y;
            v.Z = v1.Z + v2.Z;

            return v;
        }

        public static Vector3D Sub(Vector3D v1, Vector3D v2)
        {
            Vector3D v = new Vector3D();
            v.X = v1.X - v2.X;
            v.Y = v1.Y - v2.Y;
            v.Z = v1.Z - v2.Z;

            return v;
        }

        public static Vector3D Mult(Vector3D v, double d)
        {
            Vector3D result = new Vector3D();
            result.X = v.X * d;
            result.Y = v.Y * d;
            result.Z = v.Z * d;

            return result;
        }

        public static Vector3D Mult(Vector3D v1, Vector3D v2)
        {
            Vector3D v = new Vector3D();
            v.X = v1.Y * v2.Z - v1.Z * v2.Y;
            v.Y = v1.Z * v2.X - v1.X * v2.Z;
            v.Z = v1.X * v2.Y - v1.Y * v2.X;

            return v;
        }

        public static double DotProduct(Vector3D v1, Vector3D v2)
        {
            return v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z;
        }

        public void Normalize()
        {
            double length = Math.Sqrt(X * X + Y * Y + Z * Z);

            X /= length;
            Y /= length;
            Z /= length;
        }

        #endregion

        #region Operators

        public static Vector3D operator +(Vector3D v1, Vector3D v2)
        {
            return Add(v1, v2);
        }

        public static Vector3D operator -(Vector3D v1, Vector3D v2)
        {
            return Sub(v1, v2);
        }

        public static Vector3D operator *(Vector3D v, double d)
        {
            return Mult(v, d);
        }

        public static Vector3D operator *(double d, Vector3D v)
        {
            return Mult(v, d);
        }

        public static Vector3D operator *(Vector3D v1, Vector3D v2)
        {
            return Mult(v1, v2);
        }

        #endregion
    }
}
