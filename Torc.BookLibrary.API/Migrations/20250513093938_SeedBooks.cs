using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Torc.BookLibrary.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedBooks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "BookId", "Category", "CopiesInUse", "FirstName", "ISBN", "LastName", "Title", "TotalCopies", "Type" },
                values: new object[,]
                {
                    { 1, "Fiction", 80, "Jane", "1234567891", "Austen", "Pride and Prejudice", 100, "Hardcover" },
                    { 2, "Fiction", 65, "Harper", "1234567892", "Lee", "To Kill a Mockingbird", 75, "Paperback" },
                    { 3, "Fiction", 45, "J.D.", "1234567893", "Salinger", "The Catcher in the Rye", 50, "Hardcover" },
                    { 4, "Non-Fiction", 22, "F. Scott", "1234567894", "Fitzgerald", "The Great Gatsby", 50, "Hardcover" },
                    { 5, "Biography", 35, "Paulo", "1234567895", "Coelho", "The Alchemist", 50, "Hardcover" },
                    { 6, "Mystery", 11, "Markus", "1234567896", "Zusak", "The Book Thief", 75, "Hardcover" },
                    { 7, "Sci-Fi", 14, "C.S.", "1234567897", "Lewis", "The Chronicles of Narnia", 100, "Paperback" },
                    { 8, "Sci-Fi", 40, "Dan", "1234567898", "Brown", "The Da Vinci Code", 50, "Paperback" },
                    { 9, "Fiction", 35, "John", "1234567899", "Steinbeck", "The Grapes of Wrath", 50, "Hardcover" },
                    { 10, "Non-Fiction", 35, "Douglas", "1234567900", "Adams", "The Hitchhiker's Guide to the Galaxy", 50, "Paperback" },
                    { 11, "Fiction", 8, "Herman", "8901234567", "Melville", "Moby-Dick", 30, "Hardcover" },
                    { 12, "Non-Fiction", 0, "Harper", "9012345678", "Lee", "To Kill a Mockingbird", 20, "Paperback" },
                    { 13, "Non-Fiction", 1, "J.D.", "0123456789", "Salinger", "The Catcher in the Rye", 10, "Hardcover" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 13);
        }
    }
}
