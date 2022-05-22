using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Courses_API.ViewModels.Skill;

namespace Courses_API.ViewModels.Teacher
{
    public class TeacherViewModel
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public ICollection<SkillViewModel> Skills { get; set; } = new List<SkillViewModel>();
    }
}