using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookstoreProj.Models
{ //going to implement (inherent) an instance of a IBookstoreRepository
    public class EFBookstoreProjectRepository : IBookstoreRepository
    {
        private BookstoreContext context { get; set; }
        //doing what we used to do in the controller
        public EFBookstoreProjectRepository(BookstoreContext temp) => context = temp;
        public IQueryable<Book> Books => context.Books;

        public void SaveBook(Book b)
        {
            context.SaveChanges();
        }

        public void CreateBook(Book b)
        {
            context.Add(b);
            context.SaveChanges();

        }

        public void DeleteBook(Book b)
        {
            context.Remove(b);
            context.SaveChanges();
        }
    }
}
