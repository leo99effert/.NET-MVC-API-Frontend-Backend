using System.ComponentModel.DataAnnotations;

namespace MVC_APP.ViewModels
{
  public class PostCourseViewModel
    {
        [Required(ErrorMessage = "Title is mandatory")]
        public string? Title { get; set; }
        public string? Length { get; set; }
        public string? Category { get; set; }
        public string? Description { get; set; }
        public string? Details { get; set; }
    }
}