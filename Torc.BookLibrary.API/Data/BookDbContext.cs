using Microsoft.EntityFrameworkCore;

namespace Torc.BookLibrary.API.Data
{
    public class BookDbContext : DbContext
    {
        public BookDbContext(DbContextOptions<BookDbContext> options) : base(options) { }

        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Removed HasData seeding to avoid migration order issues with new providers.
        }

    }

    public class Book
    {
        public int BookId { get; set; }
        public required string Title { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public int TotalCopies { get; set; }
        public int CopiesInUse { get; set; }
        public required string Type { get; set; }
        public required string ISBN { get; set; }
        public required string Category { get; set; }
        // For the sake of simplicity, we are using int? to represent the OwnershipStatus
        // In a real-world scenario, you might want to use a more complex design to support many ownership statuses per user
        public int? OwnershipStatus { get; set; } // 0 = Own, 1 = Love, 2 = WantToRead
    }

    public enum OwnershipStatus
    {
        Own,
        Love,
        WantToRead
    }
}