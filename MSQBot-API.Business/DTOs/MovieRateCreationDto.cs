using System.ComponentModel.DataAnnotations;

namespace MSQBot_API.Business.DTOs
{
    /// <summary>
    /// Data used to rate a movie
    /// </summary>
    public class MovieRateCreationDto
    {
        private decimal _rate;

        /// <summary>
        /// Id of the movie rated
        /// </summary>
        [Required(ErrorMessage = "Id movie is required")]
        public int MoviId { get; set; }

        /// <summary>
        /// Id of the user who rate the movie
        /// </summary>
        [Required(ErrorMessage = "Id user is required")]
        public long UserId { get; set; }

        /// <summary>
        /// Rate given by the user to the movie (rounded to two decimal)
        /// </summary>
        [Required(ErrorMessage = "Rate is required")]
        public decimal Rate { get => _rate; set => _rate = Math.Round(value, 2); }
    }
}