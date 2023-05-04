using System.ComponentModel.DataAnnotations;
using WebApi.Models.Entities;

namespace WebApi.Models.Schemas;

public class ProductSchema
{
    [Required]
    public string ArticleNumber { get; set; } = null!;

    [Required]
    public string Name { get; set; } = null!;

    [Required]
    public int CategoryId { get; set; }


    public static implicit operator ProductEntity(ProductSchema schema)
    {
        return new ProductEntity
        {
            ArticleNumber = schema.ArticleNumber,
            Name = schema.Name,
            CategoryId = schema.CategoryId,
        };
    }
}
