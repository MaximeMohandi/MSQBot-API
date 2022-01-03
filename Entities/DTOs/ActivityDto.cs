namespace MSQBot_API.Entities.DTOs
{
    public class ActivityDto
    {
        public DateTime Date { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Desc { get; set; } = string.Empty;
    }
}