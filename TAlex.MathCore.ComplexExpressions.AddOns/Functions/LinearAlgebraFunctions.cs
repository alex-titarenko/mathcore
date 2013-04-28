using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAlex.MathCore.ExpressionEvaluation.Trees;
using TAlex.MathCore.ExpressionEvaluation.Trees.Metadata;
using TAlex.MathCore.LinearAlgebra;

namespace TAlex.MathCore.ExpressionEvaluation.ComplexExpressions.Functions
{
    [DisplayName("Matrix")]
    [Category(Categories.LinearAlgebra)]
    [Description("Creates new empty complex matrix with specified count of rows and columns.")]
    [FunctionSignature("matrix", "integer rows", "integer cols")]
    [ExampleUsage("matrix(2, 3)", "{0,0,0; 0,0,0}")]
    public class MatrixFuncExression : BinaryExpression<Object>
    {
        public MatrixFuncExression(Expression<Object> rowsExpression, Expression<Object> colsExpression)
            : base(rowsExpression, colsExpression)
        {
        }

        public override object Evaluate()
        {
            return new CMatrix(LeftExpression.EvaluateAsInt32(), RightExpression.EvaluateAsInt32());
        }
    }
}
