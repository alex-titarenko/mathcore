using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using TAlex.MathCore.ExpressionEvaluation.Trees;
using TAlex.MathCore.ExpressionEvaluation.Trees.Builders;


namespace TAlex.MathCore.ComplexExpressions.Test.Trees.Builders
{
    [TestFixture]
    public class ComplexExpressionTreeBuilderExTest
    {
        protected FunctionFactory<Object> FunctionFactory;
        protected ConstantFlyweightFactory<Object> ConstantFactory;
        protected ComplexExpressionTreeBuilder ExpressionTreeBuilder;

        [SetUp]
        public void SetUp()
        {
            var targetAssembly = typeof(TAlex.MathCore.ExpressionEvaluation.ComplexExpressions.Constants.CatalanConstantExpression).Assembly;

            ConstantFactory = new ConstantFlyweightFactory<object>();
            ConstantFactory.LoadFromAssemblies(new List<Assembly> { targetAssembly });

            FunctionFactory = new FunctionFactory<object>();
            FunctionFactory.LoadFromAssemblies(new List<Assembly> { targetAssembly });

            ExpressionTreeBuilder = new ComplexExpressionTreeBuilder
            {
                ConstantFactory = ConstantFactory,
                FunctionFactory = FunctionFactory
            };
        }

        [TestCase("sin(pi)", 0, 0)]
        [TestCase("cos(0)", 1, 0)]
        [TestCase("tan(0)", 0, 0)]
        [TestCase("sign(-5)", -1, 0)]
        [TestCase("abs(-3)", 3, 0)]
        [TestCase("inv(2)", 0.5, 0)]
        [TestCase("sqr(-5)", 25, 0)]
        public void EvaluateTest_Functions(string expression, double re, double im)
        {
            //arrange
            Complex expected = new Complex(re, im);

            //action
            Expression<Object> tree = ExpressionTreeBuilder.BuildTree(expression);
            Complex actual = (Complex)tree.Evaluate();

            //assert
            NumericUtil.FuzzyEquals(actual, expected, Machine.Epsilon).Should().BeTrue();
        }
    }
}
