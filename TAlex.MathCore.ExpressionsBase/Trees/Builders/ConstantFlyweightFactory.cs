using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using TAlex.MathCore.ExpressionEvaluation.Extensions;
using TAlex.MathCore.ExpressionEvaluation.Trees.Metadata;


namespace TAlex.MathCore.ExpressionEvaluation.Trees.Builders
{
    public class ConstantFlyweightFactory<T> : IConstantFactory<T>, IConstantsMetadataProvider
    {
        public IConstantMetadataProvider MetadataProvider { get; set; }

        public IDictionary<string, Type> Constants { get; set; }

        private IDictionary<Type, ConstantExpression<T>> _instances;


        public ConstantFlyweightFactory()
        {
            MetadataProvider = new DefaultConstantMetadataProvider();
            Constants = new Dictionary<string, Type>();
            _instances = new Dictionary<Type, ConstantExpression<T>>();
        }


        #region IConstantFactory<T> Members

        public ConstantExpression<T> CreateConstant(string constantName)
        {
            ConstantExpression<T> constant = null;
            Type constantType;

            if (Constants.TryGetValue(constantName, out constantType))
            {
                if (!_instances.TryGetValue(constantType, out constant))
                {
                    constant = (ConstantExpression<T>)Activator.CreateInstance(constantType);
                }
            }

            return constant;
        }

        #endregion

        #region IConstantsMetadataProvider Members

        public IEnumerable<ConstantMetadata> GetMetadata()
        {
            return Constants.Select(x => MetadataProvider.GetMetadata(x.Value));
        }

        #endregion


        public virtual void AddFromAssemblies(IEnumerable<Assembly> assemblies)
        {
            foreach (Assembly assembly in assemblies)
            {
                Type[] exportedTypes = assembly.GetExportedTypes();

                var constants = exportedTypes.Where(x => x.GetCustomAttribute<ConstantAttribute>() != null && typeof(Expression<T>).IsAssignableFrom(x)).ToList();

                foreach (var x in constants)
                {
                    Constants.Add(x.GetCustomAttribute<ConstantAttribute>().Name, x);
                }
            }
        }

        public void LoadFromAssemblies(IEnumerable<Assembly> assemblies)
        {
            Constants.Clear();
            AddFromAssemblies(assemblies);
        }
    }
}
