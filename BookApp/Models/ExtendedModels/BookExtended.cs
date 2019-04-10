using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ExtendedModels {
    // Why book extended is not inheriting from book
    // Why user name is stored when it is a function retrieving data from User entity?
    public class BookExtended {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDatetime { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; } // storing user name is hardly justifiable
        public IEnumerable<BookExtended> MoreUserBooks { get; set; }
    }
}
