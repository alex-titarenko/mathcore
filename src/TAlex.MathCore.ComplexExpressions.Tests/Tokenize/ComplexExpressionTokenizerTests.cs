using NUnit.Framework;
using System.Collections.Generic;
using TAlex.MathCore.ExpressionEvaluation.Tokenize;
using FluentAssertions;


namespace TAlex.MathCore.ComplexExpressions.Tests.Tokenize
{
    [TestFixture]
    public class ComplexExpressionTokenizerTests
    {
        protected IExpressionTokenizer Tokenizer = new StandardExpressionTokenizer();

        [SetUp]
        public virtual void SetUp()
        {
            Tokenizer = new ComplexExpressionTokenizer();
        }

        [Test]
        public void GetTokensTest_ComplexNumber()
        {
            //arrange
            string expression = "1/2i";
            IList<Token> expectedTokens = new List<Token>()
            {
                new Token("1", TokenType.Scalar),
                new Token("/", TokenType.Operator),
                new Token("2", TokenType.Scalar),
                new Token("*", TokenType.Operator),
                new Token("1i", TokenType.Scalar),
                new Token("$", TokenType.End)
            };

            //action
            IEnumerable<Token> actualTokens = Tokenizer.GetTokens(expression);

            //assert
            actualTokens.Should().BeEquivalentTo(expectedTokens);
        }
    }
}
