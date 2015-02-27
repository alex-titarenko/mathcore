using NUnit.Framework;
using System;
using System.Collections.Generic;
using TAlex.MathCore.SpecialFunctions;
using FluentAssertions;


namespace TAlex.MathCore.Tests.SpecialFunctions
{
    [TestFixture]
    public class CombinatoricsTests
    {
        [TestCase(0, 1)]
        [TestCase(1, 1)]
        [TestCase(2, 2)]
        [TestCase(3, 6)]
        [TestCase(10, 3628800)]
        [TestCase(30, 2.6525285981219103E+32)]
        [TestCase(170, 7.257415615307994E+306)]
        public void FactorialTest(int n, double expected)
        {
            //action
            double actual = Combinatorics.Factorial(n);

            //assert
            actual.Should().Be(expected);
        }

        [TestCase(171)]
        [TestCase(200)]
        [TestCase(1000000000)]
        public void FactorialTest_LargeArgument(int n)
        {
            //action
            double actual = Combinatorics.Factorial(n);

            //assert
            double.IsPositiveInfinity(actual).Should().BeTrue(String.Format("The value '{0}' is not positive infinity.", actual));
        }

        [TestCase(-5)]
        [TestCase(-200346)]
        public void FactorialTest_ThrowExceptionForNegativeInteger(int n)
        {
            //action
            Action action = () => Combinatorics.Factorial(n);

            //assert
            action.ShouldThrow<ArgumentException>();
        }
    }
}
