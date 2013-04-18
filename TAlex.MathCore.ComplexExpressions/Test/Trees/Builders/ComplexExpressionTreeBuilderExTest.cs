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
using TAlex.MathCore.LinearAlgebra;


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

        [TestCase("cart2pol(9, -2)", 9.2195444572928889, -0.21866894587394195)]
        [TestCase("pol2cart(9.2195444572928889, -0.21866894587394195)", 9, -2)]
        [TestCase("cart2sph(9, -2, 3)", 9.6953597148326587, -0.21866894587394195, 1.2562065842346306)]
        [TestCase("sph2cart(9.6953597148326587, -0.21866894587394195, 1.2562065842346306)", 9, -2, 2.9999999999999996)]
        [TestCase("cart2cyl(9, -2, 3)", 9.2195444572928889, -0.21866894587394195, 3)]
        [TestCase("cyl2cart(9.2195444572928889, -0.21866894587394195, 3)", 9, -2, 3)]
        public void EvaluateTest_GraphingFunctions(string expression, params double[] result)
        {
            //action
            Expression<Object> tree = ExpressionTreeBuilder.BuildTree(expression);
            CMatrix actual = (CMatrix)tree.Evaluate();

            //assert
            actual.Length.Should().Be(result.Length);
            for (int i = 0; i < actual.Length; i++) NumericUtil.FuzzyEquals(actual[i], result[i], Machine.Epsilon).Should().BeTrue(); 
        }
    }
}
