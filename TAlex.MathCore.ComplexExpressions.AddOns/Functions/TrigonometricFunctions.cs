using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using TAlex.MathCore;
using TAlex.MathCore.ExpressionEvaluation.Trees;
using TAlex.MathCore.ExpressionEvaluation.Trees.Metadata;


namespace TAlex.MathCore.ExpressionEvaluation.ComplexExpressions.Functions
{
    [DisplayName("Sine")]
    [Category(Categories.Trigonometric)]
    [Section("Trigonometry")]
    [Description("Calculates the sine of a complex number.")]
    [FunctionSignature("sin", "complex value")]
    [ExampleUsage("sin(0)", "0")]
    public class SineFuncExpression : UnaryExpression<Object>
    {
        public SineFuncExpression(Expression<Object> subExpression)
            : base(subExpression)
        {
        }

        public override object Evaluate()
        {
            return Complex.Sin(SubExpression.EvaluateAsComplex());
        }
    }

    [DisplayName("Cosine")]
    [Category(Categories.Trigonometric)]
    [Section("Trigonometry")]
    [Description("Calculates the cosine of a complex number.")]
    [FunctionSignature("cos", "complex value")]
    [ExampleUsage("cos(0)", "1")]
    public class CosineFuncExpression : UnaryExpression<Object>
    {
        public CosineFuncExpression(Expression<Object> subExpression)
            : base(subExpression)
        {
        }

        public override object Evaluate()
        {
            return Complex.Cos(SubExpression.EvaluateAsComplex());
        }
    }

    [DisplayName("Tangent")]
    [Category(Categories.Trigonometric)]
    [Section("Trigonometry")]
    [Description("Calculates the tangent of a complex number.")]
    [FunctionSignature("tan", "complex value")]
    [ExampleUsage("tan(0)", "0")]
    public class TangentFuncExpression : UnaryExpression<Object>
    {
        public TangentFuncExpression(Expression<Object> subExpression)
            : base(subExpression)
        {
        }

        public override object Evaluate()
        {
            return Complex.Tan(SubExpression.EvaluateAsComplex());
        }
    }

    [DisplayName("Cotangent")]
    [Category(Categories.Trigonometric)]
    [Section("Trigonometry")]
    [Description("Calculates the cotangent of a complex number.")]
    [FunctionSignature("cot", "complex value")]
    [ExampleUsage("cot(pi / 4)", "1")]
    public class CotangentFuncExpression : UnaryExpression<Object>
    {
        public CotangentFuncExpression(Expression<Object> subExpression)
            : base(subExpression)
        {
        }

        public override object Evaluate()
        {
            return Complex.Cot(SubExpression.EvaluateAsComplex());
        }
    }

    [DisplayName("Secant")]
    [Category(Categories.Trigonometric)]
    [Section("Trigonometry")]
    [Description("Calculates the secant of a complex number.")]
    [FunctionSignature("sec", "complex value")]
    [ExampleUsage("sec(pi / 3)", "2")]
    public class SecantFuncExpression : UnaryExpression<Object>
    {
        public SecantFuncExpression(Expression<Object> subExpression)
            : base(subExpression)
        {
        }

        public override object Evaluate()
        {
            return Complex.Sec(SubExpression.EvaluateAsComplex());
        }
    }

    [DisplayName("Cosecant")]
    [Category(Categories.Trigonometric)]
    [Section("Trigonometry")]
    [Description("Calculates the cosecant of a complex number.")]
    [FunctionSignature("csc", "complex value")]
    [ExampleUsage("csc(pi / 6)", "2")]
    public class CosecantFuncExpression : UnaryExpression<Object>
    {
        public CosecantFuncExpression(Expression<Object> subExpression)
            : base(subExpression)
        {
        }

        public override object Evaluate()
        {
            return Complex.Csc(SubExpression.EvaluateAsComplex());
        }
    }



    [DisplayName("Inverse sine")]
    [Category(Categories.Trigonometric)]
    [Section("Trigonometry")]
    [Description("Calculates the inverse sine of a complex number.")]
    [FunctionSignature("asin", "complex value")]
    [ExampleUsage("asin(0)", "0")]
    public class InverseSineFuncExpression : UnaryExpression<Object>
    {
        public InverseSineFuncExpression(Expression<Object> subExpression)
            : base(subExpression)
        {
        }

        public override object Evaluate()
        {
            return Complex.Asin(SubExpression.EvaluateAsComplex());
        }
    }

    [DisplayName("Inverse cosine")]
    [Category(Categories.Trigonometric)]
    [Section("Trigonometry")]
    [Description("Calculates the inverse cosine of a complex number.")]
    [FunctionSignature("acos", "complex value")]
    [ExampleUsage("acos(1)", "0")]
    public class InverseCosineFuncExpression : UnaryExpression<Object>
    {
        public InverseCosineFuncExpression(Expression<Object> subExpression)
            : base(subExpression)
        {
        }

        public override object Evaluate()
        {
            return Complex.Acos(SubExpression.EvaluateAsComplex());
        }
    }

    [DisplayName("Inverse tangent")]
    [Category(Categories.Trigonometric)]
    [Section("Trigonometry")]
    [Description("Calculates the inverse tangent of a complex number.")]
    [FunctionSignature("atan", "complex value")]
    [ExampleUsage("atan(0)", "0")]
    public class InverseTangentFuncExpression : UnaryExpression<Object>
    {
        public InverseTangentFuncExpression(Expression<Object> subExpression)
            : base(subExpression)
        {
        }

        public override object Evaluate()
        {
            return Complex.Atan(SubExpression.EvaluateAsComplex());
        }
    }

    [DisplayName("Inverse cotangent")]
    [Category(Categories.Trigonometric)]
    [Section("Trigonometry")]
    [Description("Calculates the inverse cotangent of a complex number.")]
    [FunctionSignature("acot", "complex value")]
    [ExampleUsage("acot(1)", "0.785398163397448")]
    public class InverseCotangentFuncExpression : UnaryExpression<Object>
    {
        public InverseCotangentFuncExpression(Expression<Object> subExpression)
            : base(subExpression)
        {
        }

        public override object Evaluate()
        {
            return Complex.Acot(SubExpression.EvaluateAsComplex());
        }
    }

    [DisplayName("Inverse secant")]
    [Category(Categories.Trigonometric)]
    [Section("Trigonometry")]
    [Description("Calculates the inverse secant of a complex number.")]
    [FunctionSignature("asec", "complex value")]
    [ExampleUsage("asec(2)", "1.0471975511966")]
    public class InverseSecantFuncExpression : UnaryExpression<Object>
    {
        public InverseSecantFuncExpression(Expression<Object> subExpression)
            : base(subExpression)
        {
        }

        public override object Evaluate()
        {
            return Complex.Asec(SubExpression.EvaluateAsComplex());
        }
    }

    [DisplayName("Inverse cosecant")]
    [Category(Categories.Trigonometric)]
    [Section("Trigonometry")]
    [Description("Calculates the inverse cosecant of a complex number.")]
    [FunctionSignature("acsc", "complex value")]
    [ExampleUsage("acsc(2)", "0.523598775598299")]
    public class InverseCosecantFuncExpression : UnaryExpression<Object>
    {
        public InverseCosecantFuncExpression(Expression<Object> subExpression)
            : base(subExpression)
        {
        }

        public override object Evaluate()
        {
            return Complex.Acsc(SubExpression.EvaluateAsComplex());
        }
    }



    [DisplayName("Angle")]
    [Category(Categories.Trigonometric)]
    [Description("Calculates the angle whose tangent is the quotient of two specified numbers.")]
    [FunctionSignature("atan2", "real y", "real x")]
    [ExampleUsage("atan2(4, 3)", "0.927295218001612")]
    public class AngleFuncExpression : BinaryExpression<Object>
    {
        public AngleFuncExpression(Expression<Object> yExpression, Expression<Object> xExpression)
            : base(yExpression, xExpression)
        {
        }

        public override object Evaluate()
        {
            double y = LeftExpression.EvaluateAsDouble();
            double x = RightExpression.EvaluateAsDouble();

            return (Complex)Math.Atan2(y, x);
        }
    }



    [DisplayName("Versine")]
    [Category(Categories.Trigonometric)]
    [Description("Calculates the versine (1 - cos(x)) of a complex number.")]
    [FunctionSignature("vers", "complex value")]
    [ExampleUsage("vers(pi)", "2")]
    public class VersineFuncExpression : UnaryExpression<Object>
    {
        public VersineFuncExpression(Expression<Object> subExpression)
            : base(subExpression)
        {
        }

        public override object Evaluate()
        {
            return Complex.Vers(SubExpression.EvaluateAsComplex());
        }
    }

    [DisplayName("Coversine")]
    [Category(Categories.Trigonometric)]
    [Description("Calculates the coversine (1 - sin(x)) of a complex number.")]
    [FunctionSignature("cvs", "complex value")]
    [ExampleUsage("cvs(pi)", "1")]
    public class CoversineFuncExpression : UnaryExpression<Object>
    {
        public CoversineFuncExpression(Expression<Object> subExpression)
            : base(subExpression)
        {
        }

        public override object Evaluate()
        {
            return Complex.Cvs(SubExpression.EvaluateAsComplex());
        }
    }
}
