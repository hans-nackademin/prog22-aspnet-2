using System.Linq.Expressions;
using WebApi.Helpers.Repositories;
using WebApi.Models.Dto;
using WebApi.Models.Entities;
using WebApi.Models.Schemas;

namespace WebApi.Helpers.Services;

public class ProductService : IProductService
{
    #region constructor

    private readonly ProductRepository _repo;

    public ProductService(ProductRepository repo)
    {
        _repo = repo;
    }

    #endregion

    public Product Get(Expression<Func<ProductEntity, bool>> expression)
    {
        return _repo.Get(expression);
    }

    public IEnumerable<Product> GetAll()
    {
        var items = new List<Product>();
        foreach (var item in _repo.Get())
            items.Add(item);

        return items;

    }

    public Product Add(ProductSchema schema)
    {
        var item = _repo.Get(x => x.Name == schema.Name);
        if (item == null)
        {
            item = _repo.Add(schema);
            return item;
        }

        return null!;
    }

}
