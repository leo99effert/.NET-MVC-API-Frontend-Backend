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
    private readonly CourseContext _context; // This is for communication with the database
    private readonly ICourseRepository _courseRepo; // This is for communication with the repository
    private readonly IMapper _mapper;
    public CoursesController(CourseContext context, ICourseRepository courseRepo, IMapper mapper)
    {
      _mapper = mapper;
      _courseRepo = courseRepo;
      _context = context;
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

    [HttpPost()]
    public async Task<ActionResult<Course>> AddCourse(PostCourseViewModel course) // A method that adds a course
    {
      var courseToAdd = _mapper.Map<Course>(course);
      // Contact Database with the data, place the new course in ChangeTracking
      await _context.Courses.AddAsync(courseToAdd);
      // Save Course, make all changes recorded in ChangeTracking
      await _context.SaveChangesAsync();
      // return StatusCode and course
      return StatusCode(201, courseToAdd); // StatusCode(201) == Created

    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateCourse(int id, Course model) // A method that updates the whole course
    {
      // Step 1. Get the course from the database
      var response = await _context.Courses.FindAsync(id);
      // Step 2. Check if a course has been found
      if (response is null)
        return StatusCode(404, $"There is no course with id {id}"); // StatusCode(404) == NotFound
      // Step 3 update the course values
      response.Title = model.Title;
      response.Length = model.Length;
      response.Category = model.Category;
      response.Description = model.Description;
      response.Details = model.Details;
      // Step 4. Send the new values to the database, places the new version in EF ChangeTracking
      _context.Courses.Update(response);
      // Step 5. Save all changes recorded in EF ChangeTracking
      await _context.SaveChangesAsync();
      // Step 6. Return StatusCode
      return StatusCode(204); // StatusCode(204) == NoContent
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCourse(int id) // A method that deletes a course
    {
       _courseRepo.DeleteCourse(id);     

      if(await _courseRepo.SaveAllAsync())
      {
        return StatusCode(204); // StatusCode(204) == NoContent
      }

      return StatusCode(500, "There was an error"); // StatusCode(500) == Internal Server Error       
    }
  }
}