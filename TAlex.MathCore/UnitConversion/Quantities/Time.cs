using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TAlex.MathCore.UnitConversion.Units;

namespace TAlex.MathCore.UnitConversion.Quantities
{
    public class Time : Quantity
    {
        public override string Name
        {
            get { return "Time"; }
        }

        public static readonly LinearUnit Microsecond = new LinearUnit("Microsecond", "Microseconds", "", 0.000001M);
        public static readonly LinearUnit Millisecond = new LinearUnit("Millisecond", "Milliseconds", "", 0.001M);
        public static readonly LinearUnit Second = new LinearUnit("Second", "Seconds", "", 1);
        public static readonly LinearUnit Minute = new LinearUnit("Minute", "Minutes", "", 60);
        public static readonly LinearUnit Hour = new LinearUnit("Hour", "Hours", "", 3600);
        public static readonly LinearUnit Day = new LinearUnit("Day", "Days", "", 86400);
        public static readonly LinearUnit Week = new LinearUnit("Week", "Weeks", "", 604800M);
        public static readonly LinearUnit Year = new LinearUnit("Year", "Years", "", 31557600M);

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
