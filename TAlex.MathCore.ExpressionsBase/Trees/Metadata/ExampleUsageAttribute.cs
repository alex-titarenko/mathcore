using System;


namespace TAlex.MathCore.ExpressionEvaluation.Trees.Metadata
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class ExampleUsageAttribute : Attribute
    {
        #region Properties

        public string Expression { get; set; }

        public string Result { get; set; }

        public bool CanMultipleResults { get; set; }

        #endregion

        #region Constructors

        public ExampleUsageAttribute(string expression, string result)
        {
            Expression = expression;
            Result = result;
        }

        #endregion
    }
}
