using System;
using System.Linq;
using System.Collections.Generic;

using TAlex.MathCore.ExpressionEvaluation.Tokenize;


namespace TAlex.MathCore.ExpressionEvaluation.Trees.Builders
{
    public abstract class SimpleExpressionTreeBuilder<T> : IExpressionTreeBuilder<T>
    {
        #region Fields

        protected IEnumerator<Token> Tokens;
        protected IDictionary<string, VariableExpression<T>> Variables;
        protected object SyncRoot = new object();

        #endregion

        #region Properties

        public IExpressionTokenizer Tokenizer { get; set; }

        public ConstantFlyweightFactory<T> ConstantFlyweightFactory { get; set; }
        //public Dictionary<string, Type> Constants { get; set; }

        public IList<KeyValuePair<string, Type>> Functions { get; set; }

        public IDictionary<string, Func<Expression<T>>> UnaryOperatorHandlers { get; set; }

        #endregion

        #region Constructors

        public SimpleExpressionTreeBuilder()
        {
            Tokenizer = new StandardExpressionTokenizer();

            UnaryOperatorHandlers = new Dictionary<string, Func<Expression<T>>>
            {
                { "-", () => CreateUnaryMinusExpression(Pow()) },
                { "+", () => new UnaryPlusExpression<T>(Pow()) },
                { "(", () => {
                    Expression<T> bracketsSubExpr = AddSub();
                    if (Tokens.Current.Value == ")")
                    {
                        Tokens.MoveNext();
                        return bracketsSubExpr;
                    }
                    else
                        throw new SyntaxException("\")\" expected.");
                }}
            };
        }

        #endregion

        #region Methods

        protected virtual Expression<T> AddSub()
        {
            Expression<T> left = MultDiv();

            while (true)
            {
                switch (Tokens.Current.Value)
                {
                    case "+":
                        BinaryExpression<T> add = CreateAddExpression();
                        add.LeftExpression = left;
                        add.RightExpression = MultDiv();
                        left = add;
                        break;

                    case "-":
                        BinaryExpression<T> sub = CreateSubExpression();
                        sub.LeftExpression = left;
                        sub.RightExpression = MultDiv();
                        left = sub;
                        break;

                    default:
                        return left;
                }
            }
        }

        protected virtual Expression<T> MultDiv()
        {
            Expression<T> left = Pow();

            while (true)
            {
                switch (Tokens.Current.Value)
                {
                    case "*":
                        BinaryExpression<T> mult = CreateMultExpression();
                        mult.LeftExpression = left;
                        mult.RightExpression = Pow();
                        left = mult;
                        break;

                    case "/":
                        BinaryExpression<T> div = CreateDivExpression();
                        div.LeftExpression = left;
                        div.RightExpression = Pow();
                        left = div;
                        break;

                    default:
                        return left;
                }
            }
        }

        protected virtual Expression<T> Pow()
        {
            Expression<T> left = Unary();

            while (true)
            {
                if (Tokens.Current.Value == "^" || Tokens.Current.Value == "**")
                {
                    BinaryExpression<T> pow = CreatePowExpression();
                    pow.LeftExpression = left;
                    pow.RightExpression = Pow();
                    left = pow;
                }
                else
                {
                    return left;
                }
            }
        }

        protected virtual Expression<T> Unary()
        {
            Tokens.MoveNext();

            switch (Tokens.Current.TokenType)
            {
                case TokenType.Scalar:
                    ScalarExpression<T> subExpr = ParseScalarValue(Tokens.Current.Value);
                    Tokens.MoveNext();
                    return subExpr;

                case TokenType.Identifier:
                    string identifierName = Tokens.Current.Value;
                    Tokens.MoveNext();

                    ConstantExpression<T> consExpr = ConstantFlyweightFactory.GetConstant(identifierName);
                    if (consExpr != null) return consExpr;

                    VariableExpression<T> varExpr = null;
                    if (!Variables.TryGetValue(identifierName, out varExpr))
                    {
                        varExpr = new VariableExpression<T>(identifierName) { Value = GetDefaultVariableValue() };
                        Variables.Add(identifierName, varExpr);
                    }
                    return varExpr;

                case TokenType.Operator:
                    Func<Expression<T>> func;
                    if (UnaryOperatorHandlers.TryGetValue(Tokens.Current.Value, out func))
                        return func();
                    else
                        throw new SyntaxException(String.Format("Incorrect operator \"{0}\".", Tokens.Current.Value));

                case TokenType.Function:
                    string funcName = Tokens.Current.Value;
                    IList<Expression<T>> args = new List<Expression<T>>();

                    Tokens.MoveNext();
                    if (Tokens.Current.Value != "(")
                        throw new SyntaxException("\"(\" expected.");

                    args.Add(AddSub());
                    while (Tokens.Current.Value != ")" && Tokens.Current.Value == ",")
                    {
                        args.Add(AddSub());
                    }

                    if (Tokens.Current.Value != ")")
                        throw new SyntaxException("\")\" expected.");

                    Tokens.MoveNext();
                    return ResolveFunctionExpression(funcName, args.ToArray());

                default:
                    throw new SyntaxException("No expression.");
            }
        }

        protected virtual Expression<T> ResolveFunctionExpression(string name, Expression<T>[] args)
        {
            IEnumerable<KeyValuePair<string, Type>> targetFunctions = Functions.Where(x => x.Key == name);

            if (targetFunctions.Any())
            {
                foreach (KeyValuePair<string, Type> pair in targetFunctions)
                {
                    try
                    {
                        return (Expression<T>)Activator.CreateInstance(pair.Value, args);
                    }
                    catch
                    {
                    }
                }

                throw new SyntaxException(String.Format("Function '{0}' with {1} arguments is not defined.", name, args.Length));
            }

            throw new SyntaxException(String.Format("Function '{0}' is not defined.", name));
        }


        protected abstract T GetDefaultVariableValue();

        protected abstract BinaryExpression<T> CreateAddExpression();

        protected abstract BinaryExpression<T> CreateSubExpression();

        protected abstract BinaryExpression<T> CreateMultExpression();

        protected abstract BinaryExpression<T> CreateDivExpression();

        protected abstract BinaryExpression<T> CreatePowExpression();

        protected abstract UnaryExpression<T> CreateUnaryMinusExpression(Expression<T> subExpression);

        protected abstract ScalarExpression<T> ParseScalarValue(string s);

        #endregion

        #region IExpressionTreeBuilder<T> Members

        public Expression<T> BuildTree(string expression)
        {
            lock (SyncRoot)
            {
                Tokens = Tokenizer.GetTokens(expression).GetEnumerator();   
                Variables = new Dictionary<string, VariableExpression<T>>();

                Expression<T> result = AddSub();

                if (Tokens.MoveNext())
                    throw new SyntaxException();

                return result;
            }
        }

        #endregion
    }
}
