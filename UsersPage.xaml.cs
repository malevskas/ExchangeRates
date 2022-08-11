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
    /// Interaction logic for UsersPage.xaml
    /// </summary>
    public partial class UsersPage : Page
    {
        private UsersHelper uHelper;

        public UsersPage()
        {
            InitializeComponent();
            uHelper = new UsersHelper();
        }

        private void LoadTable(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = uHelper.loadTable();
        }
        
        private void Insert(object sender, RoutedEventArgs e)
        {
            string result = uHelper.insert(FirstName.Text, Surname.Text, checkBox.IsChecked);
            if (!result.Equals("ok"))
            {
                MessageBox.Show(result);
            }
            else
            {
                dataGrid.ItemsSource = uHelper.loadTable();
            }
        }

        private void Edit(object sender, RoutedEventArgs e)
        {
            string result = uHelper.edit((User)dataGrid.SelectedItem, FirstName.Text, Surname.Text, checkBox.IsChecked);
            if (!result.Equals("ok"))
            {
                MessageBox.Show(result);
            }
            else
            {
                dataGrid.ItemsSource = uHelper.loadTable();
            }
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            uHelper.delete((User)dataGrid.SelectedItem);
            checkBox.IsChecked = false;
            dataGrid.ItemsSource = uHelper.loadTable();
        }

        private void populateTextBox(object sender, SelectedCellsChangedEventArgs e)
        {
            User user = (User)dataGrid.SelectedItem;
            UserId.Content = user.UserId.ToString();
            FirstName.Text = user.FirstName;
            Surname.Text = user.Surname;
            if (user.IsActive == 1)
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
            Regex regex = new Regex("^[a-zA-Z ]");
            e.Handled = !regex.IsMatch(e.Text);
        }
    }
}
