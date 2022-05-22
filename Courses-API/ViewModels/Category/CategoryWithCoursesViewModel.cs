using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Courses_API.ViewModels.Course;

namespace Courses_API.ViewModels.Category
{
    public class CategoryWithCoursesViewModel
    {
        public int CategoryId { get; set; }
        public string? Name { get; set; }
        public List<CourseInCategoryListViewModel> Courses { get; set; } = new List<CourseInCategoryListViewModel>();
    }
}