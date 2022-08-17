using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRates.Repositories
{
    public interface IOperationTypesRepository
    {
        List<OperationType> GetAllOperationTypes();
        string InsertOperationType(OperationType operationType);
        string UpdateOperationType(OperationType operationType, OperationType newOperationType);
        void DeleteOperationType(OperationType operationType);
    }
}
