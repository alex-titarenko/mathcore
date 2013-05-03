using System;
using System.Collections.Generic;
using System.Linq;


namespace TAlex.MathCore.ExpressionEvaluation.Trees.Metadata
{
    public class FunctionSignature
    {
        public string Name { get; set; }

        public IList<Argument> Arguments { get; set; }

        public string Description { get; set; }

        public int ArgumentCount { get; set; }

        public FunctionSignature(string name, IList<Argument> args)
        {
            Name = name;
            Arguments = args;

            if (Arguments != null && Arguments.Any())
            {
                if (Arguments.Last().Name == "...") ArgumentCount = -1;
                else ArgumentCount = Arguments.Count;
            }
        }


        public class Argument
        {
            public string Type { get; private set; }

            public KnownType KnownType { get; private set; }

            public string Name { get; private set; }


            public Argument(string type, string name)
                : this(type, KnownType.Unknown, name)
            {
            }

            public Argument(string type, KnownType knownType, string name)
            {
                Type = type;
                KnownType = knownType;
                Name = name;
            }
        }

        public enum KnownType
        {
            Variable,
            Expression,
            Unknown
        }
    }
}
