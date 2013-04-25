using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAlex.MathCore.ExpressionEvaluation.Trees;
using TAlex.MathCore.ExpressionEvaluation.Trees.Builders;
using TAlex.MathCore.LinearAlgebra;


namespace TAlex.MathCore.ExpressionEvaluation.ComplexExpressions
{
    public static class ExpressionExtensions
    {
        #region Extensions

        public static int EvaluateAsInt32(this Expression<Object> expression)
        {
            Object o = expression.Evaluate();

            if (o is Int32) return (int)o;
            else if (o is Complex) return AsInt32((Complex)o);
            throw new ArgumentException(String.Format(Properties.Resources.EXC_VALUE_NOT_INTEGER, o));
        }

        public static double EvaluateAsDouble(this Expression<Object> expression)
        {
            Object o = expression.Evaluate();

            if (o is Double) return (double)o;
            else if (o is Complex) return AsDouble((Complex)o);
            throw new ArgumentException(String.Format(Properties.Resources.EXC_VALUE_NOT_REAL, o));
        }

        public static Complex EvaluateAsComplex(this Expression<Object> expression)
        {
            Object o = expression.Evaluate();

            if (o is Complex) return (Complex)o;
            throw new ArgumentException(String.Format(Properties.Resources.EXC_VALUE_NOT_COMPLEX_NUMBER, o));
        }

        public static CMatrix EvaluateAsCMatrix(this Expression<Object> expression)
        {
            Object o = expression.Evaluate();

            if (o is CMatrix) return (CMatrix)o;
            throw new ArgumentException(String.Format(Properties.Resources.EXC_VALUE_NOT_COMPLEX_MATRIX, o));
        }

        public static CPolynomial EvaluateAsCPolynomial(this Expression<Object> expression)
        {
            CMatrix matrix = EvaluateAsCMatrix(expression);

            if (matrix.IsVector)
                return new CPolynomial(matrix);
            else
                // TODO
                throw new ArgumentException();
        }

        public static IList<double> EvaluateAsDoubleArray(this Expression<Object> expression)
        {
            Object o = expression.Evaluate();

            if (o is CMatrix)
            {
                return AsDoubleArray((CMatrix)o);
            }
            throw new ArgumentException(String.Format(Properties.Resources.EXC_INVALID_OBJECT_TYPE, "real array", o.GetType().Name));
        }

        public static IList<double> EvaluateAsExpandableDoubleArray(this Expression<Object> expression)
        {
            Object o = expression.Evaluate();

            if (o is CMatrix)
            {
                CMatrix matrix = (CMatrix)o;
                if (matrix.IsReal)
                {
                    return matrix.Select(x => (double)x).ToArray();
                }
            }
            throw new ArgumentException(String.Format(Properties.Resources.EXC_INVALID_OBJECT_TYPE, "real matrix", o.GetType().Name));
        }

        public static IList<Complex> EvaluateAsExpandableComplexArray(this Expression<Object> expression)
        {
            return new List<Complex>(EvaluateAsCMatrix(expression));
        }

        public static IList<Complex> EvaluateAsComplexVector(this Expression<Object> expression)
        {
            CMatrix matrix = EvaluateAsCMatrix(expression);
            if (matrix.IsVector) return new List<Complex>(matrix);

            throw new ArgumentException(String.Format(Properties.Resources.EXC_INVALID_OBJECT_TYPE, "complex vector", matrix.GetType().Name));
        }


        public static Func<T, T> EvaluateAsFunction<T>(this Expression<Object> expression, Expression<Object> var)
        {
            VariableExpression<Object> varExpr = var as VariableExpression<Object>;
            if (varExpr == null)
            {
                // TODO:
                throw new ArgumentException();
            }

            Func<Object, Object> tempFunc = ParametricFunctionCreator.CreateOneParametricFunction<Object>(expression, varExpr.VariableName);
            return x => (T)tempFunc(x);
        }

        public static Func<double, double> EvaluateAsDoubleFunction(this Expression<Object> expression, Expression<Object> var)
        {
            VariableExpression<Object> varExpr = var as VariableExpression<Object>;
            if (varExpr == null)
            {
                // TODO:
                throw new ArgumentException();
            }

            Func<Object, Object> tempFunc = ParametricFunctionCreator.CreateOneParametricFunction<Object>(expression, varExpr.VariableName);
            return x => (double)(Complex)tempFunc((Complex)x);
        }

        #endregion

        #region Helpers

        public static int AsInt32(Complex c)
        {
            if (c.IsReal && ExMath.IsInt32(c.Re))
                return (int)c.Re;
            throw new ArgumentException(String.Format(Properties.Resources.EXC_VALUE_NOT_INTEGER, c));
        }

        public static IList<double> AsDoubleArray(CMatrix matrix)
        {
            if (matrix.IsVector && matrix.IsReal)
                return matrix.Select(x => (double)x).ToArray();
            else
                throw new ArgumentException(String.Format(Properties.Resources.EXC_INVALID_OBJECT_TYPE, "real array", matrix.GetType().Name));
        }

        private static double AsDouble(Complex c)
        {
            if (c.IsReal) return c.Re;
            throw new ArgumentException(String.Format(Properties.Resources.EXC_VALUE_NOT_REAL, c));
        }

        #endregion
    }
}
