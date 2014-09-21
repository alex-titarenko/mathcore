using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TAlex.MathCore.UnitConversion.Units;


namespace TAlex.MathCore.UnitConversion.Quantities
{
    public class Length : Quantity
    {
        public override string Name
        {
            get { return "Length"; }
        }

        public static readonly LinearUnit Nanometer = new LinearUnit("Nanometer", "Nanometers", "nm", 1000000000);
        public static readonly LinearUnit Micron = new LinearUnit("Micron", "Microns", "µm", 1000000);
        public static readonly LinearUnit Millimeter = new LinearUnit("Millimeter", "Millimeters", "mm", 1000);
        public static readonly LinearUnit Centimeter = new LinearUnit("Centimeter", "Centimeters", "cm", 100);
        public static readonly LinearUnit Decimeter = new LinearUnit("Decimeter", "Decimeters", "dm", 10);
        public static readonly LinearUnit Meter = new LinearUnit("Meter", "Meters", "m", 1);
        public static readonly LinearUnit Kilometer = new LinearUnit("Kilometer", "Kilometers", "km", 0.001M);
        public static readonly LinearUnit Inch = new LinearUnit("Inch", "Inches", "in", 39.37007874M);
        public static readonly LinearUnit Feet = new LinearUnit("Feet", "Feet", "ft", 3.280839895M);
        public static readonly LinearUnit Yard = new LinearUnit("Yard", "Yards", "yd", 1.0936132983M);
        public static readonly LinearUnit Mile = new LinearUnit("Mile", "Miles", "mi", 0.00062137119224M);
        public static readonly LinearUnit NauticalMile = new LinearUnit("Nautical Mile", "Nautical Miles", "nautical mi", 0.00053995680345M);


        public override List<Unit> Units
        {
            get
            {
                return new List<Unit>
                {
                    Nanometer,
                    Micron,
                    Millimeter,
                    Centimeter,
                    Decimeter,
                    Meter,
                    Kilometer,
                    Inch,
                    Feet,
                    Yard,
                    Mile,
                    NauticalMile
                };
            }
        }
    }
}
