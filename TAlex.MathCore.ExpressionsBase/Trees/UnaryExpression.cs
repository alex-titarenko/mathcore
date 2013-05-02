using System;
using System.Collections.Generic;


namespace TAlex.MathCore.ExpressionEvaluation.Trees
{
    public abstract class UnaryExpression<T> : Expression<T>
    {
        #region Fields

        public Expression<T> SubExpression;

        #endregion

        #region Constructors

        public UnaryExpression(Expression<T> subExpression)
        {
            SubExpression = subExpression;
        }

        #endregion

        #region Methods

        public override void FindVariable(string name, ref VariableExpression<T> var, ref bool isFound)
        {
            if (!isFound)
                SubExpression.FindVariable(name, ref var, ref isFound);
        }

        public override void FindAllVariables(IList<VariableExpression<T>> foundedVariables)
        {
            SubExpression.FindAllVariables(foundedVariables);
        }

        public override void ReplaceChild(Expression<T> oldExpression, Expression<T> newExpression)
        {
            if (SubExpression == oldExpression)
                SubExpression = newExpression;
            else
                SubExpression.ReplaceChild(oldExpression, newExpression);
        }

        #endregion
    }
}
