using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRates.Helpers
{
    public class Token
    {
        public static string TokenString { get; set; }

        public static void SetToken (string t)
        {
            TokenString = t;
        }

        public static string GetToken()
        {
            return TokenString;
        }
    }
}
