using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRates.Repositories
{
    internal class UsersRepository : IUsersRepository
    {
        private Entity myExchangeDatabase = new Entity();

        public void DeleteUser(User user)
        {
            user.IsActive = 0;
            myExchangeDatabase.SaveChanges();
        }

        public List<User> GetAllUsers()
        {
            return myExchangeDatabase.Users.ToList();
        }

        public string InsertUser(User user)
        {
            myExchangeDatabase.Users.Add(user);
            myExchangeDatabase.SaveChanges();
            return "ok";
        }

        public string UpdateUser(User user, User newUser)
        {
            user.FirstName = newUser.FirstName;
            user.Surname = newUser.Surname;
            user.IsActive = newUser.IsActive;
            myExchangeDatabase.SaveChanges();
            return "ok";
        }
    }
}
