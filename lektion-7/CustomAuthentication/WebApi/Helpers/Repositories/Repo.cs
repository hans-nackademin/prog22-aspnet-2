using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;
using WebApi.Contexts;

namespace WebApi.Helpers.Repositories
{
    public abstract class Repo<TEntity> where TEntity : class
    {
        private readonly DataContext _context;

        protected Repo(DataContext context)
        {
            _context = context;
        }

        public virtual async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression)
        {
            try
            {
                return await _context.Set<TEntity>().AnyAsync(expression);
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return false;
        }

        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            try
            {
                _context.Set<TEntity>().Add(entity);
                await _context.SaveChangesAsync();
                return entity!;
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return null!;
        }

        public virtual async Task<IEnumerable<TEntity>> GetAsync()
        {
            try
            {
                var result = await _context.Set<TEntity>().ToListAsync();   
                if (result != null)
                    return result!;
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return null!;
        }

        public virtual async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression)
        {
            try
            {
                var result = await _context.Set<TEntity>().FirstOrDefaultAsync(expression);
                if (result != null)
                    return result!;
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return null!;
        }

        public virtual async Task<TEntity> UpdateAsync(Expression<Func<TEntity, bool>> expression, TEntity entity)
        {
            try
            {
                var result = await _context.Set<TEntity>().FirstOrDefaultAsync(expression);
                if (result != null)
                {
                    result = entity;
                    _context.Set<TEntity>().Update(result);
                    await _context.SaveChangesAsync();
                    return result!;
                }
                    
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return null!;
        }

        public virtual async Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> expression)
        {
            try
            {
                var result = await _context.Set<TEntity>().FirstOrDefaultAsync(expression);
                if (result != null)
                {
                    _context.Set<TEntity>().Remove(result);
                    await _context.SaveChangesAsync();
                    return true;
                }

            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return false;
        }
    }
}
