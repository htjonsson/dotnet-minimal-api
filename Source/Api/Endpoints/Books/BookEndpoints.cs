using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints.Books;

internal static class BookEndpoints
{
    private const string Tag = "Books";

    public static IEndpointRouteBuilder Map(WebApplication webApplication)
    {
        return Map(webApplication, webApplication.Logger);
    }

    public static IEndpointRouteBuilder Map(IEndpointRouteBuilder builder, ILogger logger)
    {
        RouteGroupBuilder route  = builder.MapGroup("/v1/book");

        // GET
        route.MapGet("/", () => 
        {
            logger.LogInformation("logger /GET");
            
            return Results.Ok("GET");
        })
        .WithTags(Tag);

        // GET by id
        route.MapGet("/{id}", (string id) => 
        {
            return Results.Ok($"GET - {id}");
        })
        .WithTags(Tag);        

        // PATCH
        route.MapPatch("/{id}", (string id) => 
        {
            return Results.Ok($"PATCH - {id}");
        })
        .WithTags(Tag);

        // POST
        route.MapPost("/", () => 
        {
            return Results.Ok("POST");
        })
        .WithTags(Tag);

        // PUT
        route.MapPut("/{id}", (string id) => 
        {
            return Results.Ok($"PUT - {id}");
        })
        .WithTags(Tag);

        // DELETE
        route.MapDelete("/{id}", (string id) => 
        {
            return Results.Ok($"DELETE - {id}");
        })
        .WithTags(Tag);

        return builder;
    }
}