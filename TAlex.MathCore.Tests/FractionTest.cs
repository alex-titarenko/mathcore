using System;
using TAlex.MathCore;
using NUnit.Framework;
using FluentAssertions;


namespace TAlex.MathCore.Tests
{
    [TestFixture]
    public class FractionTest
    {
        [Test]
        public void op_EqualityTest()
        {
            //arrange
            Fraction frac1 = new Fraction(39, 99);
            Fraction frac2 = new Fraction(220272, 559152);
            bool expected = true;

            //action
            bool actual = (frac1 == frac2);

            //assert
            actual.Should().Be(expected);
        }

        [Test]
        public void op_AdditionTest()
        {
            //arrange
            Fraction frac1 = new Fraction(5, 6);
            Fraction frac2 = new Fraction(8, 9);
            Fraction expected = new Fraction(31, 18);

            //action
            Fraction actual = frac1 + frac2;

            //assert
            actual.Should().Be(expected);
        }

        [Test]
        public void op_SubtractionTest()
        {
            //arrange
            Fraction frac1 = new Fraction(5, 6);
            Fraction frac2 = new Fraction(15, 4);
            Fraction expected = new Fraction(-35, 12);

            //action
            Fraction actual = (frac1 - frac2);

            //assert
            actual.Should().Be(expected);
        }

        [Test]
        public void op_MultiplyTest()
        {
            //arrange
            Fraction frac1 = new Fraction(5, 6);
            Fraction frac2 = new Fraction(8, 9);
            Fraction expected = new Fraction(20, 27);

            //action
            Fraction actual = frac1 * frac2;

            //assert
            actual.Should().Be(expected);
        }

        [Test]
        public void op_DivisionTest()
        {
            //arrange
            Fraction frac1 = new Fraction(5, -8);
            Fraction frac2 = new Fraction(15, 2);
            Fraction expected = new Fraction(-1, 12);

            //action
            Fraction actual = (frac1 / frac2);

            //assert
            actual.Should().Be(expected);
        }

        [Test]
        public void PowTest()
        {
            //arrange
            Fraction frac = new Fraction(-3, 8);
            int exponent = -3;
            Fraction expected = new Fraction(-512, 27);

            //action
            Fraction actual = Fraction.Pow(frac, exponent);

            //assert
            actual.Should().Be(expected);
        }

        [Test]
        public void ParseTest()
        {
            //arrange
            string s = "-3/5";
            Fraction expected = new Fraction(-3, 5);

            //action
            Fraction actual = Fraction.Parse(s);

            //assert
            actual.Should().Be(expected);
        }
    }
}
