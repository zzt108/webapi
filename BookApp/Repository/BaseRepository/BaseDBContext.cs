using Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Repository.BaseRepository {
    public sealed class BaseDBContext : DbContext {
        #region Private
        private bool isDefault;
        #endregion

        #region Protected

        #endregion

        #region Constructor
        public BaseDBContext() {
        }

        public BaseDBContext(string connectionString)
           : base(connectionString) {
        }

        #endregion

        #region protected

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().
                HasMany(u => u.Books)
                .WithRequired(b => b.User)
                .HasForeignKey(b => b.UserId);
        }


        #endregion

        #region Public

        public override int SaveChanges() {
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync() {
            return base.SaveChangesAsync();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken) {
            return base.SaveChangesAsync(cancellationToken);
        }

        #endregion

        #region Entities
        /// <summary>
        /// Organisation Db Set
        /// </summary>
        public DbSet<Book> Books { get; set; }

        public DbSet<User> Users { get; set; }


        #endregion


    }
}
