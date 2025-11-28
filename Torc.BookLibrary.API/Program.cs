using Figgle;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Torc.BookLibrary.API.Data;
using Torc.BookLibrary.API.Data.Interfaces;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using System.Collections.Generic;


Console.WriteLine(FiggleFonts.Standard.Render("Torc.BookLibrary.API"));

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
var seqServerUrl = builder.Configuration["Serilog:Seq:ServerUrl"];
if (!string.IsNullOrEmpty(seqServerUrl))
{
    Log.Logger = new LoggerConfiguration()
        .ReadFrom.Configuration(builder.Configuration) // Read configuration from appsettings.json
        .Enrich.FromLogContext()
        .WriteTo.Console()
        .WriteTo.Seq(seqServerUrl) // Seq sink
        .CreateLogger();
} 
else
{
    Log.Logger = new LoggerConfiguration()
        .ReadFrom.Configuration(builder.Configuration) // Read configuration from appsettings.json
        .Enrich.FromLogContext()
        .WriteTo.Console()
        .CreateLogger();
}


builder.Host.UseSerilog(); // Replace default logging with Serilog

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<BookDbContext>((serviceProvider, options) =>
{
    var config = serviceProvider.GetService<IConfiguration>();
    var env = serviceProvider.GetService<IWebHostEnvironment>();

    // This ensures relational providers are only used when not running in tests
    if (env is not null && !env.EnvironmentName.Equals("Testing", StringComparison.OrdinalIgnoreCase))
    {
        var provider = config?["DatabaseProvider"] ?? "SqlServer";
        if (provider.Equals("Postgres", StringComparison.OrdinalIgnoreCase) || provider.Equals("PostgreSQL", StringComparison.OrdinalIgnoreCase))
        {
            var cs = config?.GetConnectionString("PostgresConnection");
            options.UseNpgsql(cs);
            Log.Information("Database provider selected: PostgreSQL. Host: {Host}", ExtractHost(cs));
        }
        else
        {
            var cs = config?.GetConnectionString("DefaultConnection");
            options.UseSqlServer(cs);
            Log.Information("Database provider selected: SQL Server. Server: {Server}", ExtractServer(cs));
        }
    }
});
builder.Services.AddScoped<IBookRepository, BookRepository>();

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Add Swagger/OpenAPI services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.OpenApiInfo
    {
        Title = "Torc Book Library API",
        Version = "v1",
        Description = "An API for managing a personal book library."
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Torc Book Library API v1");
        c.RoutePrefix = string.Empty; // Makes Swagger UI available at the root URL
    });

    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<BookDbContext>();
    // Apply migrations for any relational provider (SQL Server or PostgreSQL)
    dbContext.Database.Migrate();
    Log.Information("Applied migrations for provider: {Provider}", dbContext.Database.ProviderName);

    // Seed sample data if empty
    if (!dbContext.Books.AsNoTracking().Any())
    {
        dbContext.Books.AddRange(GenerateSeedBooks(50));
        dbContext.SaveChanges();
        Log.Information("Seeded {Count} sample books", 50);
    }
}

// Use CORS
app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

// Helper methods (kept simple, not exposing credentials)
static string? ExtractHost(string? cs)
{
    if (string.IsNullOrEmpty(cs)) return null;
    // Postgres: Host=...; look for Host=
    foreach (var part in cs.Split(';', StringSplitOptions.RemoveEmptyEntries))
    {
        if (part.StartsWith("Host=", StringComparison.OrdinalIgnoreCase))
            return part.Substring(5);
    }
    return null;
}
static string? ExtractServer(string? cs)
{
    if (string.IsNullOrEmpty(cs)) return null;
    // SQL Server: Server=... or Data Source=...
    foreach (var part in cs.Split(';', StringSplitOptions.RemoveEmptyEntries))
    {
        if (part.StartsWith("Server=", StringComparison.OrdinalIgnoreCase))
            return part.Substring(7);
        if (part.StartsWith("Data Source=", StringComparison.OrdinalIgnoreCase))
            return part.Substring(12);
    }
    return null;
}

static IEnumerable<Book> GenerateSeedBooks(int count)
{
    var categories = new[] { "Fiction", "Non-Fiction", "Sci-Fi", "Mystery", "Biography" };
    var types = new[] { "Hardcover", "Paperback" };
    var list = new List<Book>(capacity: count);
    for (int i = 1; i <= count; i++)
    {
        list.Add(new Book
        {
            Title = $"Sample Book {i}",
            FirstName = $"Author{i}",
            LastName = "Lastname",
            TotalCopies = 10 + (i % 40),
            CopiesInUse = i % 10,
            Type = types[i % types.Length],
            ISBN = (1000000000L + i).ToString(),
            Category = categories[i % categories.Length],
            OwnershipStatus = (i % 5 == 0) ? (int?)null : (i % 3)
        });
    }
    return list;
}

// This is only for test project discovery and WebApplicationFactory support.
public partial class Program { }