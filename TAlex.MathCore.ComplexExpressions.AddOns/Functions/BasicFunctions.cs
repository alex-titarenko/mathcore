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
            return (Complex)Complex.Sign(ConvertEx.AsComplex(SubExpr.Evaluate()));
        }
    }

    [DisplayName("Absolute value")]
    [Category(Categories.Basic)]
    [Description("Calculates absolute value (modulus) of a complex number. Formula to calculate: |z| = sqrt(x^2+y^2)")]
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
            return (Complex)Complex.Abs(ConvertEx.AsComplex(SubExpr.Evaluate()));
        }
    }

    [DisplayName("Inverse")]
    [Category(Categories.Basic)]
    [Description("Calculates inverse value of a complex number or matrix.")]
    [FunctionSignature("inv", "complex value")]
    [FunctionSignature("inv", "complex matrix value")]
    [ExampleUsage("inv(5)", "0.2")]
    [ExampleUsage("inv({1, 2; 3, 4})", "{-2, 1; 1.5, -0.5}")]
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
                throw ExceptionHelper.ThrowWrongArgumentType(value);
        }
    }

    [DisplayName("Square")]
    [Category(Categories.Basic)]
    [Description("Calculates complex number or matrix value raised to second power.")]
    [FunctionSignature("sqr", "complex value")]
    [FunctionSignature("sqr", "complex matrix value")]
    [ExampleUsage("sqr(-5)", "25")]
    [ExampleUsage("sqr(2 - 3i)", "-5 - 12i")]
    [ExampleUsage("sqr({2, 3; 5, 8})", "{19, 30; 50, 79}")]
    public class SquareFuncExpression : UnaryExpression<Object>
    {
        public SquareFuncExpression(Expression<Object> subExpression)
            : base(subExpression)
        {
        }

        public override object Evaluate()
        {
            object value = SubExpr.Evaluate();

            if (value is Complex)
                return Complex.Pow((Complex)value, 2);
            else if (value is CMatrix)
                return CMatrix.Pow((CMatrix)value, 2);
            else
                throw ExceptionHelper.ThrowWrongArgumentType(value);
        }
    }

    [DisplayName("Cube")]
    [Category(Categories.Basic)]
    [Description("Calculates complex number or matrix value raised to third power.")]
    [FunctionSignature("cube", "complex value")]
    [FunctionSignature("cube", "complex matrix value")]
    [ExampleUsage("cube(13)", "2197")]
    [ExampleUsage("cube(-1 + 3i)", "26 - 18i")]
    [ExampleUsage("cube({2i, -3; 55, 8})", "{-1320 - 668i, 315 - 48i; -5775 + 880i, -2128 - 330i}")]
    public class CubeFuncExpression : UnaryExpression<Object>
    {
        public CubeFuncExpression(Expression<Object> subExpression)
            : base(subExpression)
        {
        }

        public override object Evaluate()
        {
            object value = SubExpr.Evaluate();

            if (value is Complex)
                return Complex.Pow((Complex)value, 3);
            else if (value is CMatrix)
                return CMatrix.Pow((CMatrix)value, 3);
            else
                throw ExceptionHelper.ThrowWrongArgumentType(value);
        }
    }

    [DisplayName("Square root")]
    [Category(Categories.Basic)]
    [Description("Calculates square root of complex number or matrix.")]
    [FunctionSignature("sqrt", "complex value")]
    [FunctionSignature("sqrt", "complex matrix value")]
    [ExampleUsage("sqrt(-4)", "2i")]
    [ExampleUsage("sqrt({33, 24; 48, 57})", "{5, 2; 4, 7}")]
    public class SqrtFuncExpression : UnaryExpression<Object>
    {
        public SqrtFuncExpression(Expression<Object> subExpression)
            : base(subExpression)
        {
        }

        public override object Evaluate()
        {
            object value = SubExpr.Evaluate();

            if (value is Complex)
                return Complex.Sqrt((Complex)value);
            else if (value is CMatrix)
                return CMatrix.Sqrt((CMatrix)value);
            else
                throw ExceptionHelper.ThrowWrongArgumentType(value);
        }
    }

    [DisplayName("Nth root")]
    [Category(Categories.Basic)]
    [Description("Calculates n-th root of complex number or matrix.")]
    [FunctionSignature("nthroot", "complex value", "complex n")]
    [ExampleUsage("nthroot(27, 3)", "3")]
    public class NthRootFuncExpression : BinaryExpression<Object>
    {
        public NthRootFuncExpression(Expression<Object> valueExpression, Expression<Object> expExpression)
            : base(valueExpression, expExpression)
        {
        }

        public override object Evaluate()
        {
            object value = LeftExpression.Evaluate();
            object exp = RightExpression.Evaluate();

            return Complex.Pow(ConvertEx.AsComplex(value), 1.0 / ConvertEx.AsComplex(exp));
        }
    }

    [DisplayName("Pow ten")]
    [Category(Categories.Basic)]
    [Description("Calculates the number 10 raised to the specified power.")]
    [FunctionSignature("powten", "complex value")]
    [ExampleUsage("powten(2)", "100")]
    [ExampleUsage("powten(3)", "1000")]
    public class PowTenFuncExpression : UnaryExpression<Object>
    {
        public PowTenFuncExpression(Expression<Object> subExpression)
            : base(subExpression)
        {
        }

        public override object Evaluate()
        {
            object value = SubExpr.Evaluate();
            return Complex.Pow(10.0, ConvertEx.AsComplex(value));
        }
    }

    [DisplayName("Modulo")]
    [Category(Categories.Basic)]
    [Description("Calculates remainder of division of one number by another.")]
    [FunctionSignature("mod", "real x", "real y")]
    [ExampleUsage("mod(5, 3)", "2")]
    public class ModuloFuncExpression : BinaryExpression<Object>
    {
        public ModuloFuncExpression(Expression<Object> xExpression, Expression<Object> yExpression)
            : base(xExpression, yExpression)
        {
        }

        public override object Evaluate()
        {
            object x = LeftExpression.Evaluate();
            object y = RightExpression.Evaluate();

            return (Complex)(ConvertEx.AsDouble(x) % ConvertEx.AsDouble(y));
        }
    }

    [DisplayName("Integer part")]
    [Category(Categories.Basic)]
    [Description("Calculates the integer part of a complex number. Nearest integer number from the left.")]
    [FunctionSignature("int", "complex value")]
    [ExampleUsage("int(3.6)", "3")]
    [ExampleUsage("int(-5.2)", "-6")]
    [ExampleUsage("int(-1.1 + 13.6i)", "-2 + 13i")]
    public class IntegerPartFuncExpression : UnaryExpression<Object>
    {
        public IntegerPartFuncExpression(Expression<Object> subExpression)
            : base(subExpression)
        {
        }

        public override object Evaluate()
        {
            return ExMath.IntPart(ConvertEx.AsComplex(SubExpr.Evaluate()));
        }
    }

    [DisplayName("Fractional part")]
    [Category(Categories.Basic)]
    [Description("Calculates the fractional part of a complex number.")]
    [FunctionSignature("frac", "complex value")]
    [ExampleUsage("frac(3.6)", "0.6")]
    [ExampleUsage("frac(-5.2)", "0.8")]
    [ExampleUsage("frac(-1.1 + 13.6i)", "0.9 + 0.6i")]
    public class FractionalPartFuncExpression : UnaryExpression<Object>
    {
        public FractionalPartFuncExpression(Expression<Object> subExpression)
            : base(subExpression)
        {
        }

        public override object Evaluate()
        {
            return ExMath.FracPart(ConvertEx.AsComplex(SubExpr.Evaluate()));
        }
    }

    [DisplayName("Floor")]
    [Category(Categories.Basic)]
    [Description("Calculates the largest complex integer less than or equal to the specified complex number.")]
    [FunctionSignature("floor", "complex value")]
    [ExampleUsage("floor(3.6)", "3")]
    [ExampleUsage("floor(-5.2)", "-6")]
    [ExampleUsage("floor(-1.1 + 13.6i)", "-2 + 13i")]
    public class FloorFuncExpression : UnaryExpression<Object>
    {
        public FloorFuncExpression(Expression<Object> subExpression)
            : base(subExpression)
        {
        }

        public override object Evaluate()
        {
            return Complex.Floor(ConvertEx.AsComplex(SubExpr.Evaluate()));
        }
    }

    [DisplayName("Ceiling")]
    [Category(Categories.Basic)]
    [Description("Calculates the smallest complex integer greater than or equal to the specified complex number.")]
    [FunctionSignature("ceil", "complex value")]
    [ExampleUsage("ceil(3.6)", "4")]
    [ExampleUsage("ceil(-5.2)", "-5")]
    [ExampleUsage("ceil(-1.1 + 13.6i)", "-1 + 14i")]
    public class CeilingFuncExpression : UnaryExpression<Object>
    {
        public CeilingFuncExpression(Expression<Object> subExpression)
            : base(subExpression)
        {
        }

        public override object Evaluate()
        {
            return Complex.Ceiling(ConvertEx.AsComplex(SubExpr.Evaluate()));
        }
    }

    [DisplayName("Round")]
    [Category(Categories.Basic)]
    [Description("Rounds a complex value to the nearest integer.")]
    [FunctionSignature("round", "complex value")]
    [FunctionSignature("round", "complex value", "integer digits")]
    [ExampleUsage("round(3.6)", "4")]
    [ExampleUsage("round(5.2)", "5")]
    [ExampleUsage("round(12.24667, 3)", "12.247")]
    public class RoundFuncExpression : BinaryExpression<Object>
    {
        public RoundFuncExpression(Expression<Object> valueExpression)
            : this(valueExpression, new ScalarExpression<Object>(Complex.Zero))
        {
        }

        public RoundFuncExpression(Expression<Object> valueExpression, Expression<Object> digitsExpression)
            : base(valueExpression, digitsExpression)
        {
        }

        public override object Evaluate()
        {
            Object value = LeftExpression.Evaluate();
            Object digits = RightExpression.Evaluate();
            
            return Complex.Round(ConvertEx.AsComplex(value), ConvertEx.AsInt32(digits));
        }
    }

    [DisplayName("Truncate")]
    [Category(Categories.Basic)]
    [Description("Calculates the integral part of a specified complex number.")]
    [FunctionSignature("trunc", "complex value")]
    [ExampleUsage("trunc(3.6)", "3")]
    [ExampleUsage("trunc(-5.2)", "-5")]
    [ExampleUsage("trunc(-1.1 + 13.6i)", "-1 + 13i")]
    public class TruncateFuncExpression : UnaryExpression<Object>
    {
        public TruncateFuncExpression(Expression<Object> subExpression)
            : base(subExpression)
        {
        }

        public override object Evaluate()
        {
            return Complex.Truncate(ConvertEx.AsComplex(SubExpr.Evaluate()));
        }
    }
}
