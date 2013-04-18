using System;
using System.Collections.Generic;
using System.ComponentModel;
using TAlex.MathCore.ExpressionEvaluation.Trees;
using TAlex.MathCore.ExpressionEvaluation.Trees.Metadata;
using TAlex.MathCore.Graphing;
using TAlex.MathCore.LinearAlgebra;


namespace TAlex.MathCore.ExpressionEvaluation.ComplexExpressions.Functions
{
    [DisplayName("Cartesian to polar")]
    [Category(Categories.Graphing)]
    [Description("Transforms Cartesian coordinates to polar.")]
    [FunctionSignature("cart2pol", "real x", "real y")]
    [ExampleUsage("cart2pol(9, -2)", "{9.2195444572928889; -0.21866894587394195}")]
    public class CartesianToPolarFuncExpression : BinaryExpression<Object>
    {
        public CartesianToPolarFuncExpression(Expression<Object> xExpression, Expression<Object> yExpression)
            : base(xExpression, yExpression)
        {
        }

        public override object Evaluate()
        {
            Object x = LeftExpression.Evaluate();
            Object y = RightExpression.Evaluate();
            return new CMatrix(CoordSysConverter.CartesianToPolar(ConvertEx.AsDouble(x), ConvertEx.AsDouble(y)));
        }
    }

    [DisplayName("Polar to Cartesian")]
    [Category(Categories.Graphing)]
    [Description("Transforms polar coordinates to Cartesian.")]
    [FunctionSignature("pol2cart", "real r", "real theta")]
    [ExampleUsage("pol2cart(9.2195444572928889, -0.21866894587394195)", "{9; -2}")]
    public class PolarToCartesianFuncExpression : BinaryExpression<Object>
    {
        public PolarToCartesianFuncExpression(Expression<Object> rExpression, Expression<Object> thetaExpression)
            : base(rExpression, thetaExpression)
        {
        }

        public override object Evaluate()
        {
            Object r = LeftExpression.Evaluate();
            Object theta = RightExpression.Evaluate();
            return new CMatrix(CoordSysConverter.PolarToCartesian(ConvertEx.AsDouble(r), ConvertEx.AsDouble(theta)));
        }
    }

    [DisplayName("Cartesian to spherical")]
    [Category(Categories.Graphing)]
    [Description("Transforms Cartesian coordinates to spherical.")]
    [FunctionSignature("cart2sph", "real x", "real y", "real z")]
    [ExampleUsage("cart2sph(9, -2, 3)", "{9.6953597148326587; -0.21866894587394195; 1.2562065842346306}")]
    public class CartesianToSphericalFuncExpression : TernaryExpression<Object>
    {
        public CartesianToSphericalFuncExpression(Expression<Object> xExpression, Expression<Object> yExpression, Expression<Object> zExpression)
            : base(xExpression, yExpression, zExpression)
        {
        }

        public override object Evaluate()
        {
            Object x = FirstExpression.Evaluate();
            Object y = SecondExpression.Evaluate();
            Object z = ThirdExpression.Evaluate();
            return new CMatrix(CoordSysConverter.CartesianToSpherical(ConvertEx.AsDouble(x), ConvertEx.AsDouble(y), ConvertEx.AsDouble(z)));
        }
    }

    [DisplayName("Spherical to Cartesian")]
    [Category(Categories.Graphing)]
    [Description("Transforms spherical coordinates to Cartesian.")]
    [FunctionSignature("sph2cart", "real r", "real theta, real phi")]
    [ExampleUsage("sph2cart(9.6953597148326587, -0.21866894587394195, 1.2562065842346306)", "{9; -2; 3}")]
    public class SphericalToCartesianFuncExpression : TernaryExpression<Object>
    {
        public SphericalToCartesianFuncExpression(Expression<Object> rExpression, Expression<Object> thetaExpression, Expression<Object> phiExpression)
            : base(rExpression, thetaExpression, phiExpression)
        {
        }

        public override object Evaluate()
        {
            Object r = FirstExpression.Evaluate();
            Object theta = SecondExpression.Evaluate();
            Object phi = ThirdExpression.Evaluate();
            return new CMatrix(CoordSysConverter.SphericalToCartesian(ConvertEx.AsDouble(r), ConvertEx.AsDouble(theta), ConvertEx.AsDouble(phi)));
        }
    }

    [DisplayName("Cartesian to cylindrical")]
    [Category(Categories.Graphing)]
    [Description("Transforms Cartesian coordinates to cylindrical.")]
    [FunctionSignature("cart2cyl", "real x", "real y", "real z")]
    [ExampleUsage("cart2cyl(9, -2, 3)", "{9.2195444572928889; -0.21866894587394195; 3}")]
    public class CartesianToCylindricalFuncExpression : TernaryExpression<Object>
    {
        public CartesianToCylindricalFuncExpression(Expression<Object> xExpression, Expression<Object> yExpression, Expression<Object> zExpression)
            : base(xExpression, yExpression, zExpression)
        {
        }

        public override object Evaluate()
        {
            Object x = FirstExpression.Evaluate();
            Object y = SecondExpression.Evaluate();
            Object z = ThirdExpression.Evaluate();
            return new CMatrix(CoordSysConverter.CartesianToCylindrical(ConvertEx.AsDouble(x), ConvertEx.AsDouble(y), ConvertEx.AsDouble(z)));
        }
    }

    [DisplayName("Cylindrical to Cartesian")]
    [Category(Categories.Graphing)]
    [Description("Transforms cylindrical coordinates to Cartesian.")]
    [FunctionSignature("cyl2cart", "real r", "real theta, real z")]
    [ExampleUsage("cyl2cart(9.2195444572928889, -0.21866894587394195, 3)", "{9; -2; 3}")]
    public class CylindricalToCartesianFuncExpression : TernaryExpression<Object>
    {
        public CylindricalToCartesianFuncExpression(Expression<Object> rExpression, Expression<Object> thetaExpression, Expression<Object> zExpression)
            : base(rExpression, thetaExpression, zExpression)
        {
        }

        public override object Evaluate()
        {
            Object r = FirstExpression.Evaluate();
            Object theta = SecondExpression.Evaluate();
            Object z = ThirdExpression.Evaluate();
            return new CMatrix(CoordSysConverter.CylindricalToCartesian(ConvertEx.AsDouble(r), ConvertEx.AsDouble(theta), ConvertEx.AsDouble(z)));
        }
    }
}
