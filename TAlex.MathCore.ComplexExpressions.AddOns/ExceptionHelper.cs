using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAlex.MathCore.ExpressionEvaluation.ComplexExpressions
{
    internal static class ExceptionHelper
    {
        internal static Exception ThrowWrongArgumentType(object obj)
        {
            return new ArgumentException(String.Format("The argument type '{0}' is not supported.", obj != null ? obj.GetType() : null));
        }
    }
}
