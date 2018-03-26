using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAlex.MathCore.Optimization.EvolutionaryAlgorithms;
using TAlex.MathCore.Optimization.RandomGenerators;


namespace TAlex.MathCore.Optimization.Tests.EvolutionaryAlgorithms.GeneticAlgorithms.Problems.ArtificialAnt
{
    public class ArtificialAntProblem : Problem
    {
        #region Properties

        public int MaxStepsCount { get; set; }

        public int StatesNumber { get; set; }

        public int ViewRadius { get; set; }

        public AppleField AppleField { get; set; }


        public int ObservableCells
        {
            get
            {
                return Math.Max(1, ViewRadius * ViewRadius - 1);
            }
        }

        public int EventsNumber
        {
            get
            {
                return (int)Math.Round(Math.Pow(2, ObservableCells));
            }
        }

        #endregion

        #region Constructors

        public ArtificialAntProblem()
        {
            MaxStepsCount = 200;
            StatesNumber = 8;
            ViewRadius = 1;
        }

        #endregion

        #region Methods

        public override Individual CreateIndividual(IRandomGenerator randomGenerator)
        {
            var ant = new ArtificialAnt(this, 0, StatesNumber, EventsNumber);

            for (int state = 0; state < StatesNumber; state++)
            {
                for (int @event = 0; @event < EventsNumber; @event++)
                {
                    ant.Transitions[state, @event] =
                        new FiniteAutomaton.StateTransition(randomGenerator.Next(StatesNumber), randomGenerator.Next(ant.OutputsNumber));
                }
            }

            return ant;
        }

        public double EvaluateIndividual(ArtificialAnt ant)
        {
            var mover = new AntMover(ant, AppleField);

            int apples = 0;
            int lastStep = 0;
            for (int i = 0; i < MaxStepsCount; ++i)
            {
                // Ate an apple
                if (mover.Move())
                {
                    apples++;
                    // In which steps ate, to determine the best in the case that both ate all the apples
                    lastStep = i;
                }
                if (apples == AppleField.FoodCount)
                    break;
            }

            return apples - 1.0 * lastStep / MaxStepsCount;
        }

        #endregion
    }
}
