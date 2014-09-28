using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TAlex.MathCore.UnitConversion.Units;


namespace TAlex.MathCore.UnitConversion.Quantities
{
    public class Data : Quantity
    {
        public override string Name
        {
            get { return "Data"; }
        }

        public static readonly LinearUnit Bit = new LinearUnit("Bit", "Bits", "b", 1);
        public static readonly LinearUnit Byte = new LinearUnit("Byte", "Bytes", "B", 8);
        public static readonly LinearUnit Kilobit = new LinearUnit("Kilobit", "Kilobits", "kb", 1024);
        public static readonly LinearUnit Kilobyte = new LinearUnit("Kilobyte", "Kilobytes", "KB", 8192);
        public static readonly LinearUnit Megabit = new LinearUnit("Megabit", "Megabits", "mb", 1048576);
        public static readonly LinearUnit Megabyte = new LinearUnit("Megabyte", "Megabytes", "MB", 8388608);
        public static readonly LinearUnit Gigabit = new LinearUnit("Gigabit", "Gigabits", "gb", 1073741824);
        public static readonly LinearUnit Gigabyte = new LinearUnit("Gigabyte", "Gigabytes", "GB", 8589934592);
        public static readonly LinearUnit Terabit = new LinearUnit("Tegabit", "Tegabits", "tb", 1099511627776);
        public static readonly LinearUnit Tegabyte = new LinearUnit("Tegabyte", "Tegabytes", "TB", 8796093022208);
        public static readonly LinearUnit Petabit = new LinearUnit("Petabit", "Petabits", "pb", 1125899906842624);
        public static readonly LinearUnit Petabyte = new LinearUnit("Petabyte", "Petabytes", "PB", 9007199254740992);

        public override List<Units.Unit> Units
        {
            get
            {
                return new List<Unit>
                {
                    Bit,
                    Byte,
                    Kilobit,
                    Kilobyte,
                    Megabit,
                    Megabyte,
                    Gigabit,
                    Gigabyte,
                    Terabit,
                    Tegabyte,
                    Petabit,
                    Tegabyte
                };
            }
        }
    }
}
