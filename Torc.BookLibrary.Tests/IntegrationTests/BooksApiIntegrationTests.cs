using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Json;
using Torc.BookLibrary.API.Data;

namespace Torc.BookLibrary.Tests.IntegrationTests;

public class BooksApiIntegrationTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly BookDbContext _db;

    public BooksApiIntegrationTests(CustomWebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();

        // Get scoped db context to seed
        var scopeFactory = factory.Services.GetRequiredService<IServiceScopeFactory>();
        using var scope = scopeFactory.CreateScope();
        _db = scope.ServiceProvider.GetRequiredService<BookDbContext>();

        ResetAndSeedDatabase(_db);
    }

    private static void ResetAndSeedDatabase(BookDbContext db)
    {
        // Clear existing data to avoid key conflicts
        db.Books.RemoveRange(db.Books);
        db.SaveChanges();

        // Seed fresh test data
        db.Books.Add(new Book
        {
            BookId = 1,
            Title = "Integration Test Book",
            FirstName = "Alice",
            LastName = "Smith",
            ISBN = "999",
            Type = "Fiction",
            Category = "Test",
            TotalCopies = 1,
            CopiesInUse = 0,
            OwnershipStatus = 1
        });

        db.SaveChanges();
    }

    [Fact]
    public async Task GetBooks_ReturnsSeededBook()
    {
        // Act
        var response = await _client.GetAsync("/api/books?author=Alice");

        // Assert
        response.EnsureSuccessStatusCode();
        var books = await response.Content.ReadFromJsonAsync<List<Book>>();
        Assert.NotNull(books);
        Assert.Single(books);
        Assert.Equal("Alice", books[0].FirstName);
    }

    [Fact]
    public async Task GetBooks_ReturnsEmpty_WhenNoMatch()
    {
        // Act
        var response = await _client.GetAsync("/api/books?author=NonExistent");

        // Assert
        response.EnsureSuccessStatusCode();
        var books = await response.Content.ReadFromJsonAsync<List<Book>>();
        Assert.NotNull(books);
        Assert.Empty(books);
    }
}
