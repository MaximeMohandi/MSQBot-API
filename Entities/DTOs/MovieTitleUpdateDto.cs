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
        public int MovieId { get; set; }

        /// <summary>
        /// New movie title
        /// </summary>
        public string NewTitle { get; set; }
    }
}