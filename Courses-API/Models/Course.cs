namespace Courses_API.Models
{
  public class Course
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Length { get; set; }
        public string? Category { get; set; }
        public string? Description { get; set; }
        public string? Details { get; set; }
    }
}