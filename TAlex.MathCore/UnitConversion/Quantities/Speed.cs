using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TAlex.MathCore.UnitConversion.Units;


namespace TAlex.MathCore.UnitConversion.Quantities
{
    public class Speed : Quantity
    {
        public static readonly LinearUnit CentimeterPerSecond = new LinearUnit("Cm/S", "Cm/S", "cm/s", 0.01M);
        public static readonly LinearUnit MeterPerSecond = new LinearUnit("M/S", "M/S", "m/s", 1);
        public static readonly LinearUnit KilometerPerHour = new LinearUnit("Km/H", "Km/H", "km/h", 1 / 3.6M);
        public static readonly LinearUnit FeetPerSecond = new LinearUnit("Ft/S", "Ft/S", "fps", 0.3048M);
        public static readonly LinearUnit MilesPerHour = new LinearUnit("Mi/H", "Mi/H", "mph", 0.44704M);
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
