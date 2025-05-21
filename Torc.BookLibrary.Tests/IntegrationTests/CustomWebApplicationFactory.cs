using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Torc.BookLibrary.API.Data;

namespace Torc.BookLibrary.Tests.IntegrationTests;

public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
{
    private static readonly SqliteConnection _connection = new SqliteConnection("DataSource=:memory:");
    private static bool _databaseInitialized;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        _connection.Open();

        builder.UseEnvironment("Testing");

        builder.ConfigureServices(services =>
        {
            // Remove existing DbContext registration
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<BookDbContext>));
            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            // Add SQLite in-memory provider
            services.AddDbContext<BookDbContext>(options =>
            {
                options.UseSqlite(_connection);
            });

            // Ensure database is created only once
            using var scope = services.BuildServiceProvider().CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<BookDbContext>();

            if (!_databaseInitialized)
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
                _databaseInitialized = true;
            }
        });
    }
}
