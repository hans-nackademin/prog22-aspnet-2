using WebApi.Models.Entities;

namespace WebApi.Models.Schemas
{
    public class ProductSchema
    {
        public string ArticleNumber { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? Specification { get; set; }
        public decimal Price { get; set; }

        public static implicit operator ProductEntity(ProductSchema schema)
        {
            return new ProductEntity
            {
                ArticleNumber = schema.ArticleNumber,
                Name = schema.Name,
                Description = schema.Description,
                Specification = schema.Specification,
                Price = schema.Price
            };
        }
    }
}
