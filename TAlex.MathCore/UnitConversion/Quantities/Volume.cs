using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TAlex.MathCore.UnitConversion.Units;


namespace TAlex.MathCore.UnitConversion.Quantities
{
    public class Volume : Quantity
    {
        public static readonly LinearUnit Milliliter = new LinearUnit("Milliliter", "Milliliters", "ml", 0.000001M);
        public static readonly LinearUnit Liter = new LinearUnit("Liter", "Liters", "L", 0.001M);
        public static readonly LinearUnit CubicMeter = new LinearUnit("Cubic meter", "Cubic meters", "m3", 1);

        public static readonly LinearUnit CubicInch = new LinearUnit("Cubic inch", "Cubic inches", "cu in", 0.000016387064M);
        public static readonly LinearUnit CubicFeet = new LinearUnit("Cubic feet", "Cubic feet", "cu ft", 0.028316846592M);
        public static readonly LinearUnit CubicYard = new LinearUnit("Cubic yard", "Cubic yards", "cu yd", 0.764554857984M);

        public static readonly LinearUnit TeaspoonUS;
        public static readonly LinearUnit TablespoonUS;
        public static readonly LinearUnit FluidOunceUS;
        public static readonly LinearUnit CupUS;
        public static readonly LinearUnit PintUS;
        public static readonly LinearUnit QuartUS;
        public static readonly LinearUnit GallonUS;


        public override string Name
        {
            get { return "Volume"; }
        }

        public override List<Units.Unit> Units
        {
            get
            {
                return new List<Units.Unit>
                {
                    Milliliter,
                    Liter,
                    CubicMeter,
                    CubicInch,
                    CubicFeet,
                    CubicYard
                };
            }
        }
    }
}
