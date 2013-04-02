using System;


namespace TAlex.MathCore.NumericalAnalysis.NumericalIntegration
{
    /// <summary>
    /// Represents the abstract base class for classes implementing algorithms of numerical integration.
    /// </summary>
    public abstract class ComplexIntegrator
    {
        #region Methods

        /// <summary>
        /// Returns the numerical value of the definite integral complex function of one variable.
        /// </summary>
        /// <param name="integrand">A complex function to integrate of one variable.</param>
        /// <param name="lowerBound">The lower integration limit.</param>
        /// <param name="upperBound">The upper integration limit.</param>
        /// <returns>Approximate value of the definite integral.</returns>
        public abstract Complex Integrate(Function1Complex integrand, double lowerBound, double upperBound);

        #endregion
    }
}
