using BooksAPI.Models;
using BooksAPI.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BooksAPI.Controllers
{
    [Route("api/[controller]/v1")]
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

        [HttpGet("getAllBooks")]
        public async Task<IEnumerable<Book>> GetAllBooks()
        {
            try
            {
                _logger.LogInformation(_bookRepository.Get().ToString());
                return await _bookRepository.Get();
            }
            catch(Exception ex)
            {
                _logger.LogError(JsonConvert.SerializeObject(ex));
                return (IEnumerable<Book>)StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBooksById(string param)
        {
            try
            {
                _logger.LogInformation(_bookRepository.Get(param).ToString());
                return await _bookRepository.Get(param);
            }
            catch(Exception ex)
            {
                _logger.LogError(JsonConvert.SerializeObject(ex));
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("getBookBySearchParam")]
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
                _logger.LogError(JsonConvert.SerializeObject(ex));
                return StatusCode(500, ex.Message);
            }
            
        }

        [HttpPost("addBook")]
        public async Task<ActionResult<Book>> PostBooks([FromBody] Book book)
        {
            try
            {
                var newBook = await _bookRepository.Create(book);
                _logger.LogInformation(_bookRepository.Create(book).ToString());
                return CreatedAtAction(nameof(GetAllBooks), new { id = newBook.Id }, newBook);
            }
            catch(Exception ex)
            {
                _logger.LogError(JsonConvert.SerializeObject(ex));
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("updateBook")]
        public async Task<ActionResult> PutBooks(string id, [FromBody] Book book)
        {
            try
            {
                if (id.Equals(book.Id, StringComparison.OrdinalIgnoreCase))
                {
                    return BadRequest();
                }

                _logger.LogInformation(_bookRepository.Update(book).ToString());
                await _bookRepository.Update(book);

                return NoContent();
            }
            catch(Exception ex)
            {
                _logger.LogError(JsonConvert.SerializeObject(ex));
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("deleteBook/{id}")]
        public async Task<ActionResult> DeleteBooks(string id)
        {
            try
            {
                var bookToDelete = await _bookRepository.Get(id);
                _logger.LogInformation(_bookRepository.Get(id).ToString());
                if (bookToDelete == null)
                {
                    return NotFound();
                }

                _logger.LogInformation(_bookRepository.Delete(bookToDelete.Id).ToString());
                await _bookRepository.Delete(bookToDelete.Id);
                return NoContent();
            }
            catch(Exception ex)
            {
                _logger.LogError(JsonConvert.SerializeObject(ex));
                return StatusCode(500, ex.Message);
            }
        }
    }
}
