using MSQBot_API.Entities.Models;
using MSQBot_API.Interfaces;

namespace MSQBot_API.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(MSQBotDbContext repositoryContext) : base(repositoryContext)
        {
        }

        public IEnumerable<User> GetAllUsers()
        {
            return FindAll().ToList();
        }
    }
}