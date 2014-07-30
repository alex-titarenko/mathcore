using System;


namespace TAlex.MathCore.ExpressionEvaluation.Trees.Metadata
{
    public class DisplayNameAttribute : Attribute
    {
        #region Properties

        public string DisplayName { get; set; }

        #endregion

        #region Constructors

        public DisplayNameAttribute(string displayName)
        {
            DisplayName = displayName;
        }

        #endregion
    }
}
