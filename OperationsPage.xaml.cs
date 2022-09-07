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
using ExchangeRates.Helpers;

namespace ExchangeRates
{
    /// <summary>
    /// Interaction logic for OperationsPage.xaml
    /// </summary>
    public partial class OperationsPage : Page
    {
        private OperationsHelper oHelper;

        public OperationsPage()
        {
            InitializeComponent();
            oHelper = new OperationsHelper();
            CurrencyFromCB.ItemsSource = oHelper.fillCurrencyCB();
            CurrencyToCB.ItemsSource = oHelper.fillCurrencyCB();
            UserCB.ItemsSource = oHelper.fillUserCB();
            OperationTypeCB.ItemsSource = oHelper.fillOperationTypeCB();
        }

        private void LoadTable(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = oHelper.loadTable();
        }

        private void Insert(object sender, RoutedEventArgs e)
        {
            string result = oHelper.insert(OperationDate.SelectedDate, (User)UserCB.SelectedItem, (OperationType)OperationTypeCB.SelectedItem, (Currency)CurrencyFromCB.SelectedItem, (Currency)CurrencyToCB.SelectedItem, Amount.Text);
            if (!result.Equals("ok"))
            {
                MessageBox.Show(result);
            }
            else
            {
                dataGrid.ItemsSource = oHelper.loadTable();
            }
        }

        private void Edit(object sender, RoutedEventArgs e)
        {
            string result = oHelper.edit((Operation)dataGrid.SelectedItem, OperationDate.SelectedDate, (User)UserCB.SelectedItem, (OperationType)OperationTypeCB.SelectedItem, (Currency)CurrencyFromCB.SelectedItem, (Currency)CurrencyToCB.SelectedItem, Amount.Text);
            if (!result.Equals("ok"))
            {
                MessageBox.Show(result);
            }
            else
            {
                dataGrid.ItemsSource = oHelper.loadTable();
            }
        }

        private void populateTextBox(object sender, SelectedCellsChangedEventArgs e)
        {
            Operation operation = (Operation)dataGrid.SelectedItem;
            OperationId.Content = operation.OperationId.ToString();
            OperationTypeCB.Text = operation.OperationType.OperationName;
            OperationDate.SelectedDate = operation.OperationDate;
            UserCB.Text = operation.User.FullName;
            CurrencyFromCB.Text = operation.Currency.CurrencyName;
            CurrencyToCB.Text = operation.Currency1.CurrencyName;
            Amount.Text = operation.Amount.ToString();
        }

        private void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[.][0-9]+$|^[0-9]*[.]{0,1}[0-9]*$");
            e.Handled = !regex.IsMatch(e.Text);
        }
    }
}
