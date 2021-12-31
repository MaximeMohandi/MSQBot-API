using MSQBot_API.Entities.Models;

namespace MSQBot_API.Interfaces
{
    public interface IRateRepository : IRepositoryBase<Rate>
    {
        IEnumerable<Rate> GetAllRates();

        Rate GetMovieRate(int id);
    }
}