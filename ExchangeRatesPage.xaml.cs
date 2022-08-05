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
            erHelper = new ExchangeRatesHelper(ExchangeRatesId, Rate, CurrencyFromCB, CurrencyToCB, ValidityDate, checkBox, dataGrid);
            erHelper.fillCB();
        }

        private void LoadTable(object sender, RoutedEventArgs e)
        {
            erHelper.loadTable();
        }

        private void Insert(object sender, RoutedEventArgs e)
        {
            erHelper.insert();
            //MessageBox.Show(ValidityDate.SelectedDate.Value.Date.ToString());
        }

        private void Edit(object sender, RoutedEventArgs e)
        {
            erHelper.edit();
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            erHelper.delete();
        }

        private void populateTextBox(object sender, SelectedCellsChangedEventArgs e)
        {
            erHelper.populateTextBox();
        }

        private void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
