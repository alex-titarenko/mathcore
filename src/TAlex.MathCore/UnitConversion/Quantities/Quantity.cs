using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TAlex.MathCore.UnitConversion.Units;


namespace TAlex.MathCore.UnitConversion.Quantities
{
    public abstract class Quantity
    {
        public abstract string Name { get; }

        public abstract List<Unit> Units { get; }
    }
}
