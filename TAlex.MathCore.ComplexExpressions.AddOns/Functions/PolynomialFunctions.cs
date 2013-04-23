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
    [DisplayName("Evaluation")]
    [Category(Categories.Polynomials)]
    [Description("Calculates the value of the complex polynomial evaluated at a specified value.")]
    [FunctionSignature("polyval", "complex vector poly", "complex x")]
    [FunctionSignature("polyval", "complex vector poly", "complex matrix m")]
    [ExampleUsage("polyval({3; 5; 8; 16}, 5)", "2228")]
    [ExampleUsage("polyval({3.6; 5i; 8.2 - 1.3i; 16}, 5 + 13i)", "-39633.2 - 18273.8i")]
    [ExampleUsage("polyval({3; 0;  - 3; 16}, {1, 2; 3, 6})", "{766, 1526; 2289, 4581}")]
    public class EvaluationFuncExpression : BinaryExpression<Object>
    {
        public EvaluationFuncExpression(Expression<Object> polyExpression, Expression<Object> xExpression)
            : base(polyExpression, xExpression)
        {
        }

        public override object Evaluate()
        {
            CPolynomial poly = LeftExpression.EvaluateAsCPolynomial();
            Object x = RightExpression.Evaluate();

            if (x is Complex)
                return poly.Evaluate((Complex)x);
            else if (x is CMatrix)
                return poly.Evaluate((CMatrix)x);
            else
                throw ExceptionHelper.ThrowWrongArgumentType(x);
        }
    }

    [DisplayName("Finding Roots")]
    [Category(Categories.Polynomials)]
    [Description("Calculates the vector of approximate values of roots of a complex polynomial.")]
    [FunctionSignature("polyroots", "complex vector poly")]
    [ExampleUsage("polyroots({1; 2})", "{-0.5}")]
    [ExampleUsage("polyroots({1; -2; 5})", "{0.2 + 0.4i; 0.2 - 0.4i}")]
    public class FindingRootsFuncExpression : UnaryExpression<Object>
    {
        public FindingRootsFuncExpression(Expression<Object> polyExpression)
            : base(polyExpression)
        {
        }

        public override object Evaluate()
        {
            CPolynomial poly = SubExpression.EvaluateAsCPolynomial();
            return new CMatrix(poly.Roots());
        }
    }

    [DisplayName("Derivative")]
    [Category(Categories.Polynomials)]
    [Description("Calculates the n-th derivative of the complex polynomial.")]
    [FunctionSignature("polydr", "complex vector poly")]
    [FunctionSignature("polydr", "complex vector poly", "integer order")]
    [ExampleUsage("polydr({3; 5; 8i; -6 + 12i})", "{5; 16i; -18 + 36i}")]
    [ExampleUsage("polydr({3; 5; 8i; -6 + 12i}, 2)", "{16i; -36 + 72i}")]
    public class DerivativeFuncExpression : BinaryExpression<Object>
    {
        public DerivativeFuncExpression(Expression<Object> polyExpression)
            : this(polyExpression, new ScalarExpression<Object>(Complex.One))
        {
        }

        public DerivativeFuncExpression(Expression<Object> polyExpression, Expression<Object> orderExpression)
            : base(polyExpression, orderExpression)
        {
        }

        public override object Evaluate()
        {
            CPolynomial poly = LeftExpression.EvaluateAsCPolynomial();
            int order = RightExpression.EvaluateAsInt32();

            return new CMatrix(poly.NthDerivative(order).ToArray());
        }
    }

    [DisplayName("Antiderivative")]
    [Category(Categories.Polynomials)]
    [Description("Calculates the antiderivative of the complex polynomial.")]
    [FunctionSignature("polyadr", "complex vector poly")]
    [ExampleUsage("polyadr({3; 5; 3i; -1 + 12i})", "{0; 3; 2.5; 1i; -0.25 + 3i}")]
    public class AntiderivativeFuncExpression : UnaryExpression<Object>
    {
        public AntiderivativeFuncExpression(Expression<Object> polyExpression)
            : base(polyExpression)
        {
        }

        public override object Evaluate()
        {
            CPolynomial poly = SubExpression.EvaluateAsCPolynomial();
            return new CMatrix(poly.Antiderivative().ToArray());
        }
    }
}
