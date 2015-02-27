using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TAlex.MathCore.NumericalAnalysis.NumericalIntegration;
using FluentAssertions;


namespace TAlex.MathCore.NumericalAnalysis.Tests.NumericalIntegration
{
    [TestFixture]
    public class ComplexAdaptiveIntegratorTest
    {
        private ComplexAdaptiveIntegrator Integrator;

        [SetUp]
        public void SetUp()
        {
            Integrator = new ComplexAdaptiveIntegrator();
            Integrator.MaxIterations = 8000;
        }


        [TestCase(0.0, 1.0, Math.PI / 4.0, 0.0, TestName = "Integrate: sqrt(1-x^2)")]
        public void IntegrateTest_Pi(double lowerBound, double upperBound, double re, double im)
        {
            //arrange
            Complex expected = new Complex(re, im);
            Func<Complex, Complex> targetFunc = (c) => Complex.Sqrt(1.0 - c * c);

            //action
            Complex actual = Integrator.Integrate(targetFunc, lowerBound, upperBound);

            //assert
            NumericUtil.FuzzyEquals(actual, expected, 1E-15).Should().BeTrue();
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
            NumericUtil.FuzzyEquals(actual, expected, 1E-12).Should().BeTrue();
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
            NumericUtil.FuzzyEquals(actual, expected, 1E-14).Should().BeTrue();
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
            NumericUtil.FuzzyEquals(actual, expected, 1E-12).Should().BeTrue();
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
            NumericUtil.FuzzyEquals(actual, expected, 1E-12).Should().BeTrue();
        }


        [TestCase(0.0, double.PositiveInfinity, TestName = "Integrate Infinity: sin(x)^6/x^5")]
        public void IntegrateTest_InfinityTest1(double lowerBound, double upperBound)
        {
            //arrange
            Complex expected = -2.0 * Complex.Log(2) + 27.0 / 16.0 * Complex.Log(3);
            Func<Complex, Complex> targetFunc = (c) => Complex.Pow(Complex.Sin(c), 6) / Complex.Pow(c, 5);

            //action
            Complex actual = Integrator.Integrate(targetFunc, lowerBound, upperBound);

            //assert
            NumericUtil.FuzzyEquals(actual, expected, 1E-11).Should().BeTrue();
        }

        [TestCase(0.0, double.PositiveInfinity, TestName = "Integrate Infinity: 1/(1+x^2)")]
        public void IntegrateTest_InfinityTest2(double lowerBound, double upperBound)
        {
            //arrange
            Complex expected = Math.PI / 2.0;;
            Func<Complex, Complex> targetFunc = (c) => 1.0 / (1.0 + c * c);

            //action
            Complex actual = Integrator.Integrate(targetFunc, lowerBound, upperBound);

            //assert
            NumericUtil.FuzzyEquals(actual, expected, 1E-12).Should().BeTrue();
        }

        [TestCase(double.NegativeInfinity, 0.0, TestName = "Integrate Infinity: e^x")]
        public void IntegrateTest_ExpInfinityTest(double lowerBound, double upperBound)
        {
            //arrange
            Complex expected = 1.0;
            Func<Complex, Complex> targetFunc = (c) => Complex.Exp(c);

            //action
            Complex actual = Integrator.Integrate(targetFunc, lowerBound, upperBound);

            //assert
            NumericUtil.FuzzyEquals(actual, expected, 1E-12).Should().BeTrue();
        }

        [TestCase(1.0, double.PositiveInfinity, TestName = "Integrate Infinity: 1/x^2")]
        public void IntegrateTest_InfinityTest4(double lowerBound, double upperBound)
        {
            //arrange
            Complex expected = 1.0;
            Func<Complex, Complex> targetFunc = (c) => 1.0 / (c * c);

            //action
            Complex actual = Integrator.Integrate(targetFunc, lowerBound, upperBound);

            //assert
            NumericUtil.FuzzyEquals(actual, expected, 1E-12).Should().BeTrue();
        }

        [TestCase(double.NegativeInfinity, double.PositiveInfinity, TestName = "Integrate Infinity: e^(-x^2)")]
        public void IntegrateTest_InfinityTest5(double lowerBound, double upperBound)
        {
            //arrange
            Complex expected = Math.Sqrt(Math.PI);
            Func<Complex, Complex> targetFunc = (c) => Complex.Exp(-(c * c));

            //action
            Complex actual = Integrator.Integrate(targetFunc, lowerBound, upperBound);

            //assert
            NumericUtil.FuzzyEquals(actual, expected, 1E-12).Should().BeTrue();
        }

        [TestCase(double.NegativeInfinity, double.PositiveInfinity, TestName = "Integrate Infinity: x")]
        public void IntegrateTest_XInfinityTest(double lowerBound, double upperBound)
        {
            //arrange
            Complex expected = Complex.Zero;
            Func<Complex, Complex> targetFunc = (c) => c;

            //action
            Complex actual = Integrator.Integrate(targetFunc, lowerBound, upperBound);

            //assert
            NumericUtil.FuzzyEquals(actual, expected, 1E-12).Should().BeTrue();
        }
    }
}
