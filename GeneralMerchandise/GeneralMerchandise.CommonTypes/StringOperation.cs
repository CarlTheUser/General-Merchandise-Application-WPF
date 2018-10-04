using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GeneralMerchandise.Common
{
    public static class StringOperation
    {
        public static bool HasValue(this string item) => item.Trim().Length > 0;

        public static bool IsAlphabetic(this string item)
        {
            Regex regex = new Regex(@"([A-z]+[ \t]*)");
            return regex.IsMatch(item);
        }

        public static bool IsNumeric(this string item)
        {
            Regex regex = new Regex("[0-9]+");
            return regex.IsMatch(item);
        }

        public static bool IsInRange(this string item, int lowerBound, int upperBound) => item.Length >= lowerBound && item.Length <= upperBound;
    }
}
