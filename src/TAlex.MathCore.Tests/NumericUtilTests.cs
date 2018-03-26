using NUnit.Framework;
using System;
using FluentAssertions;


namespace TAlex.MathCore.Tests
{
    [TestFixture]
    public class NumericUtilTests
    {
        [TestCase(1, 0.999999, 10E-5)]
        [TestCase(5, 5, 10E-15)]
        [TestCase(0, 0.000000001, 10E-5)]
        [TestCase(0.0000001, 0.000000099998, 10E-5)]
        [TestCase(10036.2466, 10036.2566, 10E-7)]
        public void FuzzyEqualsTest_Success(double value1, double value2, double relativeTolerance)
        {
            //action
            bool actual = NumericUtil.FuzzyEquals(value1, value2, relativeTolerance);

            //assert
            actual.Should().BeTrue();
        }

        [TestCase(0, 0.000000001, 10E-11)]
        [TestCase(0.0000001, 0.000000099998, 10E-6)]
        [TestCase(10036.2466, 10036.2566, 10E-8)]
        public void FuzzyEqualsTest_Fail(double value1, double value2, double relativeTolerance)
        {
            //action
            bool actual = NumericUtil.FuzzyEquals(value1, value2, relativeTolerance);

            //assert
            actual.Should().BeFalse();
        }


        [TestCase(0,0, 0,0, 10E-10)]
        [TestCase(10E-8,10E-10, 0,0, 10E-7)]
        [TestCase(10036.2466,23.234, 10036.2566,23.23, 10E-6)]
        public void FuzzyEqualsComplexTest_Success(double re1, double im1, double re2, double im2, double relativeTolerance)
        {
            //arrange
            Complex value1 = new Complex(re1, im1);
            Complex value2 = new Complex(re2, im2);

            //action
            bool actual = NumericUtil.FuzzyEquals(value1, value2, relativeTolerance);

            //assert
            actual.Should().BeTrue();
        }


        [TestCase(10E-8,10E-10, 0,0, 10E-8)]
        [TestCase(10036.2466, 23.234, 10036.2566, 23.23, 10E-7)]
        public void FuzzyEqualsComplexTest_Fail(double re1, double im1, double re2, double im2, double relativeTolerance)
        {
            //arrange
            Complex value1 = new Complex(re1, im1);
            Complex value2 = new Complex(re2, im2);

            //action
            bool actual = NumericUtil.FuzzyEquals(value1, value2, relativeTolerance);

            //assert
            actual.Should().BeFalse();
        }
    }
}
