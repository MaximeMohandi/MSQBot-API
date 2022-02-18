using MSQBot_API.Core.Interfaces;

namespace MSQBot_API.Core.DTOs
{
    public abstract class RateDtoBase : IRate
    {
        private decimal? _rate;
        /// <summary>
        /// rate given by user rouded to 2 decimal
        /// </summary>
        public decimal? Rate
        {
            get => _rate;
            set => _rate = value.HasValue ? Math.Round((decimal)value, 2) : null;
        }
    }
}
