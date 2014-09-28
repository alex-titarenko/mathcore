using System;
using System.Collections.Generic;
using TAlex.MathCore.UnitConversion.Quantities;
using TAlex.MathCore.UnitConversion.Units;


namespace TAlex.MathCore.UnitConversion
{
    public static class UnitConverter
    {
        public static readonly Length Length = new Length();
        public static readonly Weight Weight = new Weight();
        public static readonly Time Time = new Time();
        public static readonly Temperature Temperature = new Temperature();
        public static readonly Angle Angle = new Angle(); 
        public static readonly Data Data = new Data();

        
        public static IList<Quantity> Quantities
        {
            get
            {
                return new List<Quantity>
                {
                    Length,
                    Weight,
                    Time,
                    Temperature,
                    Angle,
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
