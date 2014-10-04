using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace TAlex.MathCore.UnitConversion.Quantities.Annotation
{
    [AttributeUsage(AttributeTargets.Class)]
    public class QuantityAttribute : Attribute
    {
        public string DefaultInputUnitName { get; set; }

        public string DefaultOutputUnitName { get; set; }

        public string SIUnitName { get; set; }


        public QuantityAttribute()
        {
        }

        public QuantityAttribute(string defaultInputUnitName, string defaultOutputUnitName)
        {
            DefaultInputUnitName = defaultInputUnitName;
            DefaultOutputUnitName = defaultOutputUnitName;
        }
    }
}
