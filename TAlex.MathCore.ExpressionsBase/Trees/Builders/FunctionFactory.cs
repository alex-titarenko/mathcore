using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TAlex.MathCore.ExpressionEvaluation.Tokenize;
using TAlex.MathCore.ExpressionEvaluation.Trees.Metadata;


namespace TAlex.MathCore.ExpressionEvaluation.Trees.Builders
{
    public class FunctionFactory<T> : IFunctionFactory<T>, IFunctionsMetadataProvider
    {
        public IFunctionMetadataProvider MetadataProvider { get; set; }

        public IList<KeyValuePair<string, Type>> Functions { get; set; }


        public FunctionFactory()
        {
            MetadataProvider = new DefaultFunctionMetadataProvider();
            Functions = new List<KeyValuePair<string, Type>>();
        }


        #region IFunctionFactory<T> Members

        public Expression<T> CreateFunction(string functionName, Expression<T>[] args)
        {
            IEnumerable<KeyValuePair<string, Type>> targetFunctions = Functions.Where(x => x.Key == functionName);

            if (targetFunctions.Any())
            {
                foreach (KeyValuePair<string, Type> pair in targetFunctions)
                {
                    try
                    {
                        return (Expression<T>)Activator.CreateInstance(pair.Value, args);
                    }
                    catch (Exception exc)
                    {
                    }
                }

                throw new SyntaxException(String.Format("Function '{0}' with {1} arguments is not defined.", functionName, args.Length));
            }

            throw new SyntaxException(String.Format("Function '{0}' is not defined.", functionName));
        }

        #endregion

        #region IFunctionsMetadataProvider Members

        public IEnumerable<FunctionMetadata> GetMetadata()
        {
            return Functions.Select(x => MetadataProvider.GetMetadata(x.Value));
        }

        #endregion


        public virtual void AddFromAssemblies(IEnumerable<Assembly> assemblies)
        {
            foreach (Assembly assembly in assemblies)
            {
                foreach (var item in GetFunctionsFromAssembly(assembly))
                {
                    Functions.Add(item);
                }
            }
        }

        public void LoadFromAssemblies(IEnumerable<Assembly> assemblies)
        {
            Functions.Clear();
            AddFromAssemblies(assemblies);
        }

        protected virtual IEnumerable<KeyValuePair<string, Type>> GetFunctionsFromAssembly(Assembly assembly)
        {
            Type[] exportedTypes = assembly.GetExportedTypes();
            List<Type> fnTypes = exportedTypes
                .Where(x => x.GetCustomAttribute<FunctionSignatureAttribute>() != null && typeof(Expression<T>).IsAssignableFrom(x)).ToList();

            return fnTypes.Select(x => new KeyValuePair<string, Type>(x.GetCustomAttribute<FunctionSignatureAttribute>().Name, x));
        }
    }
}
