using System;
using System.Linq;
using TAlex.MathCore;
using NUnit.Framework;
using FluentAssertions;
using System.Xml.Serialization;
using System.Text;
using System.IO;
using System.Globalization;
using System.Xml;


namespace TAlex.MathCore.Test
{
    [TestFixture]
    public class CPolynomialTest
    {
        [Test]
        public void Class_DecoratedWithSerializable()
        {
            //assert
            typeof(CPolynomial).Should().BeDecoratedWith<SerializableAttribute>();
        }

        [Test]
        public void FromRootsTest()
        {
            //arrange
            double TOL = 10E-10;
            Complex[] roots = new Complex[] { 3, 0, 158.3, 13, 8 };

            //action
            CPolynomial poly = CPolynomial.FromRoots(roots);

            //assert
            for (int i = 0; i < roots.Length; i++)
            {
                Complex p = poly.Evaluate(roots[i]);
                Complex.Abs(p).Should().BeLessThan(TOL);
            }
        }

        [Test]
        public void FirstDerivativeTest()
        {
            //arrange
            CPolynomial target = new CPolynomial(new Complex[] {5, 8, -14, 3, 6, -22});
            Complex value = 3;
            Complex expected = -8257;
            
            //action
            Complex actual = target.FirstDerivative(value);

            //assert
            actual.Should().Be(expected);
        }

        [Test]
        public void FirstDerivativeTest_Analytical()
        {
            //arrange
            CPolynomial target = new CPolynomial(new Complex[] { 5, 8, -14, 3, 6, -22 });
            CPolynomial expected = new CPolynomial(new Complex[] {8, -28, 9, 24, -110});

            //action
            CPolynomial actual = target.FirstDerivative();

            //assert
            actual.Should().Be(expected);
        }

        [Test]
        public void SecondDerivativeTest()
        {
            //arrange
            CPolynomial target = new CPolynomial(new Complex[] { 5, 8, -14, 3, 6, -22 });
            Complex c = 3;
            Complex expected = -11206;

            //action
            Complex actual = target.SecondDerivative(c);

            //assert
            actual.Should().Be(expected);
        }

        [Test]
        public void SecondDerivativeTest_Analytical()
        {
            //arrange
            CPolynomial target = new CPolynomial(new Complex[] { 5, 8, -14, 3, 6, -22 });
            CPolynomial expected = new CPolynomial(new Complex[] { -28, 18, 72, -440 });

            //action
            CPolynomial actual = target.SecondDerivative();

            //assert
            actual.Should().Be(expected);
        }

        [Test]
        public void NthDerivativeTest()
        {
            //arrange
            CPolynomial target = new CPolynomial(new Complex[] { 5, 8, -14, 3, 6, -22 });
            Complex c = 3;
            Complex expected = -11430;

            //action
            Complex actual = target.NthDerivative(3, c);

            //assert
            actual.Should().Be(expected);
        }

        [Test]
        public void NthDerivativeTest_Analytical()
        {
            //arrange
            CPolynomial target = new CPolynomial(new Complex[] { 5, 8, -14, 3, 6, -22 });
            CPolynomial expected = new CPolynomial(new Complex[] { 18, 144, -1320 });

            //action
            CPolynomial actual = target.NthDerivative(3);

            //assert
            actual.Should().Be(expected);
        }

        [Test]
        public void AntiderivativeTest()
        {
            //arrange
            CPolynomial target = new CPolynomial(new Complex[] { 3, 8, 16 });
            CPolynomial expected = new CPolynomial(new Complex[] { 0, 3, 4, 16.0 / 3 });

            //action
            CPolynomial actual = target.Antiderivative();

            //assert
            actual.Should().Be(expected);
        }

        [Test]
        public void DivBinomTest()
        {
            //arrange
            double TOL = 1E-10;
            CPolynomial f = new CPolynomial(new Complex[] {3, 0.2, new Complex(3, 8.2), -0.16, new Complex(2.3, 2), 0, 18.3466, 2000});
            Complex c = 346.34645;
            Complex r, rtest;
            
            //action
            CPolynomial q = CPolynomial.DivBinom(f, c, out r);

            f[0] -= r;
            CPolynomial qtest = CPolynomial.DivBinom(f, c, out rtest);

            //assert
            Complex.Abs(rtest).Should().BeLessOrEqualTo(TOL);
            q.Should().Be(qtest);
        }

        [Test]
        public void DivTest()
        {
            //arrange
            double TOL = 1E-12;

            CPolynomial f = new CPolynomial(new Complex[] { new Complex(3467.2, 456), new Complex(2.225, -0.0123), new Complex(46.2 ,4.24), new Complex(2, 2), new Complex(12.8, 16.3), new Complex(0, 0), new Complex(22, 347) });
            CPolynomial g = new CPolynomial(new Complex[] { new Complex(3, 8.5), new Complex(11, -0.5), new Complex(0, 1), new Complex(1, 2), new Complex(346, 4.365) });
            CPolynomial zero = new CPolynomial(1);

            CPolynomial r;
            CPolynomial q = CPolynomial.Divide(f, g, out r);
            CPolynomial rtest = new CPolynomial(1);
            
            //action
            CPolynomial qtest = CPolynomial.Divide(f - r, g, out rtest);

            //assert
            for (int j = 0; j < rtest.Length; j++)
            {
                Complex.Abs(rtest[j]).Should().BeLessOrEqualTo(TOL);
            }
            qtest.Should().Be(q);
        }

        [TestCase(3.6,0, 5,-32.436, -8,0, 0,0, 1000.01,-6.6)]
        public void LaguerreRootsFindingTest(params double[] nums)
        {
            //arrange
            double TOL = 10E-7;
            CPolynomial poly = FromArray(nums);

            //action
            Complex[] roots = CPolynomial.LaguerreRootsFinding(poly);

            //assert
            for (int i = 0; i < roots.Length; i++)
            {
                Complex p = poly.Evaluate(roots[i]);
                Complex.Abs(p).Should().BeLessOrEqualTo(TOL);
            }
        }

        [Test]
        public void InterpolatingPolynomialTest()
        {
            //arrange
            double TOL = 1E-6;
            double[] xValues = new double[] { 1, 2.5, 6, 7.9, 18 };
            double[] yValues = new double[] { 8.6, 0, -2, 9, 33 };

            //action
            CPolynomial poly = CPolynomial.InterpolatingPolynomial(xValues, yValues);

            //assert
            for (int j = 0; j < xValues.Length; j++)
            {
                Complex val = poly.Evaluate(xValues[j]);
                NumericUtil.FuzzyEquals(yValues[j], val, TOL).Should().BeTrue();
            }
        }

        [Test]
        public void ToStringTest()
        {
            //arrange
            CPolynomial poly = new CPolynomial(new Complex[] { 3, 1.1, 1, 0, 0, new Complex(-5, 12.3) });
            string expected = "3 + 1.1*t + t^2 + (-5 + 12.3i)*t^5";

            //action
            var actual = poly.ToString(null, CultureInfo.InvariantCulture, "t");

            //assert
            actual.Should().Be(expected);
        }

        [Test]
        public void ParseTest_Success()
        {
            //arrange
            CPolynomial expected = new CPolynomial(new Complex[] { 0, 1 });

            //action
            CPolynomial actual = CPolynomial.Parse("+x");

            //assert
            actual.Should().Be(expected);
        }

        [Test]
        public void ParseTest_Success2()
        {
            //arrange
            CPolynomial expected = new CPolynomial(new Complex[] { new Complex(0, -0.3) });

            //action
            CPolynomial actual = CPolynomial.Parse("-0.3i");

            //assert
            actual.Should().Be(expected);
        }

        [Test]
        public void ParseTest_Success3()
        {
            //arrange
            CPolynomial expected = new CPolynomial(new Complex[] { 0, 2 });

            //action
            CPolynomial actual = CPolynomial.Parse("2x");

            //assert
            actual.Should().Be(expected);
        }

        [Test]
        public void ParseTest_Success4()
        {
            //arrange
            CPolynomial expected = new CPolynomial(new Complex[] { 3, 1, 1, 0, 0, new Complex(-5, 12.3) });

            //action
            CPolynomial actual = CPolynomial.Parse("x+3 + x^2-(5-12.3i)*x^5");

            //assert
            actual.Should().Be(expected);
        }

        [TestCase("2*x+y")]
        [TestCase("2*x^3.5")]
        public void ParseTest_Fail(string s)
        {
            //action
            Action action = () => CPolynomial.Parse(s);

            //assert
            action.ShouldThrow<FormatException>();
        }


        [Test]
        public void WriteXmlTest_Serialize()
        {
            //arrange
            CPolynomial poly = new CPolynomial(new Complex[] { 3, 5.2, new Complex(3, -0.2), 0, new Complex(2, 35) });
            string expected = "<CPolynomial>3+5.2*x+(3-0.2i)*x^2+(2+35i)*x^4</CPolynomial>";
            XmlSerializer serializer = new XmlSerializer(typeof(CPolynomial));
            StringBuilder sb = new StringBuilder();

            //action
            using (XmlWriter xw = XmlWriter.Create(sb, new XmlWriterSettings { OmitXmlDeclaration = true }))
            {
                serializer.Serialize(xw, poly);
            }

            //assert
            sb.ToString().Should().Be(expected);
        }

        [Test]
        public void ReadXmlTest_Deserialize()
        {
            //arrange
            CPolynomial expected = new CPolynomial(new Complex[] { 3, 10.2, new Complex(3, -0.2), 0, new Complex(2, 35) });
            CPolynomial actual;
            XmlSerializer serializer = new XmlSerializer(typeof(CPolynomial));
            string xml = "<CPolynomial>3 + 10.2*x + (3 - 0.2i)*x^2 + (2 + 35i)*x^4</CPolynomial>";

            //action
            using (StringReader reader = new StringReader(xml))
            {
                actual = (CPolynomial)serializer.Deserialize(reader);
            }

            //assert
            actual.Should().Be(expected);
        }


        #region Helpers

        public static CPolynomial FromArray(double[] nums)
        {
            if (nums.Length % 2 != 0)
                throw new ArgumentException();

            Complex[] data = new Complex[nums.Length / 2];
            for (int i = 0; i < nums.Length / 2; i++)
            {
                data[i] = new Complex(nums[i * 2], nums[i * 2 + 1]);
            }

            return new CPolynomial(data);
        }

        #endregion
    }
}
