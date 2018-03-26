using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TAlex.MathCore.ExpressionEvaluation.Trees
{
    public abstract class ConditionalExpression<T> : Expression<T>
    {
        public Expression<T> Condition;

        public Expression<T> IfTrue;
        public Expression<T> IfFalse;
        
    }
}
