using System;
using TAlex.MathCore.Optimization.RandomGenerators;


namespace TAlex.MathCore.Optimization.EvolutionaryAlgorithms
{
    public abstract class Problem
    {
        public abstract Individual CreateIndividual(IRandomGenerator randomGenerator);
    }
}
