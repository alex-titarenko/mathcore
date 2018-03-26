using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAlex.MathCore.ExpressionEvaluation.ComplexExpressions
{
    internal static class ExceptionHelper
    {
        internal static Exception ThrowInvalidArgumentType(string expected, Object actual)
        {
            return new ArgumentException(String.Format(Properties.Resources.EXC_INVALID_ARG_TYPE, expected, actual.GetType().Name));
        }

        internal static Exception ThrowInvalidArgumentType(string expected, string actual)
        {
            return new ArgumentException(String.Format(Properties.Resources.EXC_INVALID_ARG_TYPE, expected, actual));
        }

        internal static Exception ThrowWrongArgumentType(object obj)
        {
            return new ArgumentException(String.Format(Properties.Resources.EXC_WRONG_ARG_TYPE, obj));
        }
    }
}
