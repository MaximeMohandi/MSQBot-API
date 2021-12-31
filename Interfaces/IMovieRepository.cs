using MSQBot_API.Entities.Models;

namespace MSQBot_API.Interfaces
{
    public interface IMovieRepository : IRepositoryBase<Movie>
    {
        IEnumerable<Movie> GetAllMovies();

        Movie GetMovie(long id);

        void AddMovie(Movie movie);

        void AddPoster(Movie movie, string posterPath);
    }
}