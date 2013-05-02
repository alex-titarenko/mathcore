using System;
using System.Linq;
using System.Collections.Generic;
using TAlex.MathCore.ExpressionEvaluation.Tokenize;
using TAlex.MathCore.ExpressionEvaluation.Trees.Metadata;


namespace TAlex.MathCore.ExpressionEvaluation.Trees.Builders
{
    public abstract class SimpleExpressionTreeBuilder<T> : IExpressionTreeBuilder<T>
    {
        #region Properties

        public IExpressionTokenizer Tokenizer { get; set; }

        public IConstantFactory<T> ConstantFactory { get; set; }

        public IFunctionFactory<T> FunctionFactory { get; set; }

        public IDictionary<string, Func<IEnumerator<Token>, IDictionary<string, VariableExpression<T>>, Expression<T>>> UnaryOperatorHandlers { get; set; }

        #endregion

        #region Constructors

        public SimpleExpressionTreeBuilder()
        {
            Tokenizer = new StandardExpressionTokenizer();

            UnaryOperatorHandlers = new Dictionary<string, Func<IEnumerator<Token>, IDictionary<string, VariableExpression<T>>, Expression<T>>>
            {
                { "-", (tokens, vars) => CreateUnaryMinusExpression(Pow(tokens, vars)) },
                { "+", (tokens, vars) => new UnaryPlusExpression<T>(Pow(tokens, vars)) },
                { "(", (tokens, vars) => {
                    Expression<T> bracketsSubExpr = AddSub(tokens, vars);
                    if (tokens.Current.Value == ")")
                    {
                        tokens.MoveNext();
                        return bracketsSubExpr;
                    }
                    else
                        throw new SyntaxException("\")\" expected.");
                }}
            };
        }

        #endregion

        #region Methods

        protected virtual Expression<T> AddSub(IEnumerator<Token> tokens, IDictionary<string, VariableExpression<T>> variables)
        {
            Expression<T> left = MultDiv(tokens, variables);

            while (true)
            {
                switch (tokens.Current.Value)
                {
                    case "+":
                        BinaryExpression<T> add = CreateAddExpression();
                        add.LeftExpression = left;
                        add.RightExpression = MultDiv(tokens, variables);
                        left = add;
                        break;

                    case "-":
                        BinaryExpression<T> sub = CreateSubExpression();
                        sub.LeftExpression = left;
                        sub.RightExpression = MultDiv(tokens, variables);
                        left = sub;
                        break;

                    default:
                        return left;
                }
            }
        }

        protected virtual Expression<T> MultDiv(IEnumerator<Token> tokens, IDictionary<string, VariableExpression<T>> variables)
        {
            Expression<T> left = Pow(tokens, variables);

            while (true)
            {
                switch (tokens.Current.Value)
                {
                    case "*":
                        BinaryExpression<T> mult = CreateMultExpression();
                        mult.LeftExpression = left;
                        mult.RightExpression = Pow(tokens, variables);
                        left = mult;
                        break;

                    case "/":
                        BinaryExpression<T> div = CreateDivExpression();
                        div.LeftExpression = left;
                        div.RightExpression = Pow(tokens, variables);
                        left = div;
                        break;

                    default:
                        return left;
                }
            }
        }

        protected virtual Expression<T> Pow(IEnumerator<Token> tokens, IDictionary<string, VariableExpression<T>> variables)
        {
            Expression<T> left = Unary(tokens, variables);

            while (true)
            {
                if (tokens.Current.Value == "^" || tokens.Current.Value == "**")
                {
                    BinaryExpression<T> pow = CreatePowExpression();
                    pow.LeftExpression = left;
                    pow.RightExpression = Pow(tokens, variables);
                    left = pow;
                }
                else
                {
                    return left;
                }
            }
        }

        protected virtual Expression<T> Unary(IEnumerator<Token> tokens, IDictionary<string, VariableExpression<T>> variables)
        {
            tokens.MoveNext();

            switch (tokens.Current.TokenType)
            {
                case TokenType.Scalar:
                    ScalarExpression<T> subExpr = ParseScalarValue(tokens.Current.Value);
                    tokens.MoveNext();
                    return subExpr;

                case TokenType.Identifier:
                    string identifierName = tokens.Current.Value;
                    tokens.MoveNext();

                    ConstantExpression<T> consExpr = ConstantFactory.CreateConstant(identifierName);
                    if (consExpr != null) return consExpr;

                    VariableExpression<T> varExpr = null;
                    if (!variables.TryGetValue(identifierName, out varExpr))
                    {
                        varExpr = new VariableExpression<T>(identifierName);
                        variables.Add(identifierName, varExpr);
                    }
                    return varExpr;

                case TokenType.Operator:
                    Func<IEnumerator<Token>, IDictionary<string, VariableExpression<T>>, Expression<T>> func;
                    if (UnaryOperatorHandlers.TryGetValue(tokens.Current.Value, out func))
                        return func(tokens, variables);
                    else
                        throw new SyntaxException(String.Format("Incorrect operator \"{0}\".", tokens.Current.Value));

                case TokenType.Function:
                    string funcName = tokens.Current.Value;
                    IList<Expression<T>> args = new List<Expression<T>>();

                    tokens.MoveNext();
                    if (tokens.Current.Value != "(")
                        throw ThrowExpectedException("(");

                    args.Add(AddSub(tokens, variables));
                    while (tokens.Current.Value != ")" && tokens.Current.Value == ",")
                    {
                        args.Add(AddSub(tokens, variables));
                    }

                    if (tokens.Current.Value != ")")
                        throw ThrowExpectedException(")");

                    tokens.MoveNext();
                    return FunctionFactory.CreateFunction(funcName, args.ToArray());

                default:
                    throw new SyntaxException();
            }
        }


        protected abstract BinaryExpression<T> CreateAddExpression();

        protected abstract BinaryExpression<T> CreateSubExpression();

        protected abstract BinaryExpression<T> CreateMultExpression();

        protected abstract BinaryExpression<T> CreateDivExpression();

        protected abstract BinaryExpression<T> CreatePowExpression();

        protected abstract UnaryExpression<T> CreateUnaryMinusExpression(Expression<T> subExpression);

        protected abstract ScalarExpression<T> ParseScalarValue(string s);

        #endregion

        #region Helpers

        private Exception ThrowExpectedException(string s)
        {
            return new SyntaxException(String.Format(Properties.Resources.EXC_EXPECTED, s));
        }

        #endregion

        #region IExpressionTreeBuilder<T> Members

        public Expression<T> BuildTree(string expression)
        {
            IEnumerator<Token> tokens = Tokenizer.GetTokens(expression).GetEnumerator();
            IDictionary<string, VariableExpression<T>> Variables = new Dictionary<string, VariableExpression<T>>();

            Expression<T> result = AddSub(tokens, Variables);

            if (tokens.MoveNext())
                throw new SyntaxException();

            return result;
        }

        #endregion
    }
}
