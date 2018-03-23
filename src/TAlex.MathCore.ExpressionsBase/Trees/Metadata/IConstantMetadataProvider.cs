using System;


namespace TAlex.MathCore.ExpressionEvaluation.Trees.Metadata
{
    public interface IConstantMetadataProvider
    {
        ConstantMetadata GetMetadata(Type constantType);
    }
}
