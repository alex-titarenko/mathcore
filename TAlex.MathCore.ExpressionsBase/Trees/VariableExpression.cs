using System;
using System.Linq;
using System.Collections.Generic;


namespace TAlex.MathCore.ExpressionEvaluation.Trees
{
    public class VariableExpression<T> : Expression<T>
    {
        public readonly string VariableName;

        public T Value;

        public VariableExpression(string variableName)
        {
            VariableName = variableName;
        }

        public override T Evaluate()
        {
            return Value;
        }

        public override string ToString()
        {
            return VariableName;
        }


        public override void FindVariable(string name, ref VariableExpression<T> var, ref bool isFound)
        {
            if (!isFound)
            {
                if (VariableName == name)
                {
                    var = this;
                    isFound = true;
                }
            }
        }

        public override void FindAllVariables(IList<VariableExpression<T>> foundedVariables)
        {
            if (!foundedVariables.Contains(this))
            {
                foundedVariables.Add(this);
            }
        }
    }
}
