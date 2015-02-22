# MathCore
Advanced .NET math library (PCL).

## Structure
* **MathCore** - base library, contains the following features: complex numbers, fractions, complex polynomials, unit conversion, coordinate system conversion, numeric utils and some extensions for Math class.
* **MathCore.LinearAlgebra** - linear algebra extension for math core, contains the following features: complex matrices, eigen problem solver, bunch operations with matrices (concat, add, sub, mult, rise to power, multiply, sqrt, inverse, trace, determinant and etc.).
* **MathCore.NumericalAnalysis** - numeric analysis extension for math core, contains the following features: equation solver, interpolation, numeric integration, optimization.
* **MathCore.SpecialFunctions** - special functions extension for math core, contains the following features: calculating of error functions and etc.
* **MathCore.Statistics** - statistics extension for math core, contains the following features: distribution (normal, uniform, exponential), calculation of mean, mode, correlation, standard deviation and etc.
* **MathCore.ExpressionsBase** - base library, which allow from usual string make expression tree to be able evaluate math expression of any level of complexity.
* **MathCore.ComplexExpressions** - extension for expression base library, which adds support complex numbers in expressions.
* **MathCore.ComplexExpressions.Extensions** - sets of functions and constants for complex expressions library.

## Get it on NuGet!
```
Install-Package TAlex.MathCore
Install-Package TAlex.MathCore.LinearAlgebra
Install-Package TAlex.MathCore.NumericalAnalysis
Install-Package TAlex.MathCore.SpecialFunctions
Install-Package TAlex.MathCore.Statistics
Install-Package TAlex.MathCore.ExpressionsBase
Install-Package TAlex.MathCore.ComplexExpressions
Install-Package TAlex.MathCore.ComplexExpressions.Extensions
```

## License
MathCore is under the [MIT license](https://github.com/T-Alex/MathCore/blob/master/LICENSE.md).
