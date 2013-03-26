using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TAlex.MathCore.ExpressionEvaluation.Tokenize
{
    public class Token
    {
        public readonly string Value;

        public readonly TokenType TokenType;

        public Token(string value, TokenType type)
        {
            Value = value;
            TokenType = type;
        }


        public override bool Equals(object obj)
        {
            if (obj is Token)
                return Equals((Token)obj);
            else
                return false;
        }

        public virtual bool Equals(Token token)
        {
            return (String.Equals(Value, token.Value) && TokenType == token.TokenType);
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public override string ToString()
        {
            return String.Format("(Value: {0}, Type: {1})", Value, TokenType);
        }
    }
}
