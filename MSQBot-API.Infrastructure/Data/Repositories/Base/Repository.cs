using Microsoft.EntityFrameworkCore;
using MSQBot_API.Core.Repositories.Base;

namespace MSQBot_API.Infrastructure.Data.Repositories.Base
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public readonly MSQBotDbContext _dbContext;
        public Repository(MSQBotDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task Add(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task<T> Get(int id) => await _dbContext.Set<T>().FindAsync(id);

        public virtual async Task<List<T>> GetAll() => await _dbContext.Set<T>().ToListAsync();

        public async Task Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
