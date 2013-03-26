using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TAlex.MathCore.ExpressionEvaluation.Tokenize
{
    public class StandardExpressionTokenizer : IExpressionTokenizer
    {
        #region Properties

        public readonly SortedSet<string> Operators;

        #endregion

        #region Constructors

        public StandardExpressionTokenizer()
        {
            Operators = new SortedSet<string>(new OperatorComparer())
            {
                "+", "-", "*", "/", "%", "^", "**", ",", ";", "(", ")", "{", "}", "[", "]", "<", "<=", ">", ">=", "<>", "==", "|", "||", "&", "&&", "<<", ">>", "!"
            };
        }

        #endregion

        #region IExpressionTokenizer Members

        public IEnumerable<Token> GetTokens(string expression)
        {
            if (String.IsNullOrWhiteSpace(expression))
                throw new ArgumentNullException("expression");

            int idx = 0;
            int len = expression.Length;
            IList<Token> tokens = new List<Token>();

            while (idx <= len)
            {
                TokenType tokenType = TokenType.End;
                String tokenValue = String.Empty;

                // Skipping white space characters
                while (idx < len && Char.IsWhiteSpace(expression[idx]))
                {
                    idx++;
                }

                if (idx == len)
                {
                    tokenType = TokenType.End;
                    tokenValue = "$";
                    tokens.Add(new Token(tokenValue, tokenType));
                    break;
                }


                if (Char.IsDigit(expression[idx])) // Token type is Scalar
                {
                    tokenType = TokenType.Scalar;
                    while (Char.IsLetterOrDigit(expression[idx]) || (expression[idx] == '.'))
                    {
                        tokenValue += expression[idx++];
                        if (idx >= len) break;
                    }
                }
                else if (GetOperator(expression, idx, out tokenValue)) // Token type is Operator
                {
                    tokenType = TokenType.Operator;
                    idx += tokenValue.Length;
                }
                else if (Char.IsLetter(expression[idx]) || expression[idx] == '_') // Token type is function or constant or variable
                {
                    while (Char.IsLetterOrDigit(expression[idx]) || expression[idx] == '_')
                    {
                        tokenValue += expression[idx++];
                        if (idx >= len) break;
                    }

                    if (idx < len && expression[idx] == '(')
                        tokenType = TokenType.Function;
                    else
                        tokenType = TokenType.Identifier;
                }
                else
                {
                    throw new SyntaxException(String.Format("Invalid character '{0}'.", expression[idx]));
                }

                tokens.Add(new Token(tokenValue, tokenType));
            }

            return tokens.ToArray();
        }

        protected bool GetOperator(string expression, int idx, out string @operator)
        {
            string substr = expression.Substring(idx);

            foreach (string op in Operators)
            {
                if (substr.StartsWith(op))
                {
                    @operator = op;
                    return true;
                }
            }

            @operator = null;
            return false;
        }

        #endregion

        #region Nested Types

        protected class OperatorComparer : IComparer<string>
        {
            #region IComparer<string> Members

            public int Compare(string x, string y)
            {
                int result = y.Length.CompareTo(x.Length);

                if (result == 0)
                    return x.CompareTo(y);
                else
                    return result;
            }

            #endregion
        }

        #endregion
    }
}
