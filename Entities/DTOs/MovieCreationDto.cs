using System.ComponentModel.DataAnnotations;

namespace MSQBot_API.Entities.DTOs
{
    /// <summary>
    /// Movie model inserted from client
    /// </summary>
    public class MovieCreationDto
    {
        /// <summary>
        /// movie title
        /// </summary>
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(50, ErrorMessage = "Max length is 50")]
        public string Title { get; set; } = string.Empty;
    }
}