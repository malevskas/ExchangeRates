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
        //private readonly IUsersRepository usersRepository = new UsersRepository();
        private readonly IRepository<User> userRepo = new Repository<User>();

        public List<User> loadTable()
        {
            return userRepo.GetAll();
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

                return userRepo.Insert(user);
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

                return userRepo.Update(user);
            }
        }

        public void delete(User user)
        {
            user.IsActive = 0;
            userRepo.Update(user);
        }
    }
}
