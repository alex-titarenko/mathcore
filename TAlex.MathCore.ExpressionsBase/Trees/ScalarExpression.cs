using System;


namespace TAlex.MathCore.ExpressionEvaluation.Trees
{
    public class ScalarExpression<T> : Expression<T>
    {
        public readonly T Scalar;

        public ScalarExpression(T scalar)
        {
            Scalar = scalar;
        }

        public override T Evaluate()
        {
            return Scalar;
        }


        public override void FindVariable(string name, ref VariableExpression<T> var, ref bool isFound)
        {
        }

        public override void FindAllVariables(System.Collections.Generic.IList<VariableExpression<T>> foundedVariables)
        {
        }

        public override void ReplaceChild(Expression<T> oldExpression, Expression<T> newExpression)
        {
        }


        public override string ToString()
        {
            return Scalar.ToString();
        }
    }
}
