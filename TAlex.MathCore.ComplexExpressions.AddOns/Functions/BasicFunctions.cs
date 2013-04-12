using System;
using System.Collections.Generic;
using System.ComponentModel;
using TAlex.MathCore.ExpressionEvaluation.Trees;
using TAlex.MathCore.ExpressionEvaluation.Trees.Metadata;


namespace TAlex.MathCore.ExpressionEvaluation.ComplexExpressions.Functions
{
    [DisplayName("Signature")]
    [Category(Categories.Basics)]
    [Description("Calculates sign of a complex number.")]
    [FunctionSignature("sign", "complex value")]
    [ExampleUsage("sing(12.8)", "1")]
    [ExampleUsage("sing(-5)", "-1")]
    public class SignFuncExpression : UnaryExpression<Object>
    {
        public SignFuncExpression(Expression<Object> subExpression)
            : base(subExpression)
        {
        }

        public override object Evaluate()
        {
            return (Complex)Complex.Sign((Complex)SubExpr.Evaluate());
        }
    }
}
