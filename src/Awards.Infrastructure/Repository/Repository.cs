
using System.Reflection.Metadata.Ecma335;
using Awards.Domain.Abstract;
using Awards.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Awards.Infrastructure.Repositories;

internal abstract class Repository<T> where T : Entity
{
    protected readonly AwardsDbContext _dbContext;

    protected Repository(AwardsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public virtual async Task<Guid> Add(T entity)
    {
        _dbContext.Add(entity);
        var resultInsert = await _dbContext.SaveChangesAsync();

        if(resultInsert > 0) 
            return entity.Id;
        else 
            return new Guid(); 
        
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await _dbContext.Set<T>().ToListAsync();
    }

    public async Task<T> GetByIdAsync(
     Guid id)
    {
        return await _dbContext
            .Set<T>()
            .FirstOrDefaultAsync(user => user.Id == id);
    }

     public async Task Exclude(Guid id)
    {
        var item =  await _dbContext
            .Set<T>()
            .FirstOrDefaultAsync(user => user.Id == id);

        _dbContext.Remove(item);
        await _dbContext.SaveChangesAsync();
    }

}
