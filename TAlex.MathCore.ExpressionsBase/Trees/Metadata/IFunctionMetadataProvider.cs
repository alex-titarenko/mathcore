using System;


namespace TAlex.MathCore.ExpressionEvaluation.Trees.Metadata
{
    public interface IFunctionMetadataProvider
    {
        FunctionMetadata GetMetadata(Type functionType);
    }
}
