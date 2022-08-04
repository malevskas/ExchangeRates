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
    /// Interaction logic for OfficialRatesPage.xaml
    /// </summary>
    public partial class OfficialRatesPage : Page
    {
        public OfficialRatesPage()
        {
            InitializeComponent();
            fillCB();
        }

        private Entity myExchangeDatabase = new Entity();

        private void fillCB()
        {
            var allMyCurrencies = myExchangeDatabase.Currencies.ToList<Currency>();
            CurrencyCB.Items.Clear();
            CurrencyCB.ItemsSource = allMyCurrencies;
        }

        private void loadTable()
        {
            var allMyOfficialRates = myExchangeDatabase.OfficialRates.ToList<OfficialRate>();
            dataGrid.ItemsSource = allMyOfficialRates;
        }

        private void LoadTable(object sender, RoutedEventArgs e)
        {
            loadTable();
        }

        private void Insert(object sender, RoutedEventArgs e)
        {
            OfficialRate or = new OfficialRate();
            or.ValidityDate = ValidityDate.SelectedDate.Value.Date;
            or.Currency = int.Parse(CurrencyCB.Text);
            or.Rate = int.Parse(Rate.Text);
            if (checkBox.IsChecked == true)
            {
                or.IsActive = 1;
            }
            else
            {
                or.IsActive = 0;
            }

            myExchangeDatabase.OfficialRates.Add(or);
            myExchangeDatabase.SaveChanges();
            loadTable();
        }

        private void Edit(object sender, RoutedEventArgs e)
        {
            OfficialRate or = (OfficialRate)dataGrid.SelectedItem;
            or.ValidityDate = ValidityDate.SelectedDate.Value.Date;
            or.Currency = int.Parse(CurrencyCB.Text);
            or.Rate = int.Parse(Rate.Text);
            if (checkBox.IsChecked == true)
            {
                or.IsActive = 1;
            }
            else
            {
                or.IsActive = 0;
            }

            myExchangeDatabase.OfficialRates.Add(or);
            myExchangeDatabase.SaveChanges();
            loadTable();
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            OfficialRate or = (OfficialRate)dataGrid.SelectedItem;
            or.IsActive = 0;
            checkBox.IsChecked = false;

            myExchangeDatabase.SaveChanges();
            loadTable();
        }

        private void populateTextBox(object sender, SelectedCellsChangedEventArgs e)
        {
            OfficialRate or = (OfficialRate)dataGrid.SelectedItem;
            OfficialRatesId.Content = or.OfficialRatesId.ToString();
            ValidityDate.SelectedDate = or.ValidityDate;
            Rate.Text = or.Rate.ToString();
            CurrencyCB.Text = or.Currency.ToString();
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
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
