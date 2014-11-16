using System;
using System.Collections.Generic;
using System.Linq;


namespace TAlex.MathCore.ExpressionEvaluation.Trees.Metadata
{
    public class FunctionSignature
    {
        public string Name { get; private set; }

        public IList<Argument> Arguments { get; private set; }

        public string Description { get; set; }

        public int ArgumentCount { get; private set; }

        public bool HasVariableArgumentNumber
        {
            get
            {
                return (Arguments != null && Arguments.Any() && Arguments.Last().Name == "...");
            }
        }


        public FunctionSignature(string name, IList<Argument> args)
        {
            Name = name;
            Arguments = args;

            if (Arguments != null)
            {
                if (HasVariableArgumentNumber) ArgumentCount = -1;
                else ArgumentCount = Arguments.Count;
            }
        }


        public virtual string ToPlaceholderString()
        {
            return String.Format("{0}({1})", Name, String.Join(",", Enumerable.Repeat(String.Empty, Arguments.Count)));
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
