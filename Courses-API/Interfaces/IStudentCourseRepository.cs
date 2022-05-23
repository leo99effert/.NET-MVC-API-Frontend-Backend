using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Courses_API.ViewModels.StudentCourse;

namespace Courses_API.Interfaces
{
    public interface IStudentCourseRepository
    {
        public Task<List<StudentCourseViewModel>> ListStudentCoursesAsync();
        public Task<List<StudentCourseViewModel>> ListStudentCoursesByStudentIdAsync(int studentIt);
        public Task<StudentCourseViewModel?> GetStudentCourseAsync(int studentId, int courseId);  
        public Task AddStudentCourseAsync(PostStudentCourseViewModel model);
        public Task DeleteStudentCourseAsync(int studentId, int courseId);
        public Task<bool> SaveAllAsync();
    }
}