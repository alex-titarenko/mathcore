using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using TAlex.MathCore.ExpressionEvaluation.Trees;
using TAlex.MathCore.ExpressionEvaluation.Trees.Metadata;
using TAlex.MathCore.LinearAlgebra;


namespace TAlex.MathCore.ExpressionEvaluation.ComplexExpressions.Functions
{
    [DisplayName("Matrix")]
    [Category(Categories.LinearAlgebra)]
    [Section(Sections.Creation)]
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
    [Section(Sections.Creation)]
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
    [Section(Sections.Creation)]
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
    [Section(Sections.Creation)]
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
    [Section(Sections.Creation)]
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
    [Section(Sections.Creation)]
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
    [Section(Sections.Creation)]
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
    [Section(Sections.Creation)]
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
    [Section(Sections.Creation)]
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
    [Section(Sections.Behaviors)]
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
    [Section(Sections.Behaviors)]
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
    [Section(Sections.Behaviors)]
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
    [Section(Sections.Behaviors)]
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
    [Section(Sections.Behaviors)]
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
    [Section(Sections.Behaviors)]
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
    [Section(Sections.Behaviors)]
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

    [DisplayName("Rank")]
    [Category(Categories.LinearAlgebra)]
    [Section(Sections.Behaviors)]
    [Description("Calculates the number of linearly independent rows.")]
    [FunctionSignature("rank", "complex matrix m")]
    [ExampleUsage("rank({1, 2, 1; -2, -3, 1; 3, 5, 0})", "2")]
    public class RankFuncExpression : UnaryExpression<Object>
    {
        public RankFuncExpression(Expression<Object> subExpression)
            : base(subExpression)
        {
        }

        public override object Evaluate()
        {
            return (Complex)CMatrix.Rank(SubExpression.EvaluateAsCMatrix());
        }
    }

    [DisplayName("Determinant")]
    [Category(Categories.LinearAlgebra)]
    [Section(Sections.Behaviors)]
    [Description("Calculates determinant of the complex square matrix.")]
    [FunctionSignature("det", "complex matrix m")]
    [ExampleUsage("det({-2, 2, 3; -1, 1, 3; 2, 0, -1})", "6")]
    public class DeterminantFuncExpression : UnaryExpression<Object>
    {
        public DeterminantFuncExpression(Expression<Object> subExpression)
            : base(subExpression)
        {
        }

        public override object Evaluate()
        {
            return CMatrix.Determ(SubExpression.EvaluateAsCMatrix());
        }
    }



    [DisplayName("Linear system solve")]
    [Category(Categories.LinearAlgebra)]
    [Section(Sections.SolveAndInverse)]
    [Description("Calculates the solution of the linear system.")]
    [FunctionSignature("lsolve", "complex matrix a", "complex vector b")]
    [ExampleUsage("lsolve({3, 2, -1; 2, -2, 4; -1, 1/2, -1}, {1; -2; 0})", "{1; -2; -2}")]
    public class LinearSolveFuncExpression : BinaryExpression<Object>
    {
        public LinearSolveFuncExpression(Expression<Object> aExpression, Expression<Object> bExpression)
            : base(aExpression, bExpression)
        {
        }

        public override object Evaluate()
        {
            return CMatrix.Solve(LeftExpression.EvaluateAsCMatrix(), RightExpression.EvaluateAsCMatrix());
        }
    }

    [DisplayName("Pseudo inverse")]
    [Category(Categories.LinearAlgebra)]
    [Section(Sections.SolveAndInverse)]
    [Description("Calculates the Moore-Penrose inverse (pseudoinverse) of the complex matrix.")]
    [FunctionSignature("pinv", "complex matrix m")]
    [ExampleUsage("pinv({-8, 0, 1; 7, 9, 7})", "{-0.117933723196881, 0.00682261208576995; 0.0477582846003899, 0.0633528265107213; 0.0565302144249513, 0.0545808966861599}")]
    public class PseudoInverseExpression : UnaryExpression<Object>
    {
        public PseudoInverseExpression(Expression<Object> subExpression)
            : base(subExpression)
        {
        }

        public override object Evaluate()
        {
            return CMatrix.PseudoInverse(SubExpression.EvaluateAsCMatrix());
        }
    }



    [DisplayName("Dot product")]
    [Category(Categories.LinearAlgebra)]
    [Section(Sections.Products)]
    [Description("Calculates the vector dot product of two column vectors.")]
    [FunctionSignature("dot", "complex vector v1", "complex vector v2")]
    [ExampleUsage("dot({2; 5; -8}, {16; 3i; 2})", "16 + 15i")]
    public class DotProductFuncExpression : BinaryExpression<Object>
    {
        public DotProductFuncExpression(Expression<Object> v1Expression, Expression<Object> v2Expression)
            : base(v1Expression, v2Expression)
        {
        }

        public override object Evaluate()
        {
            return CMatrix.DotProduct(LeftExpression.EvaluateAsCMatrix(), RightExpression.EvaluateAsCMatrix());
        }
    }

    [DisplayName("Cross product")]
    [Category(Categories.LinearAlgebra)]
    [Section(Sections.Products)]
    [Description("Calculates the vector cross product of two column vectors.")]
    [FunctionSignature("cross", "complex vector v1", "complex vector v2")]
    [ExampleUsage("cross({2; 5; -8}, {16; 3i; 2})", "{10 + 24i; -132; -80 + 6i}")]
    public class CrossProductFuncExpression : BinaryExpression<Object>
    {
        public CrossProductFuncExpression(Expression<Object> v1Expression, Expression<Object> v2Expression)
            : base(v1Expression, v2Expression)
        {
        }

        public override object Evaluate()
        {
            return CMatrix.CrossProduct(LeftExpression.EvaluateAsCMatrix(), RightExpression.EvaluateAsCMatrix());
        }
    }



    [DisplayName("One norm")]
    [Category(Categories.LinearAlgebra)]
    [Section(Sections.Norms)]
    [Description("Calculates the maximum absolute column sum norm (also known as taxi norm).")]
    [FunctionSignature("onenorm", "complex matrix m")]
    [ExampleUsage("onenorm({-2, 2, 3; -1, 1, 3; 2, 0, -1})", "7")]
    public class OneNormFuncExpression : UnaryExpression<Object>
    {
        public OneNormFuncExpression(Expression<Object> subExpression)
            : base(subExpression)
        {
        }

        public override object Evaluate()
        {
            return (Complex)CMatrix.OneNorm(SubExpression.EvaluateAsCMatrix());
        }
    }

    [DisplayName("Infinity norm")]
    [Category(Categories.LinearAlgebra)]
    [Section(Sections.Norms)]
    [Description("Calculates the maximum absolute row sum norm (also known as max norm).")]
    [FunctionSignature("infnorm", "complex matrix m")]
    [ExampleUsage("infnorm({-2, 5, 3; -1, 1, 3; 2, 0, -1})", "10")]
    public class InfinityNormFuncExpression : UnaryExpression<Object>
    {
        public InfinityNormFuncExpression(Expression<Object> subExpression)
            : base(subExpression)
        {
        }

        public override object Evaluate()
        {
            return (Complex)CMatrix.InfinityNorm(SubExpression.EvaluateAsCMatrix());
        }
    }

    [DisplayName("Frobenius norm")]
    [Category(Categories.LinearAlgebra)]
    [Section(Sections.Norms)]
    [Description("Calculates the Frobenius norm of a complex matrix.")]
    [FunctionSignature("fronorm", "complex matrix m")]
    [ExampleUsage("fronorm({2, 4; 4, 0})", "6")]
    public class FrobeniusNormFuncExpression : UnaryExpression<Object>
    {
        public FrobeniusNormFuncExpression(Expression<Object> subExpression)
            : base(subExpression)
        {
        }

        public override object Evaluate()
        {
            return (Complex)CMatrix.FrobeniusNorm(SubExpression.EvaluateAsCMatrix());
        }
    }

    [DisplayName("P-norm")]
    [Category(Categories.LinearAlgebra)]
    [Section(Sections.Norms)]
    [Description("Calculates the p-norm of a complex matrix.")]
    [FunctionSignature("pnorm", "complex matrix m", "real p")]
    [ExampleUsage("pnorm({2, 4; 4, 0}, 2)", "6")]
    [ExampleUsage("pnorm({4, 0, 2; 2, 3, 1; 1, 2, 2}, 3)", "5")]
    public class PNormFuncExpression : BinaryExpression<Object>
    {
        public PNormFuncExpression(Expression<Object> mExpression, Expression<Object> pExpression)
            : base(mExpression, pExpression)
        {
        }

        public override object Evaluate()
        {
            return (Complex)CMatrix.PNorm(LeftExpression.EvaluateAsCMatrix(), RightExpression.EvaluateAsReal());
        }
    }



    [DisplayName("Cofactor")]
    [Category(Categories.LinearAlgebra)]
    [Section(Sections.Minor)]
    [Description("Calculates the signed minor of the complex square matrix.")]
    [FunctionSignature("cofactor", "complex matrix m", "integer rowIndex", "integer colIndex")]
    [ExampleUsage("cofactor({1, 4, 7; 3, 0, 5; -1, 9, 11}, 1, 2)", "-13")]
    public class CofactorFuncExpression : TernaryExpression<Object>
    {
        public CofactorFuncExpression(Expression<Object> mExpression, Expression<Object> rowExpression, Expression<Object> colExpression)
            : base(mExpression, rowExpression, colExpression)
        {
        }

        public override object Evaluate()
        {
            return CMatrix.Cofactor(FirstExpression.EvaluateAsCMatrix(), SecondExpression.EvaluateAsInt32(), ThirdExpression.EvaluateAsInt32());
        }
    }

    [DisplayName("Minor")]
    [Category(Categories.LinearAlgebra)]
    [Section(Sections.Minor)]
    [Description("Calculates the determinant of some smaller square matrix, cut down from the matrix by removing one or more of its rows or columns.")]
    [FunctionSignature("minor", "complex matrix m", "integer rowIndex", "integer colIndex")]
    [ExampleUsage("minor({1, 4, 7; 3, 0, 5; -1, 9, 11}, 1, 2)", "13")]
    public class MinorFuncExpression : TernaryExpression<Object>
    {
        public MinorFuncExpression(Expression<Object> mExpression, Expression<Object> rowExpression, Expression<Object> colExpression)
            : base(mExpression, rowExpression, colExpression)
        {
        }

        public override object Evaluate()
        {
            return CMatrix.Minor(FirstExpression.EvaluateAsCMatrix(), SecondExpression.EvaluateAsInt32(), ThirdExpression.EvaluateAsInt32());
        }
    }



    [DisplayName("Eigenvalues")]
    [Category(Categories.LinearAlgebra)]
    [Section(Sections.EigenSystem)]
    [Description("Calculates the vector whose elements are the eigenvalues of the complex square matrix.")]
    [FunctionSignature("eigvals", "complex matrix m")]
    [ExampleUsage("eigvals({2, 0, 0; 0, 3, 4; 0, 4, 9})", "{11; 1; 2}")]
    public class EigenvaluesFuncExpression : UnaryExpression<Object>
    {
        public EigenvaluesFuncExpression(Expression<Object> subExpression)
            : base(subExpression)
        {
        }

        public override object Evaluate()
        {
            return CMatrix.Eigenvalues(SubExpression.EvaluateAsCMatrix());
        }
    }

    [DisplayName("Eigenvectors")]
    [Category(Categories.LinearAlgebra)]
    [Section(Sections.EigenSystem)]
    [Description("Calculates the matrix containing all normalized eigenvectors of the matrix.")]
    [FunctionSignature("eigvecs", "complex matrix m")]
    [ExampleUsage("eigvecs({0, 1, 0; 0, 2, 0; 0, 0, 3})", "{1, 0.447213595499958, 0; 0, 0.894427190999916, 0; 0, 0, 1}")]
    public class EigenvectorsFuncExpression : UnaryExpression<Object>
    {
        public EigenvectorsFuncExpression(Expression<Object> subExpression)
            : base(subExpression)
        {
        }

        public override object Evaluate()
        {
            return CMatrix.Eigenvectors(SubExpression.EvaluateAsCMatrix());
        }
    }

    [DisplayName("Left eigenvectors")]
    [Category(Categories.LinearAlgebra)]
    [Section(Sections.EigenSystem)]
    [Description("Calculates the matrix containing all normalized left eigenvectors of the matrix.")]
    [FunctionSignature("leigvecs", "complex matrix m")]
    [ExampleUsage("leigvecs({0, 1, 0; 0, 2, 0; 0, 0, 3})", "{0.894427190999916, 0, 0; -0.447213595499958, 1, 0; 0, 0, 1}")]
    public class LeftEigenvectorsFuncExpression : UnaryExpression<Object>
    {
        public LeftEigenvectorsFuncExpression(Expression<Object> subExpression)
            : base(subExpression)
        {
        }

        public override object Evaluate()
        {
            return CMatrix.LeftEigenvectors(SubExpression.EvaluateAsCMatrix());
        }
    }

    [DisplayName("Singularvalues")]
    [Category(Categories.LinearAlgebra)]
    [Section(Sections.EigenSystem)]
    [Description("Calculates the vector whose elements are the singular values of the complex matrix.")]
    [FunctionSignature("sngvals", "complex matrix m")]
    [ExampleUsage("sngvals({0, 1, 0; 0, 2, 0; 0, 0, 3})", "{3; 2.23606797749979; 0}")]
    public class SingularvaluesFuncExpression : UnaryExpression<Object>
    {
        public SingularvaluesFuncExpression(Expression<Object> subExpression)
            : base(subExpression)
        {
        }

        public override object Evaluate()
        {
            return CMatrix.Singularvalues(SubExpression.EvaluateAsCMatrix());
        }
    }



    [DisplayName("Characteristic polynomial")]
    [Category(Categories.LinearAlgebra)]
    [Section(Sections.CharacteristicPolynomial)]
    [Description("Calculates the characteristic polynomial of the complex square matrix.")]
    [FunctionSignature("char", "complex matrix m")]
    [ExampleUsage("char({0, 1, 0; 0, 2, 0; 0, 0, 3})", "{0; 6; -5; 1}")]
    public class CharacteristicPolynomialFuncExpression : UnaryExpression<Object>
    {
        public CharacteristicPolynomialFuncExpression(Expression<Object> subExpression)
            : base(subExpression)
        {
        }

        public override object Evaluate()
        {
            return new CMatrix(CMatrix.CharacteristicPolynomial(SubExpression.EvaluateAsCMatrix()).ToArray());
        }
    }



    [DisplayName("Cholesky decomposition")]
    [Category(Categories.LinearAlgebra)]
    [Section(Sections.Decompositions)]
    [Description("Calculates Cholesky decomposition for hermitian positive definite matrix.")]
    [FunctionSignature("chol", "complex matrix m")]
    [ExampleUsage("chol({4, 12, -16; 12, 37, -43; -16, -43, 98})", "{2, 0, 0; 6, 1, 0; -8, 5, 3}")]
    public class CholeskyDecompositionFuncExpression : UnaryExpression<Object>
    {
        public CholeskyDecompositionFuncExpression(Expression<Object> subExpression)
            : base(subExpression)
        {
        }

        public override object Evaluate()
        {
            return CMatrix.CholeskyDecomposition(SubExpression.EvaluateAsCMatrix());
        }
    }

    [DisplayName("LU decomposition")]
    [Category(Categories.LinearAlgebra)]
    [Section(Sections.Decompositions)]
    [Description("Calculates LU-decomposition with column pivoting for the complex square matrix. The resulting matrix includes three matrix: permutation matrix P, the lower and upper triangular matrices L, U.")]
    [FunctionSignature("lu", "complex matrix m")]
    [ExampleUsage("lu({2, 0, 0; 0, 3, 4; 0, 4, 9})", "{1, 0, 0, 1, 0, 0, 2, 0, 0; 0, 0, 1, 0, 1, 0, 0, 4, 9; 0, 1, 0, 0, 0.75, 1, 0, 0, -2.75}")]
    public class LUPDecompositionFuncExpression : UnaryExpression<Object>
    {
        public LUPDecompositionFuncExpression(Expression<Object> subExpression)
            : base(subExpression)
        {
        }

        public override object Evaluate()
        {
            return CMatrix.AugmentConcat(CMatrix.LUPDecomposition(SubExpression.EvaluateAsCMatrix()));
        }
    }

    [DisplayName("QR decomposition")]
    [Category(Categories.LinearAlgebra)]
    [Section(Sections.Decompositions)]
    [Description("Calculates QR-decomposition for the complex matrix. The resulting matrix includes two matrices: The unitary matrix Q and upper triangular matrix R.")]
    [FunctionSignature("qr", "complex matrix m")]
    [ExampleUsage("qr({3, -6; 4, -8; 0, 1})", "{-0.6, 0, -0.8, -5, 10; -0.8, 0, 0.6, 0, 1; 0, 1, 0, 0, 0}")]
    public class QRDecompositionFuncExpression : UnaryExpression<Object>
    {
        public QRDecompositionFuncExpression(Expression<Object> subExpression)
            : base(subExpression)
        {
        }

        public override object Evaluate()
        {
            return CMatrix.AugmentConcat(CMatrix.QRDecomposition(SubExpression.EvaluateAsCMatrix()));
        }
    }

    [DisplayName("SVD")]
    [Category(Categories.LinearAlgebra)]
    [Section(Sections.Decompositions)]
    [Description("Calculates the singular value decomposition (the matrix formed by the horizontal concatenation of matrices U, S, VH).")]
    [FunctionSignature("svd", "complex matrix m")]
    [ExampleUsage("svd({3, 1, 1; -1, 3, 1})", "{-0.70710678118655, -0.707106781186545, 3.46410161513775, 0, 0, -0.408248290463867, -0.816496580927724, -0.408248290463863; -0.707106781186545, 0.70710678118655, 0, 3.16227766016838, 0, -0.894427190999914, 0.447213595499961, 0; 0, 0, 0, 0, 0, -0.182574185835055, -0.365148371670111, 0.912870929175277}")]
    public class SVDecompositionFuncExpression : UnaryExpression<Object>
    {
        public SVDecompositionFuncExpression(Expression<Object> subExpression)
            : base(subExpression)
        {
        }

        public override object Evaluate()
        {
            return new CSVD(SubExpression.EvaluateAsCMatrix()).SVD;
        }
    }
}
