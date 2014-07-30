using System;


namespace TAlex.MathCore.ExpressionEvaluation.Trees.Metadata
{
    public class CategoryAttribute : Attribute
    {
        #region Properties

        public string Category { get; set; }

        #endregion

        #region Constructors

        public CategoryAttribute(string category)
        {
            Category = category;
        }

        #endregion
    }
}
