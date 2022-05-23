using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Courses_API.Data;
using Courses_API.Interfaces;
using Courses_API.Models;
using Courses_API.ViewModels.TeacherSkill;
using Microsoft.EntityFrameworkCore;

namespace Courses_API.Repositories
{
    public class TeacherSkillRepository : ITeacherSkillRepository
    {
        private readonly CourseContext _context;
        private readonly IMapper _mapper;
        public TeacherSkillRepository(CourseContext context, IMapper mapper)
        {
        _mapper = mapper;
        _context = context;
        }

        public async Task<List<TeacherSkillViewModel>> ListAllTeacherSkillsAsync()
        {
          return await _context.TeacherSkills.ProjectTo<TeacherSkillViewModel>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<List<TeacherSkillViewModel>> ListAllTeacherSkillsByTeacherIdAsync(int teacherId)
        {
          var fullList = await _context.TeacherSkills.ProjectTo<TeacherSkillViewModel>(_mapper.ConfigurationProvider).ToListAsync();
          var reducedList = fullList.Where(t => t.TeacherId == teacherId).ToList();
          return reducedList;
        }

        public async Task<TeacherSkillViewModel?> GetTeacherSkillAsync(int teacherId, int skillId)
        {
          return await _context.TeacherSkills.Where(c => c.TeacherId == teacherId && c.SkillId == skillId)
            .ProjectTo<TeacherSkillViewModel>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync();
        }

        public async Task AddTeacherSkillAsync(PostTeacherSkillViewModel model)
        {
          var teacher = await _context.Teachers.FindAsync(model.TeacherId);
          var skill = await _context.Skills.FindAsync(model.SkillId);

          if (teacher is not null && skill is not null)
          {
            var teacherSkill = new TeacherSkill 
            {
              TeacherId = teacher.Id,
              SkillId = skill.Id,
              Teacher = teacher,
              Skill = skill
            };
            await _context.TeacherSkills.AddAsync(teacherSkill);
          }     
        }

        public async Task DeleteTeacherSkillAsync(int teacherid, int skillId)
        {
          var response = await _context.TeacherSkills.FindAsync(teacherid, skillId);
          if (response is not null)
          {
            _context.TeacherSkills.Remove(response);
          }
        }

        public async Task<bool> SaveAllAsync()
        {
          return await _context.SaveChangesAsync() > 0;
        }
  }
}