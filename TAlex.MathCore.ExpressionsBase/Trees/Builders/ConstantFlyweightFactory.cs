using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using TAlex.MathCore.ExpressionEvaluation.Trees.Metadata;


namespace TAlex.MathCore.ExpressionEvaluation.Trees.Builders
{
    public class ConstantFlyweightFactory<T> : IConstantFactory<T>
    {
        public IDictionary<string, Type> Constants { get; set; }

        private IDictionary<Type, ConstantExpression<T>> _instances;


        public ConstantFlyweightFactory()
        {
            Constants = new Dictionary<string, Type>();
            _instances = new Dictionary<Type, ConstantExpression<T>>();
        }
        

        public ConstantExpression<T> GetConstant(string constantName)
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

        public void LoadFromAssemblies(IEnumerable<Assembly> assemblies)
        {
            Constants.Clear();

            foreach (Assembly assembly in assemblies)
            {
                Type[] exportedTypes = assembly.GetExportedTypes();

                var constants = exportedTypes.Where(x => x.GetCustomAttribute<ConstantAttribute>() != null && typeof(Expression<T>).IsAssignableFrom(x)).ToList();
                constants.ForEach(x => Constants.Add(x.GetCustomAttribute<ConstantAttribute>().Name, x));
            }
        }
    }
}
