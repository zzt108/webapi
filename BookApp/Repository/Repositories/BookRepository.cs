using Interfaces.Repositories;
using Models.DomainModels;
using Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using System.Data.Entity;
using Models.ExtendedModels;

namespace Repository.Repositories {
    public class BookRepository : BaseRepository<Book>, IBookRepository {
        public BookRepository(IUnityContainer container) : base(container) {
        }
        protected override IDbSet<Book> DbSet => MasterDB.Books;

        public Book GetBookByID(Guid bookId) {
            return GetAll().Where(b => b.Id == bookId).SingleOrDefault();
        }

        public IQueryable<BookExtended> GetBooksByUserId(Guid userId) {
            return GetAll().Where(b => b.UserId == userId).Select(b =>
                new BookExtended() {
                    Author = b.Author,
                    Content = b.Content,
                    CreatedDatetime = b.CreatedDatetime,
                    Id = b.Id,
                    Title = b.Title,
                    UserId = b.UserId,
                    UserName = b.User.FirstName + " " + b.User.LastName
                }
            );
        }

    }
}
