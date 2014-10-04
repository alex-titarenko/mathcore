using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TAlex.MathCore.UnitConversion.Quantities.Annotation;
using TAlex.MathCore.UnitConversion.Units;


namespace TAlex.MathCore.UnitConversion.Quantities
{
    [Quantity("Mi/H", "Km/H")]
    public class Speed : Quantity
    {
        public static readonly LinearUnit CentimeterPerSecond = new LinearUnit("Cm/S", "Centimeters per second", "cm/s", 0.01M);
        public static readonly LinearUnit MeterPerSecond = new LinearUnit("M/S", "Meters per second", "m/s", 1);
        public static readonly LinearUnit KilometerPerHour = new LinearUnit("Km/H", "Kilometers per hour", "km/h", 1 / 3.6M);
        public static readonly LinearUnit FeetPerSecond = new LinearUnit("Ft/S", "Feet per second", "fps", 0.3048M);
        public static readonly LinearUnit MilesPerHour = new LinearUnit("Mi/H", "Miles per hour", "mph", 0.44704M);
        public static readonly LinearUnit Knot = new LinearUnit("Knot", "Knots", "kn", 1852M / 3600);
        public static readonly LinearUnit Mach = new LinearUnit("Mach", "Mach", "M", 340.2933M);


        public override string Name
        {
            get { return "Speed"; }
        }


        public override List<Units.Unit> Units
        {
            get
            {
                return new List<Units.Unit>
                {
                    CentimeterPerSecond,
                    MeterPerSecond,
                    KilometerPerHour,
                    FeetPerSecond,
                    MilesPerHour,
                    Knot,
                    Mach
                };
            }
        }
    }
}
