using NUnit.Framework;
using FluentAssertions;


namespace TAlex.MathCore.Statistics.Tests
{
    [TestFixture]
    public class MathStatsTests
    {
        [Test]
        public void ModeTest()
        {
            //arrange
            double[] v = new double[] {1, 2, 2, 3, 6, 8, 8, 8, 9};
            double expected = 8.0;

            //action
            double actual = MathStats.Mode(v);

            //assert
            actual.Should().Be(expected);
        }
    }
}
