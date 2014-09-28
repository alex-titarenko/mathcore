using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace TAlex.MathCore.UnitConversion.Units
{
    public class TemperatureUnit : Unit
    {
        public Func<decimal, decimal> FromCelsius { get; private set; }
        public Func<decimal, decimal> ToCelsius { get; private set; }


        public TemperatureUnit(string name, string plural, string symbol, Func<decimal, decimal> fromCelsius, Func<decimal, decimal> toCelsius)
            : base(name, plural, symbol)
        {
            FromCelsius = fromCelsius;
            ToCelsius = toCelsius;
        }


        public override decimal Convert(decimal sourceValue, Unit outputUnit)
        {
            return ((TemperatureUnit)outputUnit).FromCelsius(ToCelsius(sourceValue));
        }
    }
}
