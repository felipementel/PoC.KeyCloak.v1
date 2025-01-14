using Asp.Versioning;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PoC.KeyCloak.API.Endpoints.v1
{
    public static class NumbersEndpoints
    {
        public static void MapNumbersEndpoints(this IEndpointRouteBuilder app)
        {
            var apiVersionSetNumbers = app
               .NewApiVersionSet("numbers")
               .HasApiVersion(new ApiVersion(1))
               .ReportApiVersions()
               .Build();

            var api = app
                .MapGroup("/api/v{apiVersion:apiVersion}/numbers")
                .RequireAuthorization()
                .WithApiVersionSet(apiVersionSetNumbers);

            api
                 .MapPost("/post-random-number", GetRandomNumberAsync)
                 .Produces(401)
                 .Produces(422)
                 .Produces(500)
                 .WithOpenApi(operation => new(operation)
                 {
                     OperationId = "post-random-number",
                     Summary = "Post Random Number",
                     Description = "Canal DEPLOY",
                     Tags = new List<OpenApiTag> { new() { Name = "Random Number" } }
                 });

            async Task<IResult> GetRandomNumberAsync(
                CancellationToken cancellationToken = default)
            {
                if (Random.Shared.Next(0, 2) == 0)
                {
                    return TypedResults.UnprocessableEntity(new ValueTask());
                }

                return TypedResults.Ok();
            }
        }
    }
}
