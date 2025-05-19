using Figgle;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using Torc.BookLibrary.API.Data;
using Torc.BookLibrary.API.Data.Interfaces;

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
builder.Services.AddDbContext<BookDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
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
    c.SwaggerDoc("v1", new OpenApiInfo
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
    dbContext.Database.Migrate(); // Applies migrations and creates the database if it doesn't exist
}

// Use CORS
app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
