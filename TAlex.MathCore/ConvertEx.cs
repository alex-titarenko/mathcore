using System;
using System.Globalization;
using System.Linq;
using System.Text;


namespace TAlex.MathCore
{
    public static class ConvertEx
    {
        public static double ToSafeDouble(Complex c)
        {
            if (!c.IsReal) throw new ArgumentException();
            return c.Re;
        }

        public static double ToDouble(string s)
        {
            return ToDouble(s, null);
        }

        public static double ToDouble(string s, IFormatProvider formatProvider)
        {
            if (s.Length > 1 && Char.IsLetter(s.Last()))
            {
                int radix = 10;
                char ch = s.Last();
                switch (ch)
                {
                    case 'b': radix = 2; break;
                    case 'd': radix = 10; break;
                    case 'o': radix = 8; break;
                    case 'h': radix = 16; break;

                    default: throw new FormatException(String.Format(Properties.Resources.EXC_INVALID_NUMBER_FORMAT, s, ch));
                }

                return ToDouble(s.Remove(s.Length - 1), formatProvider, radix);
            }

            return ToDouble(s, formatProvider, 10);
        }

        public static double ToDouble(string s, int fromBase)
        {
            return ToDouble(s, null, fromBase);
        }

        public static double ToDouble(string s, IFormatProvider formatProvider, int fromBase)
        {
            double result = 0.0;

            if (fromBase == 10 && Double.TryParse(s, NumberStyles.Float, formatProvider, out result))
            {
                return result;
            }

            NumberFormatInfo numberFormatInfo = (NumberFormatInfo)formatProvider.GetFormat(typeof(NumberFormatInfo));
            string str = s.Trim().ToUpper();
            int sign = 1;
            if (str.IndexOfAny(new char[] {'+', '-'}) == 0)
            {
                sign = Int32.Parse(str[0] + "1");
                str = str.Substring(1);
            }
            int delimiterIdx = str.IndexOf(numberFormatInfo.NumberDecimalSeparator);

            if (delimiterIdx == -1)
                delimiterIdx = 0;
            else
            {
                str = str.Remove(delimiterIdx, 1);
                delimiterIdx = -(str.Length - delimiterIdx);
            }

            int idx = delimiterIdx;
            for (int i = str.Length - 1; i >= 0; i--)
            {
                int digit;
                if (Char.IsDigit(str[i]))
                    digit = Int32.Parse(str[i].ToString());
                else if (Char.IsLetter(str[i]))
                    digit = 10 + (str[i] - 'A');
                else
                    throw new FormatException(String.Format(Properties.Resources.EXC_INVALID_NUMBER_FORMAT, s, str[i]));

                if (digit >= fromBase)
                    throw new FormatException(String.Format(Properties.Resources.EXC_INVALID_NUMBER_FORMAT, s, str[i]));

                result += digit * Math.Pow(fromBase, idx++);
            }

            return sign * result;
        }
    }
}
