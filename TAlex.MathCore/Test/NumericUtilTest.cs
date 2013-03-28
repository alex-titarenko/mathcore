using NUnit.Framework;
using System;
using FluentAssertions;


namespace TAlex.MathCore.Test
{
    [TestFixture]
    public class NumericUtilTest
    {
        [TestCase(1, 0.999999, 10E-5)]
        [TestCase(5, 5, 10E-15)]
        [TestCase(0, 0.000000001, 10E-5)]
        [TestCase(0.0000001, 0.000000099998, 10E-5)]
        public void FuzzyEqualsTest_Success(double value1, double value2, double relativeTolerance)
        {
            //action
            bool actual = NumericUtil.FuzzyEquals(value1, value2, relativeTolerance);

            //assert
            actual.Should().BeTrue();
        }
    }
}
