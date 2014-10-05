using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace TAlex.MathCore.UnitConversion.Quantities.Annotation
{
    [AttributeUsage(AttributeTargets.Class)]
    public class QuantityAttribute : Attribute
    {
        public string DefaultInputUnitSymbol { get; set; }

        public string DefaultOutputUnitSymbol { get; set; }

        public string SIUnitSymbol { get; set; }


        public QuantityAttribute()
        {
        }

        public QuantityAttribute(string defaultInputUnitSymbol, string defaultOutputUnitSymbol)
        {
            DefaultInputUnitSymbol = defaultInputUnitSymbol;
            DefaultOutputUnitSymbol = defaultOutputUnitSymbol;
        }
    }
}
