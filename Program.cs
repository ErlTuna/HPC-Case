
using System.Linq;
using Microsoft.EntityFrameworkCore;
using HappenCodeECommerceAPI.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<HappenCodeECommerceAPIContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
Console.Write("its over");