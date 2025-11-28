using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Torc.BookLibrary.API.Data
{
    public class BookDbContextFactory : IDesignTimeDbContextFactory<BookDbContext>
    {
        public BookDbContext CreateDbContext(string[] args)
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true)
                .AddJsonFile($"appsettings.{env}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            var provider = configuration["DatabaseProvider"] ?? "SqlServer";
            var optionsBuilder = new DbContextOptionsBuilder<BookDbContext>();

            if (provider.Equals("Postgres", StringComparison.OrdinalIgnoreCase) || provider.Equals("PostgreSQL", StringComparison.OrdinalIgnoreCase))
            {
                var cs = configuration.GetConnectionString("PostgresConnection");
                optionsBuilder.UseNpgsql(cs);
            }
            else
            {
                var cs = configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(cs);
            }

            return new BookDbContext(optionsBuilder.Options);
        }
    }
}
