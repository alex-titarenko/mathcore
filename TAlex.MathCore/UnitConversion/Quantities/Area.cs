using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TAlex.MathCore.UnitConversion.Quantities.Annotation;
using TAlex.MathCore.UnitConversion.Units;


namespace TAlex.MathCore.UnitConversion.Quantities
{
    [Quantity("Square feet", "Square meter")]
    public class Area : Quantity
    {
        public static readonly LinearUnit SquareMillimeter = new LinearUnit("Square millimeter", "Square millimeters", "sq mm", 0.000001M);
        public static readonly LinearUnit SquareCentimeter = new LinearUnit("Square centimeter", "Square centimeters", "sq cm", 0.0001M);
        public static readonly LinearUnit SquareMeter = new LinearUnit("Square meter", "Square meters", "sq m", 1);
        public static readonly LinearUnit Hectare = new LinearUnit("Hectare", "Hectares", "ha", 10000);
        public static readonly LinearUnit SquareKilometer = new LinearUnit("Square kilometer", "Square kilometers", "sq km", 1000000);
        public static readonly LinearUnit SquareInch = new LinearUnit("Square inch", "Square inches", "sq in", 0.00064516M);
        public static readonly LinearUnit SquareFeet = new LinearUnit("Square feet", "Square feet", "sq ft", 0.09290304M);
        public static readonly LinearUnit SquareYard = new LinearUnit("Square yard", "Square yards", "sq yd", 0.83612736M);
        public static readonly LinearUnit Acre = new LinearUnit("Acre", "Acres", "ac", 4046.8564224M);
        public static readonly LinearUnit SquareMile = new LinearUnit("Square mile", "Square miles", "sq mi", 2589988.110336M);


        public override string Name
        {
            get { return "Area"; }
        }

        public override List<Units.Unit> Units
        {
            get
            {
                return new List<Units.Unit>
                {
                    SquareMillimeter,
                    SquareCentimeter,
                    SquareMeter,
                    Hectare,
                    SquareKilometer,
                    SquareInch,
                    SquareFeet,
                    SquareYard,
                    Acre,
                    SquareMile
                };
            }
        }
    }
}
