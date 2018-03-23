using System;


namespace TAlex.MathCore.Optimization.RandomGenerators
{
    public interface IRandomGenerator
    {
        int Next(int maxValue);
        double NextDouble();
    }
}
