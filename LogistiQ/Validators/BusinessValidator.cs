using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogistiQ.Validators
{
    public static class BusinessValidator
    {
        //Sprawdź czy cena jest poprawna
        public static string ValidateIsPricePositive(decimal? price)
        {
            if (!price.HasValue)
            {
                return "To pole jest wymagane";
            }
            if (price > 0)
            {
                return "Wartość powinna być większa od 0";
            }
            return string.Empty;
        }
    }
}
