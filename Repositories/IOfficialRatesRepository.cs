using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRates.Repositories
{
    public interface IOfficialRatesRepository
    {
        List<Currency> GetAllCurrencies();
        Currency GetCurrencyByName(string name);
        bool EntryExists(string name);
    }
}
