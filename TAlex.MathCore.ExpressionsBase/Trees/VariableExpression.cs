using System;
using System.Linq;
using System.Collections.Generic;


namespace TAlex.MathCore.ExpressionEvaluation.Trees
{
    public class VariableExpression<T> : Expression<T>
    {
        private T _value;
        private bool _assigned;


        public readonly string VariableName;

        public T Value
        {
            get
            {
                return _value;
            }

            set
            {
                _assigned = true;
                _value = value;
            }
        }


        public VariableExpression(string variableName)
        {
            VariableName = variableName;
        }

        public override T Evaluate()
        {
            if (!_assigned) throw new UnassignedVariableException(VariableName);
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
