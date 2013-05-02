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
    [ExampleUsage("cart2pol(9, -2)", "{9.21954445729289; -0.218668945873942}")]
    public class CartesianToPolarFuncExpression : BinaryExpression<Object>
    {
        public CartesianToPolarFuncExpression(Expression<Object> xExpression, Expression<Object> yExpression)
            : base(xExpression, yExpression)
        {
        }

        public override object Evaluate()
        {
            double x = LeftExpression.EvaluateAsDouble();
            double y = RightExpression.EvaluateAsDouble();
            return new CMatrix(CoordSysConverter.CartesianToPolar(x, y));
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
            double r = LeftExpression.EvaluateAsDouble();
            double theta = RightExpression.EvaluateAsDouble();
            return new CMatrix(CoordSysConverter.PolarToCartesian(r, theta));
        }
    }

    [DisplayName("Cartesian to spherical")]
    [Category(Categories.Graphing)]
    [Description("Transforms Cartesian coordinates to spherical.")]
    [FunctionSignature("cart2sph", "real x", "real y", "real z")]
    [ExampleUsage("cart2sph(9, -2, 3)", "{9.69535971483266; -0.218668945873942; 1.25620658423463}")]
    public class CartesianToSphericalFuncExpression : TernaryExpression<Object>
    {
        public CartesianToSphericalFuncExpression(Expression<Object> xExpression, Expression<Object> yExpression, Expression<Object> zExpression)
            : base(xExpression, yExpression, zExpression)
        {
        }

        public override object Evaluate()
        {
            double x = FirstExpression.EvaluateAsDouble();
            double y = SecondExpression.EvaluateAsDouble();
            double z = ThirdExpression.EvaluateAsDouble();
            return new CMatrix(CoordSysConverter.CartesianToSpherical(x, y, z));
        }
    }

    [DisplayName("Spherical to Cartesian")]
    [Category(Categories.Graphing)]
    [Description("Transforms spherical coordinates to Cartesian.")]
    [FunctionSignature("sph2cart", "real r", "real theta", "real phi")]
    [ExampleUsage("sph2cart(9.6953597148326587, -0.21866894587394195, 1.2562065842346306)", "{9; -2; 3}")]
    public class SphericalToCartesianFuncExpression : TernaryExpression<Object>
    {
        public SphericalToCartesianFuncExpression(Expression<Object> rExpression, Expression<Object> thetaExpression, Expression<Object> phiExpression)
            : base(rExpression, thetaExpression, phiExpression)
        {
        }

        public override object Evaluate()
        {
            double r = FirstExpression.EvaluateAsDouble();
            double theta = SecondExpression.EvaluateAsDouble();
            double phi = ThirdExpression.EvaluateAsDouble();
            return new CMatrix(CoordSysConverter.SphericalToCartesian(r, theta, phi));
        }
    }

    [DisplayName("Cartesian to cylindrical")]
    [Category(Categories.Graphing)]
    [Description("Transforms Cartesian coordinates to cylindrical.")]
    [FunctionSignature("cart2cyl", "real x", "real y", "real z")]
    [ExampleUsage("cart2cyl(9, -2, 3)", "{9.21954445729289; -0.218668945873942; 3}")]
    public class CartesianToCylindricalFuncExpression : TernaryExpression<Object>
    {
        public CartesianToCylindricalFuncExpression(Expression<Object> xExpression, Expression<Object> yExpression, Expression<Object> zExpression)
            : base(xExpression, yExpression, zExpression)
        {
        }

        public override object Evaluate()
        {
            double x = FirstExpression.EvaluateAsDouble();
            double y = SecondExpression.EvaluateAsDouble();
            double z = ThirdExpression.EvaluateAsDouble();
            return new CMatrix(CoordSysConverter.CartesianToCylindrical(x, y, z));
        }
    }

    [DisplayName("Cylindrical to Cartesian")]
    [Category(Categories.Graphing)]
    [Description("Transforms cylindrical coordinates to Cartesian.")]
    [FunctionSignature("cyl2cart", "real r", "real theta", "real z")]
    [ExampleUsage("cyl2cart(9.2195444572928889, -0.21866894587394195, 3)", "{9; -2; 3}")]
    public class CylindricalToCartesianFuncExpression : TernaryExpression<Object>
    {
        public CylindricalToCartesianFuncExpression(Expression<Object> rExpression, Expression<Object> thetaExpression, Expression<Object> zExpression)
            : base(rExpression, thetaExpression, zExpression)
        {
        }

        public override object Evaluate()
        {
            double r = FirstExpression.EvaluateAsDouble();
            double theta = SecondExpression.EvaluateAsDouble();
            double z = ThirdExpression.EvaluateAsDouble();
            return new CMatrix(CoordSysConverter.CylindricalToCartesian(r, theta, z));
        }
    }
}
