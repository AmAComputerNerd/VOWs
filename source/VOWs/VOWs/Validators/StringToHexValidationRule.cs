using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace VOWs.Validators
{
    public class StringToHexValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string s = (string)value;
            if (s == null || !s.StartsWith("#") || s.Length != 7)
            {
                return new ValidationResult(false, "#000000");
            }
            Regex pattern = new Regex("/^#?([a-f0-9]{6}|[a-f0-9]{3})$/");
            if(pattern.IsMatch(s))
            {
                return new ValidationResult(true, null);
            }
            return new ValidationResult(false, "#000000");
        }
    }
}
