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
    [Category(Categories.General)]
    [Section("Trigonometry")]
    [Description("Calculates sine of a complex number.")]
    [FunctionSignature("sin", "complex value")]
    [ExampleUsage("sin(0)", "0")]
    public class SinFuncExpression : UnaryExpression<Object>
    {
        public SinFuncExpression(Expression<Object> subExpression)
            : base(subExpression)
        {
        }

        public override object Evaluate()
        {
            return Complex.Sin((Complex)SubExpr.Evaluate());
        }
    }

    [DisplayName("Cosine")]
    [Category(Categories.General)]
    [Section("Trigonometry")]
    [Description("Calculates cosine of a complex number.")]
    [FunctionSignature("cos", "complex value")]
    [ExampleUsage("cos(0)", "1")]
    public class CosFuncExpression : UnaryExpression<Object>
    {
        public CosFuncExpression(Expression<Object> subExpression)
            : base(subExpression)
        {
        }

        public override object Evaluate()
        {
            return Complex.Cos((Complex)SubExpr.Evaluate());
        }
    }

    [DisplayName("Tangent")]
    [Category(Categories.General)]
    [Section("Trigonometry")]
    [Description("Calculates tangent of a complex number.")]
    [FunctionSignature("tan", "complex value")]
    [ExampleUsage("tan(0)", "0")]
    public class TanFuncExpression : UnaryExpression<Object>
    {
        public TanFuncExpression(Expression<Object> subExpression)
            : base(subExpression)
        {
        }

        public override object Evaluate()
        {
            return Complex.Tan((Complex)SubExpr.Evaluate());
        }
    }
}
