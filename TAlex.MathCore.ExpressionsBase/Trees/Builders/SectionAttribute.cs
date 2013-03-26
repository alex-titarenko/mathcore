using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TAlex.MathCore.ExpressionEvaluation.Trees.Builders
{
    public class SectionAttribute : Attribute
    {
        #region Properties

        public string Name { get; set; }

        #endregion

        #region Constructors

        public SectionAttribute(string name)
        {
            Name = name;
        }

        #endregion
    }
}
