using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace TAlex.MathCore.Optimization.EvolutionaryAlgorithms.GeneticAlgorithms
{
    public class CellularGeneticAlgorithm : GeneticAlgorithm
    {
        #region Constructors

        public CellularGeneticAlgorithm(Problem problem)
            : base(problem)
        {
        }

        #endregion

        #region Methods

        protected override void NextIteration()
        {
            int populationSize = Population.Count;
            int n = (int)Math.Sqrt(populationSize);
            int m = populationSize / n;
            int rem = populationSize - n * m;
            
            var newGeneration = new List<Individual>(populationSize);
            
            int indexOfBest = 0;
            double maxProba = 0;
            double currentProba = 0;
            
            for (int i = 0; i < populationSize - rem; i++)
            {
                var neighbours = FindNeighbours(i, n, m);

                indexOfBest = 0;
                maxProba = 0;
                for (int j = 0; j < 4; j++)
                {
                    currentProba = neighbours[j].Fitness * (RandomGenerator.NextDouble() * 0.5 + 0.5);
                    if (currentProba > maxProba)
                    {
                        indexOfBest = j;
                        maxProba = currentProba;
                    }
                }

                var children = neighbours[indexOfBest].Crossover(Population[i], RandomGenerator);
                newGeneration.Add(children.Max());
            }

            for (int i = populationSize - rem; i < populationSize; i++)
            {
                newGeneration.Add(Population[i]);
            }
            if (newGeneration.Count < populationSize)
                newGeneration.Add(Population[RandomGenerator.Next(populationSize)].Mutate(RandomGenerator));

            for (int i = 0; i < newGeneration.Count; i++)
            {
                if (RandomGenerator.NextDouble() < MutationProbability)
                    newGeneration[i] = newGeneration[i].Mutate(RandomGenerator);
            }

            Population = newGeneration;

            foreach (Individual individual in Population)
            {
                if (BestIndividual == null || BestIndividual.CompareTo(individual) < 0)
                    BestIndividual = individual;
            }
        }

        private List<Individual> FindNeighbours(int i, int n, int m)
        {
            var neighbours = new List<Individual>(4);
            if ((i != 0) && (i % (n - 1)) == 0)
            {
                neighbours.Add(Population[i - 1]);
                neighbours.Add(Population[i - n + 1]);
            }
            else
            {
                if ((i % n) == 0)
                {
                    neighbours.Add(Population[i + n - 1]);
                    neighbours.Add(Population[i + 1]);
                }
                else
                {
                    neighbours.Add(Population[i - 1]);
                    neighbours.Add(Population[i + 1]);

                }
            }
            if (i < n)
            {
                neighbours.Add(Population[i + (m - 1) * n]);
                neighbours.Add(Population[i + n]);
            }
            else
            {
                if (i / n == (m - 1))
                {
                    neighbours.Add(Population[i % n]);
                    neighbours.Add(Population[i - n]);
                }
                else
                {
                    neighbours.Add(Population[i + n]);
                    neighbours.Add(Population[i - n]);
                }
            }

            return neighbours;
        }

        #endregion
    }
}
