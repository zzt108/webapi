using Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.DomainModels;
using Interfaces.Repositories;

namespace Implementation {
    public class UserService : IUserService {
        private readonly IUserRepository UserRepository;

        public UserService(IUserRepository userRepository) {
            UserRepository = userRepository;
        }
        public User AddUser(User user) {
            UserRepository.Add(user);
            UserRepository.SaveChanges();
            return user;
        }

        public bool DeleteUser(User user) {
            UserRepository.Delete(user);
            UserRepository.SaveChanges();
            return true;
        }

        public User GetUserById(Guid userId) {
            return UserRepository.GetUserById(userId);
        }

        public User GetUserByName(string name) {
            return UserRepository.GetUserByName(name);
        }

        public User UpdateUser(User user) {
            UserRepository.Update(user);
            UserRepository.SaveChanges();
            return user;
        }
    }
}
