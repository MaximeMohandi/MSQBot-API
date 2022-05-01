using System.ComponentModel.DataAnnotations;

namespace MSQBot_API.Core.DTOs
{
    /// <summary>
    /// Data used to rate a movie
    /// </summary>
    public record MovieRateCreationDto
    {

        /// <summary>
        /// Id of the movie rated
        /// </summary>
        [Required(ErrorMessage = "Id movie is required")]
        public int MovieId { get; init; }

        /// <summary>
        /// Id of the user who rate the movie
        /// </summary>
        [Required(ErrorMessage = "Id user is required")]
        public long UserId { get; init; }

        /// <summary>
        /// Rate given by the user to the movie (rounded to two decimal)
        /// </summary>
        [Required(ErrorMessage = "Rate is required")]
        public decimal Rate { get; init; }
    }
}