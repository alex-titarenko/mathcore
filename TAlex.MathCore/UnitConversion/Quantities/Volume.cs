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

        public static readonly LinearUnit FluidOunceUS = new LinearUnit("Fluid ounce (US)", "Fluid ounces (US)", "US fl oz", 0.0000295735295625M);
        public static readonly LinearUnit PintUS = new LinearUnit("Pint (US)", "Pints (US)", "pt (US fl)", 0.000473176473M);
        public static readonly LinearUnit QuartUS = new LinearUnit("Quart (US)", "Quarts (US)", "qt (US)", 0.000946352946M);
        public static readonly LinearUnit GallonUS = new LinearUnit("Gallon (US)", "Gallons (US)", "gal (US)", 0.003785411784M);

        public static readonly LinearUnit FluidOunceUK = new LinearUnit("Fluid ounce (UK)", "Fluid ounces (UK)", "UK fl oz", 0.0000284130625M);
        public static readonly LinearUnit PintUK = new LinearUnit("Pint (UK)", "Pints (UK)", "pt (UK fl)", 0.00056826125M);
        public static readonly LinearUnit QuartUK = new LinearUnit("Quart (UK)", "Quarts (UK)", "qt (UK)", 0.0011365225M);
        public static readonly LinearUnit GallonUK = new LinearUnit("Gallon (UK)", "Gallons (UK)", "gal (UK)", 0.00454609M);


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
                    CubicYard,
                    FluidOunceUS,
                    PintUS,
                    QuartUS,
                    GallonUS,
                    FluidOunceUK,
                    PintUK,
                    QuartUK,
                    GallonUK
                };
            }
        }
    }
}
