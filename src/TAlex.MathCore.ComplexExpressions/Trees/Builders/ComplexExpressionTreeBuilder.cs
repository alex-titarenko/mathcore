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

            UnaryOperatorHandlers.Add("{", HandleMatrixBracket);
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


        private Expression<Object> HandleMatrixBracket(IEnumerator<Token> tokens, IDictionary<string, VariableExpression<Object>> vars)
        {
            Expression<Object> value = AddSub(tokens, vars);

            if (tokens.Current.Value == "}")
            {
                tokens.MoveNext();
                return new CMatrixExpression(1, value);
            }
            else if (tokens.Current.Value == "," || tokens.Current.Value == ";")
            {
                int? stride = null;
                int currLineLenght = 1;
                List<Expression<Object>> m = new List<Expression<Object>>();
                m.Add(value);

                while (tokens.Current.Value != "}" && (tokens.Current.Value == "," || tokens.Current.Value == ";"))
                {
                    if (tokens.Current.Value == ";")
                    {
                        if (!stride.HasValue) stride = currLineLenght;
                        else if (stride != currLineLenght) throw new MatrixSizeMismatchException();
                        currLineLenght = 1;
                    }
                    else
                    {
                        currLineLenght++;
                        if (currLineLenght > stride) throw new MatrixSizeMismatchException();
                    }

                    value = AddSub(tokens, vars);
                    m.Add(value);
                }

                if (tokens.Current.Value != "}")
                    throw new SyntaxException("\"}\" expected.");

                tokens.MoveNext();
                return new CMatrixExpression(stride ?? m.Count, m.ToArray());
            }
            else
            {
                throw new SyntaxException("\"}\" expected.");
            }
        }

        private static Exception ThrowWrongArgumentType(string op)
        {
            return new ArgumentException(String.Format(Properties.Resources.EXC_WRONG_TYPE_ARGUMENTS_OPERATION, op));
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
                    else throw ThrowWrongArgumentType("+");
                }
                else if (left is CMatrix)
                {
                    if (right is Complex) return (CMatrix)left + (Complex)right;
                    else if (right is CMatrix) return (CMatrix)left + (CMatrix)right;
                    else throw ThrowWrongArgumentType("+");
                }
                else
                {
                    throw ThrowWrongArgumentType("+");
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
                    else throw ThrowWrongArgumentType("-");
                }
                else if (left is CMatrix)
                {
                    if (right is Complex) return (CMatrix)left - (Complex)right;
                    else if (right is CMatrix) return (CMatrix)left - (CMatrix)right;
                    else throw ThrowWrongArgumentType("-");
                }
                else
                {
                    throw ThrowWrongArgumentType("-");
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
                    else throw ThrowWrongArgumentType("*");
                }
                else if (left is CMatrix)
                {
                    if (right is Complex) return (CMatrix)left * (Complex)right;
                    else if (right is CMatrix) return (CMatrix)left * (CMatrix)right;
                    else throw ThrowWrongArgumentType("*");
                }
                else
                {
                    throw ThrowWrongArgumentType("*");
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
                    else throw ThrowWrongArgumentType("/");
                }
                else if (left is CMatrix)
                {
                    if (right is Complex) return (CMatrix)left / (Complex)right;
                    else if (right is CMatrix) return (CMatrix)left / (CMatrix)right;
                    else throw ThrowWrongArgumentType("/");
                }
                else
                {
                    throw ThrowWrongArgumentType("/");
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
                        throw ThrowWrongArgumentType("^");
                }
                else if (left is CMatrix)
                {
                    if (right is Complex)
                        return EvaluateCMatrixComplex((CMatrix)left, (Complex)right);
                    else
                        throw ThrowWrongArgumentType("^");
                }
                else
                {
                    throw ThrowWrongArgumentType("^");
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
                    if (right.IsReal && ExMath.IsInt32(right.Re))
                        return CMatrix.Pow(left, (int)right.Re);
                    else
                        throw new ArgumentException(Properties.Resources.EXC_MATRIX_NOT_INTEGER_POWER);
                }
                else
                {
                    throw new ArgumentException(Properties.Resources.EXC_MATRIX_MUST_BE_SQUARE_OR_VECTOR);
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
                object value = SubExpression.Evaluate();

                if (value is Complex)
                    return Complex.Negate((Complex)value);
                else if (value is CMatrix)
                    return CMatrix.Negate((CMatrix)value);
                else
                    throw ThrowWrongArgumentType("-");
            }
        }

        #endregion
    }
}
