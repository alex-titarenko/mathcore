using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TAlex.MathCore.ExpressionEvaluation.Trees.Metadata
{
    public class FunctionSignature
    {
        public string Name { get; set; }

        public IEnumerable<Argument> Arguments { get; set; }


        public FunctionSignature(string name, IEnumerable<Argument> args)
        {
            Name = name;
            Arguments = args;
        }


        public class Argument
        {
            public string Type { get; private set; }

            public string Name { get; private set; }


            public Argument(string type, string name)
            {
                Type = type;
                Name = name;
            }
        }
    }
}
