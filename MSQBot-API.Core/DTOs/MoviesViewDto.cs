namespace MSQBot_API.Core.DTOs
{
    /// <summary>
    /// Get all movies with key data and last activities
    /// </summary>
    public class MoviesViewDto
    {
        /// <summary>
        /// Number of movie seen
        /// </summary>
        public int SeenMovieCount { get; init; } = 0;

        /// <summary>
        /// Number of movie not seen yet
        /// </summary>
        public int ToSeeMovieCount { get; init; } = 0;

        /// <summary>
        /// Movie with the best average rate
        /// </summary>
        public MovieRatedDto? BestMovie { get; init; } = null;

        /// <summary>
        /// Movie with worst average rate
        /// </summary>
        public MovieRatedDto? WorstMovie { get; init; } = null;

        /// <summary>
        /// Average rate given to movies
        /// </summary>
        public decimal? AvgRate { get; init; } = null;

        /// <summary>
        /// All the movies
        /// </summary>
        public List<MovieRatedDto>? Movies { get; init; } = null;

        /// <summary>
        /// Last activities on the movies data
        /// </summary>
        public List<ActivityDto>? Activities { get; init; } = null;
    }
}