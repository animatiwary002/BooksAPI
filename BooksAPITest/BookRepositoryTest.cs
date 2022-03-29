using BooksAPI.Models;
using BooksAPI.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Xunit;


namespace BooksAPITest
{
    public class BookRepositoryTests
    {
        private DbContextOptions<BookContext> options;
        public BookRepositoryTests()
        {
            options = new DbContextOptionsBuilder<BookContext>()
           .UseInMemoryDatabase(databaseName: "BookDB")
           .Options;
            using (var context = new BookContext(options))
            {
                context.Books.Add(new Book
                {
                    Title = "Asp.NET Core",
                    Id = "1",
                    Authors = "Jhon",
                    Isbn = "11-0001-12-17",
                    PublicationDate = "23-08-2020"
                });
                context.Books.Add(new Book
                {
                    Title = "Asp.NET Web API",
                    Id = "2",
                    Authors = "Alexis",
                    Isbn = "11-0001-12-45",
                    PublicationDate = "28-08-2021"
                });
                context.SaveChanges();
            }
        }
        [Fact]
        public void GetBooksTest()
        {
            using (var context = new BookContext(options))
            {
                var repo = new BookRepository(context);
                var books = repo.Get();
                Assert.Equal(2, books.ToString().Length);
            }
        }
        [Fact]
        public void GetBookByIDTest()
        {
            using (var context = new BookContext(options))
            {
                var repo = new BookRepository(context);
                var bok = repo.Get("2");
                Assert.Equal("Alexis", bok.Result.Authors);
            }
        }
        [Fact]
        public async void InsertBookTest()
        {
            Book bok = new Book { Id = "3", Title = "WPF", Authors = "Smith", Isbn = "11-0001-12-45", PublicationDate = "28-08-2021" };
            using (var context = new BookContext(options))
            {
                var repo = new BookRepository(context);
                await repo.Create(bok);
                var allBooks = repo.Get();
                Assert.Equal(3, allBooks.ToString().Length);
            }
        }

        [Fact]
        public async void UpdateBookTest()
        {
            InsertBookTest();
            Book bok = new Book { Id = "3", Title = "WPF Volumn2", Authors = "Smith", Isbn = "11-0001-12-45", PublicationDate = "28-08-2021" };
            using (var context = new BookContext(options))
            {
                var repo = new BookRepository(context);
                await repo.Update(bok);
                var book = repo.Get("2");
                Assert.Equal("WPF Volumn2", book.Result.Title);
            }
        }

        [Fact]
        public async void DeleteBookTest()
        {
            InsertBookTest();
            using (var context = new BookContext(options))
            {
                var repo = new BookRepository(context);
                await repo.Delete("3");
                var books = repo.Get();
                Assert.Equal(2, books.ToString().Length);
            }
        }
    }
}
