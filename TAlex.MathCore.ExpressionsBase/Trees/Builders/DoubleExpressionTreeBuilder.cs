using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

using System.ComponentModel;

using TAlex.MathCore.ExpressionEvaluation.Tokenize;


namespace TAlex.MathCore.ExpressionEvaluation.Trees.Builders
{
    public sealed class DoubleExpressionTreeBuilder : SimpleExpressionTreeBuilder<double>
    {
        #region Constructors

        public DoubleExpressionTreeBuilder()
        {
            ConstantFlyweightFactory = new ConstantFlyweightFactory<double>()
            {
                Constants = new Dictionary<string, Type>()
                {
                    { "pi", typeof(PiConstantExpression) },
                    { "e", typeof(EConstantExpression) }
                }
            };

            Functions = new List<KeyValuePair<string, Type>>()
            {
                new KeyValuePair<string, Type>("rnd", typeof(RandomFuncExpression)),
                new KeyValuePair<string, Type>("min", typeof(MinFuncExpression)),
                new KeyValuePair<string, Type>("max", typeof(MaxFuncExpression)),
                new KeyValuePair<string, Type>("abs", typeof(AbsFuncExpression)),
                new KeyValuePair<string, Type>("sqrt", typeof(SqrtFuncExpression)),
                new KeyValuePair<string, Type>("ln", typeof(LogFuncExpression)),
                new KeyValuePair<string, Type>("log", typeof(Log10FuncExpression)),
                new KeyValuePair<string, Type>("log", typeof(LogFreeBaseFuncExpression)),
                new KeyValuePair<string, Type>("exp", typeof(ExpFuncExpression)),
                new KeyValuePair<string, Type>("sin", typeof(SinFuncExpression)),
                new KeyValuePair<string, Type>("cos", typeof(CosFuncExpression)),
                new KeyValuePair<string, Type>("tan", typeof(TanFuncExpression)),
                new KeyValuePair<string, Type>("asin", typeof(AsinFuncExpression)),
                new KeyValuePair<string, Type>("acos", typeof(AcosFuncExpression)),
                new KeyValuePair<string, Type>("atan", typeof(AtanFuncExpression)),
                new KeyValuePair<string, Type>("atan2", typeof(Atan2FuncExpression)),
                new KeyValuePair<string, Type>("sinh", typeof(SinhFuncExpression)),
                new KeyValuePair<string, Type>("cosh", typeof(CoshFuncExpression)),
                new KeyValuePair<string, Type>("tanh", typeof(TanhFuncExpression)),
            };
        }

        #endregion

        #region Methods

        protected override double GetDefaultVariableValue()
        {
            return Double.NaN;
        }

        protected override BinaryExpression<double> CreateAddExpression()
        {
            return new AddDoubleExpression();
        }

        protected override BinaryExpression<double> CreateSubExpression()
        {
            return new SubDoubleExpression();
        }

        protected override BinaryExpression<double> CreateMultExpression()
        {
            return new MultDoubleExpression();
        }

        protected override BinaryExpression<double> CreateDivExpression()
        {
            return new DivDoubleExpression();
        }

        protected override BinaryExpression<double> CreatePowExpression()
        {
            return new PowDoubleExpression();
        }

        protected override UnaryExpression<double> CreateUnaryMinusExpression(Expression<double> subExpression)
        {
            return new UnaryMinusDoubleExpression(subExpression);
        }

        protected override ScalarExpression<double> ParseScalarValue(string s)
        {
            return new ScalarExpression<double>(Double.Parse(s, CultureInfo.InvariantCulture));
        }

        #endregion

        #region Nested Types

        private class AddDoubleExpression : AddExpression<double>
        {
            public override double Evaluate()
            {
                return LeftExpression.Evaluate() + RightExpression.Evaluate();
            }
        }

        private class SubDoubleExpression : SubExpression<double>
        {
            public override double Evaluate()
            {
                return LeftExpression.Evaluate() - RightExpression.Evaluate();
            }
        }

        private class MultDoubleExpression : MultExpression<double>
        {
            public override double Evaluate()
            {
                return LeftExpression.Evaluate() * RightExpression.Evaluate();
            }
        }

        private class DivDoubleExpression : DivExpression<double>
        {
            public override double Evaluate()
            {
                return LeftExpression.Evaluate() / RightExpression.Evaluate();
            }
        }

        private class PowDoubleExpression : PowExpression<double>
        {
            public override double Evaluate()
            {
                return Math.Pow(LeftExpression.Evaluate(), RightExpression.Evaluate());
            }
        }

        private class UnaryMinusDoubleExpression : UnaryMinusExpression<double>
        {
            public UnaryMinusDoubleExpression(Expression<double> subExpression)
                : base(subExpression)
            {
            }

            public override double Evaluate()
            {
                return -SubExpr.Evaluate();
            }
        }


        [FunctionSignature("rnd", "real maxValue")]
        [FunctionSignature("rnd", "real minValue", "real maxValue")]
        public class RandomFuncExpression : BinaryExpression<double>
        {
            public static Random Random = new Random();

            public RandomFuncExpression(Expression<double> maxValueExpression)
                : this(new ScalarExpression<double>(0), maxValueExpression)
            {
            }

            public RandomFuncExpression(Expression<double> minValueExpression, Expression<double> maxValueExpression)
                //: base(expression)
            {
                LeftExpression = minValueExpression;
                RightExpression = maxValueExpression;
            }

            public override double Evaluate()
            {
                double left = LeftExpression.Evaluate();
                double right = RightExpression.Evaluate();
                
                return left + Random.NextDouble() * (right - left);
            }
        }

        [DisplayName("Minimum")]
        [Category("General")]
        [Section("Basic")]
        [Description("Some description")]
        [FunctionSignature("min", "real arg1", "real arg2", "...")]
        public class MinFuncExpression : MultiaryExpression<double>
        {
            public MinFuncExpression(params Expression<double>[] expressions)
                : base(expressions)
            {
            }

            public override double Evaluate()
            {
                double min = double.PositiveInfinity;

                foreach (Expression<double> expr in Expressions)
                    min = Math.Min(min, expr.Evaluate());

                return min;
            }
        }

        [FunctionSignature("max", "real arg1", "real arg2", "...")]
        public class MaxFuncExpression : MultiaryExpression<double>
        {
            public MaxFuncExpression(params Expression<double>[] expressions)
                : base(expressions)
            {
            }

            public override double Evaluate()
            {
                double max = double.NegativeInfinity;

                foreach (Expression<double> expr in Expressions)
                    max = Math.Max(max, expr.Evaluate());

                return max;
            }
        }


        [FunctionSignature("abs", "real value")]
        public class AbsFuncExpression : UnaryExpression<double>
        {
            public AbsFuncExpression(Expression<double> subExpression)
                : base(subExpression)
            {
            }

            public override double Evaluate()
            {
                return Math.Abs(SubExpr.Evaluate());
            }
        }

        [FunctionSignature("sqrt")]
        public class SqrtFuncExpression : UnaryExpression<double>
        {
            public SqrtFuncExpression(Expression<double> subExpression)
                : base(subExpression)
            {
            }

            public override double Evaluate()
            {
                return Math.Sqrt(SubExpr.Evaluate());
            }
        }


        [FunctionSignature("ln")]
        public class LogFuncExpression : UnaryExpression<double>
        {
            public LogFuncExpression(Expression<double> subExpression)
                : base(subExpression)
            {
            }

            public override double Evaluate()
            {
                return Math.Log(SubExpr.Evaluate());
            }
        }

        [FunctionSignature("log")]
        public class Log10FuncExpression : UnaryExpression<double>
        {
            public Log10FuncExpression(Expression<double> subExpression)
                : base(subExpression)
            {
            }

            public override double Evaluate()
            {
                return Math.Log10(SubExpr.Evaluate());
            }
        }

        [FunctionSignature("log", "real a", "real newBase")]
        public class LogFreeBaseFuncExpression : BinaryExpression<double>
        {
            public LogFreeBaseFuncExpression(Expression<double> leftExpression, Expression<double> rightExpression)
                //: base(subExpression)
            {
                LeftExpression = leftExpression;
                RightExpression = rightExpression;
            }

            public override double Evaluate()
            {
                return Math.Log(LeftExpression.Evaluate(), RightExpression.Evaluate());
            }
        }

        [FunctionSignature("exp")]
        public class ExpFuncExpression : UnaryExpression<double>
        {
            public ExpFuncExpression(Expression<double> subExpression)
                : base(subExpression)
            {
            }

            public override double Evaluate()
            {
                return Math.Exp(SubExpr.Evaluate());
            }
        }


        [FunctionSignature("sin")]
        public class SinFuncExpression : UnaryExpression<double>
        {
            public SinFuncExpression(Expression<double> subExpression)
                : base(subExpression)
            {
            }

            public override double Evaluate()
            {
                return Math.Sin(SubExpr.Evaluate());
            }
        }

        [FunctionSignature("cos")]
        public class CosFuncExpression : UnaryExpression<double>
        {
            public CosFuncExpression(Expression<double> subExpression)
                : base(subExpression)
            {
            }

            public override double Evaluate()
            {
                return Math.Cos(SubExpr.Evaluate());
            }
        }

        [FunctionSignature("tan")]
        public class TanFuncExpression : UnaryExpression<double>
        {
            public TanFuncExpression(Expression<double> subExpression)
                : base(subExpression)
            {
            }

            public override double Evaluate()
            {
                return Math.Tan(SubExpr.Evaluate());
            }
        }


        [FunctionSignature("asin")]
        public class AsinFuncExpression : UnaryExpression<double>
        {
            public AsinFuncExpression(Expression<double> subExpression)
                : base(subExpression)
            {
            }

            public override double Evaluate()
            {
                return Math.Asin(SubExpr.Evaluate());
            }
        }

        [FunctionSignature("acos")]
        public class AcosFuncExpression : UnaryExpression<double>
        {
            public AcosFuncExpression(Expression<double> subExpression)
                : base(subExpression)
            {
            }

            public override double Evaluate()
            {
                return Math.Acos(SubExpr.Evaluate());
            }
        }

        [FunctionSignature("atan")]
        public class AtanFuncExpression : UnaryExpression<double>
        {
            public AtanFuncExpression(Expression<double> subExpression)
                : base(subExpression)
            {
            }

            public override double Evaluate()
            {
                return Math.Atan(SubExpr.Evaluate());
            }
        }

        [FunctionSignature("atan2")]
        public class Atan2FuncExpression : BinaryExpression<double>
        {
            public Atan2FuncExpression(Expression<double> leftExpression, Expression<double> rightExpression)
                //: base(subExpression)
            {
                LeftExpression = leftExpression;
                RightExpression = rightExpression;
            }

            public override double Evaluate()
            {
                return Math.Atan2(LeftExpression.Evaluate(), RightExpression.Evaluate());
            }
        }

        [FunctionSignature("sinh", "real value")]
        public class SinhFuncExpression : UnaryExpression<double>
        {
            public SinhFuncExpression(Expression<double> subExpression)
                : base(subExpression)
            {
            }

            public override double Evaluate()
            {
                return Math.Sinh(SubExpr.Evaluate());
            }
        }

        [FunctionSignature("cosh", "real value")]
        public class CoshFuncExpression : UnaryExpression<double>
        {
            public CoshFuncExpression(Expression<double> subExpression)
                : base(subExpression)
            {
            }

            public override double Evaluate()
            {
                return Math.Cosh(SubExpr.Evaluate());
            }
        }

        [FunctionSignature("tanh", "real value")]
        public class TanhFuncExpression : UnaryExpression<double>
        {
            public TanhFuncExpression(Expression<double> subExpression)
                : base(subExpression)
            {
            }

            public override double Evaluate()
            {
                return Math.Tanh(SubExpr.Evaluate());
            }
        }


        [Constant("pi")]
        public class PiConstantExpression : ConstantExpression<double>
        {
            public override string Name
            {
                get { return "PI"; }
            }

            public override double Evaluate()
            {
                return Math.PI;
            }
        }

        [Constant("e")]
        public class EConstantExpression : ConstantExpression<double>
        {
            public override string Name
            {
                get { return "E"; }
            }

            public override double Evaluate()
            {
                return Math.E;
            }
        }

        #endregion
    }
}
