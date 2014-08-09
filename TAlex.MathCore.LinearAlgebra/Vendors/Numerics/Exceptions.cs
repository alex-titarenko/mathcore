using System;
using MathNet.Numerics.Properties;

namespace MathNet.Numerics
{
    /// <summary>
    /// An algorithm failed to converge.
    /// </summary>
    public class NonConvergenceException : Exception
    {
        public NonConvergenceException() : base(Resources.ConvergenceFailed)
        {
        }

        public NonConvergenceException(string message) : base(message)
        {
        }

        public NonConvergenceException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }

    /// <summary>
    /// An algorithm failed to converge due to a numerical breakdown.
    /// </summary>
    public class NumericalBreakdownException : NonConvergenceException
    {
        public NumericalBreakdownException()
            : base(Resources.NumericalBreakdown)
        {
        }

        public NumericalBreakdownException(string message)
            : base(message)
        {
        }

        public NumericalBreakdownException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
