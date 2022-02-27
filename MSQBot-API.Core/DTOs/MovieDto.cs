namespace MSQBot_API.Core.DTOs
{
    /// <summary>
    /// Base movie datas
    /// </summary>
    public record MovieDto
    {
        /// <summary>
        /// Movie unique id
        /// </summary>
        public int MovieId { get; init; }

        /// <summary>
        /// movie title
        /// </summary>
        public string Title { get; init; } = string.Empty;

        /// <summary>
        /// Movie poster
        /// </summary>
        public string Poster { get; init; } = string.Empty;

        /// <summary>
        /// The date when the movie has been added
        /// </summary>
        public DateTime AddedDate { get; init; }

        /// <summary>
        /// Date when the movie has been seen
        /// </summary>
        public DateTime? SeenDate { get; init; }
    }
}