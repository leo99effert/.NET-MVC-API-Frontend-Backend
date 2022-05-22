using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Courses_API.ViewModels.Student;

namespace Courses_API.Interfaces
{
    public interface IStudentRepository
    {
        public Task<List<StudentViewModel>> ListAllStudentsAsync();
        public Task<StudentViewModel?> GetStudentAsync(int id);  
        public Task AddStudentAsync(PostStudentViewModel model);
        public Task UpdateStudentAsync(int id, PostStudentViewModel model);
        public Task DeleteStudentAsync(int id);
        public Task<bool> SaveAllAsync();
    }
}