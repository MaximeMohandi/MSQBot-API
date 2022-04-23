﻿using MSQBot_API.Core.Entities;
using MSQBot_API.Core.Repositories.Base;

namespace MSQBot_API.Core.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        bool IsUserExist(long id, string name);
        Task<User> Get(long userId);
    }
}
