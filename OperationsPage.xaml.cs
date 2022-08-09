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
            oHelper = new OperationsHelper(OperationId, OperationTypeCB, UserCB, CurrencyFromCB, CurrencyToCB, Amount, OperationDate, dataGrid);
            oHelper.fillCurrencyCB();
            oHelper.fillUserCB();
            oHelper.fillOperationTypeCB();
        }

        private void LoadTable(object sender, RoutedEventArgs e)
        {
            oHelper.loadTable();
        }

        private void Insert(object sender, RoutedEventArgs e)
        {
            oHelper.insert();
        }

        private void Edit(object sender, RoutedEventArgs e)
        {
            oHelper.edit();
        }

        private void populateTextBox(object sender, SelectedCellsChangedEventArgs e)
        {
            oHelper.populateTextBox();
        }

        private void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[.][0-9]+$|^[0-9]*[.]{0,1}[0-9]*$");
            e.Handled = !regex.IsMatch(e.Text);
        }
    }
}
