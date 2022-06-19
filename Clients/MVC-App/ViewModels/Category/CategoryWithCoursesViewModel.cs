using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVC_APP.ViewModels.Course;

namespace MVC_APP.ViewModels.Category
{
    public class CategoryWithCoursesViewModel
    {
        public int CategoryId { get; set; }
        public string? Name { get; set; }
        public List<CourseInCategoryListViewModel> Courses { get; set; } = new List<CourseInCategoryListViewModel>();
    }
}