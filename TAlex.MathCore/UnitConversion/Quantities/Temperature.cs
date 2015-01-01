using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TAlex.MathCore.UnitConversion.Quantities.Annotation;
using TAlex.MathCore.UnitConversion.Units;


namespace TAlex.MathCore.UnitConversion.Quantities
{
    [Quantity("F", "C")]
    public class Temperature : Quantity
    {
        public static readonly CustomUnit Celsius = new CustomUnit("Celsius", "Celsius", "C", C => C, C => C);
        public static readonly CustomUnit Fahrenheit = new CustomUnit("Fahrenheit", "Fahrenheit", "F", C => C * (9M / 5) + 32, F => (F - 32) * (5M / 9));
        public static readonly CustomUnit Kelvin = new CustomUnit("Kelvin", "Kelvin", "K", C => C + 273.15M, K => K - 273.15M);
        public static readonly CustomUnit Rankine = new CustomUnit("Rankine", "Rankine", "R", C => (C + 273.15M) * (9M / 5), R => (R - 491.67M) * (5M / 9));
        public static readonly CustomUnit Delisle = new CustomUnit("Delisle", "Delisle", "De", C => (100 - C) * 1.5M, De => 100 - De * (2M / 3));
        public static readonly CustomUnit Newton = new CustomUnit("Newton", "Newton", "N", C => C * (33M / 100), N => N * (100M / 33));

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
                    Kelvin,
                    Rankine,
                    Delisle,
                    Newton
                };
            }
        }
    }
}
