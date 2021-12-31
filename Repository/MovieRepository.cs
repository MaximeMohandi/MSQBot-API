using Microsoft.EntityFrameworkCore;
using MSQBot_API.Entities.Models;
using MSQBot_API.Interfaces;

namespace MSQBot_API.Repository
{
    public class MovieRepository : Repository<Movie>, IMovieRepository
    {
        public MovieRepository(MSQBotDbContext repositoryContext) : base(repositoryContext)
        {
        }

        public IEnumerable<Movie> GetAllMovies()
        {
            return RepositoryContext.movies.Include(r => r.Rates).ThenInclude(r => r.User);
        }

        public Movie GetMovie(long id)
        {
            return FindByCondition(m => m.MovieId == id).Include(m => m.Rates).ThenInclude(r => r.User).First();
        }

        public void AddMovie(Movie movie)
        {
            Create(movie);
        }

        public void AddPoster(Movie movie, string posterPath)
        {
            throw new NotImplementedException();
        }
    }
}