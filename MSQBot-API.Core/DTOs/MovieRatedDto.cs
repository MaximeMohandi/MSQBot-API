using MSQBot_API.Core.Interfaces;

namespace MSQBot_API.Core.DTOs
{
    /// <summary>
    /// Front representation of a movie
    /// </summary>
    public record MovieRatedDto : MovieDto
    {
        /// <summary>
        /// All rates given to the movie
        /// </summary>
        public List<RatesMovieDto>? Rates { get; set; }

        /// <summary>
        /// Average rate of the movie
        /// </summary>
        public decimal? AvgRate { get; init; }

        /// <summary>
        /// Best Rate given to the movie
        /// </summary>
        public decimal? MaxRate { get; init; }

        /// <summary>
        /// Worst rate given to the movie
        /// </summary>
        public decimal? MinRate { get; init; }
    }
}