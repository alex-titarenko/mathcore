using FluentAssertions;
using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using TAlex.MathCore.ExpressionEvaluation.Tokenize;
using NUnit.Framework;


namespace TAlex.MathCore.ExpressionsBase.Tests.Tokenize
{
    [TestFixture]
    public class StandardExpressionTokenizerTests
    {
        protected StandardExpressionTokenizer ExpressionTokenizer;

        [SetUp]
        public virtual void SetUp()
        {
            ExpressionTokenizer = new StandardExpressionTokenizer();
        }


        [Test]
        public void GetTokensTest()
        {
            //arrange
            string expression = "sin(x + 2) - 3.6";

            IList<Token> expected = new List<Token>()
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

            //action
            IEnumerable<Token> actual = ExpressionTokenizer.GetTokens(expression).ToList();

            //assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void GetTokensTest_ComplexNumber()
        {
            //arrange
            string expression = "1/2i";
            
            IList<Token> expected = new List<Token>()
            {
                new Token("1", TokenType.Scalar),
                new Token("/", TokenType.Operator),
                new Token("2i", TokenType.Scalar),
                new Token("$", TokenType.End)
            };

            //action
            IEnumerable<Token> actual = ExpressionTokenizer.GetTokens(expression);

            //assert
            actual.Should().BeEquivalentTo(expected);
        }
    }
}
