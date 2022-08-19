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
    }
}
