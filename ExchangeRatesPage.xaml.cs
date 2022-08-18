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
using ExchangeRates.Helpers;

namespace ExchangeRates
{
    /// <summary>
    /// Interaction logic for ExchangeRatesPage.xaml
    /// </summary>
    public partial class ExchangeRatesPage : Page
    {
        private ExchangeRatesHelper erHelper;
        public ExchangeRatesPage()
        {
            InitializeComponent();
            erHelper = new ExchangeRatesHelper();
            CurrencyFromCB.ItemsSource = erHelper.fillCB();
            CurrencyToCB.ItemsSource = erHelper.fillCB();
        }

        private void LoadTable(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = erHelper.loadTable();
        }

        private void Insert(object sender, RoutedEventArgs e)
        {
            string result = erHelper.insert(ValidityDate.SelectedDate, (Currency)CurrencyFromCB.SelectedItem, (Currency)CurrencyToCB.SelectedItem, Rate.Text, checkBox.IsChecked);
            if (!result.Equals("ok"))
            {
                MessageBox.Show(result);
            }
            else
            {
                dataGrid.ItemsSource = erHelper.loadTable();
            }
        }

        private void Edit(object sender, RoutedEventArgs e)
        {
            string result = erHelper.edit((ExchangeRate)dataGrid.SelectedItem, ValidityDate.SelectedDate, (Currency)CurrencyFromCB.SelectedItem, (Currency)CurrencyToCB.SelectedItem, Rate.Text, checkBox.IsChecked);
            if (!result.Equals("ok"))
            {
                MessageBox.Show(result);
            }
            else
            {
                dataGrid.ItemsSource = erHelper.loadTable();
            }
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            string result = erHelper.delete((ExchangeRate)dataGrid.SelectedItem);
            if (result != "ok")
            {
                MessageBox.Show(result);
            }
            else
            {
                checkBox.IsChecked = false;
                dataGrid.ItemsSource = erHelper.loadTable();
            }
        }

        private void populateTextBox(object sender, SelectedCellsChangedEventArgs e)
        {
            ExchangeRate er = (ExchangeRate)dataGrid.SelectedItem;
            ExchangeRatesId.Content = er.ExchangeRatesId.ToString();
            ValidityDate.SelectedDate = er.ValidityDate;
            Rate.Text = er.Rate.ToString();
            CurrencyFromCB.Text = er.Currency.CurrencyName;
            CurrencyToCB.Text = er.Currency1.CurrencyName;
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
            Regex regex = new Regex("^[.][0-9]+$|^[0-9]*[.]{0,1}[0-9]*$");
            e.Handled = !regex.IsMatch(e.Text);
        }
    }
}
