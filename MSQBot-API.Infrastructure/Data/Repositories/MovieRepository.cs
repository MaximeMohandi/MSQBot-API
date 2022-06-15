using Microsoft.EntityFrameworkCore;
using MSQBot_API.Core.Entitites.Movies;
using MSQBot_API.Core.Exception;
using MSQBot_API.Core.Repositories;
using MSQBot_API.Infrastructure.Data.Repositories.Base;

namespace MSQBot_API.Infrastructure.Data.Repositories
{
    public class MovieRepository : Repository<Movie>, IMovieRepository
    {
        public MovieRepository(MSQBotDbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<List<Movie>> GetAll()
        {
            var movies = await _dbContext.Movies
                .Include(r => r.Rates)
                .ThenInclude(r => r.User)
                .ToListAsync();
            if (movies.Any())
            {
                return movies;
            }

            throw new NoMovieFoundException();
        }

        public override async Task<Movie> Get(int id)
        {
            return await _dbContext.Movies
                .Include(r => r.Rates)
                .ThenInclude(r => r.User)
                .FirstOrDefaultAsync(m => m.MovieId == id)
           ?? throw new NoMovieFoundException();

        }
        public override async Task Add(Movie movie)
        {
            bool isExist = _dbContext.Movies.Any(m => m.Title == movie.Title);
            if (isExist)
            {
                throw new MovieAlreadyExistException(movie.Title);
            }
            await base.Add(movie);
        }
    }
}
