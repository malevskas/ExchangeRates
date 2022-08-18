using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRates.Repositories
{
    internal class Repository<T> : IRepository<T> where T : class
    {
        private Entity myExchangeDatabase = new Entity();
        private DbSet<T> table;

        public Repository()
        {
            table = myExchangeDatabase.Set<T>();
        }

        public List<T> GetAll()
        {
            return table.ToList();
        }

        public string Insert(T t)
        {
            table.Add(t);
            if (myExchangeDatabase.SaveChanges() >= 0)
            {
                return "ok";
            }
            return "Action failed!";
        }

        public string Update(T t)
        {
            //table.Attach(t);
            //myExchangeDatabase.SaveChanges();
            //return "ok";
            if (myExchangeDatabase.SaveChanges() >= 0)
            {
                return "ok";
            }
            return "Action failed!";
        }

        public List<string> GetCurrencyCodes()
        {
            return myExchangeDatabase.Currencies.Select(x => x.Code).ToList();
        }

        public List<string> GetCurrencyNames()
        {
            return myExchangeDatabase.Currencies.Select(x => x.CurrencyName).ToList();
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

        public List<User> GetAllUsers()
        {
            return myExchangeDatabase.Users.ToList();
        }

        public List<OperationType> GetAllOperationTypes()
        {
            return myExchangeDatabase.OperationTypes.ToList();
        }
    }
}
