using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TAlex.MathCore.ExpressionEvaluation.Trees.Builders
{
    public interface IConstantFlyweightFactory<T>
    {
        ConstantExpression<T> GetConstant(string constantName);
    }

    public class ConstantFlyweightFactory<T> : IConstantFlyweightFactory<T>
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
    }
}
