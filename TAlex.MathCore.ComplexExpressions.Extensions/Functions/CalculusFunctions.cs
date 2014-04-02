using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAlex.MathCore.ExpressionEvaluation.Trees;
using TAlex.MathCore.ExpressionEvaluation.Trees.Builders;
using TAlex.MathCore.ExpressionEvaluation.Trees.Metadata;
using TAlex.MathCore.NumericalAnalysis;
using TAlex.MathCore.NumericalAnalysis.NumericalIntegration;
using TAlex.MathCore.Performance;


namespace TAlex.MathCore.ExpressionEvaluation.ComplexExpressions.Functions
{
    [DisplayName("Integration")]
    [Category(Categories.Calculus)]
    [Section(Sections.Integration)]
    [Description("Calculates the numerical value of the definite integral complex function of one variable using adaptive method.")]
    [FunctionSignature("integ", "expression expr", "real a", "real b", "variable var")]
    [ExampleUsage("integ(sin(x), 0, pi, x)", "2")]
    [ExampleUsage("integ(e^x, -inf, 0, x)", "1")]
    [ExampleUsage("integ(x, 0, 10, x)", "50")]
    public class IntegrationFuncExpression : MultiaryExpression<Object>
    {
        protected ComplexIntegrator Integrator;

        public IntegrationFuncExpression(Expression<Object> exprExpression, Expression<Object> aExpression, Expression<Object> bExpression, Expression<Object> varExpression)
            : base(exprExpression, aExpression, bExpression, varExpression)
        {
            Integrator = new ComplexAdaptiveIntegrator();
        }

        public override object Evaluate()
        {
            Func<Complex, Complex> targetFunc = Expressions[0].EvaluateAsFunction<Complex>(Expressions[3]);
            double a = Expressions[1].EvaluateAsReal();
            double b = Expressions[2].EvaluateAsReal();

            return Integrator.Integrate(targetFunc, a, b);
        }
    }



    [DisplayName("1st derivation")]
    [Category(Categories.Calculus)]
    [Section(Sections.Derivation)]
    [Description("Calculates the value of the central derivative of the first order.")]
    [FunctionSignature("derive", "expression expr", "complex x", "variable var")]
    [ExampleUsage("derive(sin(x), pi/2, x)", "0")]
    public class FirstDerivationFuncExpression : TernaryExpression<Object>
    {
        public FirstDerivationFuncExpression(Expression<Object> exprExpression, Expression<Object> xExpression, Expression<Object> varExpression)
            : base(exprExpression, xExpression, varExpression)
        {
        }

        public override object Evaluate()
        {
            Func<Complex, Complex> targetFunc = FirstExpression.EvaluateAsFunction<Complex>(ThirdExpression);
            Complex x = SecondExpression.EvaluateAsComplex();

            return NumericalDerivation.FirstDerivative(targetFunc, x);
        }
    }

    [DisplayName("2nd derivation")]
    [Category(Categories.Calculus)]
    [Section(Sections.Derivation)]
    [Description("Calculates the value of the central derivative of the second order.")]
    [FunctionSignature("derive2", "expression expr", "complex x", "variable var")]
    [ExampleUsage("derive2(e^x, 1, x)", "2.71828182845911")]
    public class SecondDerivationFuncExpression : TernaryExpression<Object>
    {
        public SecondDerivationFuncExpression(Expression<Object> exprExpression, Expression<Object> xExpression, Expression<Object> varExpression)
            : base(exprExpression, xExpression, varExpression)
        {
        }

        public override object Evaluate()
        {
            Func<Complex, Complex> targetFunc = FirstExpression.EvaluateAsFunction<Complex>(ThirdExpression);
            Complex x = SecondExpression.EvaluateAsComplex();

            return NumericalDerivation.SecondDerivative(targetFunc, x);
        }
    }

    [DisplayName("3rd derivation")]
    [Category(Categories.Calculus)]
    [Section(Sections.Derivation)]
    [Description("Calculates the value of the central derivative of the third order.")]
    [FunctionSignature("derive3", "expression expr", "complex x", "variable var")]
    [ExampleUsage("derive3(1 + x^2, 1, x)", "0")]
    public class ThirdDerivationFuncExpression : TernaryExpression<Object>
    {
        public ThirdDerivationFuncExpression(Expression<Object> exprExpression, Expression<Object> xExpression, Expression<Object> varExpression)
            : base(exprExpression, xExpression, varExpression)
        {
        }

        public override object Evaluate()
        {
            Func<Complex, Complex> targetFunc = FirstExpression.EvaluateAsFunction<Complex>(ThirdExpression);
            Complex x = SecondExpression.EvaluateAsComplex();

            return NumericalDerivation.ThirdDerivative(targetFunc, x);
        }
    }

    [DisplayName("4th derivation")]
    [Category(Categories.Calculus)]
    [Section(Sections.Derivation)]
    [Description("Calculates the value of the central derivative of the fourth order.")]
    [FunctionSignature("derive4", "expression expr", "complex x", "variable var")]
    [ExampleUsage("derive4(sin(x^2), 2, x)", "-59.1602336374673")]
    public class FourthDerivationFuncExpression : TernaryExpression<Object>
    {
        public FourthDerivationFuncExpression(Expression<Object> exprExpression, Expression<Object> xExpression, Expression<Object> varExpression)
            : base(exprExpression, xExpression, varExpression)
        {
        }

        public override object Evaluate()
        {
            Func<Complex, Complex> targetFunc = FirstExpression.EvaluateAsFunction<Complex>(ThirdExpression);
            Complex x = SecondExpression.EvaluateAsComplex();

            return NumericalDerivation.FourthDerivative(targetFunc, x);
        }
    }



    [DisplayName("Summation of a sequance")]
    [Category(Categories.Calculus)]
    [Section(Sections.Sequence)]
    [Description("Calculates the sum of a sequence using initial value m and end value n.")]
    [FunctionSignature("sums", "expression expr", "integer m", "integer n", "variable var")]
    [ExampleUsage("sums(x^2/2, 2, 3, x)", "6.5")]
    [ExampleUsage("sums(sin(x + 2i), 0, 10, x)", "5.30916680950373 - 1.51402470396589i")]
    public class SummationSequanceFuncExpression : MultiaryExpression<Object>
    {
        public SummationSequanceFuncExpression(Expression<Object> exprExpression, Expression<Object> mExpression, Expression<Object> nExpression, Expression<Object> varExpression)
            : base(exprExpression, mExpression, nExpression, varExpression)
        {
        }

        public override object Evaluate()
        {
            Func<Complex, Complex> targetFunc = Expressions[0].EvaluateAsFunction<Complex>(Expressions[3]);
            int m = Expressions[1].EvaluateAsInt32();
            int n = Expressions[2].EvaluateAsInt32();
            PerformanceManager.Current.EnsureAcceptableIterationCount(Math.Abs(n - m));

            return Sequence.Summation(targetFunc, m, n);
        }
    }

    [DisplayName("Product of a sequance")]
    [Category(Categories.Calculus)]
    [Section(Sections.Sequence)]
    [Description("Calculates the product of a sequence using initial value m and end value n.")]
    [FunctionSignature("prods", "expression expr", "integer m", "integer n", "variable var")]
    [ExampleUsage("prods(x^2 / 2, 2, 3, x)", "9")]
    [ExampleUsage("prods(x * 2i, 0, 10, x)", "0")]
    public class ProductSequanceFuncExpression : MultiaryExpression<Object>
    {
        public ProductSequanceFuncExpression(Expression<Object> exprExpression, Expression<Object> mExpression, Expression<Object> nExpression, Expression<Object> varExpression)
            : base(exprExpression, mExpression, nExpression, varExpression)
        {
        }

        public override object Evaluate()
        {
            Func<Complex, Complex> targetFunc = Expressions[0].EvaluateAsFunction<Complex>(Expressions[3]);
            int m = Expressions[1].EvaluateAsInt32();
            int n = Expressions[2].EvaluateAsInt32();
            PerformanceManager.Current.EnsureAcceptableIterationCount(Math.Abs(n - m));

            return Sequence.Product(targetFunc, m, n);
        }
    }
}
