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
        private readonly IOperationsRepository operationsRepository = new OperationsRepository();

        public List<Currency> fillCurrencyCB()
        {
            return operationsRepository.GetAllCurrencies();
        }

        public List<User> fillUserCB()
        {
            return operationsRepository.GetAllUsers();
        }

        public List<OperationType> fillOperationTypeCB()
        {
            return operationsRepository.GetAllOperationTypes();
        }

        public List<Operation> loadTable()
        {
            return operationsRepository.GetAllOperations();
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

                return operationsRepository.InsertOperation(operation);
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
                Operation newOperation = new Operation();
                newOperation.OperationTypeId = ot.OperationTypeId;
                newOperation.OperationDate = SelectedDate.Value.Date;
                newOperation.UserId = u.UserId;
                newOperation.CurrencyFrom = from.CurrencyId;
                newOperation.CurrencyTo = to.CurrencyId;
                newOperation.Amount = int.Parse(Amount);

                return operationsRepository.UpdateOperation(operation, newOperation);
            }
        }
    }
}
