using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BooksAPI.Models
{
    public class Book
    {
        [Required(AllowEmptyStrings = true)]
        public string Id { get; set; }
        public string Title { get; set; }
        public string Authors { get; set; }

        [IsbnValidator(ErrorMessage = "ISBN should be of 13 digits")]
        public string Isbn { get; set; }
        public string PublicationDate { get; set; }
    }
}
