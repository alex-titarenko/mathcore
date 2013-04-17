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

        // Basic
        [TestCase("sin(pi)", 0, 0)]
        [TestCase("cos(0)", 1, 0)]
        [TestCase("tan(0)", 0, 0)]
        [TestCase("sign(-5)", -1, 0)]
        [TestCase("abs(-3)", 3, 0)]
        [TestCase("inv(2)", 0.5, 0)]
        [TestCase("sqr(-5)", 25, 0)]
        [TestCase("cube(2)", 8, 0)]
        [TestCase("sqrt(100)", 10, 0)]
        [TestCase("nthroot(27, 3)", 3, 0)]
        [TestCase("powten(3)", 1000, 0)]
        [TestCase("mod(5,3)", 2, 0)]
        [TestCase("int(-3.1)", -4, 0)]
        [TestCase("frac(-5.2)", 0.79999999999999982, 0)]
        [TestCase("floor(3.2)", 3, 0)]
        [TestCase("ceil(3.1)", 4, 0)]
        [TestCase("round(3.8)", 4, 0)]
        [TestCase("round(3.246, 2)", 3.25, 0)]
        [TestCase("trunc(3.8)", 3, 0)]
        // Complex numbers
        [TestCase("Re(3+5i)", 3, 0)]
        [TestCase("Im(3+5i)", 5, 0)]
        [TestCase("arg(3+5i)", 1.0303768265243125, 0)]
        [TestCase("conj(3+5i)", 3, -5)]
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
