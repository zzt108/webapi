using Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.DomainModels;
using Models.ExtendedModels;
using Interfaces.Repositories;

namespace Implementation {
    public class BookService : IBookService {
        private readonly IBookRepository BookRepository;
        private readonly IUserRepository UserRepository;

        public BookService(IBookRepository bookRepository, IUserRepository userRepository) {
            BookRepository = bookRepository;
            UserRepository = userRepository;
        }


        public Book AddBook(Book book) {
            BookRepository.Add(book);
            BookRepository.SaveChanges();
            return book;
        }

        public bool DeleteBook(Book book) {
            BookRepository.Delete(book);
            BookRepository.SaveChanges();
            return true;
        }

        public Book GetBookById(Guid bookId) {
            return BookRepository.GetBookByID(bookId);
        }

        public IEnumerable<BookExtended> GetBooksByUserId(Guid userId) {
            var books = BookRepository.GetBooksByUserId(userId).ToList();
            var result = new List<BookExtended>();
            foreach (var item in books) {
                item.MoreUserBooks = UserRepository.GetMoreUserBooks(item.UserId, item.Id);
                result.Add(item);
            }
            return result;
        }

        public Book UpdateBook(Book book) {
            BookRepository.Update(book);
            BookRepository.SaveChanges();
            return book;
        }
    }
}
