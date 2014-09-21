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

        public static readonly LinearUnit Millimetre = new LinearUnit("Millimetre", "Millimeters", "mm", 1000);
        public static readonly LinearUnit Centimetre = new LinearUnit("Centimetre", "Centimeters", "cm", 100);
        public static readonly LinearUnit Decimetre = new LinearUnit("Decimetre", "Decimeters", "dm", 10);
        public static readonly LinearUnit Meter = new LinearUnit("Meter", "Meters", "m", 1);
        public static readonly LinearUnit Kilometer = new LinearUnit("Kilometer", "Kilometers", "km", 0.001M);
        public static readonly LinearUnit Inch = new LinearUnit("Inch", "Inches", "in", 39.37007874015748M);


        public override List<Unit> Units
        {
            get
            {
                return new List<Unit>
                {
                    Millimetre,
                    Centimetre,
                    Decimetre,
                    Meter,
                    Kilometer,
                    Inch
                };
            }
        }
    }
}
