using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAlex.MathCore.ExpressionEvaluation.Trees;
using TAlex.MathCore.LinearAlgebra;

namespace TAlex.MathCore.ExpressionEvaluation.ComplexExpressions
{
    public static class ExpressionExtensions
    {
        #region Extensions

        public static int EvaluateAsInt32(this Expression<Object> expression)
        {
            return AsInt32(expression.Evaluate());
        }

        public static double EvaluateAsDouble(this Expression<Object> expression)
        {
            return AsDouble(expression.Evaluate());
        }

        public static Complex EvaluateAsComplex(this Expression<Object> expression)
        {
            return AsComplex(expression.Evaluate());
        }

        //public static CMatrix EvaluateAsCMatrix(this Expression<Object> expression)
        //{
        //}

        public static IList<double> EvaluateAsDoubleArray(this Expression<Object> expression)
        {
            return AsDoubleArray(expression.Evaluate());
        }

        #endregion


        public static int AsInt32(object o)
        {
            if (o is Int32) return (int)o;
            else if (o is Complex) return AsInt32((Complex)o);
            throw new ArgumentException(String.Format(Properties.Resources.EXC_VALUE_NOT_INTEGER, o));
        }

        public static double AsDouble(object o)
        {
            if (o is Double) return (double)o;
            else if (o is Complex) return AsDouble((Complex)o);
            throw new ArgumentException(String.Format(Properties.Resources.EXC_VALUE_NOT_REAL, o));
        }

        private static Complex AsComplex(object o)
        {
            if (o is Complex) return (Complex)o;
            throw new ArgumentException(String.Format(Properties.Resources.EXC_VALUE_NOT_COMPLEX_NUMBER, o));
        }

        private static double[] AsDoubleArray(object o)
        {
            if (o is CMatrix) return AsDoubleArray((CMatrix)o);
            throw new ArgumentException(String.Format(Properties.Resources.EXC_INVALID_OBJECT_TYPE, "double array", o.GetType()));
        }

        

        private static int AsInt32(Complex c)
        {
            if (c.IsReal && ExMath.IsInt32(c.Re))
                return (int)c.Re;
            throw new ArgumentException(String.Format(Properties.Resources.EXC_VALUE_NOT_INTEGER, c));
        }

        private static double AsDouble(Complex c)
        {
            if (c.IsReal) return c.Re;
            throw new ArgumentException(String.Format(Properties.Resources.EXC_VALUE_NOT_REAL, c));
        }

        private static double[] AsDoubleArray(this CMatrix matrix)
        {
            if (matrix.IsVector && matrix.IsReal)
            {
                return matrix.Select(x => (double)x).ToArray();
            }
            else
            {
                //TODO: Need to correct.
                throw new ArgumentException();
            }
        }
    }
}
