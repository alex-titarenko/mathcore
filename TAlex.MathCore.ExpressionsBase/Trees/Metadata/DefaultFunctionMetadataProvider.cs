using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Reflection;


namespace TAlex.MathCore.ExpressionEvaluation.Trees.Metadata
{
    public class DefaultFunctionMetadataProvider : IFunctionMetadataProvider
    {
        #region IFunctionMetadataProvider Members

        public FunctionMetadata GetMetadata(Type functionType)
        {
            FunctionMetadata functionMetadata = new FunctionMetadata(functionType);

            var displayNameAttr = functionType.GetCustomAttribute<DisplayNameAttribute>();
            functionMetadata.DisplayName = displayNameAttr != null ? displayNameAttr.DisplayName : null;

            var categoryAttr = functionType.GetCustomAttribute<CategoryAttribute>();
            functionMetadata.Category = categoryAttr != null ? categoryAttr.Category : null;

            var sectionAttr = functionType.GetCustomAttribute<SectionAttribute>();
            functionMetadata.Section = sectionAttr != null ? sectionAttr.Name : null;

            var descriptionAttr = functionType.GetCustomAttribute(typeof(DescriptionAttribute)) as DescriptionAttribute;
            functionMetadata.Description = descriptionAttr != null ? descriptionAttr.Description : null;

            foreach (var signature in functionType.GetCustomAttributes<FunctionSignatureAttribute>())
            {
                functionMetadata.Signatures.Add(new FunctionSignature(signature.Name,
                    signature.Arguments.Select(x =>
                    {
                        var parts = x.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                        return new FunctionSignature.Argument(parts[0], parts[1]);
                    })) { Description = signature.Description });
            }

            foreach (var exampleUsage in functionType.GetCustomAttributes<ExampleUsageAttribute>())
            {
                functionMetadata.ExampleUsages.Add(new ExampleUsage(exampleUsage.Expression, exampleUsage.Result));
            }


            return functionMetadata;
        }

        #endregion
    }
}
