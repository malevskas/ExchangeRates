using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRates.Repositories
{
    internal class OperationsRepository : IOperationsRepository
    {
        private Entity myExchangeDatabase = new Entity();

        public List<Currency> GetAllCurrencies()
        {
            return myExchangeDatabase.Currencies.ToList();
        }

        public List<User> GetAllUsers()
        {
            return myExchangeDatabase.Users.ToList();
        }

        public List<OperationType> GetAllOperationTypes()
        {
            return myExchangeDatabase.OperationTypes.ToList();
        }

        public List<Operation> GetAllOperations()
        {
            return myExchangeDatabase.Operations.ToList();
        }

        public string InsertOperation(Operation operation)
        {
            myExchangeDatabase.Operations.Add(operation);
            myExchangeDatabase.SaveChanges();
            return "ok";
        }

        public string UpdateOperation(Operation operation, Operation newOperation)
        {
            operation.OperationTypeId = newOperation.OperationTypeId;
            operation.OperationDate = newOperation.OperationDate;
            operation.UserId = newOperation.UserId;
            operation.CurrencyFrom = newOperation.CurrencyFrom;
            operation.CurrencyTo = newOperation.CurrencyTo;
            operation.Amount = newOperation.Amount;
            myExchangeDatabase.SaveChanges();
            return "ok";
        }
    }
}
