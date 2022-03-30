using BooksAPI.Models;
using BooksAPI.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BooksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        private readonly ILogger<BooksController> _logger;

        public BooksController(IBookRepository bookRepository, ILogger<BooksController> logger)
        {
            _bookRepository = bookRepository;
            _logger = logger;
        }

        [HttpGet("allBooks")]
        public async Task<IEnumerable<Book>> GetAllBooks()
        {
            _logger.LogTrace(_bookRepository.Get().ToString());
            return await _bookRepository.Get();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBooksById(string param)
        {
            return await _bookRepository.Get(param);
        }

        [HttpGet]
        public async Task<ActionResult<Book>> GetBooksBySearchParam([FromQuery] BookSearchParams book)
        {
            try
            {
                if (!string.IsNullOrEmpty(book.Title))
                {
                    return await _bookRepository.Get(book.Title);
                }
                else if (!string.IsNullOrEmpty(book.Authors)) {
                    return await _bookRepository.Get(book.Authors);
                }
                else if (!string.IsNullOrEmpty(book.Isbn))
                {
                    return await _bookRepository.Get(book.Isbn);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            
        }

        [HttpPost]
        public async Task<ActionResult<Book>> PostBooks([FromBody] Book book)
        {
            var newBook = await _bookRepository.Create(book);
            return CreatedAtAction(nameof(GetAllBooks), new { id = newBook.Id }, newBook);
        }

        [HttpPut]
        public async Task<ActionResult> PutBooks(string id, [FromBody] Book book)
        {
            if(id.Equals(book.Id, StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest();
            }

            await _bookRepository.Update(book);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBooks(string id)
        {
            var bookToDelete = await _bookRepository.Get(id);
            if (bookToDelete == null)
            {
                return NotFound();
            }

            await _bookRepository.Delete(bookToDelete.Id);
            return NoContent();
        }
    }
}
