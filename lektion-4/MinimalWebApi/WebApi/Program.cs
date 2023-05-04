using Microsoft.EntityFrameworkCore;
using WebApi.Contexts;
using WebApi.Helpers.Services;
using WebApi.Models.Schemas;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Contexts
builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("Sql")));

// Services
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<CategoryService>();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

app.MapPost("/api/products", async (ProductSchema schema, ProductService productService, CategoryService categoryService) =>
{
    var categoryEntity = await categoryService.GetAsync(x => x.Id == schema.CategoryId);
    if (categoryEntity != null)
    {
        var item = await productService.GetAsync(x => x.ArticleNumber == schema.ArticleNumber);
        if (item == null)
        {
            item = await productService.CreateAsync(schema);
            if (item != null)
                return Results.Created($"/api/products/{item.ArticleNumber}", item);
        }

        return Results.Conflict($"No product with article number {schema.ArticleNumber} already exists");
    }

    return Results.BadRequest($"No category with id {schema.CategoryId} found");

});
app.MapGet("/api/products/{articleNumber}", async (string articleNumber, ProductService productService) =>
{
    var item = await productService.GetAsync(x => x.ArticleNumber == articleNumber);
    if (item != null)
        return Results.Ok(item);

    return Results.NotFound();
});
app.MapPut("/api/products", async (ProductService productService) => await productService.UpdateAsync());
app.MapDelete("/api/products", async (ProductService productService) => await productService.DeleteAsync());

app.MapPost("/api/categories", async (CategorySchema schema, CategoryService categoryService) =>
{
    var item = await categoryService.CreateAsync(schema);
    if (item != null)
        return Results.Created($"/api/categories/{item.Id}", item);

    return Results.BadRequest();
});
app.MapGet("/api/categories/{id}", async (int id, CategoryService categoryService) =>
{
    var item = await categoryService.GetAsync(x => x.Id == id);
    if (item != null)
        return Results.Ok(item);

    return Results.NotFound();
});
app.MapGet("/api/categories/category/{categoryName}", async (string categoryName, CategoryService categoryService) =>
{
    var item = await categoryService.GetAsync(x => x.CategoryName == categoryName);
    if (item != null)
        return Results.Ok(item);

    return Results.NotFound();
});
app.MapGet("/api/categories", async (CategoryService categoryService) => await categoryService.GetAllAsync());
app.MapPut("/api/categories", async (CategoryService categoryService) => await categoryService.UpdateAsync());
app.MapDelete("/api/categories", async (CategoryService categoryService) => await categoryService.DeleteAsync());

app.Run();
