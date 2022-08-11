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
using System.Diagnostics;
using System.Xml;

namespace ExchangeRates
{
    /// <summary>
    /// Interaction logic for OfficialRatesPage.xaml
    /// </summary>
    public partial class OfficialRatesPage : Page
    {
        private OfficialRatesHelper orHelper;

        public OfficialRatesPage()
        {
            InitializeComponent();
            orHelper = new OfficialRatesHelper();
            CurrencyCB.ItemsSource = orHelper.fillCB();
        }

        private void LoadTable(object sender, RoutedEventArgs e)
        {
            orHelper.loadTable();
            dataGrid.ItemsSource = orHelper.loadTable();
        }

        private void Insert(object sender, RoutedEventArgs e)
        {
            string result = orHelper.insert(ValidityDate, /*ValidityDate.SelectedDate, */(Currency)CurrencyCB.SelectedItem, Rate.Text, checkBox.IsChecked);
            if (!result.Equals("ok"))
            {
                MessageBox.Show(result);
            }
            else
            {
                dataGrid.ItemsSource = orHelper.loadTable();
            }
        }

        private void Edit(object sender, RoutedEventArgs e)
        {
            string result = orHelper.edit((OfficialRate)dataGrid.SelectedItem, ValidityDate.SelectedDate, (Currency)CurrencyCB.SelectedItem, Rate.Text, checkBox.IsChecked);
            if (!result.Equals("ok"))
            {
                MessageBox.Show(result);
            }
            else
            {
                dataGrid.ItemsSource = orHelper.loadTable();
            }
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            orHelper.delete((OfficialRate)dataGrid.SelectedItem);
            checkBox.IsChecked = false;
            dataGrid.ItemsSource = orHelper.loadTable();
        }

        private void populateTextBox(object sender, SelectedCellsChangedEventArgs e)
        {
            OfficialRate or = (OfficialRate)dataGrid.SelectedItem;
            OfficialRatesId.Content = or.OfficialRatesId.ToString();
            ValidityDate.SelectedDate = or.ValidityDate;
            Rate.Text = or.Rate.ToString();
            CurrencyCB.Text = or.Currency1.CurrencyName;
            if (or.IsActive == 1)
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
