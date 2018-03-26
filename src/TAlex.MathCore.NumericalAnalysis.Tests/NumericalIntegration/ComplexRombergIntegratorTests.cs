using NUnit.Framework;
using System;
using TAlex.MathCore.NumericalAnalysis.NumericalIntegration;
using FluentAssertions;


namespace TAlex.MathCore.NumericalAnalysis.Tests.NumericalIntegration
{
    [TestFixture]
    public class ComplexRombergIntegratorTests
    {
        private ComplexRombergIntegrator Integrator;

        [SetUp]
        public void SetUp()
        {
            Integrator = new ComplexRombergIntegrator();
        }


        [TestCase(-68, 20, TestName = "Integrate: log(x)*x")]
        public void IntegrateTest_LogXX(double lowerBound, double upperBound)
        {
            //arrange
            Complex expected = (2 * upperBound * upperBound * Complex.Log(upperBound) - upperBound * upperBound) / 4.0 -
                    (2 * lowerBound * lowerBound * Complex.Log(lowerBound) - lowerBound * lowerBound) / 4.0; ;
            Func<Complex, Complex> targetFunc = (c) => Complex.Log(c) * c;

            //action
            Complex actual = Integrator.Integrate(targetFunc, lowerBound, upperBound);

            //assert
            NumericUtil.FuzzyEquals(actual, expected, 1E-9).Should().BeTrue();
        }

        [TestCase(0, 3000, TestName = "Integrate: x")]
        public void IntegrateTest_X(double lowerBound, double upperBound)
        {
            //arrange
            Complex expected = upperBound * upperBound / 2.0 - lowerBound * lowerBound / 2.0;
            Func<Complex, Complex> targetFunc = (c) => c;

            //action
            Complex actual = Integrator.Integrate(targetFunc, lowerBound, upperBound);

            //assert
            NumericUtil.FuzzyEquals(actual, expected, 1E-15).Should().BeTrue();
        }

        [TestCase(0, 300, TestName = "Integrate: x^2")]
        public void IntegrateTest_Square(double lowerBound, double upperBound)
        {
            //arrange
            Complex expected = Complex.Pow(upperBound, 3) / 3.0 - Complex.Pow(lowerBound, 3) / 3.0; ;
            Func<Complex, Complex> targetFunc = (c) => c * c;

            //action
            Complex actual = Integrator.Integrate(targetFunc, lowerBound, upperBound);

            //assert
            NumericUtil.FuzzyEquals(actual, expected, 1E-12).Should().BeTrue();
        }

        [TestCase(-300, 405, TestName = "Integrate: sqrt(x)")]
        public void IntegrateTest_Sqrt(double lowerBound, double upperBound)
        {
            //arrange
            Complex expected = 2.0 / 3.0 * Complex.Pow(upperBound, 3.0 / 2.0) - 2.0 / 3.0 * Complex.Pow(lowerBound, 3.0 / 2.0);
            Func<Complex, Complex> targetFunc = (c) => Complex.Sqrt(c);

            //action
            Complex actual = Integrator.Integrate(targetFunc, lowerBound, upperBound);

            //assert
            NumericUtil.FuzzyEquals(actual, expected, 1E-7).Should().BeTrue();
        }

        [TestCase(0, 1000, TestName = "Integrate: x*sin(x)")]
        public void IntegrateTest_XSin(double lowerBound, double upperBound)
        {
            //arrange
            Complex expected = (Complex.Sin(upperBound) - upperBound * Complex.Cos(upperBound)) - (Complex.Sin(lowerBound) - lowerBound * Complex.Cos(lowerBound));
            Func<Complex, Complex> targetFunc = (c) => c * Complex.Sin(c);

            //action
            Complex actual = Integrator.Integrate(targetFunc, lowerBound, upperBound);

            //assert
            NumericUtil.FuzzyEquals(actual, expected, 1E-14).Should().BeTrue();
        }
    }
}
