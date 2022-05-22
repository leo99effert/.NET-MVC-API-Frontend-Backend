using AutoMapper;
using AutoMapper.QueryableExtensions;
using Courses_API.Data;
using Courses_API.Interfaces;
using Courses_API.Models;
using Courses_API.ViewModels;
using Courses_API.ViewModels.Course;
using Microsoft.EntityFrameworkCore;

namespace Courses_API.Repositories
{
  public class CourseRepository : ICourseRepository
  {
    private readonly CourseContext _context;
    private readonly IMapper _mapper;
    public CourseRepository(CourseContext context, IMapper mapper)
    {
      _mapper = mapper;
      _context = context;
    }

    public async Task<List<CourseViewModel>> ListAllCoursesAsync()
    {
      return await _context.Courses.ProjectTo<CourseViewModel>(_mapper.ConfigurationProvider).ToListAsync();
    }

    public async Task<CourseViewModel?> GetCourseAsync(int id)
    {
      return await _context.Courses.Where(c => c.Id == id)
        .ProjectTo<CourseViewModel>(_mapper.ConfigurationProvider)
        .SingleOrDefaultAsync();
    }

    public async Task<CourseSmallViewModel?> GetSmallCourseAsync(int id)
    {
      return await _context.Courses.Where(c => c.Id == id)
        .ProjectTo<CourseSmallViewModel>(_mapper.ConfigurationProvider)
        .SingleOrDefaultAsync();
    }

    public async Task<CourseViewModel?> GetCourseAsync(string title)
    {
      return await _context.Courses.Where(c => c.Title!.ToLower() == title.ToLower())
        .ProjectTo<CourseViewModel>(_mapper.ConfigurationProvider)
        .SingleOrDefaultAsync();
    }

    public async Task AddCourseAsync(PostCourseViewModel model)
    {
      var category = await _context.Categories
                          .Include(c => c.Courses).Where(c => c.Name!.ToLower() == model.Category!.ToLower())
                          .SingleOrDefaultAsync();

      if(category is null)
      {
        throw new Exception($"We dont have the category {model.Category}");
      }

      var courseToAdd = _mapper.Map<Course>(model);
      courseToAdd.Category = category;
      await _context.Courses.AddAsync(courseToAdd);
    }

    public async Task UpdateCourseAsync(int id, PostCourseViewModel model)
    {
      var course = await _context.Courses.FindAsync(id);
      if(course is null)
      {
        throw new Exception($"There is no course with id {id}");
      }

      var category = await _context.Categories
                          .Include(c => c.Courses).Where(c => c.Name!.ToLower() == model.Category!.ToLower())
                          .SingleOrDefaultAsync();
      if(category is null)
      {
        throw new Exception($"We dont have the category {model.Category}");
      }

      course.Title = model.Title;
      course.Length = model.Length;
      course.Category = category;
      course.Description = model.Description;
      course.Details = model.Details;

      _context.Courses.Update(course);
    }

    public async Task UpdateCourseAsync(int id, PatchCourseViewModel model)
    {
      var course = await _context.Courses.FindAsync(id);
      if(course is null)
      {
        throw new Exception($"There is no course with id {id}");
      }
      
      course.Length = model.Length;
      course.Description = model.Description;
      course.Details = model.Details;

      _context.Courses.Update(course);
    }

    public async Task DeleteCourseAsync(int id)
    {
      var response = await _context.Courses.FindAsync(id);
      if (response is not null)
      {
        _context.Courses.Remove(response);
      }
    }

    public async Task<bool> SaveAllAsync()
    {
      return await _context.SaveChangesAsync() > 0;
    }
  }
}