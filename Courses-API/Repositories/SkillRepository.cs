using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Courses_API.Data;
using Courses_API.Interfaces;
using Courses_API.Models;
using Courses_API.ViewModels.Skill;
using Microsoft.EntityFrameworkCore;

namespace Courses_API.Repositories
{
    public class SkillRepository : ISkillRepository
    {
    private readonly CourseContext _context;
    private readonly IMapper _mapper;
    public SkillRepository(CourseContext context, IMapper mapper)
    {
      _mapper = mapper;
      _context = context;
    }

    public async Task<List<SkillViewModel>> ListAllSkillsAsync()
    {
      return await _context.Skills.ProjectTo<SkillViewModel>(_mapper.ConfigurationProvider).ToListAsync();
    }

    public async Task<SkillViewModel?> GetSkillAsync(int id)
    {
      return await _context.Skills.Where(c => c.Id == id)
        .ProjectTo<SkillViewModel>(_mapper.ConfigurationProvider)
        .SingleOrDefaultAsync();
    }

    public async Task AddSkillAsync(PostSkillViewModel model)
    {
      var skill = _mapper.Map<Skill>(model);
      await _context.Skills.AddAsync(skill);
    }

    public async Task UpdateSkillAsync(int id, PostSkillViewModel model)
    {
      var skill = await _context.Skills.FindAsync(id);
      if(skill is null)
      {
        throw new Exception($"There is no teacher with id {id}");
      }

      skill.Name = model.Name;

      _context.Skills.Update(skill);
    }

    public async Task DeleteSkillAsync(int id)
    {
      var response = await _context.Skills.FindAsync(id);
      if (response is not null)
      {
        _context.Skills.Remove(response);
      }
    }    

    public async Task<bool> SaveAllAsync()
    {
      return await _context.SaveChangesAsync() > 0;
    }    
  }
}