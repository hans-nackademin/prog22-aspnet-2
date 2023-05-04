using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebApi.Contexts;
using WebApi.Models.Entities;
using WebApi.Models.Schemas;

namespace WebApi.Helpers.Services
{
    public class ProductService
    {
        private readonly DataContext _context;

        public ProductService(DataContext context)
        {
            _context = context;
        }

        public async Task<ProductEntity> CreateAsync(ProductEntity entity) 
        {
            _context.Products.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        public async Task<ProductEntity> GetAsync(Expression<Func<ProductEntity, bool>> expression)
        {
            var entity = await _context.Products.FirstOrDefaultAsync(expression);
            return entity!;
        }
        public async Task<IEnumerable<ProductEntity>> GetAllAsync()
        {
            var entities = await _context.Products.ToListAsync();
            return entities;
        }

        public async Task UpdateAsync() { }
        public async Task DeleteAsync() { }

    }
}
