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

        public bool CanMultipleResults { get; set; }

        #endregion

        #region Constructors

        public ExampleUsage(string expression, string result)
        {
            Expression = expression;
            Result = result;
        }

        #endregion

        #region Methods

        public override string ToString()
        {
            return String.Format("{0} = {1}", Expression, Result);
        }

        #endregion
    }
}
