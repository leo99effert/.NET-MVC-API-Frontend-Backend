using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Courses_API.Models;
using Courses_API.ViewModels.Skill;

namespace Courses_API.ViewModels.Teacher
{
    public class PostTeacherViewModel
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public ICollection<PostSkillViewModel> Skills { get; set; } = new List<PostSkillViewModel>();
    }
}