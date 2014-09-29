using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TAlex.MathCore.UnitConversion.Units;

namespace TAlex.MathCore.UnitConversion.Quantities
{
    public class Time : Quantity
    {
        public static readonly LinearUnit Microsecond = new LinearUnit("Microsecond", "Microseconds", "μs", 0.000001M);
        public static readonly LinearUnit Millisecond = new LinearUnit("Millisecond", "Milliseconds", "ms", 0.001M);
        public static readonly LinearUnit Second = new LinearUnit("Second", "Seconds", "s", 1);
        public static readonly LinearUnit Minute = new LinearUnit("Minute", "Minutes", "min", 60);
        public static readonly LinearUnit Hour = new LinearUnit("Hour", "Hours", "h", 3600);
        public static readonly LinearUnit Day = new LinearUnit("Day", "Days", "d", 86400);
        public static readonly LinearUnit Week = new LinearUnit("Week", "Weeks", "wk", 604800M);
        public static readonly LinearUnit Year = new LinearUnit("Year", "Years", "yr", 31557600M);


        public override string Name
        {
            get { return "Time"; }
        }

        public override List<Units.Unit> Units
        {
            get
            {
                return new List<Units.Unit>
                {
                    Microsecond,
                    Millisecond,
                    Second,
                    Minute,
                    Hour,
                    Day,
                    Week,
                    Year
                };
            }
        }
    }
}
