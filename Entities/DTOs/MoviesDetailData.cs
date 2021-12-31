namespace MSQBot_API.Entities.DTOs
{
    public class MoviesDetailData
    {
        public int SeenMovieCount { get; set; }
        public int ToSeeMovieCount { get; set; }
        public MovieDto BestMovie { get; set; }
        public MovieDto WorstMovie { get; set; }
        public decimal? AvgRate { get; set; }
        public ICollection<MovieDto> MovieList { get; set; }
        public ICollection<ActivityDto> Activities { get; set; }
    }
}