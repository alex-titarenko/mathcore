using System;
using System.Runtime.Serialization;


namespace TAlex.MathCore.ExpressionEvaluation.Tokenize
{
    public class SyntaxException : Exception
    {
        public SyntaxException()
            : this(Properties.Resources.EXC_INCORRECT_SYNTAX)
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
