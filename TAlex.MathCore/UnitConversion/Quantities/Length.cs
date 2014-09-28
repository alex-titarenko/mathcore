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

        public static readonly LinearUnit Nanometer = new LinearUnit("Nanometer", "Nanometers", "nm", 0.000000001M);
        public static readonly LinearUnit Micron = new LinearUnit("Micron", "Microns", "µm", 0.000001M);
        public static readonly LinearUnit Millimeter = new LinearUnit("Millimeter", "Millimeters", "mm", 0.001M);
        public static readonly LinearUnit Centimeter = new LinearUnit("Centimeter", "Centimeters", "cm", 0.01M);
        public static readonly LinearUnit Decimeter = new LinearUnit("Decimeter", "Decimeters", "dm", 0.1M);
        public static readonly LinearUnit Meter = new LinearUnit("Meter", "Meters", "m", 1);
        public static readonly LinearUnit Kilometer = new LinearUnit("Kilometer", "Kilometers", "km", 1000);
        public static readonly LinearUnit Inch = new LinearUnit("Inch", "Inches", "in", 0.0254M);
        public static readonly LinearUnit Feet = new LinearUnit("Feet", "Feet", "ft", 0.3048M);
        public static readonly LinearUnit Yard = new LinearUnit("Yard", "Yards", "yd", 0.9144M);
        public static readonly LinearUnit Mile = new LinearUnit("Mile", "Miles", "mi", 1609.344M);
        public static readonly LinearUnit NauticalMile = new LinearUnit("Nautical Mile", "Nautical Miles", "nautical mi", 1852);


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
