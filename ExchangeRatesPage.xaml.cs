using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
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
    /// Interaction logic for ExchangeRatesPage.xaml
    /// </summary>
    public partial class ExchangeRatesPage : Page
    {
        public ExchangeRatesPage()
        {
            InitializeComponent();
            fillCB();
        }
        
        private Entity myExchangeDatabase = new Entity();

        private void fillCB()
        {
            var allMyCurrencies = myExchangeDatabase.Currencies.ToList<Currency>();
            CurrencyFromCB.Items.Clear();
            CurrencyToCB.Items.Clear();
            CurrencyFromCB.ItemsSource = allMyCurrencies;
            CurrencyToCB.ItemsSource = allMyCurrencies;
        }

        private void loadTable()
        {
            var allMyExchangeRates = myExchangeDatabase.ExchangeRates.ToList<ExchangeRate>();
            dataGrid.ItemsSource = allMyExchangeRates;
        }

        private void LoadTable(object sender, RoutedEventArgs e)
        {
            loadTable();
        }

        private void Insert(object sender, RoutedEventArgs e)
        {
            ExchangeRate er = new ExchangeRate();
            er.ValidityDate = ValidityDate.SelectedDate.Value.Date;
            er.CurrencyFrom = int.Parse(CurrencyFromCB.Text);
            er.CurrencyTo = int.Parse(CurrencyToCB.Text);
            er.Rate = int.Parse(Rate.Text);
            if (checkBox.IsChecked == true)
            {
                er.IsActive = 1;
            }
            else
            {
                er.IsActive = 0;
            }

            myExchangeDatabase.ExchangeRates.Add(er);
            myExchangeDatabase.SaveChanges();
            loadTable();
            //MessageBox.Show(ValidityDate.SelectedDate.Value.Date.ToString());
        }

        private void Edit(object sender, RoutedEventArgs e)
        {
            ExchangeRate er = (ExchangeRate)dataGrid.SelectedItem;
            er.ValidityDate = ValidityDate.SelectedDate.Value.Date;
            er.CurrencyFrom = int.Parse(CurrencyFromCB.Text);
            er.CurrencyTo = int.Parse(CurrencyToCB.Text);
            er.Rate = int.Parse(Rate.Text);
            if (checkBox.IsChecked == true)
            {
                er.IsActive = 1;
            }
            else
            {
                er.IsActive = 0;
            }

            myExchangeDatabase.SaveChanges();
            loadTable();
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            ExchangeRate er = (ExchangeRate)dataGrid.SelectedItem;
            er.IsActive = 0;
            checkBox.IsChecked = false;
            myExchangeDatabase.SaveChanges();

            var allMyExchangeRates = myExchangeDatabase.ExchangeRates.ToList<ExchangeRate>();
            dataGrid.ItemsSource = allMyExchangeRates;
        }

        private void populateTextBox(object sender, SelectedCellsChangedEventArgs e)
        {
            ExchangeRate er = (ExchangeRate)dataGrid.SelectedItem;
            ExchangeRatesId.Content = er.ExchangeRatesId.ToString();
            ValidityDate.SelectedDate = er.ValidityDate;
            Rate.Text = er.Rate.ToString();
            CurrencyFromCB.Text = er.CurrencyFrom.ToString();
            CurrencyToCB.Text = er.CurrencyTo.ToString();
            if (er.IsActive == 1)
            {
                checkBox.IsChecked = true;
            }
            else
            {
                checkBox.IsChecked = false;
            }
        }

        private void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
