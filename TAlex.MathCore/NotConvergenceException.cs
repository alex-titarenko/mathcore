using System;
using System.Runtime.Serialization;


namespace TAlex.MathCore
{
    /// <summary>
    /// Represents errors that occur when the solution do not convergence.
    /// </summary>
    [Serializable]
    public class NotConvergenceException : Exception
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the NotConvergenceException class.
        /// </summary>
        public NotConvergenceException() :
            base("Calculation does not converge to a solution.") { }

        /// <summary>
        /// Initializes a new instance of the NotConvergenceException class
        /// with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public NotConvergenceException(string message) : base(message) { }

        /// <summary>
        /// Initializes a new instance of the NotConvergenceException class with a specified
        /// error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">
        /// The exception that is the cause of the current exception,
        /// or a null reference if no inner exception is specified.
        /// </param>
        public NotConvergenceException(string message, Exception innerException) :
            base(message, innerException) { }

        /// <summary>
        /// Initializes a new instance of the NotConvergenceException class with serialized data.
        /// </summary>
        /// <param name="info">
        /// The System.Runtime.Serialization.SerializationInfo that holds the serialized
        /// object data about the exception being thrown.
        /// </param>
        /// <param name="context">
        /// The System.Runtime.Serialization.StreamingContext that contains contextual
        /// information about the source or destination.
        /// </param>
        /// <exception cref="System.ArgumentNullException">The info parameter is null.</exception>
        /// <exception cref="System.Runtime.Serialization.SerializationException">
        /// The class name is null or System.Exception.HResult is zero (0).
        /// </exception>
        public NotConvergenceException(SerializationInfo info, StreamingContext context) :
            base(info, context) { }

        #endregion
    }
}
