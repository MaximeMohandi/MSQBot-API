using MSQBot_API.Core.Entitites.Meters;
using MSQBot_API.Core.Repositories;
using MSQBot_API.Infrastructure.Data.Repositories.Base;

namespace MSQBot_API.Infrastructure.Data.Repositories
{
    public class MeterRepository : Repository<Meter>, IMeterRepository
    {
        public MeterRepository(MSQBotDbContext dbContext) : base(dbContext) { }

    }
}
