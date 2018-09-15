using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ParkingManagementSystem.Constant
{
    public static class AppConstant
    {
        public enum MyEnum
        {
            Select,
            Fourwheeler,
            Twowheeler,
            Others

        }

        public static bool ValidateInput(string str)
        {
            bool IsValid = false;
            String reg = @"^[a-z]{2}[a-z0-9]{1,2}[a-z]{1,2}[0-9]{4}$";//@"^[a-z]{2}[0-9][a-z]{1,2}[a-z]{1,2}[0-9]{4}$";
            Regex regexVeh = new Regex(reg, RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.CultureInvariant | RegexOptions.Compiled);
            Match match = regexVeh.Match(str.Trim());
            if (match.Success)
            {
                IsValid = true;
            }

            return IsValid;

        }
    }
}
