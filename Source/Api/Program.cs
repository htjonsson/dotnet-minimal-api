using Api.Endpoints.Books;
using Api.Database;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;
using Scalar.AspNetCore;

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
    builder.Services.AddOpenApi();

    var app = builder.Build();

    // SERILOG
    // app.UseSerilogRequestLogging();

    if (app.Environment.IsDevelopment())
    {
        app.MapOpenApi();

        // /swagger/index.html#/
        app.UseSwaggerUI(options =>
        {
           options.SwaggerEndpoint(url: "/openapi/v1.json", name: "OpenAPI V1");
        });

        // /api-docs/index.html#/
        app.UseReDoc(options =>
        {
            options.SpecUrl("/openapi/v1.json");
        });

        // /scalar/index.html
        app.MapScalarApiReference();
    }

    // APIENDPOINTS
    BaseEndpoints.Map(app);
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