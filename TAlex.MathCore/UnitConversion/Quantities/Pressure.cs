using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TAlex.MathCore.UnitConversion.Units;


namespace TAlex.MathCore.UnitConversion.Quantities
{
    public class Pressure : Quantity
    {
        public static readonly LinearUnit Pascal = new LinearUnit("Pascal", "Pascal", "Pa", 1);
        public static readonly LinearUnit KiloPascal = new LinearUnit("Kilo Pascal", "Kilo Pascal", "kPa", 1000);
        public static readonly LinearUnit Bar = new LinearUnit("Bar", "Bar", "bar", 100000);
        public static readonly LinearUnit MillimeterOfMercury = new LinearUnit("Millimeter of mercury", "Millimeters of mercury", "mmHg", 133.3224M);
        public static readonly LinearUnit PaundPerSquareInch = new LinearUnit("PSI", "PSI", "psi", 6894.757M);
        public static readonly LinearUnit Atmosphere = new LinearUnit("Atmosphere", "Atmospheres", "atm", 101325);


        public override string Name
        {
            get { return "Pressure"; }
        }

        public override List<Units.Unit> Units
        {
            get
            {
                return new List<Units.Unit>
                {
                    Pascal,
                    KiloPascal,
                    Bar,
                    MillimeterOfMercury,
                    PaundPerSquareInch,
                    Atmosphere
                };
            }
        }
    }
}
