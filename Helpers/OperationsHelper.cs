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
    internal class OperationsHelper : IOperationsRepository
    {
        private Entity myExchangeDatabase = new Entity();
        private readonly IRepository<Operation> operationRepo = new Repository<Operation>();

        public List<Currency> fillCurrencyCB()
        {
            return GetAllCurrencies();
        }

        public List<User> fillUserCB()
        {
            return GetAllUsers();
        }

        public List<OperationType> fillOperationTypeCB()
        {
            return GetAllOperationTypes();
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
            else if (DateTime.Compare(SelectedDate.Value.Date, DateTime.Today) < 0)
            {
                return "Please select a later date.";
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

        public List<User> GetAllUsers()
        {
            return myExchangeDatabase.Users.ToList();
        }

        public List<OperationType> GetAllOperationTypes()
        {
            return myExchangeDatabase.OperationTypes.ToList();
        }
        public List<Currency> GetAllCurrencies()
        {
            return myExchangeDatabase.Currencies.ToList<Currency>();
        }
    }
}
