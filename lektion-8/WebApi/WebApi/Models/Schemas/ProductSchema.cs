using WebApi.Models.Entities;

namespace WebApi.Models.Schemas;

public interface IProductSchema
{
    string Name { get; set; }
}

public class ProductSchema : IProductSchema
{
    public string Name { get; set; } = null!;

    public static implicit operator ProductEntity(ProductSchema schema)
    {
        return new ProductEntity
        {
            Name = schema.Name
        };
    }
}
