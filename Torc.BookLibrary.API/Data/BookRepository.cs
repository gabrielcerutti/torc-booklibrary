namespace Torc.BookLibrary.API.Data
{
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Torc.BookLibrary.API.Data.Interfaces;

    public class BookRepository : IBookRepository
    {
        private readonly BookDbContext _context;

        public BookRepository(BookDbContext context)
        {
            _context = context;
        }

        public async Task<List<Book>> GetBooksAsync(string? author, string? isbn, int? ownershipStatus)
        {
            var query = _context.Books.AsQueryable();

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

            return await query.ToListAsync();
        }
    }

}
