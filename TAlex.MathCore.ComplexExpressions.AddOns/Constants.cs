using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            return Math.PI;
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
            return Math.E;
        }
    }
}
