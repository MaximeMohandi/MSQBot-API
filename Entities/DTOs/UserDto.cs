namespace MSQBot_API.Entities.DTOs
{
    public class UserDto
    {
        /// <summary>
        /// User id
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// User Name
        /// </summary>
        public string Name { get; set; } = string.Empty;
    }
}