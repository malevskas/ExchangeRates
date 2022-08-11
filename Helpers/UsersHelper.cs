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

        public List<User> loadTable()
        {
            List<User> allMyUsers = myExchangeDatabase.Users.ToList<User>();
            return allMyUsers;
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

                myExchangeDatabase.Users.Add(user);
                myExchangeDatabase.SaveChanges();
                return "ok";
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

                myExchangeDatabase.SaveChanges();
                return "ok";
            }
        }

        public void delete(User user)
        {
            user.IsActive = 0;
            myExchangeDatabase.SaveChanges();
        }
    }
}
