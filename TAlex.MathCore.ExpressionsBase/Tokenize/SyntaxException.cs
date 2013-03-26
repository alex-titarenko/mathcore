using System;
using System.Runtime.Serialization;


namespace TAlex.MathCore.ExpressionEvaluation.Tokenize
{
    public class SyntaxException : Exception
    {
        public SyntaxException()
            : base()
        {
        }

        public SyntaxException(string message)
            : base(message)
        {
        }

        public SyntaxException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public SyntaxException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
