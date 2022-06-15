namespace MSQBot_API.Core.DTOs.Movies
{
    /// <summary>
    /// Represent an user action made on database
    /// </summary>
    public record ActivityDto
    {
        /// <summary>
        /// Date of the action
        /// </summary>
        public DateTime Date { get; init; }

        /// <summary>
        /// The title of the event
        /// </summary>
        public string Title { get; init; } = string.Empty;

        /// <summary>
        /// The event description
        /// </summary>
        public string Description { get; init; } = string.Empty;
    }
}