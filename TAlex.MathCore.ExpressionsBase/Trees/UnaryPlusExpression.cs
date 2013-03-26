using System;


namespace TAlex.MathCore.ExpressionEvaluation.Trees
{
    public class UnaryPlusExpression<T> : UnaryExpression<T>
    {
        public UnaryPlusExpression(Expression<T> subExpression)
            : base(subExpression)
        {
        }

        public override T Evaluate()
        {
            return SubExpr.Evaluate();
        }
    }
}
