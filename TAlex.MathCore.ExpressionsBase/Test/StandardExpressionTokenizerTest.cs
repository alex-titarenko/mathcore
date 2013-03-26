using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using TAlex.MathCore.ExpressionEvaluation.Tokenize;


namespace TAlex.MathCore.ExpressionEvaluation.Test
{
    [TestClass]
    public class StandardExpressionTokenizerTest
    {
        [TestMethod]
        public void GetTokensTest()
        {
            string expression = "sin(x + 2) - 3.6";
            StandardExpressionTokenizer target = new StandardExpressionTokenizer();
            IEnumerable<Token> actualTokens = target.GetTokens(expression);

            IList<Token> expectedTokens = new List<Token>()
            {
                new Token("sin", TokenType.Function),
                new Token("(", TokenType.Operator),
                new Token("x", TokenType.Identifier),
                new Token("+", TokenType.Operator),
                new Token("2", TokenType.Scalar),
                new Token(")", TokenType.Operator),
                new Token("-", TokenType.Operator),
                new Token("3.6", TokenType.Scalar),
                new Token("$", TokenType.End)
            };

            CollectionAssert.AreEqual(expectedTokens.ToList(), actualTokens.ToList());
        }
    }
}
