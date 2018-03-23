using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using TAlex.MathCore.Optimization.EvolutionaryAlgorithms;
using TAlex.MathCore.Optimization.EvolutionaryAlgorithms.GeneticAlgorithms;
using TAlex.MathCore.Optimization.Tests.EvolutionaryAlgorithms.GeneticAlgorithms.Problems.ArtificialAnt;
using FluentAssertions;
using TAlex.MathCore.Optimization.RandomGenerators;
using System.Diagnostics;


namespace TAlex.MathCore.Optimization.Tests.EvolutionaryAlgorithms.GeneticAlgorithms
{
    [TestFixture]
    public class SimpleGeneticAlgorithmTests
    {
        private static readonly int Iterations = 50;
        protected Problem Problem;
        protected SimpleGeneticAlgorithm Target;


        [SetUp]
        public virtual void SetUp()
        {
            Problem = new ArtificialAntProblem
            {
                AppleField = AppleField.ReadFieldFromStrings(Fields.SimpleField)
            };

            Target = new SimpleGeneticAlgorithm(Problem)
            {
                GenerationSize = 200,
                RandomGenerator = new RandomGenerator(0)
            };
        }


        #region NextGeneration

        [Test]
        public void NextGeneration_BestIndividual()
        {
            //arrange
            Target.Initialize();

            //action
            for (var i = 0; i < Iterations; i++)
            {
                Target.NextGeneration();
            }

            //assert
            Target.Population.Count.Should().Be(Target.GenerationSize);
            Target.BestIndividual.Fitness.Should().BeGreaterThan(78);
        }

        #endregion
    }
}
