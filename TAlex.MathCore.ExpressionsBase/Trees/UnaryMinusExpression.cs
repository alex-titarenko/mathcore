using System;


namespace TAlex.MathCore.ExpressionEvaluation.Trees
{
    public abstract class UnaryMinusExpression<T> : UnaryExpression<T>
    {
        public UnaryMinusExpression(Expression<T> subExpression)
            : base(subExpression)
        {
        }

        public override string ToString()
        {
            return "-" + SubExpr.ToString();
        }
    }
}
