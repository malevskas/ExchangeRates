using ExchangeRates.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ExchangeRates.Helpers
{
    internal class OperationsHelper
    {
        //private readonly IOperationsRepository operationsRepository = new OperationsRepository();
        private readonly IRepository<Operation> operationRepo = new Repository<Operation>();

        public List<Currency> fillCurrencyCB()
        {
            return operationRepo.GetAllCurrencies();
        }

        public List<User> fillUserCB()
        {
            return operationRepo.GetAllUsers();
        }

        public List<OperationType> fillOperationTypeCB()
        {
            return operationRepo.GetAllOperationTypes();
        }

        public List<Operation> loadTable()
        {
            return operationRepo.GetAll();
        }

        public string insert(DateTime? SelectedDate, User u, OperationType ot, Currency from, Currency to, string Amount)
        {
            if (SelectedDate == null || SelectedDate.Value.Date == null || u == null || from == null || to == null || Amount == "")
            {
                return "Please fill out all fields.";
            }
            else if (DateTime.Compare(SelectedDate.Value.Date, DateTime.Today) < 0)
            {
                return "Please select a later date.";
            }
            else
            {
                Operation operation = new Operation();
                operation.OperationTypeId = ot.OperationTypeId;
                operation.OperationDate = SelectedDate.Value.Date;
                operation.UserId = u.UserId;
                operation.CurrencyFrom = from.CurrencyId;
                operation.CurrencyTo = to.CurrencyId;
                operation.Amount = int.Parse(Amount);

                return operationRepo.Insert(operation);
            }
        }

        public string edit(Operation operation, DateTime? SelectedDate, User u, OperationType ot, Currency from, Currency to, string Amount)
        {
            if (SelectedDate == null || SelectedDate.Value.Date == null || u == null || from == null || to == null || Amount == "")
            {
                return "Please fill out all fields.";
            }
            else
            {
                operation.OperationTypeId = ot.OperationTypeId;
                operation.OperationDate = SelectedDate.Value.Date;
                operation.UserId = u.UserId;
                operation.CurrencyFrom = from.CurrencyId;
                operation.CurrencyTo = to.CurrencyId;
                operation.Amount = int.Parse(Amount);

                return operationRepo.Update(operation);
            }
        }
    }
}
