using System;
using System.Runtime.Serialization;


namespace TAlex.MathCore.LinearAlgebra
{
    /// <summary>
    /// Represents errors that occur when the size mismatch of the matrix.
    /// </summary>
    [Serializable]
    public class MatrixSizeMismatchException : Exception
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the MatrixSizeMismatchException class.
        /// </summary>
        public MatrixSizeMismatchException() :
            base("Matrix size does not match.") { }

        /// <summary>
        /// Initializes a new instance of the MatrixSizeMismatchException class
        /// with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public MatrixSizeMismatchException(String message) : base(message) { }

        /// <summary>
        /// Initializes a new instance of the MatrixSizeMismatchException class with a specified
        /// error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">
        /// The exception that is the cause of the current exception,
        /// or a null reference if no inner exception is specified.
        /// </param>
        public MatrixSizeMismatchException(string message, Exception innerException) :
            base(message, innerException) { }

        /// <summary>
        /// Initializes a new instance of the MatrixSizeMismatchException class with serialized data.
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
        public MatrixSizeMismatchException(SerializationInfo info, StreamingContext context) :
            base(info, context) { }

        #endregion
    }
}
