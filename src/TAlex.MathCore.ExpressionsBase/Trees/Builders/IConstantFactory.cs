using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAlex.MathCore.ExpressionEvaluation.Trees.Builders
{
    public interface IConstantFactory<T>
    {
        ConstantExpression<T> CreateConstant(string constantName);
    }
}
