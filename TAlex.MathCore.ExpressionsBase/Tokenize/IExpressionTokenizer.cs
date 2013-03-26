using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TAlex.MathCore.ExpressionEvaluation.Tokenize
{
    public interface IExpressionTokenizer
    {
        IEnumerable<Token> GetTokens(string expression);
    }
}
