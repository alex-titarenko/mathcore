using System;
using TAlex.MathCore.ExpressionEvaluation.Tokenize;
using TAlex.MathCore.ExpressionEvaluation.Trees;
using TAlex.MathCore.ExpressionEvaluation.Trees.Builders;
using NUnit.Framework;
using FluentAssertions;


namespace TAlex.MathCore.ExpressionsBase.Tests.Trees.Builders
{
    [TestFixture]
    public class DoubleExpressionTreeBuilderTests
    {
        protected DoubleExpressionTreeBuilder ExpressionTreeBuilder;


        [SetUp]
        public virtual void SetUp()
        {
            ExpressionTreeBuilder = new DoubleExpressionTreeBuilder();
        }


        #region Evaluate

        [TestCase("3", 3)]
        public void Evaluate_Scalar(string expression, double expected)
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
        public void Evaluate_UnaryMinus(string expression, double expected)
        {
            //arrange
            Expression<double> target = ExpressionTreeBuilder.BuildTree(expression);

            //action
            double actual = target.Evaluate();

            //assert
            actual.Should().Be(expected);
        }

        [TestCase("3 + 5", 8)]
        public void Evaluate_Add(string expression, double expected)
        {
            //arrange
            Expression<double> target = ExpressionTreeBuilder.BuildTree(expression);

            //action
            double actual = target.Evaluate();

            //assert
            actual.Should().Be(expected);
        }

        [TestCase("3 - 5", -2)]
        public void Evaluate_Sub(string expression, double expected)
        {
            //arrange
            Expression<double> target = ExpressionTreeBuilder.BuildTree(expression);

            //action
            double actual = target.Evaluate();

            //assert
            actual.Should().Be(expected);
        }

        [TestCase("3*5", 15)]
        public void Evaluate_Mult(string expression, double expected)
        {
            //arrange
            Expression<double> target = ExpressionTreeBuilder.BuildTree(expression);

            //action
            double actual = target.Evaluate();

            //assert
            actual.Should().Be(expected);
        }

        [TestCase("3/5/2", 3.0 / 5 / 2)]
        public void Evaluate_Div(string expression, double expected)
        {
            //arrange
            Expression<double> target = ExpressionTreeBuilder.BuildTree(expression);

            //action
            double actual = target.Evaluate();

            //assert
            actual.Should().Be(expected);
        }

        [TestCase("4^2^3", 65536)]
        public void Evaluate_Pow(string expression, double expected)
        {
            //arrange
            Expression<double> target = ExpressionTreeBuilder.BuildTree(expression);

            //action
            double actual = target.Evaluate();

            //assert
            actual.Should().Be(expected);
        }

        [TestCase("(2+3)*5", 25)]
        public void Evaluate_Brackets(string expression, double expected)
        {
            //arrange
            Expression<double> target = ExpressionTreeBuilder.BuildTree(expression);

            //action
            double actual = target.Evaluate();

            //assert
            actual.Should().Be(expected);
        }

        [TestCase("3*5+16*-3", -33)]
        public void Evaluate_AddMult(string expression, double expected)
        {
            //arrange
            Expression<double> target = ExpressionTreeBuilder.BuildTree(expression);

            //action
            double actual = target.Evaluate();

            //assert
            actual.Should().Be(expected);
        }


        [Test]
        public void Evaluate_NullExpression()
        {
            //arrange
            string expression = null;

            //action
            Action action = () => ExpressionTreeBuilder.BuildTree(expression);

            //arrange
            action.Should().Throw<Exception>();
        }

        [TestCase("3+")]
        [TestCase("3*(2+8")]
        [TestCase("3+5{")]
        public void Evaluate_NoExpressionSyntaxException(string expression)
        {
            //action
            Action action = () => ExpressionTreeBuilder.BuildTree(expression);

            //assert
            action.Should().Throw<SyntaxException>();
        }

        #endregion

        #region ToString

        [TestCase("2 + 3", "2+3")]
        [TestCase("2 - 3", "2-3")]
        [TestCase("2 * 3", "2*3")]
        [TestCase("2 / 3", "2/3")]
        [TestCase("2 * 3 + 5", "2*3+5")]
        [TestCase("2 * (3 + 5)", "2*(3+5)")]
        [TestCase("(1 + 2) * (3 + 5)", "(1+2)*(3+5)")]
        [TestCase("1 + 2 * 3 + 5", "1+2*3+5")]
        [TestCase("(3 + 5) / 18", "(3+5)/18")]
        [TestCase("2^3", "2^3")]
        [TestCase("2^3+5", "2^3+5")]
        [TestCase("2^(3+5)", "2^(3+5)")]
        [TestCase("(3+11*5)^2", "(3+11*5)^2")]
        [TestCase("((3+11)*5)^2", "((3+11)*5)^2")]
        [TestCase("((((3+11))*5)^2)", "((3+11)*5)^2")]
        public void ToString(string expression, string expected)
        {
            //arrange
            Expression<double> target = ExpressionTreeBuilder.BuildTree(expression);

            //action
            var actual = target.ToString();

            //assert
            actual.Should().Be(expected);
        }

        #endregion
    }
}
