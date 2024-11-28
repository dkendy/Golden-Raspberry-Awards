using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Awards.Domain.Entities;

namespace Awards.Domain.Entities;

public interface INominateRepository
{
    Task<List<Nominate>> GetAll();
 
    Task<Guid> Insert(Nominate item);

    Task<Nominate> GetById(Guid id);
    Task Delete(Guid id);
}
