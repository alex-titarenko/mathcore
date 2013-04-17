using NUnit.Framework;
using System;
using FluentAssertions;

namespace TAlex.MathCore.Test
{
    [TestFixture]
    public class ExMathTest
    {
        [TestCase(5.2, 0, 5, 0)]
        [TestCase(-1.1, 13.6, -2, 13)]
        public void IntPartTest(double re, double im, double reRes, double imRes)
        {
            //arrange
            Complex c = new Complex(re, im);
            Complex expected = new Complex(reRes, imRes);

            //action
            Complex actual = ExMath.IntPart(c);

            //assert
            actual.Should().Be(expected);
        }

        [TestCase(5.2, 0, 0.2, 0)]
        [TestCase(-1.1, 13.6, 0.9, 0.6)]
        public void FracPartTest(double re, double im, double reRes, double imRes)
        {
            //arrange
            Complex c = new Complex(re, im);
            Complex expected = new Complex(reRes, imRes);

            //action
            Complex actual = ExMath.FracPart(c);

            //assert
            NumericUtil.FuzzyEquals(actual, expected, 10E-15).Should().BeTrue();
        }
    }
}
