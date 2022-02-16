using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookstoreProj.Models.ViewModels
{
    public class PageInfo
    {
        public int TotalNumBooks { get; set; }
        public int BooksPerPage { get; set; }
        public int CurrentPage { get; set; }

        //figure out how many pages we need. 
        //we are casting TotalNumBooks as a double then casting the result as an int.
        public int TotalPages => (int) Math.Ceiling((double) TotalNumBooks / BooksPerPage);

    }
}
