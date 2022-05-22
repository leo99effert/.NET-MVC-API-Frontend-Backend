using AutoMapper;
using Courses_API.Data;
using Courses_API.Interfaces;
using Courses_API.Models;
using Courses_API.ViewModels;
using Courses_API.ViewModels.Course;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Courses_API.Controllers
{
  [ApiController]
  [Route("api/v1/courses")]
  public class CoursesController : ControllerBase
  {
    private readonly ICourseRepository _courseRepo; 
    private readonly IMapper _mapper;
    public CoursesController(ICourseRepository courseRepo, IMapper mapper)
    {
      _mapper = mapper;
      _courseRepo = courseRepo;
    }

    [HttpGet()]
    public async Task<ActionResult<List<CourseViewModel>>> ListCourses() 
    {     
      var courseList = await _courseRepo.ListAllCoursesAsync();
      return StatusCode(200, courseList); // StatusCode(200) == Ok()
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<CourseViewModel>> GetCourseById(int id) 
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

    [HttpGet("smallview/{id}")]        //  single course view with limited information as requested
    public async Task<ActionResult<CourseSmallViewModel>> GetSmallCourseById(int id) 
    {
      try
      {      
        var response = await _courseRepo.GetSmallCourseAsync(id);
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
    public async Task<ActionResult<CourseViewModel>> GetCourseByTitle(string title)
    {
      try
      {
        var response = await _courseRepo.GetCourseAsync(title);
        if (response is null)
          return StatusCode(404, $"There is no course with title {title}");// StatusCode(404) == NotFound
        
        return StatusCode(200, response); // StatusCode(200) == Ok
      }
      catch (Exception ex)
      {
        return StatusCode(500, ex.Message); // StatusCode(500) == Internal Server Error
      }
      

      
    }

    [HttpPost()]
    public async Task<ActionResult> AddCourse(PostCourseViewModel model) 
    {
      try
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
      catch (Exception ex)
      {
        return StatusCode(500, ex.Message); // StatusCode(500) == Internal Server Error
      }       
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateCourse(int id, PostCourseViewModel model) 
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
    public async Task<ActionResult> DeleteCourse(int id) 
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