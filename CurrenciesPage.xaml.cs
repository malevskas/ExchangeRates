using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ExchangeRates.Helpers;
using System.Text.RegularExpressions;

namespace ExchangeRates
{
    /// <summary>
    /// Interaction logic for CurrenciesPage.xaml
    /// </summary>
    public partial class CurrenciesPage : Page
    {

        private CurrenciesHelper cHelper;

        public CurrenciesPage()
        {
            InitializeComponent();
            cHelper = new CurrenciesHelper();
        }

        private void LoadTable(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = cHelper.loadTable();
        }

        private void Insert(object sender, RoutedEventArgs e)
        {
            string result = cHelper.insert(Code.Text, CurrencyName.Text, checkBox.IsChecked);
            if(!result.Equals("ok"))
            {
                MessageBox.Show(result);
            }
            else
            {
                dataGrid.ItemsSource = cHelper.loadTable();
            }
        }

        private void Edit(object sender, RoutedEventArgs e)
        {
            string result = cHelper.edit((Currency)dataGrid.SelectedItem, Code.Text, CurrencyName.Text, checkBox.IsChecked);
            if (!result.Equals("ok"))
            {
                MessageBox.Show(result);
            }
            else
            {
                dataGrid.ItemsSource = cHelper.loadTable();
            }
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            string result = cHelper.delete((Currency)dataGrid.SelectedItem);
            if (result != "ok")
            {
                MessageBox.Show(result);
            }
            else
            {
                checkBox.IsChecked = false;
                dataGrid.ItemsSource = cHelper.loadTable();
            }
        }

        private void populateTextBox(object sender, SelectedCellsChangedEventArgs e)
        {
            Currency currency = (Currency)dataGrid.SelectedItem;
            CurrencyId.Content = currency.CurrencyId.ToString();
            Code.Text = currency.Code;
            CurrencyName.Text = currency.CurrencyName;
            if (currency.IsActive == 1)
            {
                checkBox.IsChecked = true;
            }
            else
            {
                checkBox.IsChecked = false;
            }
        }

        private void PreviewTextNumericInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void PreviewTextAlphabeticInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[a-zA-Z ]");
            e.Handled = !regex.IsMatch(e.Text);
        }
    }
}
