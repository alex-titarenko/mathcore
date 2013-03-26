using System;
using System.Linq;
using System.Collections.Generic;

using TAlex.MathCore.ExpressionEvaluation.Tokenize;


namespace TAlex.MathCore.ExpressionEvaluation.Trees.Builders
{
    public abstract class SimpleExpressionTreeBuilder<T> : IExpressionTreeBuilder<T>
    {
        #region Fields

        protected IEnumerator<Token> _tokens;
        protected IDictionary<string, VariableExpression<T>> _variables;
        protected object _syncRoot = new object();

        #endregion

        #region Properties

        public IExpressionTokenizer Tokenizer { get; set; }

        public ConstantFlyweightFactory<T> ConstantFlyweightFactory { get; set; }
        //public Dictionary<string, Type> Constants { get; set; }

        public IList<KeyValuePair<string, Type>> Functions { get; set; }

        #endregion

        #region Constructors

        public SimpleExpressionTreeBuilder()
        {
            Tokenizer = new StandardExpressionTokenizer();
        }

        #endregion

        #region Methods

        protected virtual Expression<T> AddSub()
        {
            Expression<T> left = MultDiv();

            while (true)
            {
                switch (_tokens.Current.Value)
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
                switch (_tokens.Current.Value)
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
                if (_tokens.Current.Value == "^" || _tokens.Current.Value == "**")
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
            _tokens.MoveNext();

            switch (_tokens.Current.TokenType)
            {
                case TokenType.Scalar:
                    ScalarExpression<T> subExpr = ParseScalarValue(_tokens.Current.Value);
                    _tokens.MoveNext();
                    return subExpr;

                case TokenType.Identifier:
                    string identifierName = _tokens.Current.Value;
                    _tokens.MoveNext();

                    ConstantExpression<T> consExpr = ConstantFlyweightFactory.GetConstant(identifierName);
                    if (consExpr != null) return consExpr;

                    VariableExpression<T> varExpr = null;
                    if (!_variables.TryGetValue(identifierName, out varExpr))
                    {
                        varExpr = new VariableExpression<T>(identifierName) { Value = GetDefaultVariableValue() };
                        _variables.Add(identifierName, varExpr);
                    }
                    return varExpr;

                case TokenType.Operator:
                    switch (_tokens.Current.Value)
                    {
                        case "-":
                            return CreateUnaryMinusExpression(Pow());

                        case "+":
                            return new UnaryPlusExpression<T>(Pow());

                        case "(":
                            Expression<T> bracketsSubExpr = AddSub();
                            if (_tokens.Current.Value == ")")
                            {
                                _tokens.MoveNext();
                                return bracketsSubExpr;
                            }
                            else
                                throw new SyntaxException("\")\" expected.");

                        default:
                            throw new SyntaxException(String.Format("Incorrect operator \"{0}\".", _tokens.Current.Value));
                    }

                case TokenType.Function:
                    string funcName = _tokens.Current.Value;
                    IList<Expression<T>> args = new List<Expression<T>>();

                    _tokens.MoveNext();
                    if (_tokens.Current.Value != "(")
                        throw new SyntaxException("\"(\" expected.");

                    args.Add(AddSub());
                    while (_tokens.Current.Value != ")" && _tokens.Current.Value == ",")
                    {
                        args.Add(AddSub());
                    }

                    if (_tokens.Current.Value != ")")
                        throw new SyntaxException("\")\" expected.");

                    _tokens.MoveNext();
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
            lock (_syncRoot)
            {
                _tokens = Tokenizer.GetTokens(expression).GetEnumerator();   
                _variables = new Dictionary<string, VariableExpression<T>>();

                Expression<T> result = AddSub();

                if (_tokens.MoveNext())
                    throw new SyntaxException();

                return result;
            }
        }

        #endregion
    }
}
