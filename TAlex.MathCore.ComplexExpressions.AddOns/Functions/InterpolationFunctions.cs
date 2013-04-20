using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using TAlex.MathCore.ExpressionEvaluation.Trees;
using TAlex.MathCore.ExpressionEvaluation.Trees.Metadata;
using TAlex.MathCore.LinearAlgebra;
using TAlex.MathCore.NumericalAnalysis.Interpolation;


namespace TAlex.MathCore.ExpressionEvaluation.ComplexExpressions.Functions
{
    [DisplayName("Linear")]
    [Category(Categories.Interpolation)]
    [Description("Calculates the interpolated value at specified value using linear interpolation.")]
    [FunctionSignature("linterp", "real vector xValues", "real vector yValues", "real x")]
    [ExampleUsage("linterp({1; 2; 5; 8}, {3; 12; -6; 0}, 7)", "-2")]
    public class LinearInterpolationFuncExpression : TernaryExpression<Object>
    {
        public LinearInterpolationFuncExpression(Expression<Object> xValuesExpression, Expression<Object> yValuesExpression, Expression<Object> xExpression)
            : base(xValuesExpression, yValuesExpression, xExpression)
        {
        }

        public override object Evaluate()
        {
            IList<double> xValues = FirstExpression.EvaluateAsDoubleArray();
            IList<double> yValues = SecondExpression.EvaluateAsDoubleArray();
            double x = ThirdExpression.EvaluateAsDouble();
            
            return (Complex)(new LinearInterpolator(xValues, yValues).Interpolate(x));
        }
    }


    [DisplayName("Polynomial")]
    [Category(Categories.Interpolation)]
    [Description("Calculates the interpolated value at specified value using Newton's polynomial interpolation.")]
    [FunctionSignature("interp", "real vector xValues", "real vector yValues", "real x")]
    [ExampleUsage("interp({1; 2; 5; 8}, {3; 12; -6; 0}, 5.5)", "-9.84375")]
    public class PolynomialInterpolationFuncExpression : TernaryExpression<Object>
    {
        public PolynomialInterpolationFuncExpression(Expression<Object> xValuesExpression, Expression<Object> yValuesExpression, Expression<Object> xExpression)
            : base(xValuesExpression, yValuesExpression, xExpression)
        {
        }

        public override object Evaluate()
        {
            IList<double> xValues = FirstExpression.EvaluateAsDoubleArray();
            IList<double> yValues = SecondExpression.EvaluateAsDoubleArray();
            double x = ThirdExpression.EvaluateAsDouble();

            return (Complex)(new NewtonPolynomialInterpolator(xValues, yValues).Interpolate(x));
        }
    }
}
