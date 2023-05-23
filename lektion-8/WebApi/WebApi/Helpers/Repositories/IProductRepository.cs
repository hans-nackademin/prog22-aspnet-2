using System.Linq.Expressions;
using WebApi.Models.Entities;

namespace WebApi.Helpers.Repositories
{
    public interface IProductRepository
    {
        ProductEntity Add(ProductEntity entity);
        IEnumerable<ProductEntity> Get();
        ProductEntity Get(Expression<Func<ProductEntity, bool>> expression);
    }
}