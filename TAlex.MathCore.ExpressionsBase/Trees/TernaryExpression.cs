using System;
using System.Collections.Generic;


namespace TAlex.MathCore.ExpressionEvaluation.Trees
{
    public abstract class TernaryExpression<T> : Expression<T>
    {
        #region Fields

        public Expression<T> FirstExpression;

        public Expression<T> SecondExpression;

        public Expression<T> ThirdExpression;

        #endregion

        #region Constructors

        public TernaryExpression()
        {
        }

        public TernaryExpression(Expression<T> firstExpression, Expression<T> secondExpression, Expression<T> thirdExpression)
            : this()
        {
            FirstExpression = firstExpression;
            SecondExpression = secondExpression;
            ThirdExpression = thirdExpression;
        }

        #endregion

        #region Methods

        public override void FindVariable(string name, ref VariableExpression<T> var, ref bool isFound)
        {
            if (!isFound)
            {
                FirstExpression.FindVariable(name, ref var, ref isFound);
                SecondExpression.FindVariable(name, ref var, ref isFound);
                ThirdExpression.FindVariable(name, ref var, ref isFound);
            }
        }

        public override void FindAllVariables(IList<VariableExpression<T>> foundedVariables)
        {
            FirstExpression.FindAllVariables(foundedVariables);
            SecondExpression.FindAllVariables(foundedVariables);
            ThirdExpression.FindAllVariables(foundedVariables);
        }

        #endregion
    }
}
