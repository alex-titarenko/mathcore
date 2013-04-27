using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAlex.MathCore.ExpressionEvaluation.Trees;
using TAlex.MathCore.ExpressionEvaluation.Trees.Metadata;


namespace TAlex.MathCore.ExpressionEvaluation.ComplexExpressions.Functions
{
    [DisplayName("Sine")]
    [Category(Categories.Trigonometric)]
    [Section("Trigonometry")]
    [Description("Calculates the hyperbolic sine of a complex number.")]
    [FunctionSignature("sinh", "complex value")]
    [ExampleUsage("sinh(0)", "0")]
    public class HyperbolicSineFuncExpression : UnaryExpression<Object>
    {
        public HyperbolicSineFuncExpression(Expression<Object> subExpression)
            : base(subExpression)
        {
        }

        public override object Evaluate()
        {
            return Complex.Sinh(SubExpression.EvaluateAsComplex());
        }
    }

    [DisplayName("Cosine")]
    [Category(Categories.Trigonometric)]
    [Section("Trigonometry")]
    [Description("Calculates the hyperbolic cosine of a complex number.")]
    [FunctionSignature("cosh", "complex value")]
    [ExampleUsage("cosh(0)", "1")]
    public class HyperbolicCosineFuncExpression : UnaryExpression<Object>
    {
        public HyperbolicCosineFuncExpression(Expression<Object> subExpression)
            : base(subExpression)
        {
        }

        public override object Evaluate()
        {
            return Complex.Cosh(SubExpression.EvaluateAsComplex());
        }
    }

    [DisplayName("Tangent")]
    [Category(Categories.Trigonometric)]
    [Section("Trigonometry")]
    [Description("Calculates the hyperbolic tangent of a complex number.")]
    [FunctionSignature("tanh", "complex value")]
    [ExampleUsage("tanh(0)", "0")]
    public class HyperbolicTangentFuncExpression : UnaryExpression<Object>
    {
        public HyperbolicTangentFuncExpression(Expression<Object> subExpression)
            : base(subExpression)
        {
        }

        public override object Evaluate()
        {
            return Complex.Tanh(SubExpression.EvaluateAsComplex());
        }
    }

    [DisplayName("Cotangent")]
    [Category(Categories.Trigonometric)]
    [Section("Trigonometry")]
    [Description("Calculates the hyperbolic cotangent of a complex number.")]
    [FunctionSignature("coth", "complex value")]
    [ExampleUsage("coth(pi / 4)", "1.52486861882206")]
    public class HyperbolicCotangentFuncExpression : UnaryExpression<Object>
    {
        public HyperbolicCotangentFuncExpression(Expression<Object> subExpression)
            : base(subExpression)
        {
        }

        public override object Evaluate()
        {
            return Complex.Coth(SubExpression.EvaluateAsComplex());
        }
    }

    [DisplayName("Secant")]
    [Category(Categories.Trigonometric)]
    [Section("Trigonometry")]
    [Description("Calculates the hyperbolic secant of a complex number.")]
    [FunctionSignature("sech", "complex value")]
    [ExampleUsage("sech(pi / 3)", "0.624887966296087")]
    public class HyperbolicSecantFuncExpression : UnaryExpression<Object>
    {
        public HyperbolicSecantFuncExpression(Expression<Object> subExpression)
            : base(subExpression)
        {
        }

        public override object Evaluate()
        {
            return Complex.Sech(SubExpression.EvaluateAsComplex());
        }
    }

    [DisplayName("Cosecant")]
    [Category(Categories.Trigonometric)]
    [Section("Trigonometry")]
    [Description("Calculates the hyperbolic cosecant of a complex number.")]
    [FunctionSignature("csch", "complex value")]
    [ExampleUsage("csch(pi / 6)", "1.82530557468795")]
    public class HyperbolicCosecantFuncExpression : UnaryExpression<Object>
    {
        public HyperbolicCosecantFuncExpression(Expression<Object> subExpression)
            : base(subExpression)
        {
        }

        public override object Evaluate()
        {
            return Complex.Csch(SubExpression.EvaluateAsComplex());
        }
    }



    [DisplayName("Inverse sine")]
    [Category(Categories.Trigonometric)]
    [Section("Trigonometry")]
    [Description("Calculates the inverse hyperbolic sine of a complex number.")]
    [FunctionSignature("asinh", "complex value")]
    [ExampleUsage("asinh(0)", "0")]
    public class InverseHyperbolicSineFuncExpression : UnaryExpression<Object>
    {
        public InverseHyperbolicSineFuncExpression(Expression<Object> subExpression)
            : base(subExpression)
        {
        }

        public override object Evaluate()
        {
            return Complex.Asinh(SubExpression.EvaluateAsComplex());
        }
    }

    [DisplayName("Inverse cosine")]
    [Category(Categories.Trigonometric)]
    [Section("Trigonometry")]
    [Description("Calculates the inverse hyperbolic cosine of a complex number.")]
    [FunctionSignature("acosh", "complex value")]
    [ExampleUsage("acosh(1)", "0")]
    public class InverseHyperbolicCosineFuncExpression : UnaryExpression<Object>
    {
        public InverseHyperbolicCosineFuncExpression(Expression<Object> subExpression)
            : base(subExpression)
        {
        }

        public override object Evaluate()
        {
            return Complex.Acosh(SubExpression.EvaluateAsComplex());
        }
    }

    [DisplayName("Inverse tangent")]
    [Category(Categories.Trigonometric)]
    [Section("Trigonometry")]
    [Description("Calculates the inverse hyperbolic tangent of a complex number.")]
    [FunctionSignature("atanh", "complex value")]
    [ExampleUsage("atanh(0)", "0")]
    public class InverseHyperbolicTangentFuncExpression : UnaryExpression<Object>
    {
        public InverseHyperbolicTangentFuncExpression(Expression<Object> subExpression)
            : base(subExpression)
        {
        }

        public override object Evaluate()
        {
            return Complex.Atanh(SubExpression.EvaluateAsComplex());
        }
    }

    [DisplayName("Inverse cotangent")]
    [Category(Categories.Trigonometric)]
    [Section("Trigonometry")]
    [Description("Calculates the inverse hyperbolic cotangent of a complex number.")]
    [FunctionSignature("acoth", "complex value")]
    [ExampleUsage("acoth(3)", "0.346573590279973")]
    public class InverseHyperbolicCotangentFuncExpression : UnaryExpression<Object>
    {
        public InverseHyperbolicCotangentFuncExpression(Expression<Object> subExpression)
            : base(subExpression)
        {
        }

        public override object Evaluate()
        {
            return Complex.Acoth(SubExpression.EvaluateAsComplex());
        }
    }

    [DisplayName("Inverse secant")]
    [Category(Categories.Trigonometric)]
    [Section("Trigonometry")]
    [Description("Calculates the inverse hyperbolic secant of a complex number.")]
    [FunctionSignature("asech", "complex value")]
    [ExampleUsage("asech(1)", "0")]
    public class InverseHyperbolicSecantFuncExpression : UnaryExpression<Object>
    {
        public InverseHyperbolicSecantFuncExpression(Expression<Object> subExpression)
            : base(subExpression)
        {
        }

        public override object Evaluate()
        {
            return Complex.Asech(SubExpression.EvaluateAsComplex());
        }
    }

    [DisplayName("Inverse cosecant")]
    [Category(Categories.Trigonometric)]
    [Section("Trigonometry")]
    [Description("Calculates the inverse hyperbolic cosecant of a complex number.")]
    [FunctionSignature("acsch", "complex value")]
    [ExampleUsage("acsch(2)", "0.481211825059603")]
    public class InverseHyperbolicCosecantFuncExpression : UnaryExpression<Object>
    {
        public InverseHyperbolicCosecantFuncExpression(Expression<Object> subExpression)
            : base(subExpression)
        {
        }

        public override object Evaluate()
        {
            return Complex.Acsch(SubExpression.EvaluateAsComplex());
        }
    }



    [DisplayName("Sine cardinal")]
    [Category(Categories.Trigonometric)]
    [Description("Calculates the hyperbolic sine cardinal (sinh(x) / x) of a complex number.")]
    [FunctionSignature("sinhc", "complex value")]
    [ExampleUsage("sinhc(0)", "1")]
    [ExampleUsage("sinhc(1)", "1.1752011936438")]
    public class HyperbolicSineCardinalFuncExpression : UnaryExpression<Object>
    {
        public HyperbolicSineCardinalFuncExpression(Expression<Object> subExpression)
            : base(subExpression)
        {
        }

        public override object Evaluate()
        {
            return Complex.Sinhc(SubExpression.EvaluateAsComplex());
        }
    }

    [DisplayName("Tangent cardinal")]
    [Category(Categories.Trigonometric)]
    [Description("Calculates the hyperbolic tangent cardinal (tanh(x) / x) of a complex number.")]
    [FunctionSignature("tanhc", "complex value")]
    [ExampleUsage("tanhc(0)", "1")]
    [ExampleUsage("tanhc(1)", "0.761594155955765")]
    public class HyperbolicTangentCardinalFuncExpression : UnaryExpression<Object>
    {
        public HyperbolicTangentCardinalFuncExpression(Expression<Object> subExpression)
            : base(subExpression)
        {
        }

        public override object Evaluate()
        {
            return Complex.Tanhc(SubExpression.EvaluateAsComplex());
        }
    }
}
