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
        RouteGroupBuilder route  = builder.MapGroup("/v1/image");

        // DOWNLOAD
        route.MapGet("svg/{fileName}", async (string fileName) => 
        {
            CreateImage createImage = new ();

            byte[] bytes = createImage.Handle();

            return Results.File(bytes, MimeType, $"{fileName}.svg");
        })
        .Produces(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .WithName($"GetImage")
        .WithTags(Tag);

        // BUILD
        route.MapGet("image/{fileName}", async (string fileName) => 
        {
            ImageBuilder builder = new ();

            ImageModel result = new ();

            result.Image = builder.buildSimpleDemo();

            return Results.Ok(result);
        })
        .Produces(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .WithName($"BuildImage")
        .WithTags(Tag);        

        return builder;
    }
}