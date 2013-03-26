using System;
using System.Collections.Generic;
using System.Text;

using TAlex.MathCore;
using TAlex.MathCore.LinearAlgebra;

namespace TAlex.MathCore.LinearAlgebra.Test.Helpers
{
    public class RandomGenerator
    {
        #region Fields

        private Random _rand;

        #endregion

        #region Constructors

        public RandomGenerator()
        {
            _rand = new Random();
        }

        public RandomGenerator(int Seed)
        {
            _rand = new Random(Seed);
        }

        #endregion

        #region Methods

        public double NextDouble(double lowerBound, double upperBound, int decimals)
        {
            double width = upperBound - lowerBound;
            return Math.Round(_rand.NextDouble() * width + lowerBound, decimals);
        }

        public Complex NextComplex(double lowerBound, double upperBound, int decimals)
        {
            double width = upperBound - lowerBound;
            return new Complex(
                    Math.Round(_rand.NextDouble() * width + lowerBound, decimals),
                    Math.Round(_rand.NextDouble() * width + lowerBound, decimals));
        }

        public Complex NextComplex(double realLowerBound, double realUpperBound, double imagLowerBound, double imagUpperBound, int decimals)
        {
            double realWidth = realUpperBound - realLowerBound;
            double imagWidth = imagUpperBound - imagLowerBound;
            return new Complex(
                Math.Round(_rand.NextDouble() * realWidth + realLowerBound, decimals),
                Math.Round(_rand.NextDouble() * imagWidth + imagLowerBound, decimals));
        }

        public void Fill(double[] array, double lowerBound, double upperBound, int decimals)
        {
            for (int i = 0; i < array.Length; i++)
                array[i] = NextDouble(lowerBound, upperBound, decimals);
        }

        public void Fill(Complex[] array, double lowerBound, double upperBound, int decimals)
        {
            for (int i = 0; i < array.Length; i++)
                array[i] = NextComplex(lowerBound, upperBound, decimals);
        }

        public void Fill(Complex[] array, double lowerBound, double upperBound, int decimals, bool imaginaryFill)
        {
            if (imaginaryFill)
            {
                for (int i = 0; i < array.Length; i++)
                    array[i] = NextComplex(lowerBound, upperBound, decimals);
            }
            else
            {
                for (int i = 0; i < array.Length; i++)
                    array[i] = NextDouble(lowerBound, upperBound, decimals);
            }
        }

        public void Fill(CPolynomial poly, double lowerBound, double upperBound, int decimals)
        {
            for (int i = 0; i < poly.Length; i++)
                poly[i] = NextComplex(lowerBound, upperBound, decimals);
        }

        public void Fill(CPolynomial poly, double lowerBound, double upperBound, int decimals, bool imaginaryFill)
        {
            if (imaginaryFill)
            {
                for (int i = 0; i < poly.Length; i++)
                    poly[i] = NextComplex(lowerBound, upperBound, decimals);
            }
            else
            {
                for (int i = 0; i < poly.Length; i++)
                    poly[i] = NextDouble(lowerBound, upperBound, decimals);
            }
        }

        public void Fill(CMatrix m, double lowerBound, double upperBound, int decimals)
        {
            for (int i = 0; i < m.RowCount; i++)
            {
                for (int j = 0; j < m.ColumnCount; j++)
                    m[i, j] = NextComplex(lowerBound, upperBound, decimals);
            }
        }

        public void Fill(CMatrix m, double lowerBound, double upperBound, int decimals, bool imaginaryFill)
        {
            if (imaginaryFill)
            {
                for (int i = 0; i < m.RowCount; i++)
                {
                    for (int j = 0; j < m.ColumnCount; j++)
                    {
                        m[i, j] = NextComplex(lowerBound, upperBound, decimals);
                    }
                }
            }
            else
            {
                for (int i = 0; i < m.RowCount; i++)
                {
                    for (int j = 0; j < m.ColumnCount; j++)
                    {
                        m[i, j] = NextDouble(lowerBound, upperBound, decimals);
                    }
                }
            }
        }

        #endregion
    }
}
