using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Courses_API.ViewModels.Teacher;

namespace Courses_API.Interfaces
{
    public interface ITeacherRepository
    {
        public Task<List<TeacherViewModel>> ListAllTeachersAsync();
        public Task<TeacherViewModel?> GetTeacherAsync(int id);  
        public Task AddTeacherAsync(PostTeacherViewModel model);
        public Task UpdateTeacherAsync(int id, PostTeacherViewModel model);
        public Task DeleteTeacherAsync(int id);
        public Task<bool> SaveAllAsync();
    }
}