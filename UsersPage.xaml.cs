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
    /// Interaction logic for UsersPage.xaml
    /// </summary>
    public partial class UsersPage : Page
    {
        private UsersHelper uHelper;

        public UsersPage()
        {
            InitializeComponent();
            uHelper = new UsersHelper(UserId, FirstName, Surname, checkBox, dataGrid);
        }

        private void LoadTable(object sender, RoutedEventArgs e)
        {
            uHelper.loadTable();
        }
        
        private void Insert(object sender, RoutedEventArgs e)
        {
            uHelper.insert();
        }

        private void Edit(object sender, RoutedEventArgs e)
        {
            uHelper.edit();
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            uHelper.delete();
        }

        private void populateTextBox(object sender, SelectedCellsChangedEventArgs e)
        {
            uHelper.populateTextBox();
        }
    }
}
