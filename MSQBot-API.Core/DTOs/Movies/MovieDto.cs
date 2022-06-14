using MSQBot_API.Core.Interfaces.Movies;

namespace MSQBot_API.Core.DTOs.Movies
{

    /// <summary>
    /// Front representation of a movie
    /// </summary>
    public class MovieDto : IMovie
    {
        public int MovieId { get; init; }
        public string Title { get; set; } = string.Empty;
        public string Poster { get; set; } = string.Empty;
        public DateTime AddedDate { get; set; }
        public DateTime? SeenDate { get; set; }
    }
}
