using Microsoft.EntityFrameworkCore;
using MSQBot_API.Entities.DTOs;
using MSQBot_API.Entities.Models;
using MSQBot_API.Helpers;

namespace MSQBot_API.Services
{
    /// <summary>
    /// Service to manage rates datas
    /// </summary>
    public class RateServices
    {
        private readonly MSQBotDbContext _dbContext;

        public RateServices(MSQBotDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Get all the rate a user had given to a movie.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>List of rates with movie associated for a user</returns>
        public List<RatesMovieDto> GetRatesUser(long userId)
        {
            return _dbContext.Rates
                    .Include(r => r.Movie)
                    .Include(r => r.User)
                    .Where(r => r.UserId == userId)
                    .ToList()
                    .MapRatesToDTOs();
        }
    }
}