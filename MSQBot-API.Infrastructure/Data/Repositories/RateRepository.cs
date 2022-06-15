using Microsoft.EntityFrameworkCore;
using MSQBot_API.Core.Entitites.Movies;
using MSQBot_API.Core.Repositories;
using MSQBot_API.Infrastructure.Data.Repositories.Base;

namespace MSQBot_API.Infrastructure.Data.Repositories
{
    public class RateRepository : Repository<Rate>, IRateRepository
    {
        public RateRepository(MSQBotDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Rate>> GetRatesUser(long userId)
        {
            return await _dbContext.Rates
                .Include(r => r.Movie)
                .Include(r => r.User)
                .Where(r => userId == r.UserId)
                .ToListAsync();
        }
    }
}
