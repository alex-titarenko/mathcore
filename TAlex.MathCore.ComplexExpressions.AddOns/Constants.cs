using System;
using TAlex.MathCore.ExpressionEvaluation.Trees;
using TAlex.MathCore.ExpressionEvaluation.Trees.Metadata;


namespace TAlex.MathCore.ExpressionEvaluation.ComplexExpressions
{
    [Constant("i")]
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

    [Constant("pi")]
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

    [Constant("e")]
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

    [Constant("goldrat")]
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

    [Constant("euler")]
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

    [Constant("catalan")]
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

    [Constant("sqrt2")]
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

    [Constant("sqrt3")]
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

    [Constant("maxval")]
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

    [Constant("minval")]
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

    [Constant("inf")]
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
