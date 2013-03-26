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


        public override string ToString()
        {
            return Scalar.ToString();
        }
    }
}
