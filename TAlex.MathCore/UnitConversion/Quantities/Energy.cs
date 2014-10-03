using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TAlex.MathCore.UnitConversion.Units;


namespace TAlex.MathCore.UnitConversion.Quantities
{
    public class Energy : Quantity
    {
        public static readonly LinearUnit ElectronVolt = new LinearUnit("Electron volt", "Electron volts", "eV", 1.60217653e-19M);
        public static readonly LinearUnit Joule = new LinearUnit("Joule", "Joules", "J", 1);
        public static readonly LinearUnit Kilojoule = new LinearUnit("Kilojoule", "Kilojoules", "kJ", 1000);
        public static readonly LinearUnit Calorie = new LinearUnit("Calorie", "Calories", "cal", 4.1868M);
        public static readonly LinearUnit Kilocalorie = new LinearUnit("Kilocalorie", "Kilocalories", "kcal", 4186.8M);
        public static readonly LinearUnit FoodPound = new LinearUnit("Food-pound", "Food-pounds", "ft lbf", 1.3558179483314004M);
        public static readonly LinearUnit BritishThermalUnit = new LinearUnit("British thermal unit", "British thermal units", "BTU", 1055.056M);


        public override string Name
        {
            get { return "Energy"; }
        }

        public override List<Units.Unit> Units
        {
            get
            {
                return new List<Units.Unit>
                {
                    ElectronVolt,
                    Joule,
                    Kilojoule,
                    Calorie,
                    Kilocalorie,
                    FoodPound,
                    BritishThermalUnit
                };
            }
        }
    }
}
