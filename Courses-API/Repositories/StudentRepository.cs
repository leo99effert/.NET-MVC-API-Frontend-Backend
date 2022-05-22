using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Courses_API.Data;
using Courses_API.Interfaces;
using Courses_API.Models;
using Courses_API.ViewModels;
using Courses_API.ViewModels.Student;
using Microsoft.EntityFrameworkCore;

namespace Courses_API.Repositories
{
    public class StudentRepository : IStudentRepository
    {
    private readonly CourseContext _context;
    private readonly IMapper _mapper;
    public StudentRepository(CourseContext context, IMapper mapper)
    {
      _mapper = mapper;
      _context = context;
    }

    public async Task<List<StudentViewModel>> ListAllStudentsAsync()
    {
      return await _context.Students.ProjectTo<StudentViewModel>(_mapper.ConfigurationProvider).ToListAsync();
    }

    public async Task<StudentViewModel?> GetStudentAsync(int id)
    {
      return await _context.Students.Where(c => c.Id == id)
        .ProjectTo<StudentViewModel>(_mapper.ConfigurationProvider)
        .SingleOrDefaultAsync();
    }

    public async Task AddStudentAsync(PostStudentViewModel model)
    {
      var student = _mapper.Map<Student>(model);
      await _context.Students.AddAsync(student);
    }

    public async Task UpdateStudentAsync(int id, PostStudentViewModel model)
    {
      var student = await _context.Students.FindAsync(id);
      if(student is null)
      {
        throw new Exception($"There is no student with id {id}");
      }

      student.FirstName = model.FirstName;
      student.LastName = model.LastName;
      student.Email = model.Email;
      student.Phone = model.Phone;
      student.Address = model.Address;
      // student.Courses = model.Courses; 

      _context.Students.Update(student);
    }

    public async Task DeleteStudentAsync(int id)
    {
      var response = await _context.Students.FindAsync(id);
      if (response is not null)
      {
        _context.Students.Remove(response);
      }
    }   

    public async Task<bool> SaveAllAsync()
    {
      return await _context.SaveChangesAsync() > 0;
    }   
  }
}