using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Microsoft.EntityFrameworkCore;
using eco_tourism_gateway.DB;
using eco_tourism_gateway.Middleware;

var builder = WebApplication.CreateBuilder(args);

// builder.WebHost.UseUrls("http://0.0.0.0:80");
if (builder.Environment.IsProduction())
{
    builder.WebHost.UseUrls("http://0.0.0.0:80");
    builder.Configuration.AddJsonFile("ocelot-production.json", optional: false, reloadOnChange: true);
} else {
    builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
}

// var envFile = builder.Environment.IsDevelopment() ? "ocelot.json" : "ocelot-production.json";
// builder.Configuration.AddJsonFile(envFile, optional: false, reloadOnChange: true);

builder.Services.AddOcelot();

// builder.Services.AddReverseProxy()
//     .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

var connectionString = "server=localhost;database=eco_tourism;user=root;password=lwh971213"; // MySQL database connection string

builder.Services.AddDbContext<EcoEventLogContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Configure CORS to allow all origins
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin() // Allow all origins
                  .AllowAnyHeader() // Allow all headers
                  .AllowAnyMethod(); // Allow all HTTP methods
        });
});

var app = builder.Build();

// // Logger Event
// app.Use(async (context, next) =>
// {
//     var requestPath = context.Request.Path;
//     var requestMethod = context.Request.Method;

//     Console.WriteLine($"Request Path: {requestPath}, Request Method: {requestMethod}");


//     await next();
// });

// Use the token validation middleware
app.UseMiddleware<TokenValidationMiddleware>();

// Use the custom request logging middleware
app.UseMiddleware<LoggingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

await app.UseOcelot();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.UseCors("AllowAll"); // Apply the CORS policy
app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
