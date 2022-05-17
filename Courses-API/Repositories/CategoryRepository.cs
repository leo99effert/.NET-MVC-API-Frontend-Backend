using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Courses_API.Data;
using Courses_API.Models;
using Courses_API.Interfaces;
using Courses_API.ViewModels.Category;

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

    public async Task AddCategoryAsync(PostCategoryViewModel model)
    {
        var category = _mapper.Map<Category>(model);
        await _context.Categories.AddAsync(category);
    }

    public Task<List<CategoryViewModel>> ListCategoryAsync()
    {
      throw new NotImplementedException();
    }
  }
}