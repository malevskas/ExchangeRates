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
        private Entity myExchangeDatabase = new Entity();

        public List<Currency> fillCurrencyCB()
        {
            List<Currency> allMyCurrencies = myExchangeDatabase.Currencies.ToList<Currency>();
            return allMyCurrencies;
        }

        public List<User> fillUserCB()
        {
            List<User> allMyUsers = myExchangeDatabase.Users.ToList<User>();
            return allMyUsers;
        }

        public List<OperationType> fillOperationTypeCB()
        {
            List<OperationType> allMyOperationTypes = myExchangeDatabase.OperationTypes.ToList<OperationType>();
            return allMyOperationTypes;
        }

        public List<Operation> loadTable()
        {
            List<Operation> allMyOperations = myExchangeDatabase.Operations.ToList<Operation>();
            return allMyOperations;
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

                myExchangeDatabase.Operations.Add(operation);
                myExchangeDatabase.SaveChanges();
                return "ok";
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

                myExchangeDatabase.SaveChanges();
                return "ok";
            }
        }
    }
}
