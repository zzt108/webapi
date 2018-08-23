using Models.DomainModels;
using Models.ExtendedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.Repositories {
    public interface IBookRepository : IBaseRepository<Book, long> {
        IQueryable<BookExtended> GetBooksByUserId(Guid userId);
        Book GetBookByID(Guid bookId);

    }
}
