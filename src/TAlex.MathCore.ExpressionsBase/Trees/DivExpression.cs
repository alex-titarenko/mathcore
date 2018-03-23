using System;


namespace TAlex.MathCore.ExpressionEvaluation.Trees
{
    public abstract class DivExpression<T> : BinaryExpression<T>
    {
        private static readonly Type[] LowerPriorityOperators = new[]
        {
            typeof(SubExpression<T>), typeof(AddExpression<T>)
        };


        public override string ToString()
        {
            var left = WrapWithParentheses(LeftExpression, LowerPriorityOperators);
            var right = WrapWithParentheses(RightExpression, LowerPriorityOperators);

            return left + "/" + right;
        }
    }
}
