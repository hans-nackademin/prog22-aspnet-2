using WebApi.Models.Dtos;

using WebApi.Models.Schemas;

namespace WebApi.Tests.Fixtures
{
    public class ProductFixture
    {
        public static List<ProductSchema> Schemas = new List<ProductSchema>()
        {
            new ProductSchema { Name = "Product 1", Price = 100, Description = "Description for Product 1"},
            new ProductSchema { Name = "Product 2", Price = 100, Description = "Description for Product 2"},
            new ProductSchema { Name = "Product 3", Price = 300, Description = "Description for Product 3"},
            new ProductSchema { Name = "Product 4", Price = 100, Description = "Description for Product 4"},
            new ProductSchema { Name = "Product 5", Price = 500, Description = "Description for Product 5"}
        };

        public static List<ProductEntity> Entities = new List<ProductEntity>()
        {
            new ProductEntity { Id = 1, Name = "Product 1", Price = 100, Description = "Description for Product 1"},
            new ProductEntity { Id = 2, Name = "Product 2", Price = 100, Description = "Description for Product 2"},
            new ProductEntity { Id = 3, Name = "Product 3", Price = 300, Description = "Description for Product 3"},
            new ProductEntity { Id = 4, Name = "Product 4", Price = 100, Description = "Description for Product 4"},
            new ProductEntity { Id = 5, Name = "Product 5", Price = 500, Description = "Description for Product 5"}
        };

        public static List<Product> Products = new List<Product>()
        {
            new Product { Id = 1, Name = "Product 1", Price = 100, Description = "Description for Product 1"},
            new Product { Id = 2, Name = "Product 2", Price = 100, Description = "Description for Product 2"},
            new Product { Id = 3, Name = "Product 3", Price = 300, Description = "Description for Product 3"},
            new Product { Id = 4, Name = "Product 4", Price = 100, Description = "Description for Product 4"},
            new Product { Id = 5, Name = "Product 5", Price = 500, Description = "Description for Product 5"}
        };

        public static List<string> ErrorMessages = new List<string>()
        {
            "A product with the same name already exists"
        };

    }
}
