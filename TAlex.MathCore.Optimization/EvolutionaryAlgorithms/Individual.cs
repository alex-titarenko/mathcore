using System;
using System.Collections.Generic;
using TAlex.MathCore.Optimization.RandomGenerators;


namespace TAlex.MathCore.Optimization.EvolutionaryAlgorithms
{
    public abstract class Individual : IComparable<Individual>
    {
        #region Fields

        private double _cachedFitness = double.NaN;

        #endregion

        #region Properties

        public double Fitness
        {
            get
            {
                if (double.IsNaN(_cachedFitness))
                {
                    _cachedFitness = EvaluateFitness();
                }
                return _cachedFitness;
            }
        }

        #endregion

        #region Methods

        public abstract Individual Mutate(IRandomGenerator randomGenerator);
        public abstract IEnumerable<Individual> Crossover(Individual another, IRandomGenerator randomGenerator);

        public abstract double EvaluateFitness();


        public override string ToString()
        {
            return String.Format("{0}: {1}", GetType().Name, _cachedFitness);
        }

        #endregion

        #region IComparable<Individual> Members

        public int CompareTo(Individual other)
        {
            return Fitness.CompareTo(other.Fitness);
        }

        #endregion
    }
}
