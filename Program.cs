
using Microsoft.EntityFrameworkCore;
using HappenCodeECommerceAPI.Data;
using HappenCodeECommerceAPI.Interfaces;
using HappenCodeECommerceAPI.Services;
using HappenCodeECommerceAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<HappenCodeECommerceAPIContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomerService, CustomerService>();

builder.Services.AddControllers();


var app = builder.Build();
app.UseAuthorization();
app.MapControllers();
app.Run();
