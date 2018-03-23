using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace TAlex.MathCore.ExpressionEvaluation.Trees.Builders
{
    public interface IFunctionFactory<T>
    {
        Expression<T> CreateFunction(string functionName, Expression<T>[] args);
    }
}
