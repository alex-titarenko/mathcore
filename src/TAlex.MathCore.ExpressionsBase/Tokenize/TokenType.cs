using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TAlex.MathCore.ExpressionEvaluation.Tokenize
{
    public enum TokenType
    {
        Operator,
        Scalar,
        Identifier,
        Function,
        End
    }
}
