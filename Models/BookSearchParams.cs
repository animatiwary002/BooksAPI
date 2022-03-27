using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksAPI.Models
{
    public class BookSearchParams
    {
        public string Title { get; set; }
        public string Authors { get; set; }
        [IsbnValidator(ErrorMessage = "ISBN should be of 13 digits")]
        public string Isbn { get; set; }
    }
}
