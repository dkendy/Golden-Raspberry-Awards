using Awards.Domain.Entities;
using Awards.Infraestructure.Data; 

namespace Awards.Infrastructure.Repositories;

internal sealed class NominateRepository : Repository<Nominate>, INominateRepository
{
    public NominateRepository(AwardsDbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task Delete(Guid id)
    {
        await Exclude(id);
    }

    public async Task<List<Nominate>> GetAll()
    {
        return await this.GetAllAsync();
    }

    public async Task<Nominate> GetById(Guid id)
    {
        return await this.GetById(id);
    }
  
    public async Task<Guid> Insert(Nominate item)
    {
        var insertResult = await this.Add(item);

        return insertResult;
    }
}
