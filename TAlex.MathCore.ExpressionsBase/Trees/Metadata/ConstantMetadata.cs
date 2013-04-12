using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TAlex.MathCore.ExpressionEvaluation.Trees.Metadata
{
    public class ConstantMetadata
    {
        public string Name { get; set; }

        public string Value { get; set; }

        public string DisplayName { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }

        public Type ConstantType { get; private set; }


        public ConstantMetadata(Type constantType)
        {
            ConstantType = constantType;
        }
    }
}
