using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;


namespace TAlex.MathCore.SpecialFunctions.Test
{
    [TestFixture]
    public class ExponentialIntegralsTest
    {
        [TestCase(0, 0)]
        [TestCase(0.1, 0.09994446110827695016059211)]
        [TestCase(0.2, 0.19955608852623382140045694)]
        [TestCase(1, 0.9460830703671830149413533138)]
        [TestCase(5, 1.5499312449446741372744084007)]
        [TestCase(10, 1.6583475942188740493309718793)]
        [TestCase(15, 1.6181944437083687391239886314)]
        [TestCase(20, 1.5482417010434398401636433421)]
        public void SinIntegralTest_Real(double x, double expected)
        {
            //action
            double actual = ExponentialIntegrals.SinIntegral(x);

            //assert
            NumericUtil.FuzzyEquals(actual, expected, 1E-14).Should()
                .BeTrue(String.Format("Expected value '{0}', actual value '{1}'", expected, actual));
        }
    }
}
