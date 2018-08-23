using Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Unity.Resolution;

namespace Repository.BaseRepository {
    [Serializable]
    public abstract class BaseRepository<TDomainClass> : IBaseRepository<TDomainClass, long>
       where TDomainClass : class {
        #region Private
        private readonly IUnityContainer container;
        #endregion
        #region Database sets
        /// <summary>
        /// Primary database set
        /// </summary>
        protected abstract IDbSet<TDomainClass> DbSet { get; }
        #endregion
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        protected BaseRepository(IUnityContainer container) {

            if (container == null) {
                throw new ArgumentNullException("container");
            }
            this.container = container;
            MasterDB = (BaseDBContext) container.Resolve(typeof(BaseDBContext), 
                new ResolverOverride[] { new ParameterOverride("connectionString", "OurLocalDB") });
        }

        #endregion


        #region Read Operations
        /// <summary>
        /// Find entry by key
        /// </summary>
        public virtual IQueryable<TDomainClass> Find(TDomainClass instance) {
            return DbSet.Find(instance) as IQueryable<TDomainClass>;
        }
        /// <summary>
        /// Find Entity by Id
        /// </summary>
        public virtual TDomainClass Find(long id) {
            return DbSet.Find(id);
        }
        /// <summary>
        /// Get All Entites 
        /// </summary>
        /// <returns></returns>
        public virtual IQueryable<TDomainClass> GetAll() {
            return DbSet;
        }

        #endregion

        #region Public

        /// <summary>
        /// Master DB Context
        /// </summary>
        public BaseDBContext MasterDB;
        /// <summary>
        /// Create object instance
        /// </summary>
        public virtual TDomainClass Create() {
            TDomainClass result = container.Resolve<TDomainClass>();
            return result;
        }

        /// <summary>
        /// Save Changes in the entities
        /// </summary>
        public void SaveChanges() {
            try {
                MasterDB.SaveChanges();
            } catch (DbEntityValidationException ex) {
                // Retrieve the error messages as a list of strings.
                var errorMessages = new List<string>();
                foreach (DbEntityValidationResult validationResult in ex.EntityValidationErrors) {
                    var entityName = validationResult.Entry.Entity.GetType().Name;
                    errorMessages.AddRange(validationResult.ValidationErrors.Select(error => entityName + "." + error.PropertyName + ": " + error.ErrorMessage));
                }
            }
        }

        /// <summary>
        /// Delete an entry
        /// </summary>
        public virtual void Delete(TDomainClass instance) {
            DbSet.Remove(instance);
        }
        /// <summary>
        /// Add an entry
        /// </summary>
        public virtual void Add(TDomainClass instance) {
            DbSet.Add(instance);
        }
        /// <summary>
        /// Add an entry
        /// </summary>
        public virtual void Update(TDomainClass instance) {
            DbSet.AddOrUpdate(instance);
        }

        #endregion



    }

}
