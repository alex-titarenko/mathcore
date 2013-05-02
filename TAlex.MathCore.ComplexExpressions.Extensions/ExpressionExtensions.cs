using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            else if (o is CMatrix) throw ExceptionHelper.ThrowInvalidArgumentType("integer", "complex matrix");
            throw ExceptionHelper.ThrowInvalidArgumentType("integer", o);
        }

        public static long EvaluateAsInt64(this Expression<Object> expression)
        {
            Object o = expression.Evaluate();

            if (o is Int32) return (long)o;
            else if (o is Complex) return AsInt64((Complex)o);
            else if (o is CMatrix) throw ExceptionHelper.ThrowInvalidArgumentType("integer", "complex matrix");
            throw ExceptionHelper.ThrowInvalidArgumentType("integer", o);
        }

        public static double EvaluateAsReal(this Expression<Object> expression)
        {
            Object o = expression.Evaluate();

            if (o is Double) return (double)o;
            else if (o is Complex) return AsDouble((Complex)o);
            else if (o is CMatrix) throw ExceptionHelper.ThrowInvalidArgumentType("real", "complex matrix");
            throw ExceptionHelper.ThrowInvalidArgumentType("real", o);
        }

        public static Complex EvaluateAsComplex(this Expression<Object> expression)
        {
            Object o = expression.Evaluate();

            if (o is Complex) return (Complex)o;
            else if (o is CMatrix) throw ExceptionHelper.ThrowInvalidArgumentType("complex", "complex matrix");
            throw ExceptionHelper.ThrowInvalidArgumentType("complex", o);
        }

        public static CMatrix EvaluateAsCMatrix(this Expression<Object> expression)
        {
            Object o = expression.Evaluate();

            if (o is CMatrix) return (CMatrix)o;
            else if (o is Complex) throw ExceptionHelper.ThrowInvalidArgumentType("complex matrix", "complex");
            throw ExceptionHelper.ThrowInvalidArgumentType("complex matrix", o);
        }

        public static CPolynomial EvaluateAsCPolynomial(this Expression<Object> expression)
        {
            CMatrix matrix = EvaluateAsCMatrix(expression);

            if (matrix.IsVector) return new CPolynomial(matrix);

            throw ExceptionHelper.ThrowInvalidArgumentType("complex vector", "complex matrix");
        }

        public static IList<double> EvaluateAsRealVector(this Expression<Object> expression)
        {
            Object o = expression.Evaluate();

            if (o is CMatrix) return AsRealVector((CMatrix)o);
            else if (o is Complex) throw ExceptionHelper.ThrowInvalidArgumentType("real vector", "complex");

            throw ExceptionHelper.ThrowInvalidArgumentType("real array", o);
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
                throw ExceptionHelper.ThrowInvalidArgumentType("real matrix", "complex matrix");
            }
            else if (o is Complex)
            {
                throw ExceptionHelper.ThrowInvalidArgumentType("real matrix", "complex");
            }
            throw ExceptionHelper.ThrowInvalidArgumentType("real matrix", o);
        }

        public static IList<Complex> EvaluateAsExpandableComplexArray(this Expression<Object> expression)
        {
            return new List<Complex>(EvaluateAsCMatrix(expression));
        }

        public static IList<Complex> EvaluateAsComplexVector(this Expression<Object> expression)
        {
            CMatrix matrix = EvaluateAsCMatrix(expression);
            if (matrix.IsVector) return new List<Complex>(matrix);

            throw ExceptionHelper.ThrowInvalidArgumentType("complex vector", "complex matrix");
        }

        public static Func<T, T> EvaluateAsFunction<T>(this Expression<Object> expression, Expression<Object> var)
        {
            VariableExpression<Object> varExpr = var as VariableExpression<Object>;
            if (varExpr == null)
            {
                throw ExceptionHelper.ThrowInvalidArgumentType("variable", var.Evaluate());
            }

            Func<Object, Object> tempFunc = ParametricFunctionCreator.CreateOneParametricFunction<Object>(expression, varExpr.VariableName);
            return x => (T)tempFunc(x);
        }

        public static Func<double, double> EvaluateAsDoubleFunction(this Expression<Object> expression, Expression<Object> var)
        {
            VariableExpression<Object> varExpr = var as VariableExpression<Object>;
            if (varExpr == null)
            {
                throw ExceptionHelper.ThrowInvalidArgumentType("variable", var.Evaluate());
            }

            Func<Object, Object> tempFunc = ParametricFunctionCreator.CreateOneParametricFunction<Object>(expression, varExpr.VariableName);
            return x => { Complex c = (Complex)tempFunc((Complex)x); return c.IsReal ? c.Re : Double.NaN; };
        }

        #endregion

        #region Helpers

        public static int AsInt32(Complex c)
        {
            if (c.IsReal)
            {
                if (ExMath.IsInt32(c.Re)) return (int)c.Re;
                throw ExceptionHelper.ThrowInvalidArgumentType("integer", "real");
            }
            throw ExceptionHelper.ThrowInvalidArgumentType("integer", "complex");
        }

        public static long AsInt64(Complex c)
        {
            if (c.IsReal)
            {
                if (ExMath.IsInt64(c.Re)) return (long)c.Re;
                throw ExceptionHelper.ThrowInvalidArgumentType("integer", "real");
            }
            throw ExceptionHelper.ThrowInvalidArgumentType("integer", "complex");
        }

        public static double AsDouble(Complex c)
        {
            if (c.IsReal) return c.Re;
            throw ExceptionHelper.ThrowInvalidArgumentType("real", "complex");
        }

        public static IList<double> AsRealVector(CMatrix matrix)
        {
            if (matrix.IsVector)
            {
                if (matrix.IsReal) return matrix.Select(x => (double)x).ToArray();
                throw ExceptionHelper.ThrowInvalidArgumentType("real vector", "complex vector");
            }
            throw ExceptionHelper.ThrowInvalidArgumentType("real vector", "complex matrix");
        }

        #endregion
    }
}
