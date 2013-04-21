using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAlex.MathCore.ExpressionEvaluation.Trees;
using TAlex.MathCore.ExpressionEvaluation.Trees.Builders;
using TAlex.MathCore.ExpressionEvaluation.Trees.Metadata;
using TAlex.MathCore.NumericalAnalysis.NumericalIntegration;

namespace TAlex.MathCore.ExpressionEvaluation.ComplexExpressions.Functions
{
    [DisplayName("Integration")]
    [Category(Categories.Calculus)]
    [Description("Calculates the numerical value of the definite integral complex function of one variable using adaptive method.")]
    [FunctionSignature("integ", "expression expr", "real a", "real b", "variable var")]
    [ExampleUsage("integ(sin(x), 0, pi, x)", "2")]
    public class IntegrationFuncExpression : MultiaryExpression<Object>
    {
        protected ComplexIntegrator Integrator;
        protected Func<Complex, Complex> TargetFunction;

        public IntegrationFuncExpression(Expression<Object> exprExpression, Expression<Object> aExpression, Expression<Object> bExpression, Expression<Object> varExpression)
            : base(exprExpression, aExpression, bExpression, varExpression)
        {
            Integrator = new ComplexAdaptiveIntegrator();

            Func<Object, Object> tempFunc = ParametricFunctionCreator.CreateOneParametricFunction<Object>(exprExpression, ((VariableExpression<Object>)varExpression).VariableName);

            if (varExpression is VariableExpression<Object>)
                TargetFunction = (c) => (Complex)tempFunc(c);
        }

        public override object Evaluate()
        {
            double a = Expressions[1].EvaluateAsDouble();
            double b = Expressions[2].EvaluateAsDouble();

            return Integrator.Integrate(TargetFunction, a, b);
        }
    }
}
