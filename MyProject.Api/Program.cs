using Microsoft.EntityFrameworkCore;
using Azure.Identity; // Ensure you have the correct using directive
using MyProject.Infrastructure.Data;
using MyProject.Application.Interfaces;
using MyProject.Application.Services;
using MyProject.Domain.Interfaces;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure DbContext with connection string from appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString, sqlOptions =>
    {
        sqlOptions.EnableRetryOnFailure(
        maxRetryCount: 20, // You can increase the count
        maxRetryDelay: TimeSpan.FromSeconds(5), // Adjust the delay as needed
        errorNumbersToAdd: null);
    }));

// Dependency Injection for Services and Repositories
builder.Services.AddScoped<IItemRepository, ItemRepository>();
builder.Services.AddScoped<IItemService, ItemService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}
 

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
