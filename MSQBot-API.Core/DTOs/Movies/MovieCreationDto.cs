using System.ComponentModel.DataAnnotations;

namespace MSQBot_API.Core.DTOs.Movies
{
    /// <summary>
    /// Movie model inserted from client
    /// </summary>
    public record MovieCreationDto
    {
        /// <summary>
        /// movie title
        /// </summary>
        [Required(ErrorMessage = "Title is required")]
        [MaxLength(50, ErrorMessage = "Max length is 50")]
        public string Title { get; init; } = string.Empty;

        /// <summary>
        /// The date when the movie has been added
        /// </summary>
        [Required(ErrorMessage = "Added date is required")]
        public DateTime AddedDate { get; init; }
    }
}