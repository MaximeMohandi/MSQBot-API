using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSQBot_API.Core.Repositories.Base
{
    public interface IRepository<T> where T : class
    {
        Task <List<T>> GetAll();
        Task<T> Get(int id);
        Task Add(T entity);
        Task Update(T entity);
        Task DeleteAsync(T entity);

    }
}
