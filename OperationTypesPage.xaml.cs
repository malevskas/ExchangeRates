using ExchangeRates.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            otHelper = new OperationTypesHelper();
        }

        private void LoadTable(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = otHelper.loadTable();
        }

        private void Insert(object sender, RoutedEventArgs e)
        {
            string result = otHelper.insert(Code.Text, OperationName.Text, checkBox.IsChecked);
            if (!result.Equals("ok"))
            {
                MessageBox.Show(result);
            }
            else
            {
                dataGrid.ItemsSource = otHelper.loadTable();
            }
        }

        private void Edit(object sender, RoutedEventArgs e)
        {
            string result = otHelper.edit((OperationType)dataGrid.SelectedItem, Code.Text, OperationName.Text, checkBox.IsChecked);
            if (!result.Equals("ok"))
            {
                MessageBox.Show(result);
            }
            else
            {
                dataGrid.ItemsSource = otHelper.loadTable();
            }
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            otHelper.delete((OperationType)dataGrid.SelectedItem);
            checkBox.IsChecked = false;
            dataGrid.ItemsSource = otHelper.loadTable();
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

        private void PreviewTextNumericInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[.][0-9]+$|^[0-9]*[.]{0,1}[0-9]*$");
            e.Handled = !regex.IsMatch(e.Text);
        }

        private void PreviewTextAlphabeticInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[a-zA-Z ]");
            e.Handled = !regex.IsMatch(e.Text);
        }
    }
}
