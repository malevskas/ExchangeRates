using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRates.Repositories
{
    public interface ICurrenciesRepository
    {
        List<string> GetCurrencyCodes();
        List<string> GetCurrencyNames();
    }
}
