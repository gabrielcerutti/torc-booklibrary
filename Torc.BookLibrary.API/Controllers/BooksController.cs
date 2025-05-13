using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Torc.BookLibrary.API.Data;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly BookDbContext _context;
    private readonly ILogger<BooksController> _logger;

    public BooksController(BookDbContext context, ILogger<BooksController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetBooks([FromQuery] string? author, [FromQuery] string? isbn, [FromQuery] int? ownershipStatus)
    {
        _logger.LogInformation("Fetching books with filters - Author: {Author}, ISBN: {ISBN}, OwnershipStatus: {OwnershipStatus}", author, isbn, ownershipStatus);

        var query = _context.Books.AsQueryable();

        // Filter by author
        // Filter by author
        if (!string.IsNullOrEmpty(author))
        {
            var authorParts = author.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (authorParts.Length == 1)
            {
                // If only one part is provided, search in both FirstName and LastName
                query = query.Where(b => b.FirstName.Contains(author) || b.LastName.Contains(author));
            }
            else if (authorParts.Length >= 2)
            {
                // If two or more parts are provided, search for both FirstName and LastName
                var firstName = authorParts[0];
                var lastName = string.Join(' ', authorParts.Skip(1));
                query = query.Where(b => b.FirstName.Contains(firstName) && b.LastName.Contains(lastName));
            }
        }

        // Filter by ISBN
        if (!string.IsNullOrEmpty(isbn))
        {
            query = query.Where(b => b.ISBN == isbn);
        }

        // Filter by OwnershipStatus
        if (ownershipStatus.HasValue)
        {
            query = query.Where(b => b.OwnershipStatus == ownershipStatus.Value);
        }

        var books = await query.ToListAsync();

        _logger.LogInformation("Found {Count} books", books.Count);

        return Ok(books);
    }
}
