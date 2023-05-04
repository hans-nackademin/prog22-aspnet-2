using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebApi.Contexts;
using WebApi.Models.Entities;
using WebApi.Models.Schemas;

namespace WebApi.Helpers.Services;

public class CategoryService
{
    private readonly DataContext _context;

    public CategoryService(DataContext context)
    {
        _context = context;
    }

    public async Task<CategoryEntity> CreateAsync(CategoryEntity entity) 
    { 
        _context.Categories.Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }
    public async Task<CategoryEntity> GetAsync(Expression<Func<CategoryEntity, bool>> expression) 
    { 
        var entity = await _context.Categories.FirstOrDefaultAsync(expression);
        return entity!;
    }
    public async Task<IEnumerable<CategoryEntity>> GetAllAsync() 
    { 
        var entities = await _context.Categories.ToListAsync();
        return entities;
    }

    public async Task UpdateAsync() { }
    public async Task DeleteAsync() { }
}
