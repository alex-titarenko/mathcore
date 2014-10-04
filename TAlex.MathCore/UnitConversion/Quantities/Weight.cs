using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TAlex.MathCore.UnitConversion.Quantities.Annotation;
using TAlex.MathCore.UnitConversion.Units;


namespace TAlex.MathCore.UnitConversion.Quantities
{
    [Quantity("Pound", "Kilogram")]
    public class Weight : Quantity
    {
        public static readonly LinearUnit Carat = new LinearUnit("Carat", "Carats", "ct", 0.0002M);
        public static readonly LinearUnit Milligram = new LinearUnit("Milligram", "Milligrams", "mg", 0.000001M);
        public static readonly LinearUnit Centigram = new LinearUnit("Centigram", "Centigrams", "cg", 0.00001M);
        public static readonly LinearUnit Decigram = new LinearUnit("Decigram", "Decigrams", "dg", 0.0001M);
        public static readonly LinearUnit Gram = new LinearUnit("Gram", "Grams", "g", 0.001M);
        public static readonly LinearUnit Dekagram = new LinearUnit("Dekagram", "Dekagrams", "dag", 0.01M);
        public static readonly LinearUnit Hectogram = new LinearUnit("Hectogram", "Hectograms", "hg", 0.1M);
        public static readonly LinearUnit Kilogram = new LinearUnit("Kilogram", "Kilograms", "kg", 1);
        public static readonly LinearUnit Tonne = new LinearUnit("Tonne", "Tonnes", "t", 1000);
        public static readonly LinearUnit Ounce = new LinearUnit("Ounce", "Ounces", "oz", 0.028349523125M);
        public static readonly LinearUnit Pound = new LinearUnit("Pound", "Pounds", "lb", 0.45359237M);
        public static readonly LinearUnit Stone = new LinearUnit("Stone", "Stone", "st", 6.35029318M);
        public static readonly LinearUnit ShortTon = new LinearUnit("Short ton (US)", "Short tons (US)", "sh tn", 907.18474M);
        public static readonly LinearUnit LongTon = new LinearUnit("Long ton", "Long tons (UK)", "long tn", 1016.0469088M);


        public override string Name
        {
            get { return "Weight"; }
        }

        public override List<Units.Unit> Units
        {
            get
            {
                return new List<Units.Unit>
                {
                    Carat,
                    Milligram,
                    Centigram,
                    Decigram,
                    Gram,
                    Dekagram,
                    Hectogram,
                    Kilogram,
                    Tonne,
                    Ounce,
                    Pound,
                    Stone,
                    ShortTon,
                    LongTon
                };
            }
        }
    }
}
