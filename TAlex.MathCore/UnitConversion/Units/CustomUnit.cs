using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace TAlex.MathCore.UnitConversion.Units
{
    public class CustomUnit : Unit
    {
        public Func<decimal, decimal> FromBase { get; private set; }
        public Func<decimal, decimal> ToBase { get; private set; }


        public CustomUnit(string name, string plural, string symbol, Func<decimal, decimal> fromBase, Func<decimal, decimal> toBase)
            : base(name, plural, symbol)
        {
            FromBase = fromBase;
            ToBase = toBase;
        }


        public override decimal Convert(decimal sourceValue, Unit outputUnit)
        {
            return ((CustomUnit)outputUnit).FromBase(ToBase(sourceValue));
        }
    }
}
