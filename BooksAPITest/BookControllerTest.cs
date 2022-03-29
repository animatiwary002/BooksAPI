using BooksAPI.Controllers;
using BooksAPI.Models;
using BooksAPI.Repository;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BooksAPITest
{
    public class BookControllerTests
    {
        private readonly Mock<IBookRepository> service;
        public BookControllerTests()
        {
            service = new Mock<IBookRepository>();
        }
        [Fact]
        public async void GetBook_BookExistsInRepo()
        {
            //arrange
            // var book = GetSampleBook();
            service.Setup(x => x.Get())
                .Returns(GetSampleBook);
            var controller = new BooksController(service.Object);

            //act
            var actionResult = controller.GetAllBooks();
            var result = await actionResult as OkObjectResult;
            var actual = result.Value as IEnumerable<Book>;

            //assert
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(GetSampleBook().Result.Count(), actual.Count());
        }
        [Fact]
        public async void GetBookById_BookwithSpecificIdExists()
        {
            //arrange
            var books = GetSampleBook();
            var firstBook = books.Result.ElementAt(0);
            service.Setup(x => x.Get("1"))
                .Returns((firstBook));
            var controller = new BooksController(service.Object);

            //act
            var actionResult = await controller.GetBooksById("1");

            //Assert
            Assert.IsType<OkObjectResult>(actionResult);

            actionResult.Result.Equals(firstBook);
        }
        private Task<IEnumerable<Book>> GetSampleBook()
        {
            List<Book> output = new List<Book>
            {
                new Book
                {
                     Title = "Asp.NET Core",
                    Id = "1",
                    Authors = "Jhon",
                    Isbn = "11-0001-12-17",
                    PublicationDate = "23-08-2020"
                },
                new Book
                {
                  Title = "Asp.NET Web API",
                    Id = "2",
                    Authors = "Alexis",
                    Isbn = "11-0001-12-45",
                    PublicationDate = "28-08-2021"
                }
            };
            return (Task<IEnumerable<Book>>)output.AsEnumerable();

        }

    }
}
