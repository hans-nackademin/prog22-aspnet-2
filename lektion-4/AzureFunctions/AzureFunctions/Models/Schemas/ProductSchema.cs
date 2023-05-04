using AzureFunctions.Models.Entities;

namespace AzureFunctions.Models.Schemas;

public class ProductSchema
{
    public string ArticleNumber { get; set; } = null!;
    public string Name { get; set; } = null!;


    public static implicit operator ProductEntity(ProductSchema schema)
    {
        return new ProductEntity
        {
            ArticleNumber = schema.ArticleNumber,
            Name = schema.Name,
        };
    }
}
