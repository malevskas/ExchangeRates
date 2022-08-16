using ExchangeRates.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRates.Repositories
{
    internal class CurrenciesRepository : ICurrenciesRepository
    {
        private Entity myExchangeDatabase = new Entity();

        public void DeleteCurrency(Currency currency)
        {
            currency.IsActive = 0;
            myExchangeDatabase.SaveChanges();
        }

        public List<Currency> GetAllCurrencies()
        {
            List<Currency> allMyCurrencies = myExchangeDatabase.Currencies.ToList<Currency>();
            return allMyCurrencies;
        }

        public Currency GetCurrencyById(int id)
        {
            return myExchangeDatabase.Currencies.Where(c => c.CurrencyId == id).First();
        }

        public string InsertCurrency(Currency currency)
        {
            myExchangeDatabase.Currencies.Add(currency);
            myExchangeDatabase.SaveChanges();
            return "ok";
        }

        public string UpdateCurrency(Currency currency)
        {
            return "ok";
        }

        public List<string> GetCurrencyCodes()
        {
            return myExchangeDatabase.Currencies.Select(x => x.Code).ToList();
        }

        public List<string> GetCurrencyNames()
        {
            return myExchangeDatabase.Currencies.Select(x => x.CurrencyName).ToList();
        }
    }
}
