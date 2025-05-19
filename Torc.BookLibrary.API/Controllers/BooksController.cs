using Microsoft.AspNetCore.Mvc;
using Torc.BookLibrary.API.Data.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly IBookRepository _bookRepository;
    private readonly ILogger<BooksController> _logger;

    public BooksController(IBookRepository bookRepository, ILogger<BooksController> logger)
    {
        _bookRepository = bookRepository;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetBooks([FromQuery] string? author, [FromQuery] string? isbn, [FromQuery] int? ownershipStatus)
    {
        _logger.LogInformation("Fetching books with filters - Author: {Author}, ISBN: {ISBN}, OwnershipStatus: {OwnershipStatus}", author, isbn, ownershipStatus);

        var books = await _bookRepository.GetBooksAsync(author, isbn, ownershipStatus);

        _logger.LogInformation("Found {Count} books", books.Count);

        return Ok(books);
    }
}
