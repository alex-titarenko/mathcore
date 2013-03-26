using System;
using System.Collections.Generic;


namespace TAlex.MathCore.ExpressionEvaluation.Trees
{
    public abstract class MultiaryExpression<T> : Expression<T>
    {
        #region Fields

        public ICollection<Expression<T>> Expressions;

        #endregion

        #region Constructors

        public MultiaryExpression(params Expression<T>[] expressions)
        {
            Expressions = expressions;
        }

        #endregion

        #region Methods

        public override void FindVariable(string name, ref VariableExpression<T> var, ref bool isFound)
        {
            if (!isFound)
            {
                foreach (Expression<T> subExpr in Expressions)
                {
                    subExpr.FindVariable(name, ref var, ref isFound);
                }
            }
        }

        public override void FindAllVariables(IList<VariableExpression<T>> foundedVariables)
        {
            foreach (Expression<T> subExpr in Expressions)
            {
                subExpr.FindAllVariables(foundedVariables);
            }
        }

        #endregion
    }
}
