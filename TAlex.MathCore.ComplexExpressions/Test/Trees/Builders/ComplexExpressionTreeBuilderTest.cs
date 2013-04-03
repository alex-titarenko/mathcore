using NUnit.Framework;
using System;
using TAlex.MathCore;
using TAlex.MathCore.ExpressionEvaluation.Trees.Builders;
using FluentAssertions;
using TAlex.MathCore.ExpressionEvaluation.Trees;


namespace Test
{
    [TestFixture]
    public class ComplexExpressionTreeBuilderTest
    {
        protected ComplexExpressionTreeBuilder TreeBuilder;


        [SetUp]
        public void SetUp()
        {
            TreeBuilder = new ComplexExpressionTreeBuilder();
        }


        [TestCase("1.2+3i+ 16", 17.2, 3)]
        [TestCase("101.01b + 0Fh", 20.25, 0)]
        public void ComplexTest(string s, double re, double im)
        {
            //arrange
            Complex expected = new Complex(re, im);

            //assert
            Expression<object> actual = TreeBuilder.BuildTree(s);

            //assert
            actual.Evaluate().Should().Be(expected);
        }
    }
}
