using System;
using System.Collections.Generic;


namespace TAlex.MathCore.ExpressionEvaluation.Trees
{
    public abstract class BinaryExpression<T> : Expression<T>
    {
        #region Fields
        
        public Expression<T> LeftExpression;

        public Expression<T> RightExpression;

        #endregion

        #region Constructors

        public BinaryExpression()
        {
        }

        public BinaryExpression(Expression<T> leftExpression, Expression<T> rightExpression)
            : this()
        {
            LeftExpression = leftExpression;
            RightExpression = rightExpression;
        }

        #endregion

        #region Methods

        public override void FindVariable(string name, ref VariableExpression<T> var, ref bool isFound)
        {
            if (!isFound)
            {
                LeftExpression.FindVariable(name, ref var, ref isFound);
                RightExpression.FindVariable(name, ref var, ref isFound);
            }
        }

        public override void FindAllVariables(IList<VariableExpression<T>> foundedVariables)
        {
            LeftExpression.FindAllVariables(foundedVariables);
            RightExpression.FindAllVariables(foundedVariables);
        }

        public override void ReplaceChild(Expression<T> oldExpression, Expression<T> newExpression)
        {
            if (LeftExpression == oldExpression)
                LeftExpression = newExpression;
            else
                LeftExpression.ReplaceChild(oldExpression, newExpression);

            if (RightExpression == oldExpression)
                RightExpression = newExpression;
            else
                RightExpression.ReplaceChild(oldExpression, newExpression);
        }

        #endregion
    }
}
