namespace MSQBot_API.Entities.DTOs
{
    public class RateDto
    {
        /// <summary>
        /// User that gave the rate
        /// </summary>
        public UserDto User { get; set; }

        /// <summary>
        /// rate given by user
        /// </summary>
        public decimal? Rate { get; set; }
    }
}