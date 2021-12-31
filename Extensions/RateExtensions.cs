using MSQBot_API.Entities.Models;
using MSQBot_API.Utils;

namespace MSQBot_API.Extensions
{
    internal static class RateExtensions
    {
        /// <summary>
        /// Fetch the highest Rate in the list
        /// </summary>
        /// <param name="rates"></param>
        /// <returns></returns>
        public static decimal? MaxRate(this ICollection<Rate> rates) => rates.Count > 0 ? RateUtils.RoundRate(rates.Select(r => r.Note).Max()) : null;

        /// <summary>
        /// Fetch the lowest Rate in the list
        /// </summary>
        /// <param name="rates"></param>
        /// <returns></returns>
        public static decimal? MinRate(this ICollection<Rate> rates) => rates.Count > 0 ? RateUtils.RoundRate(rates.Select(r => r.Note).Min()) : null;

        /// <summary>
        /// Compute the average rate
        /// </summary>
        /// <param name="rates"></param>
        /// <returns></returns>
        public static decimal? AvgRate(this ICollection<Rate> rates) => rates.Count > 0 ? RateUtils.RoundRate(rates.Average(r => r.Note)) : null;
    }
}