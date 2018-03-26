using System;
using System.Collections.Generic;
using System.Linq;
using TAlex.MathCore.Optimization.RandomGenerators;


namespace TAlex.MathCore.Optimization.EvolutionaryAlgorithms.GeneticAlgorithms
{
    public abstract class GeneticAlgorithm : IEvolutionaryAlgorithm
    {
        #region Fields

        public static readonly double DefaultMutationProbability = 0.1;
        public static readonly int DefaultMaxGenerationNumber = 1000;
        public static readonly int DefaultGenerationSize = 500;

        #endregion

        #region Properties

        public Problem Problem { get; private set; }

        public double MutationProbability { get; set; }

        public int GenerationSize { get; set; }

        public int MaxGenerationNumber { get; set; }

        public IRandomGenerator RandomGenerator { get; set; }

        #endregion

        #region Constructors

        public GeneticAlgorithm(Problem problem)
        {
            Problem = problem;
            MutationProbability = DefaultMutationProbability;
            MaxGenerationNumber = DefaultMaxGenerationNumber;
            GenerationSize = DefaultGenerationSize;

            RandomGenerator = new RandomGenerator();
        }

        #endregion

        #region IEvolutionaryAlgorithm Members

        public int GenerationNumber
        {
            get;
            private set;
        }

        public IList<Individual> Population
        {
            get;
            protected set;
        }

        public Individual BestIndividual
        {
            get;
            protected set;
        }

        public void Initialize()
        {
            Population = new List<Individual>(GenerationSize);
            lock (Population)
            {
                for (int i = 0; i < GenerationSize; i++)
                {
                    Population.Add(Problem.CreateIndividual(RandomGenerator));
                }
            }
        }

        public void NextGeneration()
        {
            GenerationNumber++;
            NextIteration();

            if (MaxGenerationNumber > 0 && GenerationNumber >= MaxGenerationNumber)
            {
                GenerationNumber = 0;

                Initialize();
                Population = Population.OrderByDescending(x => x).ToList();
            }
        }

        protected abstract void NextIteration();

        public virtual bool Terminated()
        {
            return false;
        }

        #endregion
    }
}
