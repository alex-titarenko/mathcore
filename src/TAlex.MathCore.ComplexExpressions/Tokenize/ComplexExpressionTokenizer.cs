using System.Collections.Generic;

namespace TAlex.MathCore.ExpressionEvaluation.Tokenize
{
    public class ComplexExpressionTokenizer : StandardExpressionTokenizer
    {
        protected override void ScalarPostProcessing(IList<Token> tokens, ref TokenType tokenType, ref string tokenValue)
        {
            if (tokenValue.EndsWith("i") || tokenValue.EndsWith("j"))
            {
                tokens.Add(new Token(tokenValue.Substring(0, tokenValue.Length - 1), TokenType.Scalar));
                tokens.Add(new Token("*", TokenType.Operator));
                tokenValue = "1i";
            }
        }
    }
}
