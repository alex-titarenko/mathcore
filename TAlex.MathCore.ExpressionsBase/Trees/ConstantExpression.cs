using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TAlex.MathCore.ExpressionEvaluation.Trees
{
    public abstract class ConstantExpression<T> : Expression<T>
    {
        public abstract string Name { get; }

        public override string ToString()
        {
            return Name;
        }
    }
}
