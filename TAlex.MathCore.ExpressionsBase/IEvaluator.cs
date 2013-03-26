using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TAlex.MathCore.ExpressionEvaluation
{
    /// <summary>
    /// Defines a method for evaluating.
    /// </summary>
    /// <typeparam name="T">The type of the result of evaluating.</typeparam>
    public interface IEvaluator<T>
    {
        /// <summary>
        /// Returns the result of evaluation.
        /// </summary>
        /// <returns>An object containing the result of evaluation.</returns>
        T Evaluate();
    }
}
