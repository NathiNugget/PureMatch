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
        public static int YEARDIGITSMIN = int.Parse(CURRENTTIME.AddYears(1).ToString("yy"));
        public static int YEARDIGITSMAX = int.Parse(CURRENTTIME.AddYears(5).ToString("yy"));
        public const string PHONEFILTER = "^\\d{8}$";
        public const string MAILFILTER = "^[\\w-]+(\\.[\\w-]+)*@([\\w-]+\\.)+[\\w-]{2,4}$";
        public const string CARDNUMBERFILTER = "^\\d{16}$";
        public const string CARDCVCFILTER = "^\\d{3}$";
        public const string CARDEXPMONTHFILTER = "^\\d{2}$";
        public const string CARDEXPYEARFILTER = "^\\d{2}$";
    }
}
