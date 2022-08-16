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
            throw new NotImplementedException();
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

        public string UpdateOfficialRate(OfficialRate officialRate)
        {
            throw new NotImplementedException();
        }
    }
}
