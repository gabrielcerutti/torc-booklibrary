using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Torc.BookLibrary.API.Controllers;
using Torc.BookLibrary.API.Data;
using Torc.BookLibrary.API.Data.Interfaces;

namespace Torc.BookLibrary.Tests.UnitTests;

public class BooksControllerTests
{
    [Fact]
    public async Task GetBooks_ReturnsOk_WithBooks()
    {
        // Arrange
        var mockRepo = new Mock<IBookRepository>();
        var mockLogger = new Mock<ILogger<BooksController>>();

        var books = new List<Book>
        {
            new Book { BookId = 1, Title = "Book1", FirstName = "John", LastName = "Doe", ISBN = "123", Type = "Fiction", Category = "Novel", TotalCopies = 1, CopiesInUse = 0, OwnershipStatus = 1 }
        };

        mockRepo.Setup(r => r.GetBooksAsync("John", null, null))
                .ReturnsAsync(books);

        var controller = new BooksController(mockRepo.Object, mockLogger.Object);

        // Act
        var result = await controller.GetBooks("John", null, null);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedBooks = Assert.IsAssignableFrom<List<Book>>(okResult.Value);
        Assert.Single(returnedBooks);
        Assert.Equal("John", returnedBooks[0].FirstName);

        // Optionally verify the repository was called as expected
        mockRepo.Verify(r => r.GetBooksAsync("John", null, null), Times.Once);
    }

    [Fact]
    public async Task GetBooks_ReturnsOK_WithoutBooks()
    {
        // Arrange
        var mockRepo = new Mock<IBookRepository>();
        var mockLogger = new Mock<ILogger<BooksController>>();
        var books = new List<Book>();
        mockRepo.Setup(r => r.GetBooksAsync(null, null, null))
                .ReturnsAsync(books);
        var controller = new BooksController(mockRepo.Object, mockLogger.Object);
        
        // Act
        var result = await controller.GetBooks(null, null, null);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedBooks = Assert.IsAssignableFrom<List<Book>>(okResult.Value);
        Assert.Empty(returnedBooks);

        // Optionally verify the repository was called as expected
        mockRepo.Verify(r => r.GetBooksAsync(null, null, null), Times.Once);
    }
}
