using MSQBot_API.Core.Interfaces.Users;

namespace MSQBot_API.Core.DTOs.Movies
{
    public record UserMovieRateDto
    {
        public IUser User { get; set; }

        public decimal? AverageUserRating { get; set; }

        public List<RatesMovieDto> RatedMovies { get; set; }
    }
}
