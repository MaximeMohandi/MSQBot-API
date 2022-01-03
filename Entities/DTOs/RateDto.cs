namespace MSQBot_API.Entities.DTOs
{
    public class RateDto
    {
        private decimal? _rate;

        /// <summary>
        /// User that gave the rate
        /// </summary>
        public UserDto User { get; init; } = default!;

        /// <summary>
        /// rate given by user
        /// </summary>
        public decimal? Rate
        {
            get => _rate;
            set => _rate = value.HasValue ? Math.Round((decimal)value, 2) : null;
        }
    }
}