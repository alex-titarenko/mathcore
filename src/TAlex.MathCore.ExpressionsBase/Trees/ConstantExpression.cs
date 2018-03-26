using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TAlex.MathCore.ExpressionEvaluation.Trees
{
    public abstract class ConstantExpression<T> : Expression<T>
    {
        public abstract string Name { get; }


        public override void FindVariable(string name, ref VariableExpression<T> var, ref bool isFound)
        {
        }

        public override void FindAllVariables(IList<VariableExpression<T>> foundedVariables)
        {
        }

        public override void ReplaceChild(Expression<T> oldExpression, Expression<T> newExpression)
        {
        }


        public override string ToString()
        {
            return Name;
        }
    }
}
