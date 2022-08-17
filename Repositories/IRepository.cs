using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRates.Repositories
{
    public interface IRepository<T> where T : class
    {
        List<T> GetAll();
        string Insert(T t);
        string Update(T t);
        List<string> GetCurrencyCodes();
        List<string> GetCurrencyNames();
        List<Currency> GetAllCurrencies();
        Currency GetCurrencyByName(string name);
        bool EntryExists(string name);
        List<User> GetAllUsers();
        List<OperationType> GetAllOperationTypes();
    }
}
