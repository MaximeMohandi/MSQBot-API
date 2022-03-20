using MSQBot_API.Core.Interfaces;

namespace MSQBot_API.Core.DTOs
{
    /// <summary>
    /// Front representation of a movie with it's rate
    /// </summary>
    public record MovieRatedDto : IMovie
    {
        public int MovieId { get; init; }

        public string Title { get; init; } = string.Empty;

        public string Poster { get; init; } = string.Empty;

        public DateTime AddedDate { get; init; }

        public DateTime? SeenDate { get; init; }

        public List<RatesMovieDto>? Rates { get; set; }

        public decimal? AvgRate { get; init; }

        public decimal? MaxRate { get; init; }

        public decimal? MinRate { get; init; }
    }
}