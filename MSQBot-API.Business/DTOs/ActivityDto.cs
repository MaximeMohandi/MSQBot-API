namespace MSQBot_API.Business.DTOs
{
    /// <summary>
    /// Represent an user action made on database
    /// </summary>
    public class ActivityDto
    {
        /// <summary>
        /// Date of the action
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// The title of the event
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// The event description
        /// </summary>
        public string Desc { get; set; } = string.Empty;
    }
}