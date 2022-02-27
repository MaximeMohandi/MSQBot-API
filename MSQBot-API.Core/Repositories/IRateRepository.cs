﻿using MSQBot_API.Core.Entities;
using MSQBot_API.Core.Repositories.Base;

namespace MSQBot_API.Core.Repositories
{
    public interface IRateRepository : IRepository<Rate>
    {
        Task<List<Rate>> GetRatesUser(long userId);
    }
}