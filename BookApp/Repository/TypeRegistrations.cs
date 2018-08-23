using Interfaces.Repositories;
using Repository.BaseRepository;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Unity.AspNet.Mvc;

namespace Repository {
    public class TypeRegistrations {
        public static void RegisterType(IUnityContainer unityContainer) {
            unityContainer.RegisterType<BaseDBContext>(new PerRequestLifetimeManager());
            unityContainer.RegisterType<IUserRepository, UserRepository>();
            unityContainer.RegisterType<IBookRepository, BookRepository>();
        }
    }
}
