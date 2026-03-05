using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints.Books;

internal static class BaseEndpoints
{
    private const string Tag = "Base";

    public static IEndpointRouteBuilder Map(WebApplication webApplication)
    {
        return Map(webApplication, webApplication.Logger);
    }

    public static IEndpointRouteBuilder Map(IEndpointRouteBuilder builder, ILogger logger)
    {
        RouteGroupBuilder route  = builder.MapGroup("/v1/base");

        // GET
        route.MapGet("/", () => 
        {
            logger.LogInformation("logger /GET");
            
            return Results.Ok("GET");
        })
        .WithName("GetBase")
        .WithTags(Tag)
        .AddOpenApiOperationTransformer((operation, context, ct) =>
        {
            operation.Summary     = "Gets the current weather report.";
            operation.Description = "Returns a short description and emoji.";
            return Task.CompletedTask;
        });

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