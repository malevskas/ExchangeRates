using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ExchangeRates.Helpers
{
    internal class UsersHelper
    {
        private Entity myExchangeDatabase = new Entity();
        private Label UserId;
        private TextBox FirstName;
        private TextBox Surname;
        private CheckBox checkBox;
        private DataGrid dataGrid;

        public UsersHelper(Label UserId, TextBox FirstName, TextBox Surname, CheckBox checkBox, DataGrid dataGrid)
        {
            this.UserId = UserId;
            this.FirstName = FirstName;
            this.Surname = Surname;
            this.checkBox = checkBox;
            this.dataGrid = dataGrid;
        }

        public void loadTable()
        {
            var allMyUsers = myExchangeDatabase.Users.ToList<User>();
            dataGrid.ItemsSource = allMyUsers;
        }

        public void insert()
        {
            if (FirstName.Text == "" || Surname.Text == "")
            {
                MessageBox.Show("Please fill out all fields.");
            }
            else
            {
                User user = new User();
                user.FirstName = FirstName.Text;
                user.Surname = Surname.Text;
                if (checkBox.IsChecked == true)
                {
                    user.IsActive = 1;
                }
                else
                {
                    user.IsActive = 0;
                }

                myExchangeDatabase.Users.Add(user);
                myExchangeDatabase.SaveChanges();
                loadTable();
            }
        }

        public void edit()
        {
            if (FirstName.Text == "" || Surname.Text == "")
            {
                MessageBox.Show("Please fill out all fields.");
            }
            else
            {
                User user = (User)dataGrid.SelectedItem;
                user.FirstName = FirstName.Text;
                user.Surname = Surname.Text;
                if (checkBox.IsChecked == true)
                {
                    user.IsActive = 1;
                }
                else
                {
                    user.IsActive = 0;
                }

                myExchangeDatabase.SaveChanges();
                loadTable();
            }
        }

        public void delete()
        {
            User user = (User)dataGrid.SelectedItem;
            user.IsActive = 0;
            checkBox.IsChecked = false;

            myExchangeDatabase.SaveChanges();
            loadTable();
        }

        public void populateTextBox()
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
    }
}
