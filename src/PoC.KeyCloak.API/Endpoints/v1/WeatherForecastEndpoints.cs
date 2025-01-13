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
    public static class WeatherForecastEndpoints
    {
        public static void MapWeatherForecastEndpoints(this IEndpointRouteBuilder app)
        {
            var apiVersionSetWeatherForcast = app
               .NewApiVersionSet("WeatherForecast")
               .HasApiVersion(new ApiVersion(1))
               .ReportApiVersions()
               .Build();

            var weatherforcast = app
                .MapGroup("/api/v{apiVersion:apiVersion}/weatherforecast")
                .RequireAuthorization()
                .WithApiVersionSet(apiVersionSetWeatherForcast);

            weatherforcast
                 .MapPost("/position", AdotarAsync)
                 .Produces(201)
                 .Produces(401)
                 .Produces(422)
                 .Produces(500)
                 .WithOpenApi(operation => new(operation)
                 {
                     OperationId = "adotar-adocoes-post",
                     Summary = "Adotar um  Cachorro",
                     Description = "Operação para um tutor adotar um cachorro",
                     Tags = new List<OpenApiTag> { new() { Name = "Adocoes" } }
                 });

            async Task<IResult> AdotarAsync(
                Guid cachorroid,
                long tutorid,
                //IAdocaoAppService adocaoAppService,
                CancellationToken cancellationToken = default)
            {
                //var item = await adocaoAppService.AdotarAsync(
                //    cachorroid,
                //    tutorid,
                //    cancellationToken);

                if (Random.Shared.Next(0, 2) == 0)
                {
                    return TypedResults.UnprocessableEntity(new ValueTask());
                }

                return TypedResults.Ok();
            }
        }
    }
}
