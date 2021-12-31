using Microsoft.EntityFrameworkCore;
using MSQBot_API.Entities.Models;
using MSQBot_API.Interfaces;

namespace MSQBot_API.Repository
{
    public class RateRepository : Repository<Rate>, IRateRepository
    {
        public RateRepository(MSQBotDbContext repositoryContext) : base(repositoryContext)
        {
        }

        public IEnumerable<Rate> GetAllRates()
        {
            return FindAll().ToList();
        }

        public Rate GetMovieRate(int id)
        {
            var justOne = FindByCondition(x => x.Movie.MovieId == id).First();
            return RepositoryContext.rates.Include(x => x.Movie).Include(y => y.User).FirstOrDefault(); //join repo
        }
    }
}