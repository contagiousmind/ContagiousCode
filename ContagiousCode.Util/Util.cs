using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContagiousCode.Util
{
    public static class Util
    {

        public static DateTime StringToDate(string value, string dateFormat)
        {
            return StringToDate(value, dateFormat, "en-GB");
        }

        public static DateTime StringToDate(string value, string dateFormat, string cultureInfoCode)
        {
            DateTime result = new DateTime(1900, 1, 1);

            if (String.IsNullOrEmpty(value))
            {
                result = new DateTime(1900, 1, 1);
            }
            else
            {
                try
                {
                    IFormatProvider culture = new System.Globalization.CultureInfo(cultureInfoCode, true);
                    result = DateTimeOffset.ParseExact(value, dateFormat, culture).DateTime;
                }
                catch
                {
                    result = new DateTime(1900, 1, 1);
                }
            }

            return result;
        }








    }
}
