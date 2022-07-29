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
        public UsersPage()
        {
            InitializeComponent();
        }

        Entity myExchangeDatabase = new Entity();

        private void LoadTable(object sender, RoutedEventArgs e)
        {
            var allMyUsers = myExchangeDatabase.Users.ToList<User>();
            dataGrid.ItemsSource = allMyUsers;
        }

        private void Insert(object sender, RoutedEventArgs e)
        {
            int isActive = 0;
            if (checkBox.IsChecked == true)
            {
                isActive = 1;
            }

            User user = new User();
            user.FirstName = FirstName.Text;
            user.Surname = LastName.Text;
            user.IsActive = isActive;
            myExchangeDatabase.Users.Add(user);
            myExchangeDatabase.SaveChanges();

            var allMyUsers = myExchangeDatabase.Users.ToList<User>();
            dataGrid.ItemsSource = allMyUsers;
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            User user = (User)dataGrid.SelectedItem;
            user.IsActive = 0;
            myExchangeDatabase.SaveChanges();

            var allMyUsers = myExchangeDatabase.Users.ToList<User>();
            dataGrid.ItemsSource = allMyUsers;
        }

        private void populateTextBox(object sender, SelectedCellsChangedEventArgs e)
        {
            User user = (User)dataGrid.SelectedItem;
            UserId.Text = user.UserId.ToString();
            FirstName.Text = user.FirstName;
            LastName.Text = user.Surname;
            if (user.IsActive == 1)
            {
                checkBox.IsChecked = true;
            }
        }
    }
}
