using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAlex.MathCore.ExpressionEvaluation.Trees.Metadata
{
    public class ExampleUsage
    {
        #region Properties

        public string Expression { get; set; }

        public string Result { get; set; }

        #endregion

        #region Constructors

        public ExampleUsage(string expression, string result)
        {
            Expression = expression;
            Result = result;
        }

        #endregion
    }
}
