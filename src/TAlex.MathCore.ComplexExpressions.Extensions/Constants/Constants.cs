using System;
using TAlex.MathCore.ExpressionEvaluation.Trees;
using TAlex.MathCore.ExpressionEvaluation.Trees.Metadata;


namespace TAlex.MathCore.ExpressionEvaluation.ComplexExpressions.Constants
{
    [DisplayName("Imaginary unit")]
    [Constant("i", Value = "1i")]
    public class ImaginaryConstantExpression : ConstantExpression<Object>
    {
        public override string Name
        {
            get { return "I"; }
        }

        public override Object Evaluate()
        {
            return Complex.I;
        }
    }

    [DisplayName("Number 'pi'")]
    [Constant("pi", Value = "3.141592653589793...")]
    public class PiConstantExpression : ConstantExpression<Object>
    {
        public override string Name
        {
            get { return "PI"; }
        }

        public override Object Evaluate()
        {
            return (Complex)Math.PI;
        }
    }

    [DisplayName("Number 'e'")]
    [Constant("e", Value = "2.718281828459045...")]
    public class EConstantExpression : ConstantExpression<Object>
    {
        public override string Name
        {
            get { return "E"; }
        }

        public override Object Evaluate()
        {
            return (Complex)Math.E;
        }
    }

    [DisplayName("Golden ratio")]
    [Constant("goldrat", Value = "1.6180339887498949...")]
    public class GoldenRatioConstantExpression : ConstantExpression<Object>
    {
        public override string Name
        {
            get { return "GoldenRatio"; }
        }

        public override Object Evaluate()
        {
            return (Complex)ExMath.GoldenRatio;
        }
    }

    [DisplayName("Euler's constant")]
    [Constant("euler", Value = "0.57721566490153287...")]
    public class EulerConstantExpression : ConstantExpression<Object>
    {
        public override string Name
        {
            get { return "Euler"; }
        }

        public override Object Evaluate()
        {
            return (Complex)ExMath.EulersConstant;
        }
    }

    [DisplayName("Catalan's constant")]
    [Constant("catalan", Value = "0.91596559417721901...")]
    public class CatalanConstantExpression : ConstantExpression<Object>
    {
        public override string Name
        {
            get { return "G"; }
        }

        public override Object Evaluate()
        {
            return (Complex)ExMath.CatalansConstant;
        }
    }

    [DisplayName("Square root of 2")]
    [Constant("sqrt2", Value = "1.4142135623730952...")]
    public class Sqrt2ConstantExpression : ConstantExpression<Object>
    {
        public override string Name
        {
            get { return "Sqrt2"; }
        }

        public override Object Evaluate()
        {
            return (Complex)ExMath.Sqrt2;
        }
    }

    [DisplayName("Square root of 3")]
    [Constant("sqrt3", Value = "1.7320508075688772...")]
    public class Sqrt3ConstantExpression : ConstantExpression<Object>
    {
        public override string Name
        {
            get { return "Sqrt3"; }
        }

        public override Object Evaluate()
        {
            return (Complex)ExMath.Sqrt3;
        }
    }

    [DisplayName("Maximum value")]
    [Constant("maxval", Value = "1E+307")]
    public class MaxValueConstantExpression : ConstantExpression<Object>
    {
        public override string Name
        {
            get { return "MaxVal"; }
        }

        public override Object Evaluate()
        {
            return (Complex)1E307;
        }
    }

    [DisplayName("Minimum value")]
    [Constant("minval", Value = "-1E+307")]
    public class MinValueConstantExpression : ConstantExpression<Object>
    {
        public override string Name
        {
            get { return "MinVal"; }
        }

        public override Object Evaluate()
        {
            return (Complex)(-1E307);
        }
    }

    [DisplayName("Infinity")]
    [Constant("inf", Value = "Infinity")]
    public class InfinityConstantExpression : ConstantExpression<Object>
    {
        public override string Name
        {
            get { return "Inf"; }
        }

        public override Object Evaluate()
        {
            return (Complex)double.PositiveInfinity;
        }
    }
}
