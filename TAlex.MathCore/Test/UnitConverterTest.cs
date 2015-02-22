using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using TAlex.MathCore.UnitConversion;
using TAlex.MathCore.UnitConversion.Quantities;
using FluentAssertions;


namespace TAlex.MathCore.Test
{
    [TestFixture]
    public class UnitConverterTest
    {
        #region Convert

        [Test]
        public void Convert_Inches_Centimeters()
        {
            //arrange
            var expected = 58.42M;

            //action
            var actual = UnitConverter.Convert(23, Length.Inch, Length.Centimeter);

            //assert
            actual.Should().Be(expected);
        }

        [Test]
        public void Convert_Celsius_Fahrenheit()
        {
            //arrange
            var expected = 32M;

            //action
            var actual = UnitConverter.Convert(0, Temperature.Celsius, Temperature.Fahrenheit);

            //assert
            actual.Should().Be(expected);
        }

        #endregion
    }
}
