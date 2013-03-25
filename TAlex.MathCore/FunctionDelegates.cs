using System;


namespace TAlex.MathCore
{
    /// <summary>
    /// A delegate to a function that takes one real parameter and returns a real.
    /// </summary>
    /// <param name="value">A real number.</param>
    /// <returns>A real number.</returns>
    public delegate double Function1Real(double value);

    /// <summary>
    /// A delegate to a method that takes two real parameters and returns a real.
    /// </summary>
    /// <param name="value1">A first real number.</param>
    /// <param name="value2">A second real number.</param>
    /// <returns>A real number.</returns>
    public delegate double Function2Real(double value1, double value2);

    /// <summary>
    /// A delegate to a function that takes one complex parameter and returns a complex.
    /// </summary>
    /// <param name="value">A complex number.</param>
    /// <returns>A complex number.</returns>
    public delegate Complex Function1Complex(Complex value);

    /// <summary>
    /// A delegate to a method that takes one complex and two integer parameters and returns a complex.
    /// </summary>
    /// <param name="value">A complex number.</param>
    /// <param name="idx1">A first integer number.</param>
    /// <param name="idx2">A second integer number.</param>
    /// <returns>A complex number.</returns>
    public delegate Complex Function1Complex2Int(Complex value, int idx1, int idx2);
}
