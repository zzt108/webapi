using Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace Implementation {
    public class TypeRegistrations {
        public static void RegisterType(IUnityContainer unityContainer) {
            unityContainer.RegisterType<IUserService, UserService>();
            unityContainer.RegisterType<IBookService, BookService>();
        }
    }
}
