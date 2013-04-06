using System;
using System.Collections.Generic;
using TAlex.MathCore.LinearAlgebra;


namespace TAlex.MathCore.ExpressionEvaluation.Trees
{
    public class CMatrixExpression : MultiaryExpression<Object>
    {
        public readonly int Stride;

        public CMatrixExpression(int stride, params Expression<Object>[] expressions)
            : base(expressions)
        {
            Stride = stride;
        }


        public override object Evaluate()
        {
            CMatrix matrix = new CMatrix(Expressions.Count / Stride, Stride);

            int curr_row = 0;
            int curr_col = 0;
            foreach (Expression<Object> expr in Expressions)
            {
                Complex item = (Complex)expr.Evaluate();
                matrix[curr_row, curr_col] = item;

                if (curr_col >= Stride - 1)
                {
                    curr_col = 0;
                    curr_row++;
                }
                else
                {
                    curr_col++;
                }
            }

            return matrix;
        }
    }
}
