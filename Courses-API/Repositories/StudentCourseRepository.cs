using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Courses_API.Data;
using Courses_API.Interfaces;
using Courses_API.Models;
using Courses_API.ViewModels.StudentCourse;
using Microsoft.EntityFrameworkCore;

namespace Courses_API.Repositories
{
    public class StudentCourseRepository : IStudentCourseRepository
    {
        private readonly CourseContext _context;
        private readonly IMapper _mapper;
        public StudentCourseRepository(CourseContext context, IMapper mapper)
        {
        _mapper = mapper;
        _context = context;
        }

        public async Task<List<StudentCourseViewModel>> ListStudentCoursesAsync()
        {
          return await _context.StudentCourses.ProjectTo<StudentCourseViewModel>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<List<StudentCourseViewModel>> ListStudentCoursesByStudentIdAsync(int studentId)
        {
          var fullList = await _context.StudentCourses.ProjectTo<StudentCourseViewModel>(_mapper.ConfigurationProvider).ToListAsync();
          var reducedList = fullList.Where(t => t.StudentId == studentId).ToList();
          return reducedList;
        }

        public async Task<StudentCourseViewModel?> GetStudentCourseAsync(int studentId, int courseId)
        {
          return await _context.StudentCourses.Where(c => c.StudentId == studentId && c.CourseId == courseId)
            .ProjectTo<StudentCourseViewModel>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync();
        }  

        public async Task AddStudentCourseAsync(PostStudentCourseViewModel model)
        {
          var student = await _context.Students.FindAsync(model.StudentId);
          var course = await _context.Courses.FindAsync(model.CourseId);

          if (student is not null && course is not null)
          {
            var studentCourse = new StudentCourse 
            {
              StudentId = student.Id,
              CourseId = course.Id,
              Student = student,
              Course = course
            };
            await _context.StudentCourses.AddAsync(studentCourse);
          }  
        }

        public async Task DeleteStudentCourseAsync(int studentId, int courseId)
        {
          var response = await _context.StudentCourses.FindAsync(studentId, courseId);
          if (response is not null)
          {
            _context.StudentCourses.Remove(response);
          }
        } 

        public async Task<bool> SaveAllAsync()
        {
          return await _context.SaveChangesAsync() > 0;
        }
  }
}