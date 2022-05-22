using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Courses_API.ViewModels.Student
{
    public class StudentViewModel
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public ICollection<CourseViewModel> Courses { get; set; } = new List<CourseViewModel>();
    }
}