using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TAlex.MathCore.ExpressionEvaluation.Trees.Builders
{
    public class ConstantAttribute : Attribute
    {
        public string Name { get; set; }

        public ConstantAttribute(string name)
        {
            Name = name;
        }
    }
}
