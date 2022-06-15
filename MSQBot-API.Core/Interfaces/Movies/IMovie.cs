namespace MSQBot_API.Core.Interfaces.Movies
{
    public interface IMovie
    {
        /// <summary>
        /// Movie unique id
        /// </summary>
        public int MovieId { get; init; }

        /// <summary>
        /// movie title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Movie poster
        /// </summary>
        public string Poster { get; set; }

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
