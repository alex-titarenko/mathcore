using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TAlex.MathCore.Optimization.EvolutionaryAlgorithms;


namespace TAlex.MathCore.Optimization.Tests.EvolutionaryAlgorithms.GeneticAlgorithms.Problems.ArtificialAnt
{
    public class AntMover
    {
        #region Fields

        private FiniteAutomaton _ant;
        private readonly AppleField _originalField;

        private AppleField _currentField;
        private int _currentState;

        #endregion

        #region Properties

        public Cell CurrentCell { get; protected set; }
        public Direction CurrentDirection { get; protected set; }
        public bool[,] CurrentField { get { return _currentField.Field; } }

        protected int ObservableCells { get; private set; }
        protected int ViewRadius { get; private set; }

        private FiniteAutomaton Ant
        {
            get
            {
                return _ant;
            }

            set
            {
                _ant = value;
                ObservableCells = (int)Math.Log(_ant.EventsNumber, 2);
                ViewRadius = (ObservableCells == 1) ? 1 : (int)Math.Sqrt(ObservableCells + 1);
            }
        }

        #endregion

        #region Constructors

        public AntMover(FiniteAutomaton automation, AppleField field)
        {
            Ant = automation;
            _originalField = field;
            Restart();
        }

        #endregion

        #region Methods

        public void Restart()
        {
            CurrentDirection = Direction.Right;
            CurrentCell = new Cell(0, 0);

            _currentState = Ant.InitialState;
            _currentField = _originalField.Clone();
        }

        public bool Move()
        {
            int @event;

            var isFoodInFront = Observe(out @event);
            var transition = Ant.Transitions[_currentState, @event];

            if (transition.EndState >= 0 && transition.EndState < Ant.StatesNumber)
            {
                var action = (ArtificialAnt.AntActions)transition.Output;
                _currentState = transition.EndState;

                switch (action)
                {
                    case ArtificialAnt.AntActions.Left:
                        CurrentDirection = GetLeftDirection(CurrentDirection);
                        break;
                    case ArtificialAnt.AntActions.Right:
                        CurrentDirection = GetRightDirection(CurrentDirection);
                        break;
                    case ArtificialAnt.AntActions.Move:
                        CurrentCell = CurrentCell.Next(CurrentDirection, _currentField.FieldWidth, _currentField.FieldHeight);
                        CurrentField[CurrentCell.X, CurrentCell.Y] = false;
                        break;
                }

                return (isFoodInFront && (action == ArtificialAnt.AntActions.Move));
            }

            throw new Exception(String.Format("ends fail: {0}", transition.EndState));
        }

        private bool Observe(out int @event)
        {
            var nextCell = CurrentCell.Next(CurrentDirection, _currentField.FieldWidth, _currentField.FieldHeight);
            var isFoodInFront = CurrentField[nextCell.X, nextCell.Y];

            @event = 0;
            if (ViewRadius == 1)
            {
                @event = isFoodInFront ? 1 : 0;
            }
            else
            {
                var left = GetLeftDirection(CurrentDirection);
                var right = GetRightDirection(CurrentDirection);

                nextCell = CurrentCell;
                for (int i = 0; i < ViewRadius - 1; i++)
                    nextCell = nextCell.Next(left, _currentField.FieldWidth, _currentField.FieldHeight);
                Cell startCell = nextCell;
                int currCollLength = ViewRadius * 2 - 1;
                int cellsElapsed = 0;

                for (int i = 0; i < ObservableCells; i++)
                {
                    if (nextCell.X == CurrentCell.X && nextCell.Y == CurrentCell.Y)
                    {
                        nextCell = nextCell.Next(right, _currentField.FieldWidth, _currentField.FieldHeight);
                        cellsElapsed++;
                    }

                    @event |= _currentField[nextCell.X, nextCell.Y] ? 1 << i : 0;

                    cellsElapsed++;
                    if (cellsElapsed >= currCollLength)
                    {
                        cellsElapsed = 0;
                        currCollLength -= 2;
                        startCell = startCell.Next(right, _currentField.FieldWidth, _currentField.FieldHeight).Next(CurrentDirection, _currentField.FieldWidth, _currentField.FieldHeight);
                        nextCell = startCell;
                    }
                    else
                    {
                        nextCell = nextCell.Next(right, _currentField.FieldWidth, _currentField.FieldHeight);
                    }
                }
            }

            return isFoodInFront;
        }


        private static Direction GetRightDirection(Direction direction)
        {
            return (Direction)(((byte)direction + 1) % 4);
        }

        private static Direction GetLeftDirection(Direction direction)
        {
            return (Direction)(((byte)direction + 3) % 4);
        }

        #endregion

        #region Nested types

        public enum Direction : byte
        {
            Up = 0,
            Right,
            Down,
            Left
        }

        public class Cell
        {
            public int X { get; private set; }
            public int Y { get; private set; }


            public Cell(int x, int y)
            {
                X = x;
                Y = y;
            }


            public Cell Next(Direction direction, int fieldWidth, int fieldHeight)
            {
                switch (direction)
                {
                    case Direction.Left:
                        return new Cell((X + (fieldWidth - 1)) % fieldWidth, Y);
                    case Direction.Up:
                        return new Cell(X, (Y + (fieldHeight - 1)) % fieldHeight);
                    case Direction.Right:
                        return new Cell((X + 1) % fieldWidth, Y);
                    case Direction.Down:
                        return new Cell(X, (Y + 1) % fieldHeight);
                    default:
                        throw new ArgumentOutOfRangeException("direction");
                }
            }


            public override string ToString()
            {
                return String.Format("(x: {0}; y: {1})", X, Y);
            }
        }

        #endregion
    }
}
