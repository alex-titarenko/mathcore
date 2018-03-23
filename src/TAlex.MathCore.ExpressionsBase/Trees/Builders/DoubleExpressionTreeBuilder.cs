using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

using System.ComponentModel;

using TAlex.MathCore.ExpressionEvaluation.Tokenize;
using TAlex.MathCore.ExpressionEvaluation.Trees.Metadata;


namespace TAlex.MathCore.ExpressionEvaluation.Trees.Builders
{
    public sealed class DoubleExpressionTreeBuilder : SimpleExpressionTreeBuilder<double>
    {
        #region Constructors

        public DoubleExpressionTreeBuilder()
        {
            ConstantFactory = new ConstantFlyweightFactory<double>()
            {
                Constants = new Dictionary<string, Type>()
                {
                    { "pi", typeof(PiConstantExpression) },
                    { "e", typeof(EConstantExpression) }
                }
            };

            FunctionFactory<double> factory = new FunctionFactory<double>();
            factory.Add("rnd", typeof(RandomFuncExpression));
            factory.Add("min", typeof(MinFuncExpression));
            factory.Add("max", typeof(MaxFuncExpression));
            factory.Add("abs", typeof(AbsFuncExpression));
            factory.Add("sqrt", typeof(SqrtFuncExpression));
            factory.Add("ln", typeof(LogFuncExpression));
            factory.Add("log", typeof(Log10FuncExpression));
            factory.Add("log", typeof(LogFreeBaseFuncExpression));
            factory.Add("exp", typeof(ExpFuncExpression));
            factory.Add("sin", typeof(SinFuncExpression));
            factory.Add("cos", typeof(CosFuncExpression));
            factory.Add("tan", typeof(TanFuncExpression));
            factory.Add("asin", typeof(AsinFuncExpression));
            factory.Add("acos", typeof(AcosFuncExpression));
            factory.Add("atan", typeof(AtanFuncExpression));
            factory.Add("atan2", typeof(Atan2FuncExpression));
            factory.Add("sinh", typeof(SinhFuncExpression));
            factory.Add("cosh", typeof(CoshFuncExpression));
            factory.Add("tanh", typeof(TanhFuncExpression));
            FunctionFactory = factory;
        }

        #endregion

        #region Methods

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
                return -SubExpression.Evaluate();
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
                : base(minValueExpression, maxValueExpression)
            {
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
                return Math.Abs(SubExpression.Evaluate());
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
                return Math.Sqrt(SubExpression.Evaluate());
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
                return Math.Log(SubExpression.Evaluate());
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
                return Math.Log10(SubExpression.Evaluate());
            }
        }

        [FunctionSignature("log", "real a", "real newBase")]
        public class LogFreeBaseFuncExpression : BinaryExpression<double>
        {
            public LogFreeBaseFuncExpression(Expression<double> leftExpression, Expression<double> rightExpression)
                : base(leftExpression, rightExpression)
            {
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
                return Math.Exp(SubExpression.Evaluate());
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
                return Math.Sin(SubExpression.Evaluate());
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
                return Math.Cos(SubExpression.Evaluate());
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
                return Math.Tan(SubExpression.Evaluate());
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
                return Math.Asin(SubExpression.Evaluate());
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
                return Math.Acos(SubExpression.Evaluate());
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
                return Math.Atan(SubExpression.Evaluate());
            }
        }

        [FunctionSignature("atan2", "real y", "real x")]
        public class Atan2FuncExpression : BinaryExpression<double>
        {
            public Atan2FuncExpression(Expression<double> leftExpression, Expression<double> rightExpression)
                : base(leftExpression, rightExpression)
            {
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
                return Math.Sinh(SubExpression.Evaluate());
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
                return Math.Cosh(SubExpression.Evaluate());
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
                return Math.Tanh(SubExpression.Evaluate());
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
