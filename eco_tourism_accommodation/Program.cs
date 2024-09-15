using eco_tourism_accommodation.DB;
using eco_tourism_accommodation.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsProduction())
{
    builder.WebHost.UseUrls("http://0.0.0.0:80");
}

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString =
    "server=localhost;database=eco_tourism;user=root;password=lwh971213";

// Register the database context
builder.Services.AddDbContext<EcoTourismAccommodationContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// register services
builder.Services.AddScoped<IRewardService, RewardService>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();

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

builder.Services.AddRouting(options =>
{
    options.LowercaseUrls = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

// app.UseHttpsRedirection();

app.UseCors("AllowAll"); // Apply the CORS policy
app.Run();
