using System.Linq.Expressions;
using WebApi.Contexts;
using WebApi.Models.Entities;

namespace WebApi.Helpers.Repositories;

public class ProductRepository : IProductRepository
{
    #region constructor
    private readonly DataContext _context;

    public ProductRepository(DataContext context)
    {
        _context = context;
    }
    #endregion

    public ProductEntity Add(ProductEntity entity)
    {
        _context.Add(entity);
        _context.SaveChanges();
        return entity;
    }

    public ProductEntity Get(Expression<Func<ProductEntity, bool>> expression)
    {
        var entity = _context.Products.FirstOrDefault(expression);
        if (entity != null)
            return entity;

        return null!;
    }

    public IEnumerable<ProductEntity> Get()
    {
        var entity = _context.Products.ToList();
        return entity!;
    }
}
