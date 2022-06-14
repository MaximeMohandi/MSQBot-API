using System.ComponentModel.DataAnnotations;

namespace MSQBot_API.Core.DTOs.Movies
{
    /// <summary>
    /// Data used to change a movie title
    /// </summary>
    public record MovieTitleUpdateDto
    {
        /// <summary>
        /// Id of the movie to change
        /// </summary>
        [Required(ErrorMessage = "Id movie is required")]
        public int MovieId { get; init; }

        /// <summary>
        /// New movie title
        /// </summary>
        [Required(ErrorMessage = "Title is required")]
        [MaxLength(50, ErrorMessage = "Max length is 50")]
        public string NewTitle { get; init; } = string.Empty;
    }
}