using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRates.Repositories
{
    public interface IOperationsRepository
    {
        List<User> GetAllUsers();
        List<OperationType> GetAllOperationTypes();
        List<Currency> GetAllCurrencies();
    }
}
