using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TAlex.MathCore.ExpressionEvaluation.Extensions;


namespace TAlex.MathCore.ExpressionEvaluation.Trees.Metadata
{
    public class DefaultConstantMetadataProvider : IConstantMetadataProvider
    {
        #region IConstantMetadataProvider Members

        public ConstantMetadata GetMetadata(Type constantType)
        {
            ConstantMetadata constantMetadata = new ConstantMetadata(constantType);

            var constantAttr = constantType.GetCustomAttribute<ConstantAttribute>();
            if (constantAttr != null)
            {
                constantMetadata.Name = constantAttr.Name;
                constantMetadata.Value = constantAttr.Value;
            }

            var displayNameAttr = constantType.GetCustomAttribute<DisplayNameAttribute>();
            constantMetadata.DisplayName = displayNameAttr != null ? displayNameAttr.DisplayName : null;

            var categoryAttr = constantType.GetCustomAttribute<CategoryAttribute>();
            constantMetadata.Category = categoryAttr != null ? categoryAttr.Category : null;

            var descriptionAttr = constantType.GetCustomAttribute<DescriptionAttribute>();
            constantMetadata.Description = descriptionAttr != null ? descriptionAttr.Description : null;


            return constantMetadata;
        }

        #endregion
    }
}
