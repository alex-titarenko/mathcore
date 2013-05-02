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
            return new ArgumentException(String.Format("The argument '{0}' has unsupported type.", obj));
        }
    }
}
