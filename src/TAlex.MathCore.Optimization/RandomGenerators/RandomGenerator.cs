using System;


namespace TAlex.MathCore.Optimization.RandomGenerators
{
    public class RandomGenerator : IRandomGenerator
    {
        #region Fields

        private Random _random;

        #endregion

        #region Constructors

        public RandomGenerator()
        {
            _random = new Random();
        }

        public RandomGenerator(int seed)
        {
            _random = new Random(seed);
        }

        #endregion

        #region IRandomGenerator Members

        public int Next(int maxValue)
        {
            return _random.Next(maxValue);
        }

        public double NextDouble()
        {
            return _random.NextDouble();
        }

        #endregion
    }
}
