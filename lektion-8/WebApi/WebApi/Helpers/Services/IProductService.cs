using System.Linq.Expressions;
using WebApi.Models.Dto;
using WebApi.Models.Entities;
using WebApi.Models.Schemas;

namespace WebApi.Helpers.Services;

public interface IProductService
{
    Product Add(ProductSchema schema);
    Product Get(Expression<Func<ProductEntity, bool>> expression);
    IEnumerable<Product> GetAll();
}