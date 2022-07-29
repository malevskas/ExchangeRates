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

namespace ExchangeRates
{
    /// <summary>
    /// Interaction logic for CurrenciesPage.xaml
    /// </summary>
    public partial class CurrenciesPage : Page
    {
        public CurrenciesPage()
        {
            InitializeComponent();
        }

        private Entity myExchangeDatabase = new Entity();

        private void LoadTable(object sender, RoutedEventArgs e)
        {
            var allMyCurrencies = myExchangeDatabase.Currencies.ToList<Currency>();
            dataGrid.ItemsSource = allMyCurrencies;
        }

        private void Insert(object sender, RoutedEventArgs e)
        {
            int isActive = 0;
            if(checkBox.IsChecked == true)
            {
                isActive = 1;
            }

            Currency currency = new Currency();
            currency.Code = Code.Text;
            currency.CurrencyName = CurrencyName.Text;
            currency.IsActive = isActive;
            myExchangeDatabase.Currencies.Add(currency);
            myExchangeDatabase.SaveChanges();
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            Currency currency = (Currency)dataGrid.SelectedItem;
            currency.IsActive = 0;
            myExchangeDatabase.SaveChanges();

            var allMyCurrencies = myExchangeDatabase.Currencies.ToList<Currency>();
            dataGrid.ItemsSource = allMyCurrencies;
        }

        private void populateTextBox(object sender, SelectedCellsChangedEventArgs e)
        {
            Currency currency = (Currency)dataGrid.SelectedItem;
            CurrencyId.Text = currency.CurrencyId+"";
            Code.Text = currency.Code;
            CurrencyName.Text = currency.CurrencyName;
            if(currency.IsActive == 1)
            {
                checkBox.IsChecked = true;
            }
        }
    }
}
