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
    [DisplayName("Natural logarithm")]
    [Category(Categories.LogAndExponential)]
    [Description("Calculates the natural logarithm of a complex number.")]
    [FunctionSignature("ln", "complex value")]
    [ExampleUsage("ln(e)", "1")]
    [ExampleUsage("ln(1)", "0")]
    [ExampleUsage("ln(3)", "1.09861228866811")]
    public class NaturalLogarithmFuncExpression : UnaryExpression<Object>
    {
        public NaturalLogarithmFuncExpression(Expression<Object> subExpression)
            : base(subExpression)
        {
        }

        public override object Evaluate()
        {
            return Complex.Log(SubExpression.EvaluateAsComplex());
        }
    }

    [DisplayName("Logarithm")]
    [Category(Categories.LogAndExponential)]
    [Description("Calculates the logarithm of a complex number in a specified base.")]
    [FunctionSignature("log", "complex value")]
    [FunctionSignature("log", "complex value", "real base")]
    [ExampleUsage("log(10)", "1")]
    [ExampleUsage("log(1)", "0")]
    [ExampleUsage("log(3, 5)", "0.682606194485985")]
    [ExampleUsage("log(e, e)", "1")]
    public class LogarithmFuncExpression : BinaryExpression<Object>
    {
        public LogarithmFuncExpression(Expression<Object> valueExpression)
            : this(valueExpression, new ScalarExpression<Object>((Complex)10))
        {
        }

        public LogarithmFuncExpression(Expression<Object> valueExpression, Expression<Object> baseExpression)
            : base(valueExpression, baseExpression)
        {
        }

        public override object Evaluate()
        {
            return Complex.Log(LeftExpression.EvaluateAsComplex(), RightExpression.EvaluateAsDouble());
        }
    }

    [DisplayName("Exponential")]
    [Category(Categories.LogAndExponential)]
    [Description("Calculates the exponential of a complex number or matrix.")]
    [FunctionSignature("exp", "complex value")]
    [FunctionSignature("exp", "complex matrix value")]
    [ExampleUsage("exp(0)", "1")]
    [ExampleUsage("exp(1)", "2.71828182845905")]
    [ExampleUsage("exp(3)", "20.0855369231877")]
    [ExampleUsage("exp({1, 2; 5, 4})", "{115.528140598761, 115.16026115759; 287.900652893974, 288.268532335145}")]
    public class ExponentialFuncExpression : UnaryExpression<Object>
    {
        public ExponentialFuncExpression(Expression<Object> subExpression)
            : base(subExpression)
        {
        }

        public override object Evaluate()
        {
            object value = SubExpression.Evaluate();

            if (value is Complex)
                return Complex.Exp((Complex)value);
            else if (value is CMatrix)
                return CMatrix.Exp((CMatrix)value);

            throw ExceptionHelper.ThrowWrongArgumentType(value);
        }
    }
}
