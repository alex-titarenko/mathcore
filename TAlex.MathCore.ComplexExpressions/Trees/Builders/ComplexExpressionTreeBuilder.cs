using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAlex.MathCore.LinearAlgebra;


namespace TAlex.MathCore.ExpressionEvaluation.Trees.Builders
{
    public class ComplexExpressionTreeBuilder : SimpleExpressionTreeBuilder<Object>
    {
        protected override object GetDefaultVariableValue()
        {
            return null;
        }

        protected override BinaryExpression<object> CreateAddExpression()
        {
            return new AddComplexExpression();
        }

        protected override BinaryExpression<object> CreateSubExpression()
        {
            throw new NotImplementedException();
        }

        protected override BinaryExpression<object> CreateMultExpression()
        {
            throw new NotImplementedException();
        }

        protected override BinaryExpression<object> CreateDivExpression()
        {
            throw new NotImplementedException();
        }

        protected override BinaryExpression<object> CreatePowExpression()
        {
            throw new NotImplementedException();
        }

        protected override UnaryExpression<object> CreateUnaryMinusExpression(Expression<object> subExpression)
        {
            throw new NotImplementedException();
        }

        protected override ScalarExpression<object> ParseScalarValue(string s)
        {
            object scalar;

            if (s.EndsWith("i") || s.EndsWith("j"))
                scalar = Complex.Parse(s, CultureInfo.InvariantCulture);
            else
                scalar = (Complex)ConvertEx.ToDouble(s, CultureInfo.InvariantCulture);

            return new ScalarExpression<Object>(scalar);
        }


        #region Nested Types

        private class AddComplexExpression : AddExpression<Object>
        {
            public override Object Evaluate()
            {
                object left = LeftExpression.Evaluate();
                object right = RightExpression.Evaluate();

                if (left is Complex)
                {
                    if (right is Complex) return (Complex)left + (Complex)right;
                    else if (right is CMatrix) return (Complex)left + (CMatrix)right;
                    else throw new ArgumentException("Wrong type arguments operation '+'.");
                }
                else if (left is CMatrix)
                {
                    if (right is Complex) return (CMatrix)left + (Complex)right;
                    else if (right is CMatrix) return (CMatrix)left + (CMatrix)right;
                    else throw new ArgumentException("Wrong type arguments operation '+'.");
                }
                else
                {
                    throw new ArgumentException("Wrong type arguments operation '+'.");
                }
            }
        }

        #endregion
    }
}
