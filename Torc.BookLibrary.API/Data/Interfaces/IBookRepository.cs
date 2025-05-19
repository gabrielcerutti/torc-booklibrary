namespace Torc.BookLibrary.API.Data.Interfaces
{
    public interface IBookRepository
    {
        Task<List<Book>> GetBooksAsync(string? author, string? isbn, int? ownershipStatus);
    }
}
