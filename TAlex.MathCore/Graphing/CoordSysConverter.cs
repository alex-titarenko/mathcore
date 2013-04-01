using System;


namespace TAlex.MathCore.Graphing
{
    /// <summary>
    /// Contains methods for transforming a Cartesian coordinate system.
    /// </summary>
    public static class CoordSysConverter
    {
        #region Methods

        /// <summary>
        /// Transforms Cartesian coordinates to polar.
        /// </summary>
        /// <param name="x">A real number representing the x-coordinate.</param>
        /// <param name="y">A real number representing the y-coordinate.</param>
        /// <returns>The two-element array containing the polar coordinates (r, θ).</returns>
        public static double[] CartesianToPolar(double x, double y)
        {
            double[] pol = new double[2];
            CartesianToPolar(x, y, out pol[0], out pol[1]);

            return pol;
        }

        /// <summary>
        /// Transforms Cartesian coordinates to polar.
        /// </summary>
        /// <param name="x">A real number representing the x-coordinate.</param>
        /// <param name="y">A real number representing the y-coordinate.</param>
        /// <param name="r">Output parameter, the real number representing the polar radius.</param>
        /// <param name="theta">Output parameter, the real number representing the polar angle.</param>
        public static void CartesianToPolar(double x, double y, out double r, out double theta)
        {
            r = ExMath.Hypot(x, y);
            theta = Math.Atan2(y, x);
        }

        /// <summary>
        /// Transforms polar coordinates to Cartesian.
        /// </summary>
        /// <param name="r">A real number representing the polar radius.</param>
        /// <param name="theta">A real number representing the polar angle.</param>
        /// <returns>The two-element array containing the Cartesian coordinates (x, y).</returns>
        public static double[] PolarToCartesian(double r, double theta)
        {
            double[] xy = new double[2];
            PolarToCartesian(r, theta, out xy[0], out xy[1]);

            return xy;
        }

        /// <summary>
        /// Transforms polar coordinates to Cartesian.
        /// </summary>
        /// <param name="r">A real number representing the polar radius.</param>
        /// <param name="theta">A real number representing the polar angle.</param>
        /// <param name="x">Output parameter, the real number representing the x-coordinate.</param>
        /// <param name="y">Output parameter, the real number representing the y-coordinate.</param>
        public static void PolarToCartesian(double r, double theta, out double x, out double y)
        {
            x = r * Math.Cos(theta);
            y = r * Math.Sin(theta);
        }

        /// <summary>
        /// Transforms Cartesian coordinates to spherical.
        /// </summary>
        /// <param name="x">A real number representing the x-coordinate.</param>
        /// <param name="y">A real number representing the y-coordinate.</param>
        /// <param name="z">A real number representing the z-coordinate.</param>
        /// <returns>The three-element array containing the spherical coordinates (r, θ, φ).</returns>
        public static double[] CartesianToSpherical(double x, double y, double z)
        {
            double[] sph = new double[3];
            CartesianToSpherical(x, y, z, out sph[0], out sph[1], out sph[2]);

            return sph;
        }

        /// <summary>
        /// Transforms Cartesian coordinates to spherical.
        /// </summary>
        /// <param name="x">A real number representing the x-coordinate.</param>
        /// <param name="y">A real number representing the y-coordinate.</param>
        /// <param name="z">A real number representing the z-coordinate.</param>
        /// <param name="r">Output parameter, the real number representing the radial distance.</param>
        /// <param name="theta">Output parameter, the real number representing the azimuthal angle.</param>
        /// <param name="phi">Output parameter, the real number representing the polar angle.</param>
        public static void CartesianToSpherical(double x, double y, double z, out double r, out double theta, out double phi)
        {
            r = Math.Sqrt(x * x + y * y + z * z);
            theta = Math.Atan2(y, x);
            phi = Math.Acos(z / r);
        }

        /// <summary>
        /// Transforms spherical coordinates to Cartesian.
        /// </summary>
        /// <param name="r">A real number representing the radial distance.</param>
        /// <param name="theta">A real number representing the azimuthal angle.</param>
        /// <param name="phi">A real number representing the polar angle.</param>
        /// <returns>The three-element array containing the Cartesian coordinates (x, y, z).</returns>
        public static double[] SphericalToCartesian(double r, double theta, double phi)
        {
            double[] xyz = new double[3];
            SphericalToCartesian(r, theta, phi, out xyz[0], out xyz[1], out xyz[2]);

            return xyz;
        }

        /// <summary>
        /// Transforms spherical coordinates to Cartesian.
        /// </summary>
        /// <param name="r">A real number representing the radial distance.</param>
        /// <param name="theta">A real number representing the azimuthal angle.</param>
        /// <param name="phi">A real number representing the polar angle.</param>
        /// <param name="x">Output parameter, the real number representing the x-coordinate.</param>
        /// <param name="y">Output parameter, the real number representing the y-coordinate.</param>
        /// <param name="z">Output parameter, the real number representing the z-coordinate.</param>
        public static void SphericalToCartesian(double r, double theta, double phi, out double x, out double y, out double z)
        {
            x = r * Math.Cos(theta) * Math.Sin(phi);
            y = r * Math.Sin(theta) * Math.Sin(phi);
            z = r * Math.Cos(phi);
        }

        /// <summary>
        /// Transforms Cartesian coordinates to cylindrical.
        /// </summary>
        /// <param name="x">A real number representing the x-coordinate.</param>
        /// <param name="y">A real number representing the y-coordinate.</param>
        /// <param name="z">A real number representing the z-coordinate.</param>
        /// <returns>The three-element array containing the cylindrical coordinates (r, θ, z).</returns>
        public static double[] CartesianToCylindrical(double x, double y, double z)
        {
            double[] cyl = new double[3];
            CartesianToCylindrical(x, y, z, out cyl[0], out cyl[1]);
            cyl[2] = z;

            return cyl;
        }

        /// <summary>
        /// Transforms Cartesian coordinates to cylindrical.
        /// </summary>
        /// <param name="x">A real number representing the x-coordinate.</param>
        /// <param name="y">A real number representing the y-coordinate.</param>
        /// <param name="z">A real number representing the z-coordinate.</param>
        /// <param name="r">Output parameter, the real number representing the radial distance.</param>
        /// <param name="theta">Output parameter, the real number representing the azimuthal angle.</param>
        public static void CartesianToCylindrical(double x, double y, double z, out double r, out double theta)
        {
            r = ExMath.Hypot(x, y);
            theta = Math.Atan2(y, x);
        }

        /// <summary>
        /// Transforms cylindrical coordinates to Cartesian.
        /// </summary>
        /// <param name="r">A real number representing the radial distance.</param>
        /// <param name="theta">A real number representing the azimuthal angle.</param>
        /// <param name="z">A real number representing the height.</param>
        /// <returns>The three-element array containing the Cartesian coordinates (x, y, z).</returns>
        public static double[] CylindricalToCartesian(double r, double theta, double z)
        {
            double[] xyz = new double[3];
            CylindricalToCartesian(r, theta, z, out xyz[0], out xyz[1]);
            xyz[2] = z;

            return xyz;
        }

        /// <summary>
        /// Transforms cylindrical coordinates to Cartesian.
        /// </summary>
        /// <param name="r">A real number representing the radial distance.</param>
        /// <param name="theta">A real number representing the azimuthal angle.</param>
        /// <param name="z">A real number representing the height.</param>
        /// <param name="x">Output parameter, the real number representing the x-coordinate.</param>
        /// <param name="y">Output parameter, the real number representing the y-coordinate.</param>
        public static void CylindricalToCartesian(double r, double theta, double z, out double x, out double y)
        {
            x = r * Math.Cos(theta);
            y = r * Math.Sin(theta);
        }

        #endregion
    }
}
