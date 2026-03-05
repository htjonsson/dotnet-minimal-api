using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints.Books;

internal static class ImageEndpoints
{
    private const string Tag = "Image";
    private const string MimeType = "image/svg+xml";

    public static IEndpointRouteBuilder Map(WebApplication webApplication)
    {
        return Map(webApplication, webApplication.Logger);
    }

    public static IEndpointRouteBuilder Map(IEndpointRouteBuilder builder, ILogger logger)
    {
        RouteGroupBuilder route  = builder.MapGroup("/v1/images");

        // DOWNLOAD
        route.MapGet("svg/{fileName}", async (string fileName) => 
        {
            string content = """
                <svg viewBox="0 0 100 100" xmlns="http://www.w3.org/2000/svg">
                <line x1="0" y1="80" x2="100" y2="20" stroke="black" />
                </svg>
            """;

            byte[] bytes = Encoding.UTF8.GetBytes(content);

            return Results.File(bytes, MimeType, $"{fileName}.svg");

        })
        .Produces(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .WithName($"GET - Image file by Name")
        .WithTags(Tag);

        return builder;
    }
}