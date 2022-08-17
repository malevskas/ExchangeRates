using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRates.Repositories
{
    internal class OfficialRatesRepository : IOfficialRatesRepository
    {
        private Entity myExchangeDatabase = new Entity();

        public void DeleteOfficialRate(OfficialRate officialRate)
        {
            officialRate.IsActive = 0;
            myExchangeDatabase.SaveChanges();
        }

        public List<OfficialRate> GetAllOfficialRates()
        {
            List<OfficialRate> allMyOfficialRates = myExchangeDatabase.OfficialRates.ToList<OfficialRate>();
            return allMyOfficialRates;
        }

        public string InsertOfficialRate(OfficialRate officialRate)
        {
            myExchangeDatabase.OfficialRates.Add(officialRate);
            myExchangeDatabase.SaveChanges();
            return "ok";
        }

        public string UpdateOfficialRate(OfficialRate officialRate, OfficialRate newOfficialRate)
        {
            officialRate.Currency = newOfficialRate.Currency;
            officialRate.ValidityDate = newOfficialRate.ValidityDate;
            officialRate.Rate = newOfficialRate.Rate;
            officialRate.IsActive = newOfficialRate.IsActive;
            myExchangeDatabase.SaveChanges();
            return "ok";
        }

        public List<Currency> GetAllCurrencies()
        {
            return myExchangeDatabase.Currencies.ToList<Currency>();
        }

        public Currency GetCurrencyByName(string name)
        {
            return myExchangeDatabase.Currencies.Where(cu => cu.CurrencyName == name).FirstOrDefault();
        }

        public bool EntryExists(string name)
        {
            return myExchangeDatabase.OfficialRates.Where(or => or.ValidityDate == DateTime.Today && or.Currency1.CurrencyName == name).Any();
        }
    }
}
