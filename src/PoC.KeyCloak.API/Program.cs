using Keycloak.AuthServices.Authentication;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

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

//builder.Services.AddKeycloakWebApiAuthentication(builder.Configuration, options =>
//{
//    options.RequireHttpsMetadata = false;
//});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();
