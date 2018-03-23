using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace TAlex.MathCore.UnitConversion.Units
{
    public abstract class Unit
    {
        public string Name { get; private set; }

        public string Plural { get; private set; }

        public string Symbol { get; private set; }


        public Unit(string name, string plural, string symbol)
        {
            Name = name;
            Plural = plural;
            Symbol = symbol;
        }


        public abstract decimal Convert(decimal sourceValue, Unit outputUnit);


        public override string ToString()
        {
            return Plural;
        }
    }
}
