using System;


namespace TAlex.MathCore.ExpressionEvaluation.Trees
{
    public abstract class DivExpression<T> : BinaryExpression<T>
    {
        public override string ToString()
        {
            return "(" + LeftExpression + "/" + RightExpression + ")";
        }
    }
}
