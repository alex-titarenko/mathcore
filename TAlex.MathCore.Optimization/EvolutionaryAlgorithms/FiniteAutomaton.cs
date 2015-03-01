using System;
using System.Collections.Generic;
using System.Linq;
using TAlex.MathCore.Optimization.RandomGenerators;


namespace TAlex.MathCore.Optimization.EvolutionaryAlgorithms
{
    public abstract class FiniteAutomaton : Individual
    {
        #region Properties

        /// <summary>
        /// Gets the states matrix of finete automaton.
        /// </summary>
        public StateTransition[,] Transitions { get; private set; }

        /// <summary>
        /// Gets or sets the initial state of finete automaton.
        /// </summary>
        public int InitialState { get; set; }

        /// <summary>
        /// Gets the number of states.
        /// </summary>
        public int StatesNumber { get; private set; }

        /// <summary>
        /// Gets the number of events.
        /// </summary>
        public int EventsNumber { get; private set; }

        /// <summary>
        /// Gets the number of outputs.
        /// </summary>
        public abstract int OutputsNumber { get; }

        #endregion

        #region Constructors

        public FiniteAutomaton(int initialState, int statesNumber, int eventsNumber)
        {
            InitialState = initialState;
            StatesNumber = statesNumber;
            EventsNumber = eventsNumber;

            Transitions = new StateTransition[statesNumber, eventsNumber];
        }

        #endregion

        #region Methods

        protected abstract FiniteAutomaton Create(int initialState, int statesNumber, int eventsNumber);


        public override Individual Mutate(IRandomGenerator randomGenerator)
        {
            int newInitialState = InitialState;
            if (randomGenerator.Next(2) == 1)
            {
                newInitialState = randomGenerator.Next(StatesNumber);
            }

            var mutatedIndividual = Create(newInitialState, StatesNumber, EventsNumber);

            // copy all states
            for (int i = 0; i < mutatedIndividual.StatesNumber; i++)
            {
                for (int j = 0; j < mutatedIndividual.EventsNumber; j++)
                {
                    var t = Transitions[i, j];
                    mutatedIndividual.Transitions[i, j] = new StateTransition(t.EndState, t.Output);
                }
            }

            int randomState = randomGenerator.Next(StatesNumber);
            int randomEvent = randomGenerator.Next(EventsNumber);
            int randomEndState = randomGenerator.Next(mutatedIndividual.StatesNumber);
            mutatedIndividual.Transitions[randomState, randomEvent] = new StateTransition(randomEndState, randomGenerator.Next(OutputsNumber));

            return mutatedIndividual;
        }

        public override Individual[] Crossover(Individual anotherIndividual, IRandomGenerator randomGenerator)
        {
            var another = anotherIndividual as FiniteAutomaton;
            if (another == null) throw new ArgumentException();

            var children = new FiniteAutomaton[2];
            for (int i = 0; i < 2; i++)
                children[i] = Create(0, StatesNumber, EventsNumber);

            if (randomGenerator.Next(2) == 1)
            {
                children[0].InitialState = another.InitialState;
                children[1].InitialState = InitialState;
            }
            else
            {
                children[0].InitialState = InitialState;
                children[1].InitialState = another.InitialState;
            }

            for (int state = 0; state < StatesNumber; state++)
            {
                int flag = randomGenerator.Next(4);
                switch (flag)
                {
                    case 0:
                        for (int @event = 0; @event < EventsNumber; @event++)
                        {
                            children[0].Transitions[state, @event] = another.Transitions[state, @event];
                            children[1].Transitions[state, @event] = Transitions[state, @event];
                        }
                        break;

                    case 1:
                        for (int @event = 0; @event < EventsNumber; @event++)
                        {
                            children[0].Transitions[state, @event] = Transitions[state, @event];
                            children[1].Transitions[state, @event] = another.Transitions[state, @event];
                        }
                        break;

                    case 2:
                        for (int @event = 0; @event < EventsNumber / 2; @event++)
                        {
                            int secondHalf = @event + EventsNumber / 2;
                            children[0].Transitions[state, @event] = another.Transitions[state, @event];
                            children[0].Transitions[state, secondHalf] = Transitions[state, secondHalf];
                            children[1].Transitions[state, @event] = another.Transitions[state, @event];
                            children[1].Transitions[state, secondHalf] = Transitions[state, secondHalf];
                        }
                        break;

                    case 3:
                        for (int @event = 0; @event < EventsNumber / 2; @event++)
                        {
                            int secondHalf = @event + EventsNumber / 2;
                            children[0].Transitions[state, @event] = Transitions[state, @event];
                            children[0].Transitions[state, secondHalf] = another.Transitions[state, secondHalf];
                            children[1].Transitions[state, @event] = Transitions[state, @event];
                            children[1].Transitions[state, secondHalf] = another.Transitions[state, secondHalf];
                        }
                        break;
                }
            }

            return children;
        }

        #endregion

        #region Nested Types

        /// <summary>
        /// Represents transition, composed from output value and end state.
        /// </summary>
        public class StateTransition
        {
            #region Properties

            /// <summary>
            /// Gets the end state of transition.
            /// </summary>
            public int EndState { get; private set; }

            /// <summary>
            /// Gets the output of transition.
            /// </summary>
            public int Output { get; protected set; }

            #endregion

            #region Constructors

            public StateTransition(int endState, int output)
            {
                EndState = endState;
                Output = output;
            }

            #endregion

            #region Methods

            /// <summary>
            /// Returns the string representation of transition.
            /// </summary>
            /// <returns>the string representation of transition.</returns>
            public override string ToString()
            {
                return String.Format("-{0}->{1}", Output, EndState);
            }

            #endregion
        }

        #endregion
    }
}
