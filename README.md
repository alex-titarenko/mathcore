# MathCore
![Build](https://github.com/alex-titarenko/mathcore/workflows/Build/badge.svg?branch=master)

Advanced .NET math library (.NET Standard).

## Structure
* **MathCore** - base library, contains the following features: complex numbers, fractions, complex polynomials, unit conversion, coordinate system conversion, numeric utils and some extensions for Math class.
* **MathCore.LinearAlgebra** - linear algebra extension for math core, contains the following features: complex matrices, eigen problem solver, bunch operations with matrices (concat, add, sub, mult, rise to power, multiply, sqrt, inverse, trace, determinant and etc.).
* **MathCore.NumericalAnalysis** - numeric analysis extension for math core, contains the following features: equation solver, interpolation, numeric integration, optimization.
* **MathCore.SpecialFunctions** - special functions extension for math core, contains the following features: calculating of error functions and etc.
* **MathCore.Statistics** - statistics extension for math core, contains the following features: distribution (normal, uniform, exponential), calculation of mean, mode, correlation, standard deviation and etc.
* **MathCore.Optimization** - optimization extension for math core, contains implementation of simple genetic algorithm and cellular genetic algorithm.
* **MathCore.ExpressionsBase** - base library, which allow from usual string make expression tree to be able evaluate math expression of any level of complexity.
* **MathCore.ComplexExpressions** - extension for expression base library, which adds support complex numbers in expressions.
* **MathCore.ComplexExpressions.Extensions** - sets of functions and constants for complex expressions library.

## Examples of usage
How to initialize expression tree builder:
```C#
var targetAssembly = Assembly.LoadFrom("TAlex.MathCore.ComplexExpressions.Extensions.dll");

var constantFactory = new ConstantFlyweightFactory<object>();
constantFactory.LoadFromAssemblies(new List<Assembly> { targetAssembly });

var functionFactory = new FunctionFactory<object>();
functionFactory.LoadFromAssemblies(new List<Assembly> { targetAssembly });

var expressionTreeBuilder = new ComplexExpressionTreeBuilder
{
    ConstantFactory = constantFactory,
    FunctionFactory = functionFactory
};
```
How to use expression tree builder:
```C#
var tree = expressionTreeBuilder.BuildTree("abs(2+4.1i)*9i");
object actual = tree.Evaluate(); // 41.0562...

tree = expressionTreeBuilder.BuildTree("integ(sin(x)**x, 0, 100, x)");
actual = tree.Evaluate(); // 7.4990012... + 0.13462383i...

tree = expressionTreeBuilder.BuildTree("lsolve({2, 3; 5i, 14}, {2; -1})+10");
actual = tree.Evaluate(); // {10.860258 + 0.46085233i; 10.093162 - 0.30723489i}
```

Unit conversion example:
```C#
var value = UnitConverter.Convert(23, Length.Inch, Length.Centimeter); // 58.42
```

Matrix multiplication:
```C#
var a = new CMatrix(new Complex[,] { {2, 3}, {5, 8} });
var b = new CMatrix(new Complex[,] { {1, 1}, {18, -1} });

var result = a * b; // {56, -1; 149, -3}
```

## Get it on NuGet!
```
Install-Package TAlex.MathCore
Install-Package TAlex.MathCore.LinearAlgebra
Install-Package TAlex.MathCore.NumericalAnalysis
Install-Package TAlex.MathCore.SpecialFunctions
Install-Package TAlex.MathCore.Statistics
Install-Package TAlex.MathCore.Optimization
Install-Package TAlex.MathCore.ExpressionsBase
Install-Package TAlex.MathCore.ComplexExpressions
Install-Package TAlex.MathCore.ComplexExpressions.Extensions
```

## License
MathCore is under the [MIT license](LICENSE.md).
