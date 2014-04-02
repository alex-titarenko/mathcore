using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using TAlex.MathCore.ExpressionEvaluation.Tokenize;
using TAlex.MathCore.ExpressionEvaluation.Trees.Metadata;


namespace TAlex.MathCore.ExpressionEvaluation.Trees.Builders
{
    public class FunctionFactory<T> : IFunctionFactory<T>, IFunctionsMetadataProvider
    {
        public IFunctionMetadataProvider MetadataProvider { get; set; }

        public IList<KeyValuePair<string, FunctionMetadata>> Functions { get; set; }


        public FunctionFactory()
        {
            MetadataProvider = new DefaultFunctionMetadataProvider();
            Functions = new List<KeyValuePair<string, FunctionMetadata>>();
        }


        #region IFunctionFactory<T> Members

        public Expression<T> CreateFunction(string functionName, Expression<T>[] args)
        {
            IEnumerable<KeyValuePair<string, FunctionMetadata>> targetFunctions = Functions.Where(x => x.Key == functionName);

            if (targetFunctions.Any())
            {
                foreach (KeyValuePair<string, FunctionMetadata> pair in targetFunctions)
                {
                    FunctionSignature signature = pair.Value.GetAcceptableSignature(args.Length);
                    if (signature != null)
                    {
                        ProcessingClosedVariables(signature, args);
                        return (Expression<T>)Activator.CreateInstance(pair.Value.FunctionType, args);
                    }
                }

                throw new SyntaxException(String.Format(Properties.Resources.EXC_FUNC_WITH_ARG_COUNT_NOT_DEFINED, functionName, args.Length));
            }

            throw new SyntaxException(String.Format(Properties.Resources.EXC_FUNC_NOT_DEFINED, functionName));
        }

        private void ProcessingClosedVariables(FunctionSignature signature, Expression<T>[] args)
        {
            var expressionArg = signature.Arguments.FirstOrDefault(x => x.KnownType == FunctionSignature.KnownType.Expression);
            if (expressionArg != null)
            {
                int exprIndex = signature.Arguments.IndexOf(expressionArg);
                Expression<T> expr = args[exprIndex];

                foreach (var varArg in signature.Arguments.Where(x => x.KnownType == FunctionSignature.KnownType.Variable))
                {
                    int varIndex = signature.Arguments.IndexOf(varArg);
                    VariableExpression<T> oldVar = args[varIndex] as VariableExpression<T>;

                    if (oldVar != null)
                    {
                        VariableExpression<T> newVar = GetClosedVariable(oldVar);
                        if (expr is VariableExpression<T>)
                            args[exprIndex] = newVar;
                        else
                            expr.ReplaceChild(oldVar, newVar);
                        args[varIndex] = newVar;
                    }
                    else
                    {
                        throw new ArgumentException(String.Format(Properties.Resources.EXC_INVALID_ARG_TYPE,
                            "variable", args[varIndex].Evaluate().GetType().Name));
                    }
                }
            }
        }

        private VariableExpression<T> GetClosedVariable(VariableExpression<T> variable)
        {
            return new VariableExpression<T>(String.Format("{0}__{1}", variable.VariableName, Guid.NewGuid()), variable.DisplayName);
        }

        #endregion

        #region IFunctionsMetadataProvider Members

        public IEnumerable<FunctionMetadata> GetMetadata()
        {
            return Functions.Select(x => MetadataProvider.GetMetadata(x.Value.FunctionType));
        }

        #endregion


        public virtual void Add(string name, Type functionType)
        {
            Functions.Add(new KeyValuePair<string, FunctionMetadata>(name, MetadataProvider.GetMetadata(functionType)));
        }

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

        protected virtual IEnumerable<KeyValuePair<string, FunctionMetadata>> GetFunctionsFromAssembly(Assembly assembly)
        {
            Type[] exportedTypes = assembly.GetExportedTypes();
            List<Type> fnTypes = exportedTypes
                .Where(x => x.GetCustomAttributes<FunctionSignatureAttribute>().Any() && typeof(Expression<T>).IsAssignableFrom(x)).ToList();

            return fnTypes.SelectMany(x => x.GetCustomAttributes<FunctionSignatureAttribute>().Select(a => a.Name).Distinct().Select(name => new KeyValuePair<string, FunctionMetadata>(name, MetadataProvider.GetMetadata(x))));
        }
    }
}
