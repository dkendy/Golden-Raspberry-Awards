using System;
using Awards.Domain.Entities;
using Awards.Service.Response;

namespace Awards.Service.Interface;

public interface IAwardsService
{
    public Task<Intervals> GetIntervals(int take);
    public Task<Guid> Insert(Nominate request);

    Task<int> Count();
    Task Delete(Guid id);
}
