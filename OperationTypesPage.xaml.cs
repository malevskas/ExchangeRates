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
        public OperationTypesPage()
        {
            InitializeComponent();
        }

        private Entity myExchangeDatabase = new Entity();

        private void loadTable()
        {
            var allMyOperationTypes = myExchangeDatabase.OperationTypes.ToList<OperationType>();
            dataGrid.ItemsSource = allMyOperationTypes;
        }

        private void LoadTable(object sender, RoutedEventArgs e)
        {
            loadTable();
        }

        private void Insert(object sender, RoutedEventArgs e)
        {
            OperationType ot = new OperationType();
            ot.Code = Code.Text;
            ot.OperationName = OperationName.Text;
            if (checkBox.IsChecked == true)
            {
                ot.isActive = 1;
            }
            else
            {
                ot.isActive = 0;
            }

            myExchangeDatabase.OperationTypes.Add(ot);
            myExchangeDatabase.SaveChanges();
            loadTable();
        }

        private void Edit(object sender, RoutedEventArgs e)
        {
            OperationType ot = (OperationType)dataGrid.SelectedItem;
            ot.Code = Code.Text;
            ot.OperationName = OperationName.Text;
            if (checkBox.IsChecked == true)
            {
                ot.isActive = 1;
            }
            else
            {
                ot.isActive = 0;
            }

            myExchangeDatabase.SaveChanges();
            loadTable();
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            OperationType ot = (OperationType)dataGrid.SelectedItem;
            ot.isActive = 0;
            checkBox.IsChecked = false;

            myExchangeDatabase.SaveChanges();
            loadTable();
        }

        private void populateTextBox(object sender, SelectedCellsChangedEventArgs e)
        {
            OperationType ot = (OperationType)dataGrid.SelectedItem;
            OperationTypeId.Content = ot.OperationTypeId.ToString();
            Code.Text = ot.Code;
            OperationName.Text = ot.OperationName;
            if (ot.isActive == 1)
            {
                checkBox.IsChecked = true;
            }
            else
            {
                checkBox.IsChecked = false;
            }
        }
    }
}
