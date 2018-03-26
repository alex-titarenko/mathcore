using System;
using System.Collections.Generic;
using TAlex.MathCore.ExpressionEvaluation.Trees;
using TAlex.MathCore.ExpressionEvaluation.Trees.Metadata;
using TAlex.MathCore.LinearAlgebra;
using TAlex.MathCore.Performance;
using TAlex.MathCore.Statistics.Distributions;


namespace TAlex.MathCore.ExpressionEvaluation.ComplexExpressions.Functions
{
    [DisplayName("Uniform PDF")]
    [Category(Categories.Distributions)]
    [Section(Sections.UniformDistribution)]
    [Description("Calculates the value of the probability density function for uniform distribution.")]
    [FunctionSignature("unifpdf", "real a", "real b", "real x")]
    [ExampleUsage("unifpdf(1, 9, 7.5)", "0.125")]
    [ExampleUsage("unifpdf(1, 9, -10)", "0")]
    [ExampleUsage("unifpdf(1, 9, 20)", "0")]
    public class UniformPdfFuncExpression : TernaryExpression<Object>
    {
        public UniformPdfFuncExpression(Expression<Object> aExpression, Expression<Object> bExpression, Expression<Object> xExpression)
            : base(aExpression, bExpression, xExpression)
        {
        }

        public override object Evaluate()
        {
            double a = FirstExpression.EvaluateAsReal();
            double b = SecondExpression.EvaluateAsReal();
            double x = ThirdExpression.EvaluateAsReal();

            return (Complex)new UniformDistribution(a, b).ProbabilityDensityFunction(x);
        }
    }

    [DisplayName("Uniform CDF")]
    [Category(Categories.Distributions)]
    [Section(Sections.UniformDistribution)]
    [Description("Calculates the value of the cumulative distribution function for uniform distribution.")]
    [FunctionSignature("unifcdf", "real a", "real b", "real x")]
    [ExampleUsage("unifcdf(1, 9, 7.5)", "0.8125")]
    [ExampleUsage("unifcdf(1, 9, -10)", "0")]
    [ExampleUsage("unifcdf(1, 9, 20)", "1")]
    public class UniformCdfFuncExpression : TernaryExpression<Object>
    {
        public UniformCdfFuncExpression(Expression<Object> aExpression, Expression<Object> bExpression, Expression<Object> xExpression)
            : base(aExpression, bExpression, xExpression)
        {
        }

        public override object Evaluate()
        {
            double a = FirstExpression.EvaluateAsReal();
            double b = SecondExpression.EvaluateAsReal();
            double x = ThirdExpression.EvaluateAsReal();

            return (Complex)new UniformDistribution(a, b).CumulativeDistributionFunction(x);
        }
    }

    [DisplayName("Uniform random")]
    [Category(Categories.Distributions)]
    [Section(Sections.UniformDistribution)]
    [Description("Returns a vector of random numbers having the uniform distribution.")]
    [FunctionSignature("unifrnd", "real a", "real b", "integer count")]
    [ExampleUsage("unifrnd(5.5, 9.8, 3)", "{ 6.86095277069181; 7.84386934705259; 8.28649120823783 }", CanMultipleResults = true)]
    [ExampleUsage("unifrnd(0, 1, 3)", "{ 0.718950168564427; 0.114844275226278; 0.783870033819168 }", CanMultipleResults = true)]
    public class UniformRandomFuncExpression : TernaryExpression<Object>
    {
        private Random _random;

        public UniformRandomFuncExpression(Expression<Object> aExpression, Expression<Object> bExpression, Expression<Object> countExpression)
            : base(aExpression, bExpression, countExpression)
        {
            _random = new Random(Guid.NewGuid().GetHashCode());
        }

        public override object Evaluate()
        {
            double a = FirstExpression.EvaluateAsReal();
            double b = SecondExpression.EvaluateAsReal();
            int count = ThirdExpression.EvaluateAsInt32();
            PerformanceManager.Current.EnsureAcceptableArraySize(count);

            double[] v = new double[count];
            new UniformDistribution(a, b).GetRandomVariables(_random, v);
            return new CMatrix(v);
        }
    }

    [DisplayName("Uniform random")]
    [Category(Categories.Distributions)]
    [Section(Sections.UniformDistribution)]
    [Description("Returns uniformly distributed random number.")]
    [FunctionSignature("rnd", "real x")]
    [FunctionSignature("rnd", "real a", "real b")]
    [ExampleUsage("rnd(2.5)", "2.285463", CanMultipleResults = true)]
    [ExampleUsage("rnd(3, 4)", "3.5568", CanMultipleResults = true)]
    public class UniformRandom2FuncExpression : BinaryExpression<Object>
    {
        private Random _random;

        public UniformRandom2FuncExpression(Expression<Object> xExpression)
            : this(new ScalarExpression<Object>(Complex.Zero), xExpression)
        {
        }

        public UniformRandom2FuncExpression(Expression<Object> aExpression, Expression<Object> bExpression)
            : base(aExpression, bExpression)
        {
            _random = new Random(Guid.NewGuid().GetHashCode());
        }

        public override object Evaluate()
        {
            double a = LeftExpression.EvaluateAsReal();
            double b = RightExpression.EvaluateAsReal();

            return (Complex)new UniformDistribution(a, b).GetRandomVariable(_random);
        }
    }


    [DisplayName("Normal PDF")]
    [Category(Categories.Distributions)]
    [Section(Sections.NormalDistribution)]
    [Description("Calculates the value of the probability density function for normal distribution.")]
    [FunctionSignature("normpdf", "real mean", "real stdev", "real x")]
    [ExampleUsage("normpdf(2.5, 9, 7)", "0.0391183696404777")]
    [ExampleUsage("normpdf(2.5, 5, 15)", "0.00350566009871371")]
    public class NormalPdfFuncExpression : TernaryExpression<Object>
    {
        public NormalPdfFuncExpression(Expression<Object> meanExpression, Expression<Object> stdevExpression, Expression<Object> xExpression)
            : base(meanExpression, stdevExpression, xExpression)
        {
        }

        public override object Evaluate()
        {
            double mean = FirstExpression.EvaluateAsReal();
            double stdev = SecondExpression.EvaluateAsReal();
            double x = ThirdExpression.EvaluateAsReal();

            return (Complex)new NormalDistribution(mean, stdev).ProbabilityDensityFunction(x);
        }
    }

    [DisplayName("Normal CDF")]
    [Category(Categories.Distributions)]
    [Section(Sections.NormalDistribution)]
    [Description("Calculates the value of the cumulative distribution function for normal distribution.")]
    [FunctionSignature("normcdf", "real mean", "real stdev", "real x")]
    [ExampleUsage("normcdf(-2, 0.5, -1)", "0.977249868051821")]
    [ExampleUsage("normcdf(0, 1, 1)", "0.841344746068543")]
    public class NormalCdfFuncExpression : TernaryExpression<Object>
    {
        public NormalCdfFuncExpression(Expression<Object> meanExpression, Expression<Object> stdevExpression, Expression<Object> xExpression)
            : base(meanExpression, stdevExpression, xExpression)
        {
        }

        public override object Evaluate()
        {
            double mean = FirstExpression.EvaluateAsReal();
            double stdev = SecondExpression.EvaluateAsReal();
            double x = ThirdExpression.EvaluateAsReal();

            return (Complex)new NormalDistribution(mean, stdev).CumulativeDistributionFunction(x);
        }
    }

    [DisplayName("Normal random")]
    [Category(Categories.Distributions)]
    [Section(Sections.NormalDistribution)]
    [Description("Returns a vector of random numbers having the normal distribution using Box–Muller transform.")]
    [FunctionSignature("normrnd", "real mean", "real stdev", "integer count")]
    [ExampleUsage("normrnd(-2, 0.5, 3)", "{ -1.6537461020031; -1.96337863842117; -2.10163083882916 }", CanMultipleResults = true)]
    [ExampleUsage("normrnd(0, 1, 3)", "{ -0.914593239994143; 1.44436591846177; -0.266668290044988 }", CanMultipleResults = true)]
    public class NormalRandomFuncExpression : TernaryExpression<Object>
    {
        private Random _random;

        public NormalRandomFuncExpression(Expression<Object> meanExpression, Expression<Object> stdevExpression, Expression<Object> countExpression)
            : base(meanExpression, stdevExpression, countExpression)
        {
            _random = new Random(Guid.NewGuid().GetHashCode());
        }

        public override object Evaluate()
        {
            double mean = FirstExpression.EvaluateAsReal();
            double stdev = SecondExpression.EvaluateAsReal();
            int count = ThirdExpression.EvaluateAsInt32();
            PerformanceManager.Current.EnsureAcceptableArraySize(count);

            double[] v = new double[count];
            new NormalDistribution(mean, stdev).GetRandomVariables(_random, v);
            return new CMatrix(v);
        }
    }


    [DisplayName("Exponential PDF")]
    [Category(Categories.Distributions)]
    [Section(Sections.ExponentialDistribution)]
    [Description("Calculates the value of the probability density function for exponential distribution.")]
    [FunctionSignature("exppdf", "real rate", "real x")]
    [ExampleUsage("exppdf(5, -7)", "0")]
    [ExampleUsage("exppdf(0.5, 2)", "0.183939720585721")]
    public class ExponentialPdfFuncExpression : BinaryExpression<Object>
    {
        public ExponentialPdfFuncExpression(Expression<Object> rateExpression, Expression<Object> xExpression)
            : base(rateExpression, xExpression)
        {
        }

        public override object Evaluate()
        {
            double rate = LeftExpression.EvaluateAsReal();
            double x = RightExpression.EvaluateAsReal();

            return (Complex)new ExponentialDistribution(rate).ProbabilityDensityFunction(x);
        }
    }

    [DisplayName("Exponential CDF")]
    [Category(Categories.Distributions)]
    [Section(Sections.ExponentialDistribution)]
    [Description("Calculates the value of the cumulative distribution function for exponential distribution.")]
    [FunctionSignature("expcdf", "real rate", "real x")]
    [ExampleUsage("expcdf(2, -1)", "0")]
    [ExampleUsage("expcdf(0.8, 1)", "0.550671035882778")]
    public class ExponentialCdfFuncExpression : BinaryExpression<Object>
    {
        public ExponentialCdfFuncExpression(Expression<Object> rateExpression, Expression<Object> xExpression)
            : base(rateExpression, xExpression)
        {
        }

        public override object Evaluate()
        {
            double rate = LeftExpression.EvaluateAsReal();
            double x = RightExpression.EvaluateAsReal();

            return (Complex)new ExponentialDistribution(rate).CumulativeDistributionFunction(x);
        }
    }

    [DisplayName("Exponential random")]
    [Category(Categories.Distributions)]
    [Section(Sections.ExponentialDistribution)]
    [Description("Returns a vector of random numbers having the exponential distribution.")]
    [FunctionSignature("exprnd", "real rate", "integer count")]
    [ExampleUsage("exprnd(1.3, 3)", "{ 0.117480920987119; 0.402440526973026; 0.541124168219723 }", CanMultipleResults = true)]
    [ExampleUsage("exprnd(0.8, 3)", "{ 0.127426151753758; 1.48689597402376; 0.513328030845467 }", CanMultipleResults = true)]
    public class ExponentialRandomFuncExpression : BinaryExpression<Object>
    {
        private Random _random;

        public ExponentialRandomFuncExpression(Expression<Object> rateExpression, Expression<Object> countExpression)
            : base(rateExpression, countExpression)
        {
            _random = new Random(Guid.NewGuid().GetHashCode());
        }

        public override object Evaluate()
        {
            double rate = LeftExpression.EvaluateAsReal();
            int count = RightExpression.EvaluateAsInt32();
            PerformanceManager.Current.EnsureAcceptableArraySize(count);

            double[] v = new double[count];
            new ExponentialDistribution(rate).GetRandomVariables(_random, v);
            return new CMatrix(v);
        }
    }
}
