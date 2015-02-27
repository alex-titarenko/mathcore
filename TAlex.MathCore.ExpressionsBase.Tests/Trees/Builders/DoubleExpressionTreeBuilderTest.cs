using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using TAlex.MathCore.ExpressionEvaluation.Tokenize;
using TAlex.MathCore.ExpressionEvaluation.Trees;
using TAlex.MathCore.ExpressionEvaluation.Trees.Builders;
using NUnit.Framework;
using FluentAssertions;


namespace TAlex.MathCore.ExpressionsBase.Tests.Trees.Builders
{
    [TestFixture]
    public class DoubleExpressionTreeBuilderTest
    {
        protected DoubleExpressionTreeBuilder ExpressionTreeBuilder;


        [SetUp]
        public virtual void SetUp()
        {
            ExpressionTreeBuilder = new DoubleExpressionTreeBuilder();
        }


        [TestCase("3", 3)]
        public void EvaluateTest_Scalar(string expression, double expected)
        {
            //arrange
            Expression<double> target = ExpressionTreeBuilder.BuildTree(expression);
            
            //action
            double actual = target.Evaluate();

            //assert
            actual.Should().Be(expected);
        }

        [TestCase("-3", -3)]
        [TestCase("--3", 3)]
        public void EvaluateTest_UnaryMinus(string expression, double expected)
        {
            //arrange
            Expression<double> target = ExpressionTreeBuilder.BuildTree(expression);

            //action
            double actual = target.Evaluate();

            //assert
            actual.Should().Be(expected);
        }

        [TestCase("3 + 5", 8)]
        public void EvaluateTest_Add(string expression, double expected)
        {
            //arrange
            Expression<double> target = ExpressionTreeBuilder.BuildTree(expression);

            //action
            double actual = target.Evaluate();

            //assert
            actual.Should().Be(expected);
        }

        [TestCase("3 - 5", -2)]
        public void EvaluateTest_Sub(string expression, double expected)
        {
            //arrange
            Expression<double> target = ExpressionTreeBuilder.BuildTree(expression);

            //action
            double actual = target.Evaluate();

            //assert
            actual.Should().Be(expected);
        }

        [TestCase("3*5", 15)]
        public void EvaluateTest_Mult(string expression, double expected)
        {
            //arrange
            Expression<double> target = ExpressionTreeBuilder.BuildTree(expression);

            //action
            double actual = target.Evaluate();

            //assert
            actual.Should().Be(expected);
        }

        [TestCase("3/5/2", 3.0 / 5 / 2)]
        public void EvaluateTest_Div(string expression, double expected)
        {
            //arrange
            Expression<double> target = ExpressionTreeBuilder.BuildTree(expression);

            //action
            double actual = target.Evaluate();

            //assert
            actual.Should().Be(expected);
        }

        [TestCase("4^2^3", 65536)]
        public void EvaluateTest_Pow(string expression, double expected)
        {
            //arrange
            Expression<double> target = ExpressionTreeBuilder.BuildTree(expression);

            //action
            double actual = target.Evaluate();

            //assert
            actual.Should().Be(expected);
        }

        [TestCase("(2+3)*5", 25)]
        public void EvaluateTest_Brackets(string expression, double expected)
        {
            //arrange
            Expression<double> target = ExpressionTreeBuilder.BuildTree(expression);

            //action
            double actual = target.Evaluate();

            //assert
            actual.Should().Be(expected);
        }

        [TestCase("3*5+16*-3", -33)]
        public void EvaluateTest_AddMult(string expression, double expected)
        {
            //arrange
            Expression<double> target = ExpressionTreeBuilder.BuildTree(expression);

            //action
            double actual = target.Evaluate();

            //assert
            actual.Should().Be(expected);
        }


        [Test]
        public void EvaluateTest_NullExpression()
        {
            //arrange
            string expression = null;

            //action
            Action action = () => ExpressionTreeBuilder.BuildTree(expression);

            //arrange
            action.ShouldThrow<Exception>();
        }

        [TestCase("3+")]
        [TestCase("3*(2+8")]
        [TestCase("3+5{")]
        public void EvaluateTest_NoExpressionSyntaxException(string expression)
        {
            //action
            Action action = () => ExpressionTreeBuilder.BuildTree(expression);

            //assert
            action.ShouldThrow<SyntaxException>();
        }
    }
}
