using System;


namespace TAlex.MathCore.NumericalAnalysis.EquationSolvers
{
    /// <summary>
    /// Represents the solver of equation of a real variable that uses the Brent's algorithm.
    /// </summary>
    /// <remarks>
    /// Brent's method is a complicated but popular root-finding
    /// algorithm combining the bisection method, the secant method and
    /// inverse quadratic interpolation.
    /// </remarks>
    public class BrentEquationSolver : RootBracketingEquationSolver
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the BrentEquationSolver class.
        /// </summary>
        public BrentEquationSolver()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the BrentEquationSolver class
        /// with the specified target function and bracketing interval.
        /// </summary>
        /// <param name="function">A delegate that specifies the target function.</param>
        /// <param name="lowerBound">The lower bound of the interval.</param>
        /// <param name="upperBound">The upper bound of the interval.</param>
        public BrentEquationSolver(Function1Real function, double lowerBound, double upperBound)
            : base(function, lowerBound, upperBound)
        {
        }

        /// <summary>
        /// Initializes a new instance of the BrentEquationSolver class
        /// with the specified target function, bracketing interval and tolerance.
        /// </summary>
        /// <param name="function">A delegate that specifies the target function.</param>
        /// <param name="lowerBound">The lower bound of the interval.</param>
        /// <param name="upperBound">The upper bound of the interval.</param>
        /// <param name="tolerance">The tolerance used in the convergence test.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// tolerance must be non negative.
        /// </exception>
        public BrentEquationSolver(Function1Real function, double lowerBound, double upperBound, double tolerance)
            : base(function, lowerBound, upperBound, tolerance)
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns the best approximation to the root of the nonlinear equation.
        /// </summary>
        /// <returns>The best approximation to the root.</returns>
        /// <exception cref="System.InvalidOperationException">
        /// The function values on the points LowerBound and UpperBound have the same signs.
        /// </exception>
        /// <exception cref="NotConvergenceException">
        /// The algorithm does not converged for a certain number of iterations.
        /// </exception>
        public override double Solve()
        {
            Function1Real f = Function;

            double a = LowerBound;
            double b = UpperBound;

            if (Math.Sign(f(a)) == Math.Sign(f(b)))
                throw new InvalidOperationException("The function values on the end points must be of opposite signs.");


            if (Math.Abs(f(a)) < Math.Abs(f(b)))
            {
                double temp = a;
                a = b;
                b = temp;
            }

            double c = a;
            bool bisect = true;
            double d = 0;

            for (int i = 0; i < MaxIterations; i++)
            {
                double s;

                if (f(a) != f(c) && f(b) != f(c))
                    s = (a * f(b) * f(c)) / ((f(a) - f(b)) * (f(a) - f(c))) +
                        (b * f(a) * f(c)) / ((f(b) - f(a)) * (f(b) - f(c))) +
                        (c * f(a) * f(b)) / ((f(c) - f(a)) * (f(c) - f(b)));
                else
                    s = b - f(b) * ((b - a) / (f(b) - f(a)));

                bool between = false;
                if (b > (3 * a + b) / 4)
                    between = (s >= (3 * a + b) / 4) && (s <= b);
                else
                    between = (s >= b) && (s <= (3 * a + b) / 4);

                if (!between ||
                    bisect && Math.Abs(s - b) >= Math.Abs(b - c) / 2 ||
                    !bisect && Math.Abs(s - b) >= Math.Abs(c - d) / 2 ||
                    bisect && Math.Abs(b - c) < Tolerance ||
                    !bisect && Math.Abs(c - d) < Tolerance)
                {
                    s = (a + b) / 2;
                    bisect = true;
                }
                else
                {
                    bisect = false;
                }

                d = c;
                c = b;

                if (f(a) * f(s) < 0)
                    b = s;
                else
                    a = s;

                if (Math.Abs(f(a)) < Math.Abs(f(b)))
                {
                    double temp = a;
                    a = b;
                    b = temp;
                }

                if (Math.Abs(f(s)) <= Tolerance)
                {
                    IterationsNeeded = i + 1;
                    return s;
                }
            }

            IterationsNeeded = -1;
            throw new NotConvergenceException();
        }

        #endregion
    }
}
