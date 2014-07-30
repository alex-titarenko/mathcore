using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TAlex.MathCore.ExpressionEvaluation.Trees.Metadata
{
    public class DescriptionAttribute : Attribute
    {
        #region Properties

        public string Description { get; set; }

        #endregion

        #region Constructors

        public DescriptionAttribute(string description)
        {
            Description = description;
        }

        #endregion
    }
}
