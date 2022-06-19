using System.ComponentModel.DataAnnotations;

namespace MVC_APP.ViewModels.Category
{
  public class PostCategoryViewModel
    {
        [Required]
        public string? Name { get; set; }
    }
}