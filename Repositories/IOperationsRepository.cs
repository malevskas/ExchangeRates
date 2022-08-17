using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRates.Repositories
{
    public interface IOperationsRepository
    {
        List<Operation> GetAllOperations();
        string InsertOperation(Operation operation);
        string UpdateOperation(Operation operation, Operation newOperation);
        List<Currency> GetAllCurrencies();
        List<User> GetAllUsers();
        List<OperationType> GetAllOperationTypes();
    }
}
