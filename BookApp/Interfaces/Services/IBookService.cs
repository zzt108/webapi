using Models.DomainModels;
using Models.ExtendedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.Services {
    public interface IBookService {
        Book GetBookById(Guid bookId);
        IEnumerable<BookExtended> GetBooksByUserId(Guid userId);
        Book AddBook(Book book);
        Book UpdateBook(Book book);
        bool DeleteBook(Book book);
    }
}
