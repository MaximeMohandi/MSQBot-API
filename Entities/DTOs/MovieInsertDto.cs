using System.ComponentModel.DataAnnotations;

namespace MSQBot_API.Entities.DTOs
{
    /// <summary>
    /// Movie model inserted from client
    /// </summary>
    public class MovieInsertDto
    {
        /// <summary>
        /// movie title
        /// </summary>
        [Required(ErrorMessage = "Name is required")]
        public string Title { get; set; }

        /// <summary>
        /// The date when the movie has been added
        /// </summary>
        [Required(ErrorMessage = "Added date is required")]
        public DateTime AddedDate { get; set; }
    }
}