using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TAlex.MathCore.UnitConversion.Quantities.Annotation;
using TAlex.MathCore.UnitConversion.Units;


namespace TAlex.MathCore.UnitConversion.Quantities
{
    [Quantity("Fahrenheit", "Celsius")]
    public class Temperature : Quantity
    {
        public static readonly CustomUnit Celsius = new CustomUnit("Celsius", "Celsius", "C", C => C, C => C);
        public static readonly CustomUnit Fahrenheit = new CustomUnit("Fahrenheit", "Fahrenheit", "F", C => C * (9M / 5) + 32, F => (F - 32) * (5M / 9));
        public static readonly CustomUnit Kelvin = new CustomUnit("Kelvin", "Kelvin", "K", C => C + 273.15M, K => K - 273.15M);


        public override string Name
        {
            get { return "Temperature"; }
        }

        public override List<Units.Unit> Units
        {
            get
            {
                return new List<Units.Unit>
                {
                    Celsius,
                    Fahrenheit,
                    Kelvin
                };
            }
        }
    }
}
