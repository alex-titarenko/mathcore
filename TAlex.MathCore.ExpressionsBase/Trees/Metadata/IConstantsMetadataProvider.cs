using System;
using System.Collections.Generic;


namespace TAlex.MathCore.ExpressionEvaluation.Trees.Metadata
{
    public interface IConstantsMetadataProvider
    {
        IEnumerable<ConstantMetadata> GetMetadata();
    }
}
