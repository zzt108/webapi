using Models.DomainModels;
using Models.ExtendedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// using User classes in an interface defeats the purpose of interface itself
// should use IUser
namespace Interfaces.Repositories {
    public interface IUserRepository : IBaseRepository<User, long> {
        User GetUserById(Guid userId);
        User GetUserByName(string name);
        IEnumerable<BookExtended> GetMoreUserBooks(Guid userId, Guid bookId);
    }
}
