using System;
using System.Collections.Generic;
using System.ComponentModel;
using TAlex.MathCore.ExpressionEvaluation.Trees;
using TAlex.MathCore.ExpressionEvaluation.Trees.Metadata;
using TAlex.MathCore.LinearAlgebra;


namespace TAlex.MathCore.ExpressionEvaluation.ComplexExpressions.Functions
{
    [DisplayName("Signature")]
    [Category(Categories.Basic)]
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

    [DisplayName("Absolute value")]
    [Category(Categories.Basic)]
    [Description("Calculates abolute value (modulus) of a complex number. Formula to calculate: |z| = sqrt(x^2+y^2)")]
    [FunctionSignature("abs", "complex value")]
    [ExampleUsage("abs(-12.5)", "12.5")]
    [ExampleUsage("abs(-3+4i)", "5")]
    public class AbsFuncExpression : UnaryExpression<Object>
    {
        public AbsFuncExpression(Expression<Object> subExpression)
            : base(subExpression)
        {
        }

        public override object Evaluate()
        {
            return (Complex)Complex.Abs((Complex)SubExpr.Evaluate());
        }
    }

    [DisplayName("Inverse")]
    [Category(Categories.Basic)]
    [Description("Calculates inverse value of a complex number or matrix.")]
    [FunctionSignature("inv", "complex value")]
    [FunctionSignature("inv", "complex matrix m")]
    [ExampleUsage("inv(5)", "0.2")]
    [ExampleUsage("inv({1,2;3,4})", "{-2,1;1.5,-0.5}")]
    public class InvFuncExpression : UnaryExpression<Object>
    {
        public InvFuncExpression(Expression<Object> subExpression)
            : base(subExpression)
        {
        }

        public override object Evaluate()
        {
            object value = SubExpr.Evaluate();

            if (value is Complex)
                return Complex.Inverse((Complex)value);
            else if (value is CMatrix)
                return CMatrix.Inverse((CMatrix)value);
            else
                throw new InvalidCastException();
        }
    }
}
