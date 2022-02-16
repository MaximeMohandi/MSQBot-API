using Microsoft.EntityFrameworkCore;
using MSQBot_API.Core.Entities;
using MSQBot_API.Core.Repositories;
using MSQBot_API.Infrastructure.Data.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSQBot_API.Infrastructure.Data.Repositories
{
    public class MovieRepository : Repository<Movie>, IMovieRepository
    {
        public MovieRepository(MSQBotDbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<List<Movie>> GetAll()
        {
            return await _dbContext.Set<Movie>()
                .Include(r => r.Rates)
                .ThenInclude(r => r.User)
                .ToListAsync();
        }

        public override async Task<Movie> Get(int id)
        {
            return await _dbContext.Set<Movie>()
                .Include(r => r.Rates)
                .ThenInclude(r => r.User)
                .FirstOrDefaultAsync(m => m.MovieId == id);
        }
    }
}
