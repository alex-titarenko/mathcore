using System;


namespace TAlex.MathCore.ExpressionEvaluation.Trees.Builders
{
    public interface IExpressionTreeBuilder<T>
    {
        Expression<T> BuildTree(string expression);
    }
}
