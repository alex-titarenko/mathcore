using System;
using System.Collections.Generic;


namespace TAlex.MathCore.ExpressionEvaluation.Trees
{
    public abstract class UnaryExpression<T> : Expression<T>
    {
        #region Fields

        public Expression<T> SubExpr;

        #endregion

        #region Constructors

        public UnaryExpression(Expression<T> subExpression)
        {
            SubExpr = subExpression;
        }

        #endregion

        #region Methods

        public override void FindVariable(string name, ref VariableExpression<T> var, ref bool isFound)
        {
            if (!isFound)
                SubExpr.FindVariable(name, ref var, ref isFound);
        }

        public override void FindAllVariables(IList<VariableExpression<T>> foundedVariables)
        {
            SubExpr.FindAllVariables(foundedVariables);
        }

        #endregion
    }
}
