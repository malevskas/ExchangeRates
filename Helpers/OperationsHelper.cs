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
        private Label OperationId;
        private ComboBox OperationTypeCB;
        private ComboBox UserCB;
        private ComboBox CurrencyFromCB;
        private ComboBox CurrencyToCB;
        private TextBox Amount;
        private DatePicker OperationDate;
        private DataGrid dataGrid;

        public OperationsHelper(Label OperationId, ComboBox OperationTypeCB, ComboBox UserCB, ComboBox CurrencyFromCB, ComboBox CurrencyToCB, TextBox Amount, DatePicker OperationDate, DataGrid dataGrid)
        {
            this.OperationId = OperationId;
            this.OperationTypeCB = OperationTypeCB;
            this.UserCB = UserCB;
            this.CurrencyFromCB = CurrencyFromCB;
            this.CurrencyToCB = CurrencyToCB;
            this.Amount = Amount;
            this.OperationDate = OperationDate;
            this.dataGrid = dataGrid;
        }

        public void fillCurrencyCB()
        {
            var allMyCurrencies = myExchangeDatabase.Currencies.ToList<Currency>();
            CurrencyFromCB.Items.Clear();
            CurrencyToCB.Items.Clear();
            CurrencyFromCB.ItemsSource = allMyCurrencies;
            CurrencyToCB.ItemsSource = allMyCurrencies;
        }

        public void fillUserCB()
        {
            var allMyUsers = myExchangeDatabase.Users.ToList<User>();
            UserCB.Items.Clear();
            UserCB.ItemsSource = allMyUsers;
        }

        public void fillOperationTypeCB()
        {
            var allMyOperationTypes = myExchangeDatabase.OperationTypes.ToList<OperationType>();
            OperationTypeCB.Items.Clear();
            OperationTypeCB.ItemsSource = allMyOperationTypes;
        }

        public void loadTable()
        {
            var allMyOperations = myExchangeDatabase.Operations.ToList<Operation>();
            dataGrid.ItemsSource = allMyOperations;
        }

        public void insert()
        {
            int result = DateTime.Compare(OperationDate.SelectedDate.Value.Date, DateTime.Today);
            if (OperationDate.SelectedDate.Value.Date == null || UserCB.SelectedItem == null || CurrencyFromCB.SelectedItem == null || CurrencyToCB.SelectedItem == null || Amount.Text == "")
            {
                MessageBox.Show("Please fill out all fields.");
            }
            else if (result < 0)
            {
                MessageBox.Show("Please select a later date.");
            }
            else
            {
                Operation operation = new Operation();
                OperationType ot = (OperationType)OperationTypeCB.SelectedItem;
                operation.OperationTypeId = ot.OperationTypeId;
                operation.OperationDate = OperationDate.SelectedDate.Value.Date;
                User u = (User)UserCB.SelectedItem;
                operation.UserId = u.UserId;
                Currency c = (Currency)CurrencyFromCB.SelectedItem;
                operation.CurrencyFrom = c.CurrencyId;
                c = (Currency)CurrencyToCB.SelectedItem;
                operation.CurrencyTo = c.CurrencyId;
                operation.Amount = int.Parse(Amount.Text);

                myExchangeDatabase.Operations.Add(operation);
                myExchangeDatabase.SaveChanges();
                loadTable();
            }
        }

        public void edit()
        {
            if (OperationDate.SelectedDate.Value.Date == null || UserCB.SelectedItem == null || CurrencyFromCB.SelectedItem == null || CurrencyToCB.SelectedItem == null || Amount.Text == "")
            {
                MessageBox.Show("Please fill out all fields.");
            }
            else
            {
                Operation operation = (Operation)dataGrid.SelectedItem;
                operation.OperationTypeId = int.Parse(OperationTypeCB.Text);
                operation.OperationDate = OperationDate.SelectedDate.Value.Date;
                operation.UserId = int.Parse(UserCB.Text);
                operation.CurrencyFrom = int.Parse(CurrencyFromCB.Text);
                operation.CurrencyTo = int.Parse(CurrencyToCB.Text);
                operation.Amount = int.Parse(Amount.Text);

                myExchangeDatabase.SaveChanges();
                loadTable();
            }
        }

        public void populateTextBox()
        {
            Operation operation = (Operation)dataGrid.SelectedItem;
            OperationId.Content = operation.OperationId.ToString();
            OperationTypeCB.Text = operation.OperationTypeId.ToString();
            OperationDate.SelectedDate = operation.OperationDate;
            UserCB.Text = operation.UserId.ToString();
            CurrencyFromCB.Text = operation.CurrencyFrom.ToString();
            CurrencyToCB.Text = operation.CurrencyTo.ToString();
            Amount.Text = operation.Amount.ToString();
        }
    }
}
