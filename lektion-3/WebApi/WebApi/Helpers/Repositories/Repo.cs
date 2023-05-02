using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebApi.Contexts;

namespace WebApi.Helpers.Repositories
{
    public class Repo<TEntity> where TEntity : class
    {
        private readonly DataContext _context;

        public Repo(DataContext context)
        {
            _context = context;
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression)
        {
            var item = await _context.Set<TEntity>().FirstOrDefaultAsync(expression);
            return item!;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }
    }
}
