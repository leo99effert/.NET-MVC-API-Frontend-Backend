using System.ComponentModel.DataAnnotations.Schema;

namespace Courses_API.Models
{
  public class Course
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Length { get; set; }
        public int CategoryId { get; set; }
        public string? Description { get; set; }
        public string? Details { get; set; }       
        
        [ForeignKey("CategoryId")]
        public Category Category { get; set; } = new Category();
    }
}