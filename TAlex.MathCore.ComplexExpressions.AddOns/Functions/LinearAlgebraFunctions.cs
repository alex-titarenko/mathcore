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

    [DisplayName("Diagonal")]
    [Category(Categories.LinearAlgebra)]
    [Description("Returns a column vector containing the main diagonal of the complex square matrix, or returns a complex square matrix containing on its main diagonal the elements of the column vector.")]
    [FunctionSignature("diag", "complex matrix m")]
    [FunctionSignature("diag", "complex vector v")]
    [ExampleUsage("diag({1, 2; 3, 4})", "{1; 4}")]
    [ExampleUsage("diag({1; 2})", "{1, 0; 0, 2}")]
    public class DiagonalFuncExpresion : UnaryExpression<Object>
    {
        public DiagonalFuncExpresion(Expression<Object> subExpression)
            : base(subExpression)
        {
        }

        public override object Evaluate()
        {
            return CMatrix.Diagonal(SubExpression.EvaluateAsCMatrix());
        }
    }

    [DisplayName("Identity")]
    [Category(Categories.LinearAlgebra)]
    [Description("Creates the identity matrix of the specified size.")]
    [FunctionSignature("identity", "integer n")]
    [ExampleUsage("identity(2)", "{1, 0; 0, 1}")]
    [ExampleUsage("identity(3)", "{1, 0, 0; 0, 1, 0; 0, 0, 1}")]
    public class IdentityFuncExpression : UnaryExpression<Object>
    {
        public IdentityFuncExpression(Expression<Object> subExpression)
            : base(subExpression)
        {
        }

        public override object Evaluate()
        {
            return CMatrix.Identity(SubExpression.EvaluateAsInt32());
        }
    }
}
