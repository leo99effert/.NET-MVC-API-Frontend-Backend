using System.ComponentModel.DataAnnotations;

namespace Courses_API.ViewModels.Security
{
  public class RegisterUserViewModel
    {
        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }

        public bool IsAdmin { get; set; } = false;
    }
}