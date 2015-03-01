using System;
using System.Collections.Generic;


namespace TAlex.MathCore.Optimization.EvolutionaryAlgorithms
{
    public interface IEvolutionaryAlgorithm
    {
        int GenerationNumber { get; }
        IList<Individual> Population { get; }
        Individual BestIndividual { get; }

        void Initialize();
        void NextGeneration();
        bool Terminated();
    }
}
