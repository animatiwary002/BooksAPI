using BooksAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksAPI.Repository
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> Get();

        Task<Book> Get(string param);
        Task<Book> Create(Book book);
        Task Update(Book book);

        Task Delete(string param);

    }
}
