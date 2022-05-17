using AutoMapper;
using Courses_API.Data;
using Courses_API.Interfaces;
using Courses_API.Models;
using Courses_API.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Courses_API.Controllers
{
  [ApiController]
  [Route("api/v1/courses")]
  public class CoursesController : ControllerBase
  {
    private readonly ICourseRepository _courseRepo; // This is for communication with the repository
    private readonly IMapper _mapper; // This is for mapping the database models with the ViewModels
    public CoursesController( ICourseRepository courseRepo, IMapper mapper)
    {
      _mapper = mapper;
      _courseRepo = courseRepo;
    }

    [HttpGet()]
    public async Task<ActionResult<List<CourseViewModel>>> ListCourses() // A method that gets all courses
    {     
      var courseList = await _courseRepo.ListAllCoursesAsync();
      return StatusCode(200, courseList); // StatusCode(200) == Ok()
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<CourseViewModel>> GetCourseById(int id) // A method that gets a course by Id
    {
      try
      {      
        var response = await _courseRepo.GetCourseAsync(id);

        if (response is null)
          return StatusCode(404, $"There is no course with id {id}"); // StatusCode(404) == NotFound

        return StatusCode(200, response); // StatusCode(200) == Ok
      }
      catch (Exception ex)
      {
        return StatusCode(500, ex.Message); // StatusCode(500) == Internal Server Error
      }
    }

    [HttpGet("bytitle/{title}")]
    public async Task<ActionResult<CourseViewModel>> GetCourseByTitle(string title) // A method that gets a course by Title
    {
      var response = await _courseRepo.GetCourseAsync(title);

      if (response is null)
        return StatusCode(404, $"There is no course with title {title}");// StatusCode(404) == NotFound

      var course = new CourseViewModel
      {
        Id = response.Id,
        Title = response.Title,
        Length = response.Length,
        Category = response.Category,
        Description = response.Description,
        Details = response.Details
      };

      return StatusCode(200, course); // StatusCode(200) == Ok
    }

    [HttpGet("bycategori/{category}")]
    public async Task<ActionResult<List<CourseViewModel>>> GetCourseByCategory(string category)
    {
      //return StatusCode(200, await _courseRepo.GetCourseByCategoryAsync(category)); // StatusCode(200) == Ok
      return Ok();
    }

    [HttpPost()]
    public async Task<ActionResult> AddCourse(PostCourseViewModel model) // A method that adds a course
    {
      if(await _courseRepo.GetCourseAsync(model.Title!.ToLower()) is not null)
      {
        return StatusCode(400, $"Title {model.Title} is already taken"); // StatusCode(400) == BadRequest
      }

      await _courseRepo.AddCourseAsync(model);
      if(await _courseRepo.SaveAllAsync())
      {
        return StatusCode(201); // StatusCode(201) == Created
      }
      return StatusCode(500, "The course was NOT saved successfully."); // StatusCode(500) == Internal Server Error     
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateCourse(int id, PostCourseViewModel model) // A method that updates whole course
    {
      try
      {
        await _courseRepo.UpdateCourseAsync(id, model);
        if(await _courseRepo.SaveAllAsync())
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

    [HttpPatch("{id}")]
    public async Task<ActionResult> UpdateCourse(int id, PatchCourseViewModel model)
    {
      try
      {
        await _courseRepo.UpdateCourseAsync(id, model);

        if(await _courseRepo.SaveAllAsync())
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
    public async Task<ActionResult> DeleteCourse(int id) // A method that deletes a course
    {
       await _courseRepo.DeleteCourseAsync(id);     

      if(await _courseRepo.SaveAllAsync())
      {
        return StatusCode(204); // StatusCode(204) == NoContent
      }

      return StatusCode(500, "There was an error"); // StatusCode(500) == Internal Server Error       
    }
  }
}