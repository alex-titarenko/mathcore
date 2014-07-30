using System;
using System.Collections.Generic;
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

    [DisplayName("Interpolating polynomial")]
    [Category(Categories.Polynomials)]
    [Description("Returns the interpolating polynomial through a set of nodes using the Lagrange method.")]
    [FunctionSignature("polyinterp", "real vector xValues", "real vector yValues")]
    [ExampleUsage("polyinterp({1; 2; 5; 8}, {-2; 5; 5; 1})", "{-14.6825396825397; 15.9603174603175; -3.49603174603175; 0.218253968253968}")]
    public class InterpolatingPolynomialFuncExpression : BinaryExpression<Object>
    {
        public InterpolatingPolynomialFuncExpression(Expression<Object> xValuesExpression, Expression<Object> yValuesExpression)
            : base(xValuesExpression, yValuesExpression)
        {
        }

        public override object Evaluate()
        {
            IList<double> xValues = LeftExpression.EvaluateAsRealVector();
            IList<double> yValues = RightExpression.EvaluateAsRealVector();

            return new CMatrix(CPolynomial.InterpolatingPolynomial(xValues, yValues).ToArray());
        }
    }

    [DisplayName("From roots")]
    [Category(Categories.Polynomials)]
    [Description("Returns a complex polynomial that has the specified roots.")]
    [FunctionSignature("fromroots", "complex vector roots")]
    [ExampleUsage("fromroots({2; 5; 3 + 1i; -18})", "{-540 - 180i; 528 + 116i; -149 - 11i; 8 - 1i; 1}")]
    public class FromRootsFuncExpression : UnaryExpression<Object>
    {
        public FromRootsFuncExpression(Expression<Object> rootsExpression)
            : base(rootsExpression)
        {
        }

        public override object Evaluate()
        {
            IList<Complex> roots = SubExpression.EvaluateAsComplexVector();
            return new CMatrix(CPolynomial.FromRoots(roots).ToArray());
        }
    }



    [DisplayName("Addition")]
    [Category(Categories.Polynomials)]
    [Section(Sections.PolynomialOperations)]
    [Description("Adds two complex polynomials.")]
    [FunctionSignature("polyadd", "complex vector poly1", "complex vector poly2")]
    [ExampleUsage("polyadd({3; 5; -6i}, {2; -6; 16})", "{5; -1; 16 - 6i}")]
    [ExampleUsage("polyadd({-0.5; 16}, {0; -2; 2.8 - 3i})", "{-0.5; 14; 2.8 - 3i}")]
    public class AdditionFuncExpression : BinaryExpression<Object>
    {
        public AdditionFuncExpression(Expression<Object> poly1Expression, Expression<Object> poly2Expression)
            : base(poly1Expression, poly2Expression)
        {
        }

        public override object Evaluate()
        {
            CPolynomial poly1 = LeftExpression.EvaluateAsCPolynomial();
            CPolynomial poly2 = RightExpression.EvaluateAsCPolynomial();

            return new CMatrix(CPolynomial.Add(poly1, poly2).ToArray());
        }
    }

    [DisplayName("Subtraction")]
    [Category(Categories.Polynomials)]
    [Section(Sections.PolynomialOperations)]
    [Description("Subtracts one complex polynomial from another.")]
    [FunctionSignature("polysub", "complex vector poly1", "complex vector poly2")]
    [ExampleUsage("polysub({3; 5; -6i}, {2; -6; 16})", "{1; 11; -16 - 6i}")]
    [ExampleUsage("polysub({-0.5; 16}, {0; -2; 2.8 - 3i})", "{-0.5; 18; -2.8 + 3i}")]
    public class SubtractionFuncExpression : BinaryExpression<Object>
    {
        public SubtractionFuncExpression(Expression<Object> poly1Expression, Expression<Object> poly2Expression)
            : base(poly1Expression, poly2Expression)
        {
        }

        public override object Evaluate()
        {
            CPolynomial poly1 = LeftExpression.EvaluateAsCPolynomial();
            CPolynomial poly2 = RightExpression.EvaluateAsCPolynomial();

            return new CMatrix(CPolynomial.Subtract(poly1, poly2).ToArray());
        }
    }

    [DisplayName("Multiplication")]
    [Category(Categories.Polynomials)]
    [Section(Sections.PolynomialOperations)]
    [Description("Multiplies two complex polynomials.")]
    [FunctionSignature("polymul", "complex vector poly1", "complex vector poly2")]
    [ExampleUsage("polymul({3; 5; -6i}, {2; -6; 16})", "{6; -8; 18 - 12i; 80 + 36i; -96i}")]
    [ExampleUsage("polymul({-0.5; 16}, {0; -2; 2.8 - 3i})", "{0; 1; -33.4 + 1.5i; 44.8 - 48i}")]
    public class MultiplicationFuncExpression : BinaryExpression<Object>
    {
        public MultiplicationFuncExpression(Expression<Object> poly1Expression, Expression<Object> poly2Expression)
            : base(poly1Expression, poly2Expression)
        {
        }

        public override object Evaluate()
        {
            CPolynomial poly1 = LeftExpression.EvaluateAsCPolynomial();
            CPolynomial poly2 = RightExpression.EvaluateAsCPolynomial();

            return new CMatrix(CPolynomial.Multiply(poly1, poly2).ToArray());
        }
    }

    [DisplayName("Division")]
    [Category(Categories.Polynomials)]
    [Section(Sections.PolynomialOperations)]
    [Description("Divides two complex polynomials.")]
    [FunctionSignature("polydiv", "complex vector poly1", "complex vector poly2")]
    [ExampleUsage("polydiv({3; 5; -6i}, {3; -5})", "{-1 + 0.72i; 1.2i}")]
    [ExampleUsage("polydiv({0; -2; 2.8 - 3i}, {-0.5; 16})", "{-0.11953125 - 0.005859375i; 0.175 - 0.1875i}")]
    public class DivisionFuncExpression : BinaryExpression<Object>
    {
        public DivisionFuncExpression(Expression<Object> poly1Expression, Expression<Object> poly2Expression)
            : base(poly1Expression, poly2Expression)
        {
        }

        public override object Evaluate()
        {
            CPolynomial poly1 = LeftExpression.EvaluateAsCPolynomial();
            CPolynomial poly2 = RightExpression.EvaluateAsCPolynomial();

            return new CMatrix(CPolynomial.Divide(poly1, poly2).ToArray());
        }
    }

    [DisplayName("Remainder")]
    [Category(Categories.Polynomials)]
    [Section(Sections.PolynomialOperations)]
    [Description("Calculates the remainder of dividing the first polynomial in the second.")]
    [FunctionSignature("polyrem", "complex vector poly1", "complex vector poly2")]
    [ExampleUsage("polyrem({3; 5; -6i}, {3; -5})", "{6 - 2.16i}")]
    [ExampleUsage("polyrem({0; -2; 2.8 - 3i}, {-0.5; 16})", "{-0.059765625 - 0.0029296875i}")]
    public class RemainderFuncExpression : BinaryExpression<Object>
    {
        public RemainderFuncExpression(Expression<Object> poly1Expression, Expression<Object> poly2Expression)
            : base(poly1Expression, poly2Expression)
        {
        }

        public override object Evaluate()
        {
            CPolynomial poly1 = LeftExpression.EvaluateAsCPolynomial();
            CPolynomial poly2 = RightExpression.EvaluateAsCPolynomial();

            return new CMatrix(CPolynomial.Modulus(poly1, poly2).ToArray());
        }
    }
}
