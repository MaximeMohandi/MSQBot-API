using System.ComponentModel.DataAnnotations;

namespace MSQBot_API.Entities.DTOs
{
    /// <summary>
    /// Data used to change a movie title
    /// </summary>
    public class MovieTitleUpdateDto
    {
        /// <summary>
        /// Id of the movie to change
        /// </summary>
        [Required(ErrorMessage = "Id movie is required")]
        public int MovieId { get; set; }

        /// <summary>
        /// New movie title
        /// </summary>
        [Required(ErrorMessage = "Title is required")]
        [MaxLength(50, ErrorMessage = "Max length is 50")]
        public string NewTitle { get; set; }
    }
}