using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Courses_API.ViewModels.Skill;

namespace Courses_API.Interfaces
{
    public interface ISkillRepository
    {
        public Task<List<SkillViewModel>> ListAllSkillsAsync();
        public Task<SkillViewModel?> GetSkillAsync(int id);  
        public Task AddSkillAsync(PostSkillViewModel model);
        public Task UpdateSkillAsync(int id, PostSkillViewModel model);
        public Task DeleteSkillAsync(int id);
        public Task<bool> SaveAllAsync();
    }
}