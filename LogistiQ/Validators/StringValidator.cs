using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LogistiQ.Validators
{
    public static class StringValidator
    {
        //Sprawd czy zaczyna sié od duej litery 
        public static string ValidateIsFirstLetterUpper(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return "Pole nie może być puste";
            }
            if (char.IsLower(text[0]))
            {
                return string.Empty;
            }
            return char.IsUpper(text, 0) ? string.Empty :
                "Rozpocznij duża liter!";
        }
    }
}
