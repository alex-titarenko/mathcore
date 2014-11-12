using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using TAlex.MathCore.ExpressionEvaluation.Extensions;
using TAlex.MathCore.ExpressionEvaluation.Helpers;


namespace TAlex.MathCore.ExpressionEvaluation.Trees.Metadata
{
    public class DefaultFunctionMetadataProvider : IFunctionMetadataProvider
    {
        #region IFunctionMetadataProvider Members

        public FunctionMetadata GetMetadata(Type functionType)
        {
            FunctionMetadata functionMetadata = new FunctionMetadata(functionType);

            functionMetadata.Id = MD5.GetHashString(functionType.FullName);

            var displayNameAttr = functionType.GetCustomAttribute<DisplayNameAttribute>();
            functionMetadata.DisplayName = displayNameAttr != null ? displayNameAttr.DisplayName : null;

            var categoryAttr = functionType.GetCustomAttribute<CategoryAttribute>();
            functionMetadata.Category = categoryAttr != null ? categoryAttr.Category : null;

            var sectionAttr = functionType.GetCustomAttribute<SectionAttribute>();
            functionMetadata.Section = sectionAttr != null ? sectionAttr.Name : null;

            var descriptionAttr = functionType.GetCustomAttribute<DescriptionAttribute>();
            functionMetadata.Description = descriptionAttr != null ? descriptionAttr.Description : null;

            foreach (var signature in functionType.GetCustomAttributes<FunctionSignatureAttribute>())
            {
                functionMetadata.Signatures.Add(GetFunctionSignatureMetadata(signature));
            }

            foreach (var exampleUsage in functionType.GetCustomAttributes<ExampleUsageAttribute>())
            {
                functionMetadata.ExampleUsages.Add(new ExampleUsage(exampleUsage.Expression, exampleUsage.Result)
                {
                    CanMultipleResults = exampleUsage.CanMultipleResults
                });
            }


            return functionMetadata;
        }

        private FunctionSignature GetFunctionSignatureMetadata(FunctionSignatureAttribute signature)
        {
            IList<FunctionSignature.Argument> args = signature.Arguments.Select(x =>
            {
                var parts = x.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                string type = String.Join(" ", parts.Take(parts.Length - 1));
                string name = parts.Last();

                return new FunctionSignature.Argument(type, GetKnownArgType(type), name);
            }).ToList();

            return new FunctionSignature(signature.Name, args) { Description = signature.Description };
        }

        private FunctionSignature.KnownType GetKnownArgType(string type)
        {
            switch (type.ToUpper())
            {
                case "VARIABLE":
                    return FunctionSignature.KnownType.Variable;

                case "EXPRESSION":
                    return FunctionSignature.KnownType.Expression;

                default:
                    return FunctionSignature.KnownType.Unknown;
            }
        }

        #endregion
    }
}
