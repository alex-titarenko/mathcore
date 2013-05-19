using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAlex.MathCore.ExpressionEvaluation.Trees;
using TAlex.MathCore.ExpressionEvaluation.Trees.Metadata;
using TAlex.MathCore.NumericalAnalysis.EquationSolvers;


namespace TAlex.MathCore.ExpressionEvaluation.ComplexExpressions.Functions
{
    [DisplayName("Finding Root (Initial Guess)")]
    [Category(Categories.Solving)]
    [Description("Calculates the best approximation to the root of the nonlinear equation by initial guess using Muller's method.")]
    [FunctionSignature("root", "expression expr", "complex initGuess", "variable var")]
    [ExampleUsage("root(x^2 - 1, 3, x)", "1")]
    [ExampleUsage("root(x^2 - 2*x, 1.2, x)", "2")]
    public class InitialGuessFindingRootFuncExpression : TernaryExpression<Object>
    {
        public InitialGuessFindingRootFuncExpression(Expression<Object> exprExpression, Expression<Object> xExpression, Expression<Object> varExpression)
            : base(exprExpression, xExpression, varExpression)
        {
        }

        public override object Evaluate()
        {
            Func<Complex, Complex> targetFunc = FirstExpression.EvaluateAsFunction<Complex>(ThirdExpression);
            Complex initGuess = SecondExpression.EvaluateAsComplex();

            ComplexMullerEquationSolver solver = new ComplexMullerEquationSolver(targetFunc, initGuess);
            return solver.Solve();
        }
    }

    [DisplayName("Finding Root (Root Bracketing)")]
    [Category(Categories.Solving)]
    [Description("Calculates the best approximation to the root of the nonlinear equation by initial root bracketing using Brent's method.")]
    [FunctionSignature("root", "expression expr", "real lowerBound", "real upperBound", "variable var")]
    [ExampleUsage("root(x^2 - 1, 0, 3, x)", "1")]
    [ExampleUsage("root(x^2 - 2*x, 1, 8, x)", "2")]
    public class RootBracketingFindingRootFuncExpression : MultiaryExpression<Object>
    {
        public RootBracketingFindingRootFuncExpression(Expression<Object> exprExpression, Expression<Object> aExpression, Expression<Object> bExpression, Expression<Object> varExpression)
            : base(exprExpression, aExpression, bExpression, varExpression)
        {
        }

        public override object Evaluate()
        {
            Func<double, double> targetFunc = Expressions[0].EvaluateAsDoubleFunction(Expressions[3]);
            double a = Expressions[1].EvaluateAsReal();
            double b = Expressions[2].EvaluateAsReal();

            BrentEquationSolver solver = new BrentEquationSolver(targetFunc, a, b);
            return (Complex)solver.Solve();
        }
    }
}
