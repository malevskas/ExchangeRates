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
            cHelper = new CurrenciesHelper(CurrencyId, Code, CurrencyName, checkBox, dataGrid);
        }

        private void LoadTable(object sender, RoutedEventArgs e)
        {
            cHelper.loadTable();
        }

        private void Insert(object sender, RoutedEventArgs e)
        {
            cHelper.insert();
        }

        private void Edit(object sender, RoutedEventArgs e)
        {
            cHelper.edit();
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            cHelper.delete();
        }

        private void populateTextBox(object sender, SelectedCellsChangedEventArgs e)
        {
            cHelper.populateTextBox();
        }
    }
}
