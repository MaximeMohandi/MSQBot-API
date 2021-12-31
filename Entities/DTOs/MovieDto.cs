using System.ComponentModel.DataAnnotations;

namespace MSQBot_API.Entities.DTOs
{
    /// <summary>
    /// Front representation of a movie
    /// </summary>
    public class MovieDto
    {
        /// <summary>
        /// id movie in database
        /// </summary>
        public int _id { get; set; }

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

        /// <summary>
        /// Date when the movie has been seen
        /// </summary>
        public DateTime? SeenDate { get; set; }

        /// <summary>
        /// Average rate of the movie
        /// </summary>
        public decimal? AvgRate { get; set; }

        /// <summary>
        /// Best Rate given to the movie
        /// </summary>
        public decimal? TopRate { get; set; }

        /// <summary>
        /// Worst rate given to the movie
        /// </summary>
        public decimal? BottomRate { get; set; }

        /// <summary>
        /// Movie poster
        /// </summary>
        public string? Poster { get; set; }
    }
}