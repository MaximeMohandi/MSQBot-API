namespace MSQBot_API.Core.DTOs
{
    /// <summary>
    /// Get all movies with key data and last activities
    /// </summary>
    public class MoviesViewDto
    {
        private List<MovieRatedDto> _movies;

        public MoviesViewDto(List<MovieRatedDto> movies)
        {
            _movies = movies;
        }

        /// <summary>
        /// Number of movie seen
        /// </summary>
        public int SeenMovieCount { get => Movies.Count(m => m.SeenDate is not null); }

        /// <summary>
        /// Number of movie not seen yet
        /// </summary>
        public int ToSeeMovieCount { get => Movies.Count(m => m.SeenDate is null); }

        /// <summary>
        /// Movie with the best average rate
        /// </summary>
        public MovieRatedDto BestMovie { get => Movies.OrderByDescending(m => m.AvgRate).First(); }

        /// <summary>
        /// Movie with worst average rate
        /// </summary>
        public MovieRatedDto WorstMovie { get => Movies.Where(m => m.AvgRate.HasValue).Last(); }

        /// <summary>
        /// Average rate given to movies
        /// </summary>
        public decimal? AvgRate { get => Math.Round(Movies.Where(m => m.AvgRate.HasValue).Select(m => m.AvgRate).Average().Value, 2); }

        /// <summary>
        /// All the movies
        /// </summary>
        public List<MovieRatedDto> Movies { get => _movies; set => _movies = value; }

        /// <summary>
        /// Last activities on the movies data
        /// </summary>
        public List<ActivityDto> Activities
        {
            get
            {
                var result = new List<ActivityDto>();

                Movies
                .Where(m => m.SeenDate >= DateTime.Now.AddMonths(-1) || m.AddedDate >= DateTime.Now.AddMonths(-2))
                .ToList()
                .ForEach(m =>
                {
                    if (m.SeenDate.HasValue)
                    {
                        result.Add(new ActivityDto
                        {
                            Date = m.SeenDate.Value,
                            Title = "A movie has been rated",
                            Description = $"The movie \"{m.Title}\" has been given a {m.AvgRate}/10"
                        });
                    }
                    else
                    {
                        result.Add(new ActivityDto
                        {
                            Date = m.AddedDate,
                            Title = "A movie has been add",
                            Description = $"The movie \"{m.Title}\" is now in the watchlist"
                        }); ;
                    }
                });

                return result;
            }
        }
    }
}