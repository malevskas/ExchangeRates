using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRates.Repositories
{
    internal class OperationTypesRepository : IOperationTypesRepository
    {
        private Entity myExchangeDatabase = new Entity();

        public void DeleteOperationType(OperationType operationType)
        {
            operationType.isActive = 0;
            myExchangeDatabase.SaveChanges();
        }

        public List<OperationType> GetAllOperationTypes()
        {
            return myExchangeDatabase.OperationTypes.ToList();
        }

        public string InsertOperationType(OperationType operationType)
        {
            myExchangeDatabase.OperationTypes.Add(operationType);
            myExchangeDatabase.SaveChanges();
            return "ok";
        }

        public string UpdateOperationType(OperationType operationType, OperationType newOperationType)
        {
            operationType.Code = newOperationType.Code;
            operationType.OperationName = newOperationType.OperationName;
            operationType.isActive = newOperationType.isActive;
            myExchangeDatabase.SaveChanges();
            return "ok";
        }
    }
}
