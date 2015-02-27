using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;


namespace TAlex.MathCore.SpecialFunctions.Tests
{
    [TestFixture]
    public class ProbabilityIntegralsTests
    {
        [TestCase(0, 0)]
        [TestCase(0.25, 0.276326390168236932985068)]
        [TestCase(0.5, 0.520499877813046537682746)]
        [TestCase(1, 0.8427007929497148693412206)]
        [TestCase(2, 0.9953222650189527341620692)]
        [TestCase(4, 0.9999999845827420997199811)]
        [TestCase(26.5583093100796414, 1)]
        [TestCase(27, 1)]
        [TestCase(1000, 1)]
        [TestCase(-0.25, -0.276326390168236932985068)]
        [TestCase(-0.5, -0.520499877813046537682746)]
        [TestCase(-1, -0.8427007929497148693412206)]
        [TestCase(-2, -0.9953222650189527341620692)]
        [TestCase(-4, -0.9999999845827420997199811)]
        [TestCase(-26.5583093100796414, -1)]
        [TestCase(-27, -1)]
        [TestCase(-1000, -1)]
        public void ErfTest(double value, double expected)
        {
            //action
            double actual = ProbabilityIntegrals.Erf(value);

            //assert
            NumericUtil.FuzzyEquals(actual, expected, Machine.Epsilon).Should().BeTrue();
        }

        [TestCase(0, 1)]
        [TestCase(0.25, 0.7236736098317630670149317)]
        [TestCase(0.5, 0.479500122186953462317253346)]
        [TestCase(1, 0.15729920705028513065877936491)]
        [TestCase(2, 0.004677734981047265)]
        [TestCase(4, 1.54172579002800188521596734868E-8)]
        [TestCase(26.5583093100796414, 0)]
        [TestCase(27, 0)]
        [TestCase(1000, 0)]
        [TestCase(-0.25, 1.276326390168236932985068)]
        [TestCase(-0.5, 1.520499877813046537682746)]
        [TestCase(-1, 1.8427007929497148693412206)]
        [TestCase(-2, 1.9953222650189527341620692)]
        [TestCase(-4, 1.9999999845827420997199811)]
        [TestCase(-26.5583093100796414, 2)]
        [TestCase(-27, 2)]
        [TestCase(-1000, 2)]
        public void ErfcTest(double value, double expected)
        {
            //action
            double actual = ProbabilityIntegrals.Erfc(value);

            //assert
            NumericUtil.FuzzyEquals(actual, expected, Machine.Epsilon).Should()
                .BeTrue(String.Format("Expected value '{0:F18}', actual value '{1:F18}'", expected, actual));
        }
    }
}
