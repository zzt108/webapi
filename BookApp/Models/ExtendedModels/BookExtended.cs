using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ExtendedModels {
    public class BookExtended {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDatetime { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public IEnumerable<BookExtended> MoreUserBooks { get; set; }
    }
}
