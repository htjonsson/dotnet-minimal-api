using Api.Endpoints.Books;
using Api.Database;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;

// SERILOG
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    // .MinimumLevel.Override("Microsoft.AspNetCore.Hosting", LogEventLevel.Warning)
    // .MinimumLevel.Override("Microsoft.AspNetCore.Mvc", LogEventLevel.Warning)
    // .MinimumLevel.Override("Microsoft.AspNetCore.Routing", LogEventLevel.Warning)
    .CreateLogger();

Log.Information("Starting up");

try
{
    var builder = WebApplication.CreateBuilder(args);

    // SERILOG
    builder.Logging.AddSerilog();

    // DATABASE 
    builder.Services.AddDbContext<BookContext>(opt => opt.UseInMemoryDatabase("BookLibrary"));
    builder.Services.AddDatabaseDeveloperPageExceptionFilter();

    // OPENAPI
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddOpenApiDocument(config =>
    {
        config.DocumentName = "Books API";
        config.Title = "Books API v1";
        config.Version = "v1";
    });

    var app = builder.Build();

    // SERILOG
    // app.UseSerilogRequestLogging();

    if (app.Environment.IsDevelopment())
    {
        app.UseOpenApi();
        app.UseSwaggerUi(config =>
        {
            config.DocumentTitle = "Books API";
            config.Path = "/swagger";
            config.DocumentPath = "/swagger/{documentName}/swagger.json";
            config.DocExpansion = "list";
        });
    }

    // APIENDPOINTS
    BookEndpoints.Map(app);

    app.Run();

    return 0;
}
catch (Exception ex)
{
    Log.Fatal(ex, "Unhandled exception");
    return 1;
}
finally
{
    await Log.CloseAndFlushAsync();
}