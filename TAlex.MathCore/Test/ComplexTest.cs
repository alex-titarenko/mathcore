using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TAlex.MathCore;
using TAlex.MathCore.Test.Helpers;
using FluentAssertions;
using NUnit.Framework;
using System.Globalization;


namespace TAlex.MathCore.Test
{
    [TestFixture]
    public class ComplexTest
    {
        [TestCase(3,5, -6,16, -3,21)]
        public void AddTest(double re1, double im1, double re2, double im2, double re3, double im3)
        {
            //arrange
            Complex c1 = new Complex(re1, im1);
            Complex c2 = new Complex(re2, im2);

            Complex expected = new Complex(re3, im3);

            //action
            Complex actual = Complex.Add(c1, c2);
            
            //assert
            expected.Should().Be(actual);
        }

        [TestCase(-1, 0)]
        [TestCase(2, -1)]
        [TestCase(1, 0)]
        [TestCase(100, 0)]
        public void SqrtTest(double re, double im)
        {
            //arrange
            Complex number = new Complex(re, im);
            
            //action
            Complex sqrt = Complex.Sqrt(number);

            //assert
            NumericUtil.FuzzyEquals(sqrt * sqrt, number, 10E-10).Should().BeTrue();
        }

        [TestCase(0,0, 0,0)]
        [TestCase(ExMath.TwoPi,0, 0,0)]
        public void SinTest(double re, double im, double reExp, double imExp)
        {
            //arrange
            Complex c = new Complex(re, im);
            Complex expected = new Complex(reExp, imExp);

            //action
            Complex actual = Complex.Sin(c);

            //assert
            NumericUtil.FuzzyEquals(actual, expected, 10E-8).Should().BeTrue();
        }

        [TestCase(3, -8)]
        public void SinCosTest(double re, double im)
        {
            //arrange
            Complex number = new Complex(re, im);
            Complex sin = Complex.Sin(number);
            Complex cos = Complex.Cos(number);
                
            //action
            Complex actual = sin * sin + cos * cos;
            
            //assert
            NumericUtil.FuzzyEquals(actual, Complex.One, 10E-10).Should().BeTrue();
        }

        [TestCase(-3, 346)]
        [TestCase(3.5, 185.36)]
        public void SinhCoshTest(double re, double im)
        {
            //arrange
            Complex number = new Complex(re, im);
            Complex sinh = Complex.Sinh(number);
            Complex cosh = Complex.Cosh(number);

            //action
            Complex actual = cosh * cosh - sinh * sinh;

            //assert
            NumericUtil.FuzzyEquals(actual, Complex.One, 10E-10).Should().BeTrue();
        }

        [TestCase("5.5", 5.5, 0)]
        [TestCase("1.3i", 0, 1.3)]
        [TestCase("3 + 6i", 3, 6)]
        [TestCase("-5.2i+2.1", 2.1, -5.2)]
        [TestCase("-2.5i+2i-16", -16, -0.5)]
        public void ParseTest(string s, double re, double im)
        {
            //arrange
            Complex expected = new Complex(re, im);

            //action
            Complex actual = Complex.Parse(s, CultureInfo.InvariantCulture);

            //assert
            actual.Should().Be(expected);
        }

        [TestCase("3dfg+5i")]
        [TestCase("3. 5+6i")]
        [TestCase("1 2")]
        [TestCase("5 8i")]
        [TestCase("6-")]
        [TestCase("1. i")]
        public void ParseTest_InvalidInput_ThrowException(string s)
        {
            //action
            Action action = () => Complex.Parse(s);

            //assert
            action.ShouldThrow<FormatException>();
        }

        [TestCase(5.5, 0, "5.5")]
        [TestCase(0, -16, "-16i")]
        [TestCase(3, 8, "3 + 8i")]
        [TestCase(-1, -1, "-1 - 1i")]
        public void ToStringTest(double re, double im, string expected)
        {
            //arrange
            Complex c = new Complex(re, im);

            //action
            string actual = c.ToString(CultureInfo.InvariantCulture);

            //assert
            actual.Should().Be(expected);
        }
    }
}
