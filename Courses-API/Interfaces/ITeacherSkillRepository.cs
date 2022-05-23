using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Courses_API.ViewModels.TeacherSkill;

namespace Courses_API.Interfaces
{
    public interface ITeacherSkillRepository
    {
        public Task<List<TeacherSkillViewModel>> ListAllTeacherSkillsAsync();
        public Task<List<TeacherSkillViewModel>> ListAllTeacherSkillsByTeacherIdAsync(int teacherId);
        public Task<TeacherSkillViewModel?> GetTeacherSkillAsync(int teacherId, int skillId);  
        public Task AddTeacherSkillAsync(PostTeacherSkillViewModel model);
        public Task DeleteTeacherSkillAsync(int teacherId, int skillId);
        public Task<bool> SaveAllAsync();
    }
}