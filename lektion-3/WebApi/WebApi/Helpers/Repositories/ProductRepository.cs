using WebApi.Contexts;
using WebApi.Models.Entities;

namespace WebApi.Helpers.Repositories
{
    public class ProductRepository : Repo<ProductEntity>
    {
        public ProductRepository(DataContext context) : base(context)
        {
        }
    }
}
