using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAlex.MathCore.Optimization.EvolutionaryAlgorithms;


namespace TAlex.MathCore.Optimization.Tests.EvolutionaryAlgorithms.GeneticAlgorithms.Problems.ArtificialAnt
{
    public class ArtificialAnt : FiniteAutomaton
    {
        #region Properties

        public ArtificialAntProblem Problem { get; set; }

        public override int OutputsNumber
        {
            get { return 3; }
        }

        #endregion

        #region Constructors

        public ArtificialAnt(ArtificialAntProblem problem, int initialState, int statesNumber, int eventsNumber)
            : base(initialState, statesNumber, eventsNumber)
        {
            Problem = problem;
        }

        #endregion

        #region Methods

        protected override FiniteAutomaton Create(int initialState, int statesNumber, int eventsNumber)
        {
            return new ArtificialAnt(Problem, initialState, statesNumber, eventsNumber);
        }

        public override double EvaluateFitness()
        {
            return Problem.EvaluateIndividual(this);
        }

        #endregion
        
        #region Nested Types

        public enum AntActions
        {
            Left = 0,
            Move = 1,
            Right = 2
        }

        #endregion
    }
}
