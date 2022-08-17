using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRates.Repositories
{
    internal class ExchangeRatesRepository : IExchangeRatesRepository
    {
        private Entity myExchangeDatabase = new Entity();

        public void DeleteExchangeRate(ExchangeRate exchangeRate)
        {
            exchangeRate.IsActive = 0;
            myExchangeDatabase.SaveChanges();
        }

        public List<ExchangeRate> GetAllExchangeRates()
        {
            List<ExchangeRate> allMyExchangeRates = myExchangeDatabase.ExchangeRates.ToList<ExchangeRate>();
            return allMyExchangeRates;
        }

        public string InsertExchangeRate(ExchangeRate exchangeRate)
        {
            myExchangeDatabase.ExchangeRates.Add(exchangeRate);
            myExchangeDatabase.SaveChanges();
            return "ok";
        }

        public string UpdateExchangeRate(ExchangeRate exchangeRate, ExchangeRate newExchangeRate)
        {
            exchangeRate.ValidityDate = newExchangeRate.ValidityDate;
            exchangeRate.CurrencyFrom = newExchangeRate.CurrencyFrom;
            exchangeRate.CurrencyTo = newExchangeRate.CurrencyTo;
            exchangeRate.Rate = newExchangeRate.Rate;
            exchangeRate.IsActive = newExchangeRate.IsActive;
            myExchangeDatabase.SaveChanges();
            return "ok";
        }

        public List<Currency> GetAllCurrencies()
        {
            return myExchangeDatabase.Currencies.ToList<Currency>();
        }
    }
}
