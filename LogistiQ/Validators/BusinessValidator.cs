using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogistiQ.Validators
{
    public static class BusinessValidator
    {
        public static string ValidateIsPricePositive(decimal? price)
        {
            if (!price.HasValue)
            {
                return "Cena jest polem wymaganym.";
            }

            if (price <= 0)
            {
                return "Cena powinna być większa od 0.";
            }

            return string.Empty;
        }
    }
}
