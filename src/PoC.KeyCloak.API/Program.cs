using Asp.Versioning;
using Keycloak.AuthServices.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PoC.KeyCloak.API.Endpoints.v1;
using Scalar.AspNetCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddEndpointsApiExplorer();

builder.Services.AddRouting(opt =>
{
    opt.LowercaseUrls = true;
    opt.LowercaseQueryStrings = true;
});

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
})
    .AddApiExplorer(options =>
    {
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
    });

builder.Services.AddOpenApi();



// Add services to the container.
builder.Services.AddKeycloakWebApiAuthentication(builder.Configuration.GetSection(KeycloakAuthenticationOptions.Section), options =>
{
    options.RequireHttpsMetadata = false; // Use false apenas em desenvolvimento
    options.Authority = builder.Configuration["KeyCloak:auth-server-url"] + "realms/" + builder.Configuration["KeyCloak:realm"];
    options.Audience = builder.Configuration["KeyCloak:resource"];
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["KeyCloak:auth-server-url"] + "realms/" + builder.Configuration["KeyCloak:realm"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["KeyCloak:resource"],
        ValidateLifetime = true
    };
});

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}
else
{
    app.UseHttpsRedirection();
}

app.MapWeatherForecastEndpoints();

var versionSetPing = app.NewApiVersionSet("Ping")
                    .Build();
app
    .MapGet("/ping", () =>
    {
        return TypedResults.Ok(Assembly.GetExecutingAssembly().GetName().Version.ToString());
    }).WithOpenApi(operation => new(operation)
    {
        OperationId = "get-ping-get"
    })
.WithApiVersionSet(versionSetPing)
.RequireAuthorization()
.Produces<string>(200);



app.UseAuthentication();
app.UseAuthorization();

await app.RunAsync();
