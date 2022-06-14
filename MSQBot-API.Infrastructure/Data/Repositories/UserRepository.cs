using MSQBot_API.Core.Entitites.Users;
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

        /// <summary>
        /// Check if user exist 
        /// </summary>
        /// <param name="id">user id</param>
        /// <param name="name">user name</param>
        /// <returns>True if user exist, false otherwise</returns>
        public bool IsUserExist(long id, string name)
        {
            return _dbContext.Users.Any(u => u.UserId == id && u.Name == name);
        }

        /// <summary>
        /// Get user with given Id
        /// </summary>
        /// <param name="id">Id user to fetch</param>
        /// <returns>User</returns>
        /// <exception cref="UserNotFoundException">the user with this id doesn't exist</exception>
        public async Task<User> Get(long id)
        {
            return await _dbContext.Users.FindAsync(id)
                ?? throw new UserNotFoundException(id.ToString());
        }
    }
}
