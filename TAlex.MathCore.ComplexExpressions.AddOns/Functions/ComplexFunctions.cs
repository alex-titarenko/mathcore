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
    [Description("Calculates sine for a complex number.")]
    [FunctionSignature("sin", "complex value")]
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
    [Description("Calculates cosine for a complex number.")]
    [FunctionSignature("cos", "complex value")]
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
}
