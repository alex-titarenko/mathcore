using NUnit.Framework;
using System;
using FluentAssertions;
using System.Globalization;


namespace TAlex.MathCore.Tests
{
    [TestFixture]
    public class ConvertExTests
    {
        [TestCase("1", 1)]
        [TestCase("10.2", 10.2)]
        [TestCase("30d", 30)]
        [TestCase("101b", 5)]
        [TestCase("1001.0011b", 9.1875)]
        [TestCase("1014o", 524)]
        [TestCase("0FFh", 255)]
        [TestCase("-0F.Fh", -15.9375)]
        public void ToDouble(string s, double expected)
        {
            //action
            double actual = ConvertEx.ToDouble(s, CultureInfo.InvariantCulture);

            //assert
            actual.Should().Be(expected);
        }

        [TestCase("0346e")]
        [TestCase("0101201b")]
        [TestCase("0101!101b")]
        public void ToDouble_ThrowException_InvalidFormat(string s)
        {
            //action
            Action action = () => ConvertEx.ToDouble(s, CultureInfo.InvariantCulture);
            
            //assert
            action.Should().Throw<FormatException>();
        }
    }
}
