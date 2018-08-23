using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Dependencies;
using Unity;

namespace BookApp.Unity {
    public class UnityRegistrationCache {
        private static readonly object LockObject = new object();
        private static HashSet<Type> registrations;

        /// <summary>
        /// Register types
        /// </summary>
        private static void Register(IUnityContainer container) {
            lock (LockObject) {
                if (registrations != null) {
                    return;
                }
                registrations = new HashSet<Type>();
                foreach (var r in container.Registrations.Where(r => r.Name == null)) {
                    if (!registrations.Contains(r.RegisteredType)) {
                        registrations.Add(r.RegisteredType);
                    }
                }
            }
        }

        /// <summary>
        /// True if the type is registered
        /// </summary>
        public static bool IsRegistered(IUnityContainer container, Type typeToCheck) {
            if (container == null) {
                throw new ArgumentNullException("container");
            }

            if (typeToCheck == null) {
                throw new ArgumentNullException("typeToCheck");
            }

            if (registrations == null) {
                Register(container);
            }

            // ReSharper disable PossibleNullReferenceException
            return registrations.Contains(typeToCheck);
            // ReSharper restore PossibleNullReferenceException
        }
    }



    public class UnityDependencyScope : IDependencyScope {
        #region Protected

        protected IUnityContainer container;

        #endregion
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public UnityDependencyScope(IUnityContainer container) {
            if (container == null) {
                throw new ArgumentNullException("container");
            }
            this.container = container;
        }

        /// <summary>
        /// Resolve a type
        /// </summary>
        public object GetService(Type serviceType) {
            if (!UnityRegistrationCache.IsRegistered(container, serviceType)) {
                if (serviceType.IsAbstract || serviceType.IsInterface) {
                    return null;
                }
            }
            return container.Resolve(serviceType);
        }

        /// <summary>
        /// Resolve multiple types
        /// </summary>
        public IEnumerable<object> GetServices(Type serviceType) {
            if (UnityRegistrationCache.IsRegistered(container, serviceType)) {
                return container.ResolveAll(serviceType);
            }
            return new List<object>();
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose() {
            container.Dispose();
        }

        #endregion
    }



    public class UnityDependencyResolver : UnityDependencyScope, IDependencyResolver, System.Web.Mvc.IDependencyResolver {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="container"></param>
        public UnityDependencyResolver(IUnityContainer container)
            : base(container) {
        }

        #endregion

        #region Public

        /// <summary>
        /// Begin a scope
        /// </summary>
        /// <returns></returns>
        public IDependencyScope BeginScope() {
            IUnityContainer child = container.CreateChildContainer();
            return new UnityDependencyScope(child);
        }

        #endregion
    }
}