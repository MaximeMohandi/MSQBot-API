using MSQBot_API.Entities.Models;

namespace MSQBot_API.Interfaces
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        IEnumerable<User> GetAllUsers();
    }
}