using System;
using System.Collections.Generic;
using TAlex.MathCore.UnitConversion.Quantities;
using TAlex.MathCore.UnitConversion.Units;


namespace TAlex.MathCore.UnitConversion
{
    public static class UnitConverter
    {
        public static readonly Quantity Length = new Length();
        public static readonly Data Data = new Data();


        public static IList<Quantity> Quantities
        {
            get
            {
                return new List<Quantity>
                {
                    Length,
                    Data
                };
            }
        }


        public static decimal Convert(decimal sourceValue, Unit sourceUnit, Unit outputUnit)
        {
            return sourceUnit.Convert(sourceValue, outputUnit);
        }
    }
}
