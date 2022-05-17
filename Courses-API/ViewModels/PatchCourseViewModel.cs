using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Courses_API.ViewModels
{
    public class PatchCourseViewModel
    {
        [Required]
        public string? Length { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public string? Details { get; set; }
    }
}