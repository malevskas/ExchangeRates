using ExchangeRates.Repositories;
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
        private readonly IUsersRepository usersRepository = new UsersRepository();

        public List<User> loadTable()
        {
            return usersRepository.GetAllUsers();
        }

        public string insert(string FirstName, string Surname, bool? IsChecked)
        {
            if (FirstName == "" || Surname == "")
            {
                return "Please fill out all fields.";
            }
            else
            {
                User user = new User();
                user.FirstName = FirstName;
                user.Surname = Surname;
                if (IsChecked == true)
                {
                    user.IsActive = 1;
                }
                else
                {
                    user.IsActive = 0;
                }

                return usersRepository.InsertUser(user);
            }
        }

        public string edit(User user, string FirstName, string Surname, bool? IsChecked)
        {
            if (FirstName == "" || Surname == "")
            {
                return "Please fill out all fields.";
            }
            else
            {
                User newUser = new User();
                newUser.FirstName = FirstName;
                newUser.Surname = Surname;
                if (IsChecked == true)
                {
                    newUser.IsActive = 1;
                }
                else
                {
                    newUser.IsActive = 0;
                }

                return usersRepository.UpdateUser(user, newUser);
            }
        }

        public void delete(User user)
        {
            usersRepository.DeleteUser(user);
        }
    }
}
