using ExchangeRates.Helpers;
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

namespace ExchangeRates
{
    /// <summary>
    /// Interaction logic for OperationTypesPage.xaml
    /// </summary>
    public partial class OperationTypesPage : Page
    {
        private OperationTypesHelper otHelper;

        public OperationTypesPage()
        {
            InitializeComponent();
            otHelper = new OperationTypesHelper(OperationTypeId, Code, OperationName, checkBox, dataGrid);
        }

        private void LoadTable(object sender, RoutedEventArgs e)
        {
            otHelper.loadTable();
        }

        private void Insert(object sender, RoutedEventArgs e)
        {
            otHelper.insert();
        }

        private void Edit(object sender, RoutedEventArgs e)
        {
            otHelper.edit();
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            otHelper.delete();
        }

        private void populateTextBox(object sender, SelectedCellsChangedEventArgs e)
        {
            otHelper.populateTextBox();
        }
    }
}
