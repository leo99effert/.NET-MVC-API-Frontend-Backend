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
using Courses_API.ViewModels.Teacher;
using Microsoft.EntityFrameworkCore;

namespace Courses_API.Repositories
{
    public class TeacherRepository : ITeacherRepository
    {
    private readonly CourseContext _context;
    private readonly IMapper _mapper;
    public TeacherRepository(CourseContext context, IMapper mapper)
    {
      _mapper = mapper;
      _context = context;
    }

    public async Task<List<TeacherViewModel>> ListAllTeachersAsync()
    {
      return await _context.Teachers.ProjectTo<TeacherViewModel>(_mapper.ConfigurationProvider).ToListAsync();
    }

    public async Task<TeacherViewModel?> GetTeacherAsync(int id)
    {
      return await _context.Teachers.Where(c => c.Id == id)
        .ProjectTo<TeacherViewModel>(_mapper.ConfigurationProvider)
        .SingleOrDefaultAsync();
    }

    public async Task AddTeacherAsync(PostTeacherViewModel model)
    {
      var teacher = _mapper.Map<Teacher>(model);
      await _context.Teachers.AddAsync(teacher);
    }

    public async Task UpdateTeacherAsync(int id, PostTeacherViewModel model)
    {
      var teacher = await _context.Teachers.FindAsync(id);
      if(teacher is null)
      {
        throw new Exception($"There is no teacher with id {id}");
      }

      teacher.FirstName = model.FirstName;
      teacher.LastName = model.LastName;
      teacher.Email = model.Email;
      teacher.Phone = model.Phone;
      teacher.Address = model.Address;
      // teacher.Skills = model.Skills; 

      _context.Teachers.Update(teacher);
    }

    public async Task DeleteTeacherAsync(int id)
    {
      var response = await _context.Teachers.FindAsync(id);
      if (response is not null)
      {
        _context.Teachers.Remove(response);
      }
    }    

    public async Task<bool> SaveAllAsync()
    {
      return await _context.SaveChangesAsync() > 0;
    }    
  }
}