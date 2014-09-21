using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TAlex.MathCore.UnitConversion.Units
{
    public class DataUnit : Unit
    {
        public decimal Factor { get; private set; }


        public DataUnit(string name, string plural, string symbol, decimal factor)
            : base(name, plural, symbol)
        {
            Factor = factor;
        }


        public override decimal Convert(decimal sourceValue, Unit outputUnit)
        {
            return sourceValue * Factor / ((DataUnit)outputUnit).Factor;
        }
    }
}
