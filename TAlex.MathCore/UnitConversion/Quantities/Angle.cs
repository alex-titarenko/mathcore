using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TAlex.MathCore.UnitConversion.Quantities.Annotation;
using TAlex.MathCore.UnitConversion.Units;


namespace TAlex.MathCore.UnitConversion.Quantities
{
    [Quantity("rad", "°")]
    public class Angle : Quantity
    {
        public static readonly LinearUnit Degree = new LinearUnit("Degree", "Degrees", "°", (decimal)Math.PI / 180);
        public static readonly LinearUnit Gradian = new LinearUnit("Gradian", "Gradians", "gon", (decimal)Math.PI / 200);
        public static readonly LinearUnit Radian = new LinearUnit("Radian", "Radians", "rad", 1);
        public static readonly LinearUnit AngularMil = new LinearUnit("Angular mil", "Angular mil", "µ", (decimal)Math.PI * 2 / 6400);
        public static readonly LinearUnit Arcminute = new LinearUnit("Arcminute", "Arcminute", "′", (decimal)Math.PI / 10800);
        public static readonly LinearUnit Arcsecond = new LinearUnit("Arcsecond", "Arcsecond", "\"", (decimal)Math.PI / 64800);


        public override string Name
        {
            get { return "Angle"; }
        }

        public override List<Units.Unit> Units
        {
            get
            {
                return new List<Units.Unit>
                {
                    Degree,
                    Gradian,
                    Radian,
                    AngularMil,
                    Arcminute,
                    Arcsecond
                };
            }
        }
    }
}
