using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureLib.Model
{
    /// <summary>
    /// This class contains Regex as well as some DateTime-formatting to ensure the user-inputs are valid
    /// </summary>
    public static class ValidationRegex
    {
        private static DateTime CURRENTTIME = DateTime.Now;
        public static int YEARDIGITS = int.Parse(CURRENTTIME.ToString("yy"));
        public static string PHONEFILTER = "^\\d{8}$";
        public static string MAILFILTER = "^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$";
        public static string CARDNUMBERFILTER = "^\\d{16}$";
        public static string CARDCVCFILTER = "^\\d{3}$";
        public static string CARDEXPMONTHFILTER = "^\\d{2}$";
        public static string CARDEXPYEARFILTER = "^\\d{2}$";
    }
}
