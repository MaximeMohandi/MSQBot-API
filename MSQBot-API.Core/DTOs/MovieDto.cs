using MSQBot_API.Core.Interfaces;

namespace MSQBot_API.Core.DTOs
{
    /// <summary>
    /// Front representation of a movie
    /// </summary>
    public record MovieDto
    {
        /// <summary>
        /// Movie unique id
        /// </summary>
        public int MovieId { get; init; }

        /// <summary>
        /// movie title
        /// </summary>
        public string Title { get; init; } = string.Empty;

        /// <summary>
        /// Movie poster
        /// </summary>
        public string Poster { get; init; } = string.Empty;

        /// <summary>
        /// The date when the movie has been added
        /// </summary>
        public DateTime AddedDate { get; init; }

        /// <summary>
        /// Date when the movie has been seen
        /// </summary>
        public DateTime? SeenDate { get; init; }

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