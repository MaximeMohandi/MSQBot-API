namespace MSQBot_API.Entities.DTOs
{
    /// <summary>
    /// Base movie datas
    /// </summary>
    public class MovieDto
    {
        /// <summary>
        /// Movie unique id
        /// </summary>
        public int MovieId { get; set; }

        /// <summary>
        /// movie title
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Movie poster
        /// </summary>
        public string Poster { get; set; } = string.Empty;

        /// <summary>
        /// The date when the movie has been added
        /// </summary>
        public DateTime AddedDate { get; set; }

        /// <summary>
        /// Date when the movie has been seen
        /// </summary>
        public DateTime? SeenDate { get; set; }
    }
}