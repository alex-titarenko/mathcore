using System;


namespace TAlex.MathCore.ExpressionEvaluation.Trees
{
    public abstract class PowExpression<T> : BinaryExpression<T>
    {
        public override string ToString()
        {
            return "(" + LeftExpression + "^" + RightExpression + ")";
        }
    }
}
