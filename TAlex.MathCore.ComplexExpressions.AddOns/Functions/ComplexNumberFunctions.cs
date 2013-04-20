using System;
using System.ComponentModel;
using TAlex.MathCore.ExpressionEvaluation.Trees;
using TAlex.MathCore.ExpressionEvaluation.Trees.Metadata;


namespace TAlex.MathCore.ExpressionEvaluation.ComplexExpressions.Functions
{
    [DisplayName("Real part")]
    [Category(Categories.ComplexNumbers)]
    [Description("Calculates the real part of a complex number.")]
    [FunctionSignature("Re", "complex value")]
    [ExampleUsage("Re(3.8 + 11i)", "3.8")]
    public class RealPartFuncExpression : UnaryExpression<Object>
    {
        public RealPartFuncExpression(Expression<Object> subExpression)
            : base(subExpression)
        {
        }

        public override object Evaluate()
        {
            return (Complex)(SubExpression.EvaluateAsComplex().Re);
        }
    }

    [DisplayName("Imaginary part")]
    [Category(Categories.ComplexNumbers)]
    [Description("Calculates the imaginary part of a complex number.")]
    [FunctionSignature("Im", "complex value")]
    [ExampleUsage("Im(3.8 + 11i)", "11")]
    public class ImaginaryPartFuncExpression : UnaryExpression<Object>
    {
        public ImaginaryPartFuncExpression(Expression<Object> subExpression)
            : base(subExpression)
        {
        }

        public override object Evaluate()
        {
            return (Complex)(SubExpression.EvaluateAsComplex().Im);
        }
    }

    [DisplayName("Argument")]
    [Category(Categories.ComplexNumbers)]
    [Description("Calculates the argument of a complex number.")]
    [FunctionSignature("arg", "complex value")]
    [ExampleUsage("arg(2 + 3i)", "0.982793723247329")]
    [ExampleUsage("arg(0)", "NaN")]
    public class ArgumentFuncExpression : UnaryExpression<Object>
    {
        public ArgumentFuncExpression(Expression<Object> subExpression)
            : base(subExpression)
        {
        }

        public override object Evaluate()
        {
            return (Complex)Complex.Arg(SubExpression.EvaluateAsComplex());
        }
    }

    [DisplayName("Conjugation number")]
    [Category(Categories.ComplexNumbers)]
    [Description("Calculates conjugation number for a complex number.")]
    [FunctionSignature("conj", "complex value")]
    [ExampleUsage("conj(3 + 5i)", "3 - 5i")]
    [ExampleUsage("conj(-3 - 8i)", "-3 + 8i")]
    [ExampleUsage("conj(3)", "3")]
    public class ConjugationNumberFuncExpression : UnaryExpression<Object>
    {
        public ConjugationNumberFuncExpression(Expression<Object> subExpression)
            : base(subExpression)
        {
        }

        public override object Evaluate()
        {
            return Complex.Conjugate(SubExpression.EvaluateAsComplex());
        }
    }
}
