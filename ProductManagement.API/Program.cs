using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Application.Mappings;
using ProductManagement.Application.Services;
using ProductManagement.Application.Validators;
using ProductManagement.Application.Repositories;
using ProductManagement.Application.Data;
using ProductManagement.Application.Contracts;

var builder = WebApplication.CreateBuilder(args);

#region [-DependencyInjection-]
builder.Services.AddDbContext<ProjectDbContext>(options =>
options.UseSqlServer(
builder.Configuration.GetConnectionString("DefaultConnection")
));

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddAutoMapper(cfg => { }, typeof(ProductProfile));

builder.Services.AddValidatorsFromAssemblyContaining<CreateProductRequestValidator>();
#endregion

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
