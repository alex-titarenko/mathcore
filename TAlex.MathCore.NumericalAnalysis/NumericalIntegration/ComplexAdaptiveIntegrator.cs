using System;


namespace TAlex.MathCore.NumericalAnalysis.NumericalIntegration
{
    /// <summary>
    /// Represents the adaptive method of numerical integration.
    /// </summary>
    public class ComplexAdaptiveIntegrator : ComplexCompositeIntegrator
    {
        #region Fields

        private const int _maxRecursionDepth = 1000;

        private Quadrature _quadr = GaussKronrodQuadratures.GaussKronrod21Rule;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the integration rule (quadrature formula).
        /// </summary>
        public Quadrature IntegrationRule
        {
            get
            {
                return _quadr;
            }

            set
            {
                _quadr = value;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the ComplexAdaptiveIntegrator class.
        /// </summary>
        public ComplexAdaptiveIntegrator()
        {
            MaxIterations = 8000;
            Tolerance = 1E-9;
        }

        /// <summary>
        /// Initializes a new instance of the ComplexAdaptiveIntegrator class.
        /// </summary>
        /// <param name="quadrature">A complex function representing the quadrature formula of integration.</param>
        public ComplexAdaptiveIntegrator(Quadrature quadrature)
            : this()
        {
            _quadr = quadrature;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns the numerical value of the definite integral complex function of one variable.
        /// </summary>
        /// <returns>Approximate value of the definite integral.</returns>
        /// <exception cref="NotConvergenceException">
        /// The algorithm does not converged for a certain number of iterations.
        /// </exception>
        public override Complex Integrate(Func<Complex, Complex> integrand, double lowerBound, double upperBound)
        {
            if (lowerBound == upperBound)
            {
                return Complex.Zero;
            }

            int sign = 1;

            if (lowerBound > upperBound)
            {
                double temp = lowerBound;
                lowerBound = upperBound;
                upperBound = temp;
                sign = -1;
            }

            // Testing the limits to infinity
            if (double.IsNegativeInfinity(lowerBound) || double.IsPositiveInfinity(upperBound))
            {
                if (double.IsNegativeInfinity(lowerBound))
                {
                    if (double.IsPositiveInfinity(upperBound))
                        integrand = new TransformationLimits(integrand, LimitType.BothBoundsInfinity, 0.0).FiniteIntegrand;
                    else
                        integrand = new TransformationLimits(integrand, LimitType.LowerBoundInfinity, upperBound).FiniteIntegrand;
                }
                else if (double.IsPositiveInfinity(upperBound))
                {
                    integrand = new TransformationLimits(integrand, LimitType.UpperBoundInfinity, lowerBound).FiniteIntegrand;
                }

                lowerBound = 0.0;
                upperBound = 1.0;
            }

            IterationsNeeded = 0;

            Complex Sab = _quadr(integrand, lowerBound, upperBound);
            return sign * RecursionProcedure(integrand, lowerBound, upperBound, Sab, Tolerance, _maxRecursionDepth);
        }

        private Complex RecursionProcedure(Func<Complex, Complex> integrand, double a, double b, Complex Sab, double tol, int trace)
        {
            double c = (a + b) / 2;

            Complex Sac = _quadr(integrand, a, c);
            Complex Scb = _quadr(integrand, c, b);

            if (Complex.Abs(Sab - Sac - Scb) <= tol)
                return Sac + Scb;

            IterationsNeeded++;

            if (IterationsNeeded >= MaxIterations || trace <= 0)
                throw new NotConvergenceException("Calculation does not converge to a solution.");

            return RecursionProcedure(integrand, a, c, Sac, tol / 2, trace - 1) +
                RecursionProcedure(integrand, c, b, Scb, tol / 2, trace - 1);
        }

        #endregion

        #region Nested types

        private enum LimitType
        {
            LowerBoundInfinity,
            UpperBoundInfinity,
            BothBoundsInfinity
        }

        /// <summary>
        /// Represents transforms the infinite interval to a finite interval.
        /// </summary>
        private class TransformationLimits
        {
            #region Fields

            private Func<Complex, Complex> _infinityIntegrand;

            private double _limit;

            private Func<Complex, Complex> _finiteIntegrand;

            #endregion

            #region Properties

            public Func<Complex, Complex> FiniteIntegrand
            {
                get
                {
                    return _finiteIntegrand;
                }
            }

            #endregion

            #region Constructors

            public TransformationLimits(Func<Complex, Complex> infinityIntegrand, LimitType limitType, double limit)
            {
                _infinityIntegrand = infinityIntegrand;
                _limit = limit;

                switch (limitType)
                {
                    case LimitType.LowerBoundInfinity:
                        _finiteIntegrand = new Func<Complex, Complex>(LowerBoundInfinityTransform);
                        break;

                    case LimitType.BothBoundsInfinity:
                        _finiteIntegrand = new Func<Complex, Complex>(BothBoundsInfinityTransform);
                        break;

                    case LimitType.UpperBoundInfinity:
                        _finiteIntegrand = new Func<Complex, Complex>(UpperBoundInfinityTransform);
                        break;
                }
            }

            #endregion

            #region Methods

            private Complex LowerBoundInfinityTransform(Complex value)
            {
                Complex x = _limit - ((1.0 - value) / value);
                return (_infinityIntegrand(x) / value) / value;
            }

            private Complex BothBoundsInfinityTransform(Complex value)
            {
                Complex x = (1.0 - value) / value;
                Complex temp = _infinityIntegrand(x) + _infinityIntegrand(-x);
                return (temp / value) / value;
            }

            private Complex UpperBoundInfinityTransform(Complex value)
            {
                Complex x = _limit + ((1.0 - value) / value);
                return (_infinityIntegrand(x) / value) / value;
            }

            #endregion
        }

        #endregion
    }

    /// <summary>
    /// A delegate to a function that represents the quadrature formula of integration.
    /// </summary>
    /// <param name="integrand">A complex function to integrate of one variable.</param>
    /// <param name="lowerBound">The lower integration limit.</param>
    /// <param name="upperBound">The upper integration limit.</param>
    /// <returns>The numerical value of the definite integral.</returns>
    public delegate Complex Quadrature(Func<Complex, Complex> integrand, double lowerBound, double upperBound);
}
