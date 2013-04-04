using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAlex.MathCore.ExpressionEvaluation.Tokenize;
using TAlex.MathCore.LinearAlgebra;


namespace TAlex.MathCore.ExpressionEvaluation.Trees.Builders
{
    public class ComplexExpressionTreeBuilder : SimpleExpressionTreeBuilder<Object>
    {
        public ComplexExpressionTreeBuilder()
        {
            Tokenizer = new ComplexExpressionTokenizer();
        }


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
            return new SubComplexExpression();
        }

        protected override BinaryExpression<object> CreateMultExpression()
        {
            return new MultComplexExpression();
        }

        protected override BinaryExpression<object> CreateDivExpression()
        {
            return new DivComplexExpression();
        }

        protected override BinaryExpression<object> CreatePowExpression()
        {
            return new PowComplexExpression();
        }

        protected override UnaryExpression<object> CreateUnaryMinusExpression(Expression<object> subExpression)
        {
            return new UnaryMinusComplexExpression(subExpression);
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

        private class SubComplexExpression : SubExpression<Object>
        {
            public override Object Evaluate()
            {
                object left = LeftExpression.Evaluate();
                object right = RightExpression.Evaluate();

                if (left is Complex)
                {
                    if (right is Complex) return (Complex)left - (Complex)right;
                    else if (right is CMatrix) return (Complex)left - (CMatrix)right;
                    else throw new ArgumentException("Wrong type arguments operation '-'.");
                }
                else if (left is CMatrix)
                {
                    if (right is Complex) return (CMatrix)left - (Complex)right;
                    else if (right is CMatrix) return (CMatrix)left - (CMatrix)right;
                    else throw new ArgumentException("Wrong type arguments operation '-'.");
                }
                else
                {
                    throw new ArgumentException("Wrong type arguments operation '-'.");
                }
            }
        }

        private class MultComplexExpression : MultExpression<Object>
        {
            public override Object Evaluate()
            {
                object left = LeftExpression.Evaluate();
                object right = RightExpression.Evaluate();

                if (left is Complex)
                {
                    if (right is Complex) return (Complex)left * (Complex)right;
                    else if (right is CMatrix) return (Complex)left * (CMatrix)right;
                    else throw new ArgumentException("Wrong type arguments operation '*'.");
                }
                else if (left is CMatrix)
                {
                    if (right is Complex) return (CMatrix)left * (Complex)right;
                    else if (right is CMatrix) return (CMatrix)left * (CMatrix)right;
                    else throw new ArgumentException("Wrong type arguments operation '*'.");
                }
                else
                {
                    throw new ArgumentException("Wrong type arguments operation '*'.");
                }
            }
        }

        private class DivComplexExpression : DivExpression<Object>
        {
            public override Object Evaluate()
            {
                object left = LeftExpression.Evaluate();
                object right = RightExpression.Evaluate();

                if (left is Complex)
                {
                    if (right is Complex) return (Complex)left / (Complex)right;
                    else if (right is CMatrix) return (Complex)left / (CMatrix)right;
                    else throw new ArgumentException("Wrong type arguments operation '/'.");
                }
                else if (left is CMatrix)
                {
                    if (right is Complex) return (CMatrix)left / (Complex)right;
                    else if (right is CMatrix) return (CMatrix)left / (CMatrix)right;
                    else throw new ArgumentException("Wrong type arguments operation '/'.");
                }
                else
                {
                    throw new ArgumentException("Wrong type arguments operation '/'.");
                }
            }
        }

        private class PowComplexExpression : PowExpression<Object>
        {
            public override Object Evaluate()
            {
                object left = LeftExpression.Evaluate();
                object right = RightExpression.Evaluate();

                if (left is Complex)
                {
                    if (right is Complex)
                        return EvaluateComplexComplex((Complex)left, (Complex)right);
                    else
                        throw new ArgumentException("Wrong type arguments operation '^'.");
                }
                else if (left is CMatrix)
                {
                    if (right is Complex)
                        return EvaluateCMatrixComplex((CMatrix)left, (Complex)right);
                    else
                        throw new ArgumentException("Wrong type arguments operation '^'.");
                }
                else
                {
                    throw new ArgumentException("Wrong type arguments operation '^'.");
                }
            }

            private Complex EvaluateComplexComplex(Complex left, Complex right)
            {
                if (right.IsReal)
                {
                    if (ExMath.IsInt32(right.Re))
                    {
                        return Complex.Pow((Complex)left, (int)right.Re);
                    }
                    else
                    {
                        return Complex.Pow((Complex)left, right.Re);
                    }
                }
                else
                {
                    return Complex.Pow((Complex)left, right);
                }
            }

            private CMatrix EvaluateCMatrixComplex(CMatrix left, Complex right)
            {
                if (left.IsVector)
                {
                    return CMatrix.Pow(left, right);
                }
                else if (left.IsSquare)
                {
                    return CMatrix.Pow(left, Convert.ToInt32(right));
                }
                else
                {
                    throw new ArgumentException("The matrix must be square or column vector.");
                }
            }
        }

        private class UnaryMinusComplexExpression : UnaryMinusExpression<Object>
        {
            public UnaryMinusComplexExpression(Expression<Object> subExpression)
                : base(subExpression)
            {
            }

            public override object Evaluate()
            {
                object value = SubExpr.Evaluate();

                if (value is Complex)
                    return Complex.Negate((Complex)value);
                else if (value is CMatrix)
                    return CMatrix.Negate((CMatrix)value);
                else
                    throw new ArgumentException("Wrong type argument operation '-'.");
            }
        }

        #endregion
    }
}
