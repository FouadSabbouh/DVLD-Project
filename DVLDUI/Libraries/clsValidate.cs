using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace DVLDUI.Libraries
{
    public class clsValidate
    {
       public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;
            try
            {
                // Use a regular expression to validate the email format
                string pattern = @"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$";
                return Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
