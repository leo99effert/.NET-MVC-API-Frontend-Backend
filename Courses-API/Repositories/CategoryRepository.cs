using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Courses_API.Data;
using Courses_API.Models;
using Courses_API.Interfaces;
using Courses_API.ViewModels.Category;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Courses_API.ViewModels;
using Courses_API.ViewModels.Course;

namespace Courses_API.Repositories
{
  public class CategoryRepository : ICategoryRepository
  {
    private readonly CourseContext _context;
    private readonly IMapper _mapper;
    public CategoryRepository(CourseContext context, IMapper mapper)
    {
      _mapper = mapper;
      _context = context;
    }

    public async Task<List<CategoryViewModel>> ListCategoryAsync()
    {
      return await _context.Categories.ProjectTo<CategoryViewModel>(_mapper.ConfigurationProvider).ToListAsync();
    }

    public async Task<CategoryViewModel?> GetCategoryAsync(int id)
    {
      return await _context.Categories.Where(c => c.Id == id)
        .ProjectTo<CategoryViewModel>(_mapper.ConfigurationProvider)
        .SingleOrDefaultAsync();
    }

    public async Task<List<CategoryWithCoursesViewModel>> ListCategoriesCoursesAsync()
    {
      return await _context.Categories.Include(c => c.Courses)
        .Select(m => new CategoryWithCoursesViewModel
        {
          CategoryId = m.Id,
          Name = m.Name,
          Courses = m.Courses.Select(k => new CourseInCategoryListViewModel
          {
            Id = k.Id,
            Title = k.Title,
            Length = k.Length,
            // Category = k.Category.Name,     // Removed to fit requested list view
            // Description = k.Description, 
            // Details = k.Details
          }).ToList()
        }).ToListAsync();
    }

    public async Task<CategoryWithCoursesViewModel?> ListCategoriesCoursesAsync(int id)
    {
      return await _context.Categories.Where(c => c.Id == id).Include(c => c.Courses)
        .Select(m => new CategoryWithCoursesViewModel
        {
          CategoryId = m.Id,
          Name = m.Name,
          Courses = m.Courses.Select(k => new CourseInCategoryListViewModel
          {
            Id = k.Id,
            Title = k.Title,
            Length = k.Length,
            // Category = k.Category.Name,      // Removed to fit requested list view
            // Description = k.Description,
            // Details = k.Details
          }).ToList()
        }).SingleOrDefaultAsync();
    }
    
    public async Task AddCategoryAsync(PostCategoryViewModel model)
    {
        var category = _mapper.Map<Category>(model);
        await _context.Categories.AddAsync(category);
    }

    public async Task UpdateCategoryAsync(int id, PostCategoryViewModel model)
    {
      var category = await _context.Categories.FindAsync(id);
      if(category is null)
      {
        throw new Exception($"There is no category with id {id}");
      }

      category.Name = model.Name;

      _context.Categories.Update(category);
    }

    public async Task DeleteCategoryAsync(int id)
    {
      var response = await _context.Categories.FindAsync(id);
      if (response is not null)
      {
        _context.Categories.Remove(response);
      }
    }

    public async Task<bool> SaveAllAsync()
    {
      return await _context.SaveChangesAsync() > 0;
    }

  }
}