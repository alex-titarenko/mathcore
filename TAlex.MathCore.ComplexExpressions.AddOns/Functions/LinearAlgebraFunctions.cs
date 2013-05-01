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

    [DisplayName("Augment concat")]
    [Category(Categories.LinearAlgebra)]
    [Description("Returns a complex matrix formed by horizontal concatenating of the matrices.")]
    [FunctionSignature("augment", "complex matrix m1", "complex matrix m2", "...")]
    [ExampleUsage("augment({1; 2}, {3; 5}, {10; 20})", "{1, 3, 10; 2, 5, 20}")]
    [ExampleUsage("augment({1, 2; 3, 4}, {10, 20; 30, 40})", "{1, 2, 10, 20; 3, 4, 30, 40}")]
    public class AugmentConcatFuncExpression : MultiaryExpression<Object>
    {
        public AugmentConcatFuncExpression(params Expression<Object>[] mExpressions)
            : base(mExpressions)
        {
        }

        public override object Evaluate()
        {
            return CMatrix.AugmentConcat(Expressions.Select(x => x.EvaluateAsCMatrix()).ToArray());
        }
    }

    [DisplayName("Stack concat")]
    [Category(Categories.LinearAlgebra)]
    [Description("Returns a complex matrix formed by vertical concatenating of the matrices.")]
    [FunctionSignature("stack", "complex matrix m1", "complex matrix m2", "...")]
    [ExampleUsage("stack({1; 2}, {3; 5}, {10; 20})", "{1; 2; 3; 5; 10; 20}")]
    [ExampleUsage("stack({1, 2; 3, 4}, {10, 20; 30, 40})", "{1, 2; 3, 4; 10, 20; 30, 40}")]
    public class StackConcatFuncExpression : MultiaryExpression<Object>
    {
        public StackConcatFuncExpression(params Expression<Object>[] mExpressions)
            : base(mExpressions)
        {
        }

        public override object Evaluate()
        {
            return CMatrix.StackConcat(Expressions.Select(x => x.EvaluateAsCMatrix()).ToArray());
        }
    }

    [DisplayName("Submatrix")]
    [Category(Categories.LinearAlgebra)]
    [Description("Returns the specified submatrix.")]
    [FunctionSignature("submatrix", "complex matrix m", "integer r1", "integer r2", "integer c1", "integer c2")]
    [ExampleUsage("submatrix({1, 2, 3; 4, 5, 6}, 1, 1, 1, 2)", "{5, 6}")]
    public class SubmatrixFuncExpression : MultiaryExpression<Object>
    {
        public SubmatrixFuncExpression(Expression<Object> mExpression, Expression<Object> r1Expression, Expression<Object> r2Expression, Expression<Object> c1Expression, Expression<Object> c2Expression)
            : base(mExpression, r1Expression, r2Expression, c1Expression, c2Expression)
        {
        }

        public override object Evaluate()
        {
            CMatrix m = Expressions[0].EvaluateAsCMatrix();
            int r1 = Expressions[1].EvaluateAsInt32();
            int r2 = Expressions[2].EvaluateAsInt32();
            int c1 = Expressions[3].EvaluateAsInt32();
            int c2 = Expressions[4].EvaluateAsInt32();
            
            return m.Submatrix(r1, r2, c1, c2);
        }
    }

    [DisplayName("Get row")]
    [Category(Categories.LinearAlgebra)]
    [Description("Returns the specified row of the matrix using zero based index.")]
    [FunctionSignature("row", "complex matrix m", "integer rowIndex")]
    [ExampleUsage("row({1, 2, 3; 4, 5, 6}, 1)", "{4, 5, 6}")]
    public class RowFuncExpression : BinaryExpression<Object>
    {
        public RowFuncExpression(Expression<Object> mExpression, Expression<Object> indexExpression)
            : base(mExpression, indexExpression)
        {
        }

        public override object Evaluate()
        {
            return LeftExpression.EvaluateAsCMatrix().GetRow(RightExpression.EvaluateAsInt32());
        }
    }

    [DisplayName("Get column")]
    [Category(Categories.LinearAlgebra)]
    [Description("Returns the specified column of the matrix using zero based index.")]
    [FunctionSignature("col", "complex matrix m", "integer colIndex")]
    [ExampleUsage("col({1, 2; 3, 4; 5, 6}, 1)", "{2; 4; 6}")]
    public class ColumnFuncExpression : BinaryExpression<Object>
    {
        public ColumnFuncExpression(Expression<Object> mExpression, Expression<Object> indexExpression)
            : base(mExpression, indexExpression)
        {
        }

        public override object Evaluate()
        {
            return LeftExpression.EvaluateAsCMatrix().GetColumn(RightExpression.EvaluateAsInt32());
        }
    }

    [DisplayName("Get element")]
    [Category(Categories.LinearAlgebra)]
    [Description("Returns the element of complex vector or matrix using zero based indexes.")]
    [FunctionSignature("elem", "complex matrix m", "integer rowIndex")]
    [FunctionSignature("elem", "complex matrix m", "integer rowIndex", "integer colIndex")]
    [ExampleUsage("elem({1; 2; 3; 4}, 2)", "3")]
    [ExampleUsage("elem({1, 2; 3, 4}, 0, 1)", "2")]
    public class ElementFuncExpression : TernaryExpression<Object>
    {
        public ElementFuncExpression(Expression<Object> mExpression, Expression<Object> rowIndexExpression)
            : this(mExpression, rowIndexExpression, null)
        {
        }

        public ElementFuncExpression(Expression<Object> mExpression, Expression<Object> rowIndexExpression, Expression<Object> colIndexExpression)
            : base(mExpression, rowIndexExpression, colIndexExpression)
        {
        }


        public override object Evaluate()
        {
            if (ThirdExpression == null)
                return FirstExpression.EvaluateAsCMatrix()[SecondExpression.EvaluateAsInt32()];
            else
                return FirstExpression.EvaluateAsCMatrix()[SecondExpression.EvaluateAsInt32(), ThirdExpression.EvaluateAsInt32()];
        }
    }




    [DisplayName("Index of last element")]
    [Category(Categories.LinearAlgebra)]
    [Description("Returns the index of last element of a complex vector.")]
    [FunctionSignature("last", "complex vector v")]
    [ExampleUsage("last({1; 2; 3; 5; 15})", "4")]
    public class LastIndexFuncExpression : UnaryExpression<Object>
    {
        public LastIndexFuncExpression(Expression<Object> subExpression)
            : base(subExpression)
        {
        }

        public override object Evaluate()
        {
            return (Complex)(SubExpression.EvaluateAsComplexVector().Count - 1);
        }
    }

    [DisplayName("Length")]
    [Category(Categories.LinearAlgebra)]
    [Description("Returns the number of elements of the complex matrix.")]
    [FunctionSignature("length", "complex matrix m")]
    [ExampleUsage("length({1, 3, 5; 8, 13, -21})", "6")]
    [ExampleUsage("length({1; 2; 3})", "3")]
    public class LengthFuncExpression : UnaryExpression<Object>
    {
        public LengthFuncExpression(Expression<Object> subExpression)
            : base(subExpression)
        {
        }

        public override object Evaluate()
        {
            return (Complex)SubExpression.EvaluateAsCMatrix().Length;
        }
    }

    [DisplayName("Row count")]
    [Category(Categories.LinearAlgebra)]
    [Description("Returns the number of rows of the complex matrix.")]
    [FunctionSignature("rows", "complex matrix m")]
    [ExampleUsage("rows({1, 3, 5; 8, 13, -21})", "2")]
    [ExampleUsage("rows({1; 2; 3})", "3")]
    public class RowCountFuncExpression : UnaryExpression<Object>
    {
        public RowCountFuncExpression(Expression<Object> subExpression)
            : base(subExpression)
        {
        }

        public override object Evaluate()
        {
            return (Complex)SubExpression.EvaluateAsCMatrix().RowCount;
        }
    }

    [DisplayName("Column count")]
    [Category(Categories.LinearAlgebra)]
    [Description("Returns the number of columns of the complex matrix.")]
    [FunctionSignature("cols", "complex matrix m")]
    [ExampleUsage("cols({1, 3, 5; 8, 13, -21})", "3")]
    [ExampleUsage("cols({1; 2; 3})", "1")]
    public class ColumnCountFuncExpression : UnaryExpression<Object>
    {
        public ColumnCountFuncExpression(Expression<Object> subExpression)
            : base(subExpression)
        {
        }

        public override object Evaluate()
        {
            return (Complex)SubExpression.EvaluateAsCMatrix().ColumnCount;
        }
    }

    [DisplayName("Maximum")]
    [Category(Categories.LinearAlgebra)]
    [Description("Returns the maximum element of the complex matrix.")]
    [FunctionSignature("max", "complex matrix m")]
    [ExampleUsage("max({0, 2, -248; 16, 14, 5})", "16")]
    [ExampleUsage("max({3i, 5, 22 + 2.5i; 14, -2, 0.8})", "22 + 3i")]
    public class MaximumFuncExpression : UnaryExpression<Object>
    {
        public MaximumFuncExpression(Expression<Object> subExpression)
            : base(subExpression)
        {
        }

        public override object Evaluate()
        {
            return SubExpression.EvaluateAsCMatrix().Max;
        }
    }

    [DisplayName("Minimum")]
    [Category(Categories.LinearAlgebra)]
    [Description("Returns the minimum element of the complex matrix.")]
    [FunctionSignature("min", "complex matrix m")]
    [ExampleUsage("min({0, 2, -248; 16, 14, 5})", "-248")]
    [ExampleUsage("min({3i, 5, 22 + 2.5i; 14, -2, 0.8})", "-2")]
    [ExampleUsage("min({3i, 5, 22 - 2.5i; 14, -2, 0.8})", "-2 - 2.5i")]
    public class MinimumFuncExpression : UnaryExpression<Object>
    {
        public MinimumFuncExpression(Expression<Object> subExpression)
            : base(subExpression)
        {
        }

        public override object Evaluate()
        {
            return SubExpression.EvaluateAsCMatrix().Min;
        }
    }

    [DisplayName("Trace")]
    [Category(Categories.LinearAlgebra)]
    [Description("Returns trace (sum of diagonal elements) of the complex square matrix.")]
    [FunctionSignature("tr", "complex matrix m")]
    [ExampleUsage("tr({1, 2; 3, 4})", "5")]
    public class TraceFuncExpression : UnaryExpression<Object>
    {
        public TraceFuncExpression(Expression<Object> subExpression)
            : base(subExpression)
        {
        }

        public override object Evaluate()
        {
            return CMatrix.Trace(SubExpression.EvaluateAsCMatrix());
        }
    }
}
