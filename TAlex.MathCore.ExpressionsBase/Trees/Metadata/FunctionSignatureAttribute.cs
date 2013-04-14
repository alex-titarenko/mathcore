using System;


namespace TAlex.MathCore.ExpressionEvaluation.Trees.Metadata
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class FunctionSignatureAttribute : Attribute
    {
        #region Properties

        public string Name { get; set; }

        public string[] Arguments { get; set; }

        public string Description { get; set; }

        #endregion

        #region Constructors

        public FunctionSignatureAttribute(string name, params string[] arguments)
        {
            Name = name;
            Arguments = arguments;
        }

        #endregion
    }
}
