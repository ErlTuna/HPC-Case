
using Microsoft.EntityFrameworkCore;
using HappenCodeECommerceAPI.Data;
using HappenCodeECommerceAPI.Interfaces;
using HappenCodeECommerceAPI.Services;
using HappenCodeECommerceAPI.Repositories;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<HappenCodeECommerceAPIContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomerService, CustomerService>();

builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<ICartService, CartService>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "HappenCode E-Commerce API",
        Version = "v1"
    });
});

var app = builder.Build();

// Enable Swagger middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "HappenCode API v1");
        c.RoutePrefix = string.Empty; // Swagger UI at root: http://localhost:5000/
    });
}

app.UseAuthorization();
app.MapControllers();
app.Run();
