using WebApi.Models.Dto;

namespace WebApi.Models.Entities;

public interface IProductEntity
{
    int Id { get; set; }
    string Name { get; set; }
}

public class ProductEntity : IProductEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;



    public static implicit operator Product(ProductEntity entity)
    {
        if (entity == null)
            return null!;

        return new Product
        {
            Id = entity.Id,
            Name = entity.Name
        };
    }
}
