using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace TAlex.MathCore.UnitConversion.Units
{
    public class LinearUnit : Unit
    {
        public decimal Factor { get; private set; }


        public LinearUnit(string name, string plural, string symbol, decimal factor)
            : base(name, plural, symbol)
        {
            Factor = factor;
        }


        public override decimal Convert(decimal sourceValue, Unit outputUnit)
        {
            return sourceValue / Factor * ((LinearUnit)outputUnit).Factor;
        }
    }
}
