using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using TAlex.MathCore;

namespace TAlex.MathCore.Test
{
    /// <summary>
    /// This is a test class for FractionTest and is intended
    /// to contain all FractionTest Unit Tests
    ///</summary>
    [TestClass()]
    public class FractionTest
    {
        #region Fields

        private TestContext testContextInstance;

        #endregion

        #region Properties

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        ///A test for op_Equality
        ///</summary>
        [TestMethod()]
        public void op_EqualityTest()
        {
            Fraction frac1 = new Fraction(39, 99);
            Fraction frac2 = new Fraction(220272, 559152);
            bool expected = true;

            bool actual = (frac1 == frac2);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for op_Addition
        ///</summary>
        [TestMethod()]
        public void op_AdditionTest()
        {
            Fraction frac1 = new Fraction(5, 6);
            Fraction frac2 = new Fraction(8, 9);
            Fraction expected = new Fraction(31, 18);

            Fraction actual = frac1 + frac2;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for op_Subtraction
        ///</summary>
        [TestMethod()]
        public void op_SubtractionTest()
        {
            Fraction frac1 = new Fraction(5, 6);
            Fraction frac2 = new Fraction(15, 4);
            Fraction expected = new Fraction(-35, 12);

            Fraction actual = (frac1 - frac2);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for op_Multiply
        ///</summary>
        [TestMethod()]
        public void op_MultiplyTest()
        {
            Fraction frac1 = new Fraction(5, 6);
            Fraction frac2 = new Fraction(8, 9);
            Fraction expected = new Fraction(20, 27);

            Fraction actual = frac1 * frac2;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for op_Division
        ///</summary>
        [TestMethod()]
        public void op_DivisionTest()
        {
            Fraction frac1 = new Fraction(5, -8);
            Fraction frac2 = new Fraction(15, 2);
            Fraction expected = new Fraction(-1, 12);

            Fraction actual = (frac1 / frac2);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Pow
        ///</summary>
        [TestMethod()]
        public void PowTest()
        {
            Fraction frac = new Fraction(-3, 8);
            int exponent = -3;
            Fraction expected = new Fraction(-512, 27);

            Fraction actual = Fraction.Pow(frac, exponent);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Parse
        ///</summary>
        [TestMethod()]
        public void ParseTest()
        {
            string s = "-3/5";
            Fraction expected = new Fraction(-3, 5);

            Fraction actual = Fraction.Parse(s);
            Assert.AreEqual(expected, actual);
        }

        #endregion
    }
}
