namespace WebApi.Models.Dto;

public interface IProduct
{
    int Id { get; set; }
    string Name { get; set; }
}

public class Product : IProduct
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
}
