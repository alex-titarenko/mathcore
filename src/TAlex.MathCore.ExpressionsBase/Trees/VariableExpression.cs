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

        public readonly string DisplayName;

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
            : this(variableName, variableName)
        {
        }

        public VariableExpression(string variableName, string displayName)
        {
            VariableName = variableName;
            DisplayName = displayName;
        }

        public override T Evaluate()
        {
            if (!_assigned) throw new UnassignedVariableException(VariableName);
            return Value;
        }

        public override string ToString()
        {
            return DisplayName;
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

        public override void ReplaceChild(Expression<T> oldExpression, Expression<T> newExpression)
        {
        }
    }
}
