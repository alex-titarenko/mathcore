using System;


namespace TAlex.MathCore.NumericalAnalysis.NumericalIntegration
{
    /// <summary>
    /// Represents the Newton-Cotes quadrature formulas.
    /// </summary>
    public static class NewtonCotesQuadratures
    {
        #region Methods

        /// <summary>
        /// Returns the numerical value of the definite integral using the Trapezoid rule.
        /// </summary>
        /// <param name="integrand">A complex function to integrate of one variable.</param>
        /// <param name="lowerBound">The lower integration limit.</param>
        /// <param name="upperBound">The upper integration limit.</param>
        /// <returns>The numerical value of the definite integral.</returns>
        public static Complex TrapezoidRule(Function1Complex integrand, double lowerBound, double upperBound)
        {
            return (upperBound - lowerBound) / 2 * (integrand(lowerBound) + integrand(upperBound));
        }

        /// <summary>
        /// Returns the numerical value of the definite integral using the Simpson's rule.
        /// </summary>
        /// <param name="integrand">A complex function to integrate of one variable.</param>
        /// <param name="lowerBound">The lower integration limit.</param>
        /// <param name="upperBound">The upper integration limit.</param>
        /// <returns>The numerical value of the definite integral.</returns>
        public static Complex SimpsonsRule(Function1Complex integrand, double lowerBound, double upperBound)
        {
            double h = (upperBound - lowerBound) / 2;
            return (upperBound - lowerBound) / 6 * (integrand(lowerBound) + 4 * integrand(lowerBound + h) + integrand(upperBound));
        }

        /// <summary>
        /// Returns the numerical value of the definite integral using the Simpson's 3/8 rule.
        /// </summary>
        /// <param name="integrand">A complex function to integrate of one variable.</param>
        /// <param name="lowerBound">The lower integration limit.</param>
        /// <param name="upperBound">The upper integration limit.</param>
        /// <returns>The numerical value of the definite integral.</returns>
        public static Complex Simpsons38Rule(Function1Complex integrand, double lowerBound, double upperBound)
        {
            double h = (upperBound - lowerBound) / 3;
            return (upperBound - lowerBound) / 8 * (integrand(lowerBound) + 3 * integrand(lowerBound + h) + 3 * integrand(lowerBound + 2 * h) + integrand(upperBound));
        }

        /// <summary>
        /// Returns the numerical value of the definite integral using the Boole's rule.
        /// </summary>
        /// <param name="integrand">A complex function to integrate of one variable.</param>
        /// <param name="lowerBound">The lower integration limit.</param>
        /// <param name="upperBound">The upper integration limit.</param>
        /// <returns>The numerical value of the definite integral.</returns>
        public static Complex BoolesRule(Function1Complex integrand, double lowerBound, double upperBound)
        {
            double h = (upperBound - lowerBound) / 4;
            return (upperBound - lowerBound) / 90 * (7 * integrand(lowerBound) + 32 * integrand(lowerBound + h) + 12 * integrand(lowerBound + 2 * h) + 32 * integrand(lowerBound + 3 * h) + 7 * integrand(lowerBound + 4 * h));
        }

        #endregion
    }
}
