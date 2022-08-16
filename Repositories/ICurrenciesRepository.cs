using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRates.Repository
{
    public interface ICurrenciesRepository
    {
        List<Currency> GetAllCurrencies();
        Currency GetCurrencyById(int id);
        string InsertCurrency(Currency currency);
        string UpdateCurrency(Currency currency);
        void DeleteCurrency(Currency currency);
        List<string> GetCurrencyCodes();
        List<string> GetCurrencyNames();
    }
}
