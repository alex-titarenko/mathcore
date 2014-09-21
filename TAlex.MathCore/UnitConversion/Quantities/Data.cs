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

        public static readonly DataUnit Bit = new DataUnit("Bit", "Bits", "b", 1);
        public static readonly DataUnit Byte = new DataUnit("Byte", "Bytes", "B", 8);
        public static readonly DataUnit Kilobit = new DataUnit("Kilobit", "Kilobits", "kb", 1024);
        public static readonly DataUnit Kilobyte = new DataUnit("Kilobyte", "Kilobytes", "KB", 8192);
        public static readonly DataUnit Megabit = new DataUnit("Megabit", "Megabits", "mb", 1048576);
        public static readonly DataUnit Megabyte = new DataUnit("Megabyte", "Megabytes", "MB", 8388608);
        public static readonly DataUnit Gigabit = new DataUnit("Gigabit", "Gigabits", "gb", 1073741824);
        public static readonly DataUnit Gigabyte = new DataUnit("Gigabyte", "Gigabytes", "GB", 8589934592);
        public static readonly DataUnit Terabit = new DataUnit("Tegabit", "Tegabits", "tb", 1099511627776);
        public static readonly DataUnit Tegabyte = new DataUnit("Tegabyte", "Tegabytes", "TB", 8796093022208);
        public static readonly DataUnit Petabit = new DataUnit("Petabit", "Petabits", "pb", 1125899906842624);
        public static readonly DataUnit Petabyte = new DataUnit("Petabyte", "Petabytes", "PB", 9007199254740992);

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
