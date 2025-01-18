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
            try
            {
                if (string.IsNullOrEmpty(text))
                {
                    return "Nazwa jest polem wymaganym.";
                }

                return char.IsUpper(text, 0) ? string.Empty : "Rozpocznij dużą literą.";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public static string ValidateIsNotEmpty(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return "Pole jest wymagane.";
            }

            return string.Empty;
        }
    }
}
