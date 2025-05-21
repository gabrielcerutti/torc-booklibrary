using Microsoft.EntityFrameworkCore;
using Torc.BookLibrary.API.Data;

namespace Torc.BookLibrary.Tests.UnitTests;

public class BookRepositoryTests
{
    private BookDbContext GetDbContextWithData()
    {
        var options = new DbContextOptionsBuilder<BookDbContext>()
            .UseInMemoryDatabase(databaseName: "BookDb_Test")
            .Options;

        var context = new BookDbContext(options);
        context.Books.AddRange(
            new Book { BookId = 1, Title = "Book1", FirstName = "John", LastName = "Doe", ISBN = "123", Type = "Fiction", Category = "Novel", TotalCopies = 1, CopiesInUse = 0, OwnershipStatus = 1 },
            new Book { BookId = 2, Title = "Book2", FirstName = "Jane", LastName = "Smith", ISBN = "456", Type = "Non-Fiction", Category = "Biography", TotalCopies = 2, CopiesInUse = 1, OwnershipStatus = 2 }
        );
        context.SaveChanges();
        return context;
    }

    [Fact]
    public async Task GetBooksAsync_ReturnsBooks_ByAuthor()
    {
        // Arrange
        var context = GetDbContextWithData();
        var repo = new BookRepository(context);

        // Act
        var result = await repo.GetBooksAsync("John", null, null);

        // Assert
        Assert.Single(result);
        Assert.Equal("John", result[0].FirstName);
    }
}
