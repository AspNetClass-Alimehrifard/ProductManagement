using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Application.Contracts;
using ProductManagement.Application.Data;
using ProductManagement.Application.DTOs.Requests;
using ProductManagement.Application.Mappings;
using ProductManagement.Application.Repositories;
using ProductManagement.Application.Services;
using ProductManagement.Application.Validators;

var builder = WebApplication.CreateBuilder(args);

#region [-DependencyInjection-]
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ProjectDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddAutoMapper(cfg => { }, typeof(ProductProfile));
builder.Services.AddValidatorsFromAssemblyContaining<CreateProductRequestValidator>();
#endregion

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


#region [-EndPoints-]

app.MapGet("/api/products/GetAll", async (IProductService service) =>
{
    var products = await service.GetAllAsync();

    return Results.Ok(products);
})
.WithTags("Product");

app.MapGet("/api/products/Get/{id:int}", async (int id, IProductService service) =>
{
    if (id <= 0)
    {
        return Results.BadRequest("Invalid product ID.");
    }

    var product = await service.GetByIdAsync(id);

    if (product is null)
    {
        return Results.NotFound("Product not found.");
    }

    return Results.Ok(product);
})
.WithTags("Product");

app.MapPost("/api/products/Post", async (CreateProductRequest request, IProductService service, IValidator<CreateProductRequest> validator) =>
{
    var validationResult = await validator.ValidateAsync(request);

    if (!validationResult.IsValid)
    {
        return Results.ValidationProblem(validationResult.ToDictionary());
    }

    var product = await service.CreateAsync(request);

    return Results.Created($"/api/products/{product.Id}", product);
})
.WithTags("Product");

app.MapPut("/api/products/Put/{id:int}", async (int id, UpdateProductRequest request, IProductService service, IValidator<UpdateProductRequest> validator) =>
{
    if (id <= 0)
    {
        return Results.BadRequest("Invalid product ID.");
    }

    var validationResult = await validator.ValidateAsync(request);

    if (!validationResult.IsValid)
    {
        return Results.ValidationProblem(validationResult.ToDictionary());
    }

    var product = await service.UpdateAsync(id, request);

    if (product is null)
    {
        return Results.NotFound("Product not found.");
    }

    return Results.Ok(product);
})
.WithTags("Product");

app.MapDelete("/api/products/Delete/{id:int}", async (int id, IProductService service) =>
{
    if (id <= 0)
    {
        return Results.BadRequest("Invalid product ID.");
    }

    var deleted = await service.DeleteAsync(id);

    if (!deleted)
    {
        return Results.NotFound("Product not found.");
    }

    return Results.NoContent();
})
.WithTags("Product");

#endregion

app.Run();
