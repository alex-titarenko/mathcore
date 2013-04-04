﻿using NUnit.Framework;
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
        [TestCase("2-3.1", -1.1, 0)]
        [TestCase("2 - 1.6i+4", 6, -1.6)]
        [TestCase("2*3.1", 6.2, 0)]
        [TestCase("2*3i", 0, 6)]
        [TestCase("2+3*2", 8, 0)]
        [TestCase("(2+4)*6", 36, 0)]
        [TestCase("1/2", 0.5, 0)]
        [TestCase("1/2.5i*3", 0.0, 1.2)]
        [TestCase("(1 + 3i) / 2.5i", -1.2, 0.4)]
        [TestCase("2i^2", -2, 0)]
        [TestCase("-2.3", -2.3, 0)]
        [TestCase("2^3^2", 512, 0)]
        [TestCase("2^-1", 0.5, 0)]
        [TestCase("2*(-3+1)", -4, 0)]
        public void ComplexTest(string s, double re, double im)
        {
            //arrange
            Complex expected = new Complex(re, im);

            //assert
            Expression<object> expr = TreeBuilder.BuildTree(s);
            Complex actual = (Complex)expr.Evaluate();

            //assert
            NumericUtil.FuzzyEquals(actual, expected, 10E-16).Should().BeTrue();
        }
    }
}
