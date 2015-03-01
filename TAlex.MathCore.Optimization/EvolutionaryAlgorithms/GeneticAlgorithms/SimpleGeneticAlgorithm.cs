using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace TAlex.MathCore.Optimization.EvolutionaryAlgorithms.GeneticAlgorithms
{
    public class SimpleGeneticAlgorithm : GeneticAlgorithm
    {
        #region Fields

        public static readonly double DefaultElitePart = 0.1;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the percentage of elite individuals (value form 0 to 1).
        /// </summary>
        public double ElitePart { get; set; }

        #endregion

        #region Constructors

        public SimpleGeneticAlgorithm(Problem problem)
            : base(problem)
        {
            ElitePart = DefaultElitePart;
        }

        #endregion

        #region Methods

        protected override void NextIteration()
        {
            int populationSize = Population.Count;

            var newGeneration = new List<Individual>(populationSize);
            int eliteSize = (int)(ElitePart * GenerationSize);

            // add elite individuals to new generation
            newGeneration.AddRange(Population.Take(eliteSize));

            for (int i = 0; i < (populationSize - eliteSize) / 2; i++)
            {
                Individual a1, a2;
                a1 = Population[RandomGenerator.Next(populationSize)];
                a2 = Population[RandomGenerator.Next(populationSize)];

                Individual[] s = a1.Crossover(a2, RandomGenerator);
                newGeneration.AddRange(s);
            }

            if (newGeneration.Count < populationSize)
                newGeneration.Add(Population[RandomGenerator.Next(populationSize)].Mutate(RandomGenerator));

            for (int i = 0; i < newGeneration.Count; i++)
            {
                if (RandomGenerator.NextDouble() < MutationProbability)
                    newGeneration[i] = newGeneration[i].Mutate(RandomGenerator);
            }

            newGeneration.Sort();
            newGeneration.Reverse();
            Population = newGeneration;

            if (BestIndividual == null || BestIndividual.CompareTo(Population[0]) < 0)
                BestIndividual = Population[0];
        }

        #endregion
    }
}
