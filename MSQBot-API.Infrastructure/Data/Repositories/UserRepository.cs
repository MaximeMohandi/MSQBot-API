using MSQBot_API.Core.Entities;
using MSQBot_API.Core.Exception;
using MSQBot_API.Core.Repositories;
using MSQBot_API.Infrastructure.Data.Repositories.Base;

namespace MSQBot_API.Infrastructure.Data.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(MSQBotDbContext dbContext) : base(dbContext)
        {
        }

        public bool IsUserExist(User user)
        {
            return _dbContext.Users.Any(u => u.UserId == user.UserId && u.Name == user.Name);
        }

        public async Task<User> Get(long id)
        {
            return await _dbContext.Users.FindAsync(id)
                ?? throw new UserNotFoundException(id.ToString());
        }
    }
}
