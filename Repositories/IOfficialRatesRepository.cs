using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRates.Repositories
{
    public interface IOfficialRatesRepository
    {
        List<OfficialRate> GetAllOfficialRates();
        string InsertOfficialRate(OfficialRate officialRate);
        string UpdateOfficialRate(OfficialRate officialRate, OfficialRate newOfficialRate);
        void DeleteOfficialRate(OfficialRate officialRate);
        List<Currency> GetAllCurrencies();
        Currency GetCurrencyByName(string name);
        bool EntryExists(string name);
    }
}
