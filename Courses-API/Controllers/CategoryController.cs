using Courses_API.Interfaces;
using Courses_API.ViewModels.Category;
using Microsoft.AspNetCore.Mvc;

namespace Courses_API.Controllers
{
  [ApiController]
    [Route("api/v1/category")]
    public class CategoryController : ControllerBase
    {
    private readonly ICategoryRepository _repo;
    public CategoryController(ICategoryRepository repo)
    {
      _repo = repo;
    }

    [HttpGet()]
        public async Task<ActionResult> ListAllCategories()
        {
            var list = await _repo.ListCategoryAsync();
            return StatusCode(200, list); // StatusCode(200) == Ok
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryViewModel>> GetCourseById(int id) 
        {
        try
        {      
            var response = await _repo.GetCategoryAsync(id);
            if (response is null)
            return StatusCode(404, $"There is no category with id {id}"); // StatusCode(404) == NotFound

            return StatusCode(200, response); // StatusCode(200) == Ok
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message); // StatusCode(500) == Internal Server Error
        }
        }

        [HttpGet("Courses")]
        public async Task<ActionResult> ListCategoriesAndCourses()
        {
            return StatusCode(200, await _repo.ListCategoriesCoursesAsync()); // StatusCode(200) == Ok
        }

        [HttpGet("{id}/Courses")]
        public async Task<ActionResult> ListCategoryCourses(int id)
        {
            var result = await _repo.ListCategoriesCoursesAsync(id);
            if(result is null) 
                return StatusCode(404, $"There is no course with id {id}"); // StatusCode(404) == NotFound
            return StatusCode(200, result); // StatusCode(200) == Ok
        }

        [HttpPost()]
        public async Task<ActionResult> AddCategory(PostCategoryViewModel model)
        {
            await _repo.AddCategoryAsync(model);

            if(await _repo.SaveAllAsync())
            {
                return StatusCode(201); // StatusCode(201) == Created
            }
            return StatusCode(500, "There was an error while saving"); // StatusCode(500) == Internal Server Error
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCategory(int id, PostCategoryViewModel model)
        {
            try
            {
                await _repo.UpdateCategoryAsync(id, model);
                if(await _repo.SaveAllAsync())
                {
                return StatusCode(204); // StatusCode(204) == NoContent
                }
                return StatusCode(500, "Error while trying to update"); // StatusCode(500) == Internal Server Error
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); // StatusCode(500) == Internal Server Error
            } 
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            await _repo.DeleteCategoryAsync(id);     

            if(await _repo.SaveAllAsync())
            {
                return StatusCode(204); // StatusCode(204) == NoContent
            }

            return StatusCode(500, "There was an error"); // StatusCode(500) == Internal Server Error
        }
    }
}