using MSQBot_API.Core.Interfaces;

namespace MSQBot_API.Core.Extension
{
    public static class RateOperationExtension
    {
        /// <summary>
        /// Roud a rate
        /// </summary>
        /// <param name="source">Rate to round</param>
        /// <returns>Rate rounded to 2 decimal</returns>
        public static decimal? RoundRate<TRate>(this TRate source) where TRate : IRate
        {
            return source.Rate is not null ? Round(source.Rate.Value) : null;
        }

        /// <summary>
        /// Average of all rates
        /// </summary>
        /// <param name="sources">list of element with rate</param>
        /// <returns>the average rate of all element rounded to 2 decimal</returns>
        public static decimal? AvgRate<TRate>(this List<TRate>? sources) where TRate : IRate
        {
            return HasRates(sources) ? Round(sources.Average(r => r?.Rate is null ? 0 : r.Rate.Value)) : null;
        }

        public static decimal? MaxRate<TRate>(this List<TRate>? sources) where TRate : IRate
        {
            return HasRates(sources) ? sources.Max(r=>r?.Rate) : null;
        }

        public static decimal? MinRate<TRate>(this List<TRate>? sources) where TRate : IRate
        {
            return HasRates(sources) ? sources.Min(r=>r?.Rate) : null;
        }

        /// <summary>
        /// Check if the list has rate
        /// </summary>
        /// <param name="sources">list of rate element </param>
        /// <returns>True if list not null and has element, false otherwise</returns>
        public static bool HasRates<TRate>(this List<TRate>? sources) where TRate : IRate
        {
            return sources is not null && sources.Count() > 0;
        }


        private static decimal Round(decimal toRound)
        {
            return Math.Round(toRound, 2);
        }
    }
}
