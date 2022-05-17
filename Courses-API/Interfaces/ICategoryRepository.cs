using Courses_API.ViewModels.Category;

namespace Courses_API.Interfaces
{
  public interface ICategoryRepository
    {
        public Task AddCategoryAsync(PostCategoryViewModel model);
        public Task<List<CategoryViewModel>> ListCategoryAsync();
    }
}