using System;
using System.Linq;
using System.Collections.Generic;


namespace TAlex.MathCore.ExpressionEvaluation.Trees.Metadata
{
    public class FunctionMetadata
    {
        #region Properties

        public virtual string Id { get; set; }

        public virtual string DisplayName { get; set; }

        public virtual string Category { get; set; }

        public virtual string Section { get; set; }

        public virtual string Description { get; set; }

        public virtual IList<FunctionSignature> Signatures { get; set; }

        public virtual IList<ExampleUsage> ExampleUsages { get; set; }

        public virtual Type FunctionType
        {
            get;
            private set;
        }

        #endregion

        #region Constructors

        public FunctionMetadata(Type functionType)
        {
            FunctionType = functionType;
            Signatures = new List<FunctionSignature>();
            ExampleUsages = new List<ExampleUsage>();
        }

        #endregion

        #region Methods

        public FunctionSignature GetAcceptableSignature(int argCount)
        {
            FunctionSignature signature = Signatures.FirstOrDefault(x => x.ArgumentCount == argCount);
            if (signature == null)
            {
                signature = Signatures.FirstOrDefault(x => x.ArgumentCount == -1);
            }

            return signature;
        }

        #endregion
    }
}
