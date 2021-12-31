using MSQBot_API.Entities.DTOs;
using MSQBot_API.Utils;

namespace MSQBot_API.Extensions
{
    internal static class MovieExtensions
    {
        /// <summary>
        /// Fetch the movie with the highest average note
        /// </summary>
        /// <param name="movies"></param>
        /// <returns>The Best movie in the list</returns>
        public static MovieDto BestMovie(this List<MovieDto> movies) => movies.OrderByDescending(m => m.AvgRate).First();

        /// <summary>
        /// Fetch the movie with the lowest average note
        /// </summary>
        /// <param name="movies"></param>
        /// <returns>The worst movie in the list</returns>
        public static MovieDto WorstMovie(this List<MovieDto> movies) => movies.Where(m => m.AvgRate.HasValue).Last();

        /// <summary>
        /// Compute the average not of all the movie in the list
        /// </summary>
        /// <param name="movies"></param>
        /// <returns>Global average note</returns>
        public static decimal MoviesAvgRate(this List<MovieDto> movies)
        {
            return RateUtils.RoundRate((decimal)movies.Where(m => m.AvgRate.HasValue).Select(m => m.AvgRate).Average());
        }
    }
}