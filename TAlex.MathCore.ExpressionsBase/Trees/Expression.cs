using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TAlex.MathCore.ExpressionEvaluation.Trees
{
    /// <summary>
    /// Provides the base class from which the classes that represent expression
    /// tree nodes are derived. This is an abstract class.
    /// </summary>
    /// <typeparam name="T">The type of the result of evaluating.</typeparam>
    public abstract class Expression<T> : IEvaluator<T>
    {
        #region IEvaluator<T> Members

        public abstract T Evaluate();

        #endregion


        public VariableExpression<T> FindVariable(string name)
        {
            VariableExpression<T> var = null;
            bool isFound = false;
            FindVariable(name, ref var, ref isFound);
            return var;
        }

        public IEnumerable<VariableExpression<T>> FindAllVariables()
        {
            IList<VariableExpression<T>> vars = new List<VariableExpression<T>>();
            FindAllVariables(vars);
            return vars;
        }


        public virtual void FindVariable(string name, ref VariableExpression<T> var, ref bool isFound)
        {
        }

        public virtual void FindAllVariables(IList<VariableExpression<T>> foundedVariables)
        {
        }
    }
}
