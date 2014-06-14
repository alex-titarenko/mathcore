using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace TAlex.MathCore.ExpressionEvaluation.Trees.Builders
{
    public static class ParametricFunctionCreator
    {
        public static Func<T, T> CreateOneParametricFunction<T>(Expression<T> expression, string varName)
        {
            VariableExpression<T> variable = expression.FindVariable(varName);
            return (variable == null) ?
                (Func<T, T>)new ZeroParametricFunctionCreator<T>(expression).Evaluate :
                (Func<T, T>)new OneParametricFunctionCreator<T>(expression, variable).Evaluate;
        }

        public static Func<T, T> CreateOneParametricFunction<T>(Expression<T> expression, VariableExpression<T> var)
        {
            OneParametricFunctionCreator<T> creator = new OneParametricFunctionCreator<T>(expression, var);
            return creator.Evaluate;
        }


        public static Func<T, T, T> CreateTwoParametricFunction<T>(Expression<T> expression, string firstVarName, string secondVarName)
        {
            VariableExpression<T> variable1 = expression.FindVariable(firstVarName);
            if (variable1 == null) throw new ArgumentException(String.Format(Properties.Resources.EXC_VARIABLE_NOT_FOUND, firstVarName));

            VariableExpression<T> variable2 = expression.FindVariable(secondVarName);
            if (variable2 == null) throw new ArgumentException(String.Format(Properties.Resources.EXC_VARIABLE_NOT_FOUND, secondVarName));

            TwoParametricFunctionCreator<T> creator = new TwoParametricFunctionCreator<T>(expression, variable1, variable2);

            return creator.Evaluate;
        }

        public static Func<T, T, T> CreateTwoParametricFunction<T>(Expression<T> expression, VariableExpression<T> firstVar, VariableExpression<T> secondVar)
        {
            TwoParametricFunctionCreator<T> creator = new TwoParametricFunctionCreator<T>(expression, firstVar, secondVar);
            return creator.Evaluate;
        }


        private class ZeroParametricFunctionCreator<T>
        {
            public readonly Expression<T> Expression;


            public ZeroParametricFunctionCreator(Expression<T> expression)
            {
                Expression = expression;
            }


            public T Evaluate(T arg)
            {
                return Expression.Evaluate();
            }
        }

        private class OneParametricFunctionCreator<T>
        {
            public readonly Expression<T> Expression;

            public readonly VariableExpression<T> Variable;


            public OneParametricFunctionCreator(Expression<T> expression, VariableExpression<T> variable)
            {
                Expression = expression;
                Variable = variable;
            }


            public T Evaluate(T arg)
            {
                Variable.Value = arg;
                return Expression.Evaluate();
            }
        }

        private class TwoParametricFunctionCreator<T>
        {
            public readonly Expression<T> Expression;

            public readonly VariableExpression<T> FirstVariable;

            public readonly VariableExpression<T> SecondVariable;

            public TwoParametricFunctionCreator(Expression<T> expression, VariableExpression<T> firstVariable, VariableExpression<T> secondVariable)
            {
                Expression = expression;
                FirstVariable = firstVariable;
                SecondVariable = secondVariable;
            }


            public T Evaluate(T arg1, T arg2)
            {
                FirstVariable.Value = arg1;
                SecondVariable.Value = arg2;
                return Expression.Evaluate();
            }
        }
    }
}
