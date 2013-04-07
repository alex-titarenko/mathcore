using System;
using System.Collections.Generic;


namespace TAlex.MathCore.ExpressionEvaluation.Trees.Metadata
{
    public class FunctionMetadata
    {
        #region Properties

        public virtual string DisplayName { get; set; }

        public virtual string Category { get; set; }

        public virtual string Section { get; set; }

        public virtual string Description { get; set; }

        public virtual IList<FunctionSignature> Signatures { get; set; }

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
        }

        #endregion
    }
}
