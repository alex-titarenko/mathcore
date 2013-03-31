using NUnit.Framework;
using System;
using TAlex.MathCore.SpecialFunctions;
using FluentAssertions;


namespace TAlex.MathCore.Test.SpecialFunctions
{
    [TestFixture]
    public class NumberTheoryTest
    {
        [TestCase(7, 7)]
        [TestCase(2, 3)]
        [TestCase(8, 12)]
        public void GCDTest(long a, long b)
        {
            //action
            long gcd = NumberTheory.GCD(a, b);

            //assert
            (a % gcd).Should().Be(0L);
            (b % gcd).Should().Be(0L);
        }
    }
}
