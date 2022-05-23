using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Courses_API.Models
{
    public class TeacherSkill
    {
        public int TeacherId { get; set; }
        public int SkillId { get; set; }
        public Teacher Teacher { get; set; } = new Teacher();
        public Skill Skill { get; set; } = new Skill();
    }
}