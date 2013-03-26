using System;
using System.Collections.Generic;


namespace TAlex.MathCore.ExpressionEvaluation.Trees
{
    public abstract class BinaryExpression<T> : Expression<T>
    {
        public Expression<T> LeftExpression;

        public Expression<T> RightExpression;


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
    }
}
