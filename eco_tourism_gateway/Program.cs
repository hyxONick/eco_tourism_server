using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Options;
using MMLib.SwaggerForOcelot.Configuration;
using Microsoft.OpenApi.Models;
using Ocelot.Values;

var builder = WebApplication.CreateBuilder(args);

// Load configuration from appsettings.json
var auth0Settings = builder.Configuration.GetSection("Auth0");

builder.Configuration.AddJsonFile("ocelot.json");
    //.AddJsonFile("ocelot.development.json");

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Add Ocelot services
builder.Services.AddOcelot();

// Register the SwaggerForOcelot generator
builder.Services.AddSwaggerForOcelot(builder.Configuration);

// Configure JWT authentication with Auth0
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer("Auth0", options => 
    {
        options.Authority = $"https://{auth0Settings["Domain"]}/";
        options.Audience = auth0Settings["Audience"];
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true
        };
    });

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();

app.UseRouting();
app.UseSwagger();
//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapControllers();
//});

// Use authentication and authorization
app.UseAuthentication();
app.UseAuthorization();

// Use the SwaggerForOcelot UI
app.UseSwaggerForOcelotUI(opt =>
{
    opt.PathToSwaggerGenerator = "/swagger/docs";
});
app.UseCors("AllowAll"); // Apply the CORS policy

// Use Ocelot middleware
await app.UseOcelot();
app.Run();