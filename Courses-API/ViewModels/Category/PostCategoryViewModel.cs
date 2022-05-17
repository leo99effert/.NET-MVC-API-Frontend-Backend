using System.ComponentModel.DataAnnotations;

namespace Courses_API.ViewModels.Category
{
  public class PostCategoryViewModel
    {
        [Required]
        public string? Name { get; set; }
    }
}