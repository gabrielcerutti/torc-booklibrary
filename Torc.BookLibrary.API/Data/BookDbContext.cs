using Microsoft.EntityFrameworkCore;

namespace Torc.BookLibrary.API.Data
{
    public class BookDbContext : DbContext
    {
        public BookDbContext(DbContextOptions<BookDbContext> options) : base(options) { }

        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().HasData(
                new Book { BookId = 1, Title = "Pride and Prejudice", FirstName = "Jane", LastName = "Austen", TotalCopies = 100, CopiesInUse = 80, Type = "Hardcover", ISBN = "1234567891", Category = "Fiction", OwnershipStatus = (int?)OwnershipStatus.Love },
                new Book { BookId = 2, Title = "To Kill a Mockingbird", FirstName = "Harper", LastName = "Lee", TotalCopies = 75, CopiesInUse = 65, Type = "Paperback", ISBN = "1234567892", Category = "Fiction", OwnershipStatus = null },
                new Book { BookId = 3, Title = "The Catcher in the Rye", FirstName = "J.D.", LastName = "Salinger", TotalCopies = 50, CopiesInUse = 45, Type = "Hardcover", ISBN = "1234567893", Category = "Fiction", OwnershipStatus = null },
                new Book { BookId = 4, Title = "The Great Gatsby", FirstName = "F. Scott", LastName = "Fitzgerald", TotalCopies = 50, CopiesInUse = 22, Type = "Hardcover", ISBN = "1234567894", Category = "Non-Fiction", OwnershipStatus = (int?)OwnershipStatus.Own },
                new Book { BookId = 5, Title = "The Alchemist", FirstName = "Paulo", LastName = "Coelho", TotalCopies = 50, CopiesInUse = 35, Type = "Hardcover", ISBN = "1234567895", Category = "Biography", OwnershipStatus = (int?)OwnershipStatus.Own },
                new Book { BookId = 6, Title = "The Book Thief", FirstName = "Markus", LastName = "Zusak", TotalCopies = 75, CopiesInUse = 11, Type = "Hardcover", ISBN = "1234567896", Category = "Mystery", OwnershipStatus = null },
                new Book { BookId = 7, Title = "The Chronicles of Narnia", FirstName = "C.S.", LastName = "Lewis", TotalCopies = 100, CopiesInUse = 14, Type = "Paperback", ISBN = "1234567897", Category = "Sci-Fi", OwnershipStatus = null },
                new Book { BookId = 8, Title = "The Da Vinci Code", FirstName = "Dan", LastName = "Brown", TotalCopies = 50, CopiesInUse = 40, Type = "Paperback", ISBN = "1234567898", Category = "Sci-Fi", OwnershipStatus = (int?)OwnershipStatus.WantToRead },
                new Book { BookId = 9, Title = "The Grapes of Wrath", FirstName = "John", LastName = "Steinbeck", TotalCopies = 50, CopiesInUse = 35, Type = "Hardcover", ISBN = "1234567899", Category = "Fiction", OwnershipStatus = null },
                new Book { BookId = 10, Title = "The Hitchhiker's Guide to the Galaxy", FirstName = "Douglas", LastName = "Adams", TotalCopies = 50, CopiesInUse = 35, Type = "Paperback", ISBN = "1234567900", Category = "Non-Fiction" },
                new Book { BookId = 11, Title = "Moby-Dick", FirstName = "Herman", LastName = "Melville", TotalCopies = 30, CopiesInUse = 8, Type = "Hardcover", ISBN = "8901234567", Category = "Fiction", OwnershipStatus = null },
                new Book { BookId = 12, Title = "To Kill a Mockingbird", FirstName = "Harper", LastName = "Lee", TotalCopies = 20, CopiesInUse = 0, Type = "Paperback", ISBN = "9012345678", Category = "Non-Fiction", OwnershipStatus = null },
                new Book { BookId = 13, Title = "The Catcher in the Rye", FirstName = "J.D.", LastName = "Salinger", TotalCopies = 10, CopiesInUse = 1, Type = "Hardcover", ISBN = "0123456789", Category = "Non-Fiction", OwnershipStatus = null }
            );
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