using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;

namespace ExchangeRates
{
    /// <summary>
    /// Interaction logic for OperationsPage.xaml
    /// </summary>
    public partial class OperationsPage : Page
    {
        public OperationsPage()
        {
            InitializeComponent();
            fillCurrencyCB();
            fillUserCB();
            fillOperationTypeCB();
        }

        private Entity myExchangeDatabase = new Entity();

        private void fillCurrencyCB()
        {
            var allMyCurrencies = myExchangeDatabase.Currencies.ToList<Currency>();
            CurrencyFromCB.Items.Clear();
            CurrencyToCB.Items.Clear();
            foreach (var currency in allMyCurrencies)
            {
                CurrencyFromCB.Items.Add(currency.CurrencyId);
                CurrencyToCB.Items.Add(currency.CurrencyId);
            }
        }

        private void fillUserCB()
        {
            var allMyUsers = myExchangeDatabase.Users.ToList<User>();
            UserCB.Items.Clear();
            foreach (var user in allMyUsers)
            {
                UserCB.Items.Add(user.UserId);
            }
        }

        private void fillOperationTypeCB()
        {
            var allMyOperationTypes = myExchangeDatabase.OperationTypes.ToList<OperationType>();
            OperationTypeCB.Items.Clear();
            foreach (var oTypes in allMyOperationTypes)
            {
                OperationTypeCB.Items.Add(oTypes.OperationTypeId);
            }
        }

        private void loadTable()
        {
            var allMyOperations = myExchangeDatabase.Operations.ToList<Operation>();
            dataGrid.ItemsSource = allMyOperations;
        }

        private void LoadTable(object sender, RoutedEventArgs e)
        {
            loadTable();
        }

        private void Insert(object sender, RoutedEventArgs e)
        {
            Operation operation = new Operation();
            operation.OperationTypeId = int.Parse(OperationTypeCB.Text);
            operation.OperationDate = OperationDate.SelectedDate.Value.Date;
            operation.UserId = int.Parse(UserCB.Text);
            operation.CurrencyFrom = int.Parse(CurrencyFromCB.Text);
            operation.CurrencyTo = int.Parse(CurrencyToCB.Text);
            operation.Amount = int.Parse(Amount.Text);

            myExchangeDatabase.Operations.Add(operation);
            myExchangeDatabase.SaveChanges();
            loadTable();
        }

        private void Edit(object sender, RoutedEventArgs e)
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

        private void Delete(object sender, RoutedEventArgs e)
        {
            loadTable();
        }

        private void populateTextBox(object sender, SelectedCellsChangedEventArgs e)
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

        private void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
