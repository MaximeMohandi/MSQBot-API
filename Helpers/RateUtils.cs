namespace MSQBot_API.Utils
{
    internal static class RateUtils
    {
        public static decimal RoundRate(decimal rate) => Math.Round(rate, 2);
    }
}