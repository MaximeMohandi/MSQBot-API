using MSQBot_API.Core.Interfaces.Movies;

namespace MSQBot_API.Core.DTOs.Movies
{
    /// <summary>
    /// Front representation of a movie with it's rate
    /// </summary>
    public record MovieRatedDto : IMovie
    {
        public int MovieId { get; init; }

        public string Title { get; set; } = string.Empty;

        public string Poster { get; set; } = string.Empty;

        public DateTime AddedDate { get; set; }

        public DateTime? SeenDate { get; set; }

        public List<RatesMovieDto>? Rates { get; set; }

        public decimal? AvgRate { get; set; }

        public decimal? MaxRate { get; set; }

        public decimal? MinRate { get; set; }
    }
}