using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TAlex.MathCore.UnitConversion.Quantities.Annotation;
using TAlex.MathCore.UnitConversion.Units;


namespace TAlex.MathCore.UnitConversion.Quantities
{
    [Quantity("Kilowatt", "Horsepower")]
    public class Power : Quantity
    {
        public static readonly LinearUnit Watt = new LinearUnit("Watt", "Watts", "W", 1);
        public static readonly LinearUnit Kilowatt = new LinearUnit("Kilowatt", "Kilowatts", "kW", 1000);
        public static readonly LinearUnit Horsepower = new LinearUnit("Horsepower", "Horsepower", "hp", 745.69987158227022M);
        public static readonly LinearUnit FootPound = new LinearUnit("Foot-pound/minute", "Foot-pounds/minute", "ft lbf/min", 0.02259696580552334M);
        public static readonly LinearUnit BTU = new LinearUnit("BTU/minute", "BTUs/minute", "BTU/min", 17.584264M);


        public override string Name
        {
            get { return "Power"; }
        }

        public override List<Units.Unit> Units
        {
            get
            {
                return new List<Units.Unit>
                {
                    Watt,
                    Kilowatt,
                    Horsepower,
                    FootPound,
                    BTU
                };
            }
        }
    }
}
