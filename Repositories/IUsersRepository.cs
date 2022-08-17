using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRates.Repositories
{
    public interface IUsersRepository
    {
        List<User> GetAllUsers();
        string InsertUser(User user);
        string UpdateUser(User user, User newUser);
        void DeleteUser(User user);
    }
}
