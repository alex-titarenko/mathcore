using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAlex.MathCore.ExpressionEvaluation.Trees;
using TAlex.MathCore.ExpressionEvaluation.Trees.Metadata;
using TAlex.MathCore.SpecialFunctions;


namespace TAlex.MathCore.ExpressionEvaluation.ComplexExpressions.Functions
{
    [DisplayName("Factorial")]
    [Category(Categories.SpecialFunctions)]
    [Section(Sections.Combinatorics)]
    [Description("Calculates the factorial of a positive integer.")]
    [FunctionSignature("fact", "integer n")]
    [ExampleUsage("fact(0)", "1")]
    [ExampleUsage("fact(3)", "6")]
    [ExampleUsage("fact(11)", "39916800")]
    public class FactorialFuncExpression : UnaryExpression<Object>
    {
        public FactorialFuncExpression(Expression<Object> subExpression)
            : base(subExpression)
        {
        }

        public override object Evaluate()
        {
            return (Complex)Combinatorics.Factorial(SubExpression.EvaluateAsInt32());
        }
    }

    [DisplayName("Combinations")]
    [Category(Categories.SpecialFunctions)]
    [Section(Sections.Combinatorics)]
    [Description("Calculates the number of ways of picking k unordered outcomes from n possibilities.")]
    [FunctionSignature("combin", "integer n", "integer k")]
    [ExampleUsage("combin(2, 1)", "2")]
    [ExampleUsage("combin(5, 3)", "10")]
    [ExampleUsage("combin(11, 6)", "462")]
    public class CombinationsFuncExpression : BinaryExpression<Object>
    {
        public CombinationsFuncExpression(Expression<Object> nExpression, Expression<Object> kExpression)
            : base(nExpression, kExpression)
        {
        }

        public override object Evaluate()
        {
            return (Complex)Combinatorics.Combinations(LeftExpression.EvaluateAsInt32(), RightExpression.EvaluateAsInt32());
        }
    }

    [DisplayName("Permutations")]
    [Category(Categories.SpecialFunctions)]
    [Section(Sections.Combinatorics)]
    [Description("Calculates the number of ways of obtaining an ordered subset of k elements from a set of n elements.")]
    [FunctionSignature("permut", "integer n", "integer k")]
    [ExampleUsage("permut(2, 1)", "2")]
    [ExampleUsage("permut(5, 3)", "60")]
    [ExampleUsage("permut(11, 6)", "332640")]
    public class PermutationsFuncExpression : BinaryExpression<Object>
    {
        public PermutationsFuncExpression(Expression<Object> nExpression, Expression<Object> kExpression)
            : base(nExpression, kExpression)
        {
        }

        public override object Evaluate()
        {
            return (Complex)Combinatorics.Permutations(LeftExpression.EvaluateAsInt32(), RightExpression.EvaluateAsInt32());
        }
    }

    [DisplayName("Fibonacci")]
    [Category(Categories.SpecialFunctions)]
    [Section(Sections.Combinatorics)]
    [Description("Calculates the n-th Fibonacci number.")]
    [FunctionSignature("fib", "integer n")]
    [ExampleUsage("fib(0)", "0")]
    [ExampleUsage("fib(3)", "2")]
    [ExampleUsage("fib(8)", "21")]
    public class FibonacciFuncExpression : UnaryExpression<Object>
    {
        public FibonacciFuncExpression(Expression<Object> subExpression)
            : base(subExpression)
        {
        }

        public override object Evaluate()
        {
            return (Complex)Combinatorics.Fibonacci(SubExpression.EvaluateAsInt32());
        }
    }



    [DisplayName("Greatest common divisor")]
    [Category(Categories.SpecialFunctions)]
    [Section(Sections.NumberTheory)]
    [Description("Calculates the greatest common divisor of a set of integer numbers.")]
    [FunctionSignature("gcd", "integer n1", "integer n2", "...")]
    [ExampleUsage("gcd(5, 2)", "1")]
    [ExampleUsage("gcd(9, 6)", "3")]
    [ExampleUsage("gcd(12, 6, 8, 24)", "2")]
    public class GcdFuncExpression : MultiaryExpression<Object>
    {
        public GcdFuncExpression(params Expression<Object>[] subExpression)
            : base(subExpression)
        {
        }

        public override object Evaluate()
        {
            return (Complex)NumberTheory.GCD(Expressions.Select(x => x.EvaluateAsInt64()).ToArray());
        }
    }

    [DisplayName("Least common multiple")]
    [Category(Categories.SpecialFunctions)]
    [Section(Sections.NumberTheory)]
    [Description("Calculates the least common multiple of a set of integer numbers.")]
    [FunctionSignature("lcm", "integer n1", "integer n2", "...")]
    [ExampleUsage("lcm(5, 2)", "10")]
    [ExampleUsage("lcm(9, 6)", "18")]
    [ExampleUsage("lcm(12, 6, 5, 24)", "120")]
    public class LcmFuncExpression : MultiaryExpression<Object>
    {
        public LcmFuncExpression(params Expression<Object>[] subExpression)
            : base(subExpression)
        {
        }

        public override object Evaluate()
        {
            return (Complex)NumberTheory.LCM(Expressions.Select(x => x.EvaluateAsInt64()).ToArray());
        }
    }


    [DisplayName("Error function")]
    [Category(Categories.SpecialFunctions)]
    [Section(Sections.ProbabilityIntegrals)]
    [Description("Calculates the value of error function for the specified real argument.")]
    [FunctionSignature("erf", "real value")]
    [ExampleUsage("erf(0)", "0")]
    [ExampleUsage("erf(1)", "0.842700792949715")]
    public class ErrorFuncExpression : UnaryExpression<Object>
    {
        public ErrorFuncExpression(Expression<Object> subExpression)
            : base(subExpression)
        {
        }

        public override object Evaluate()
        {
            return (Complex)ProbabilityIntegrals.Erf(SubExpression.EvaluateAsReal());
        }
    }

    [DisplayName("Complementary error function")]
    [Category(Categories.SpecialFunctions)]
    [Section(Sections.ProbabilityIntegrals)]
    [Description("Calculates the value of complementary error function for the specified real argument.")]
    [FunctionSignature("erfc", "real value")]
    [ExampleUsage("erfc(0)", "1")]
    [ExampleUsage("erfc(1)", "0.157299207050285")]
    public class ComplementaryErrorFuncExpression : UnaryExpression<Object>
    {
        public ComplementaryErrorFuncExpression(Expression<Object> subExpression)
            : base(subExpression)
        {
        }

        public override object Evaluate()
        {
            return (Complex)ProbabilityIntegrals.Erfc(SubExpression.EvaluateAsReal());
        }
    }
}
