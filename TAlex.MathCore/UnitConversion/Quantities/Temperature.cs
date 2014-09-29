using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TAlex.MathCore.UnitConversion.Units;

namespace TAlex.MathCore.UnitConversion.Quantities
{
    public class Temperature : Quantity
    {
        public static readonly TemperatureUnit Celsius = new TemperatureUnit("Celsius", "Celsius", "C", C => C, C => C);
        public static readonly TemperatureUnit Fahrenheit = new TemperatureUnit("Fahrenheit", "Fahrenheit", "F", C => C * (9M / 5) + 32, F => (F - 32) * (5M / 9));
        public static readonly TemperatureUnit Kelvin = new TemperatureUnit("Kelvin", "Kelvin", "K", C => C + 273.15M, K => K - 273.15M);


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
