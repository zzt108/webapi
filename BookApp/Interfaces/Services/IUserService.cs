using Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.Services {
    public interface IUserService {
        User GetUserById(Guid userId);
        User GetUserByName(string name);
        User AddUser(User user);
        User UpdateUser(User user);
        bool DeleteUser(User user);
    }
}
