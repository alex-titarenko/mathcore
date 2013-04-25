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
    [Description("Calculates sine of a complex number.")]
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
    [Description("Calculates cosine of a complex number.")]
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
    [Description("Calculates tangent of a complex number.")]
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
    [Description("Calculates cotangent of a complex number.")]
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
    [Description("Calculates secant of a complex number.")]
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
    [Description("Calculates cosecant of a complex number.")]
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
}
