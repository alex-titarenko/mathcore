using System;


namespace TAlex.MathCore.NumericalAnalysis.EquationSolvers
{
    /// <summary>
    /// Represents the solver of equation of a complex variable that uses the Müller's algorithm.
    /// </summary>
    /// <remarks>
    /// Müller's method uses three points, constructs the parabola
    /// through these three points, and takes the intersection
    /// of the x-axis with the parabola to be the next approximation.
    /// </remarks>
    public class ComplexMullerEquationSolver : ComplexInitialGuessEquationSolver
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the ComplexMullerEquationSolver class.
        /// </summary>
        public ComplexMullerEquationSolver()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the ComplexMullerEquationSolver class
        /// with the specified target function and initial guess for the root.
        /// </summary>
        /// <param name="function">A delegate that specifies the target function.</param>
        /// <param name="initialGuess">The initial guess for the root.</param>
        public ComplexMullerEquationSolver(Func<Complex, Complex> function, Complex initialGuess)
            : base(function, initialGuess)
        {
        }

        /// <summary>
        /// Initializes a new instance of the ComplexMullerEquationSolver class
        /// with the specified target function, initial guess for the root and tolerance.
        /// </summary>
        /// <param name="function">A delegate that specifies the target function.</param>
        /// <param name="initialGuess">The initial guess for the root.</param>
        /// <param name="tolerance">The tolerance used in the convergence test.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// tolerance must be non negative.
        /// </exception>
        public ComplexMullerEquationSolver(Func<Complex, Complex> function, Complex initialGuess, double tolerance)
            : base(function, initialGuess, tolerance)
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns the best approximation to the root of the nonlinear equation.
        /// </summary>
        /// <returns>The best approximation to the root.</returns>
        /// <exception cref="NotConvergenceException">
        /// The algorithm does not converged for a certain number of iterations.
        /// </exception>
        public override Complex Solve()
        {
            if (Complex.Abs(Function(InitialGuess)) <= Tolerance)
            {
                IterationsNeeded = 0;
                return InitialGuess;
            }

            Func<Complex, Complex> f = Function;

            Complex x0 = InitialGuess;
            Complex x1 = x0 + 2.0 * Tolerance * ((Complex.Abs(x0) > 1.0) ? x0 : 1.0);
            Complex x2 = x1 + 2.0 * Tolerance * ((Complex.Abs(x1) > 1.0) ? x1 : 1.0);

            for (int i = 0; i < MaxIterations; i++)
            {
                Complex q = (x2 - x1) / (x1 - x0);

                Complex A = q * f(x2) - q * (1 + q) * f(x1) + q * q * f(x0);
                Complex B = (2 * q + 1) * f(x2) - (1 + q) * (1 + q) * f(x1) + q * q * f(x0);
                Complex C = (1 + q) * f(x2);

                Complex D = Complex.Sqrt(B * B - 4 * A * C);
                Complex den = (Complex.Abs(B + D) >= Complex.Abs(B - D)) ? B + D : B - D;

                Complex x3 = x2 - (x2 - x1) * ((2 * C) / den);

                if (Complex.Abs(f(x3)) <= Tolerance)
                {
                    IterationsNeeded = i + 1;
                    return x3;
                }

                x0 = x1;
                x1 = x2;
                x2 = x3;
            }

            IterationsNeeded = -1;
            throw new NotConvergenceException();
        }

        #endregion
    }
}
