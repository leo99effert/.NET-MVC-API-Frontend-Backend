using Courses_API.Data;
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
    public CoursesController(CourseContext context)
    {
      _context = context;
    }

    [HttpGet()]
    public async Task<ActionResult<List<CourseViewModel>>> ListCourses() // A method that gets all courses
    {
      var response = await _context.Courses.ToListAsync();
      var courseList = new List<CourseViewModel>();
      foreach (var course in response)
      {
        courseList.Add
        (
          new CourseViewModel
          {
            Id = course.Id,
            Title = course.Title,
            Length = course.Length,
            Category = course.Category,
            Description = course.Description,
            Details = course.Details
          }
        );
      }
      return StatusCode(200, courseList); // StatusCode(200) == Ok()
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<CourseViewModel>> GetCourseById(int id) // A method that gets a course by Id
    {
      var response = await _context.Courses.FindAsync(id);

      if (response is null)
        return StatusCode(404, $"There is no course with id {id}"); // StatusCode(404) == NotFound

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

    [HttpGet("bytitle/{title}")]
    public async Task<ActionResult<CourseViewModel>> GetCourseByTitle(string title) // A method that gets a course by Title
    {
      var response = await _context.Courses.SingleOrDefaultAsync(c => c.Title!.ToLower() == title.ToLower());

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
      var courseToAdd = new Course
      {
        Title = course.Title,
        Length = course.Length,
        Category = course.Category,
        Description = course.Description,
        Details = course.Details
      };
      // Step 1: Contact Database with the data, place the new course in ChangeTracking
      await _context.Courses.AddAsync(courseToAdd);
      // Step 2: Save Course, make all changes recorded in ChangeTracking
      await _context.SaveChangesAsync();
      // Step 3: return StatusCode and course
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
      var response = await _context.Courses.FindAsync(id);
      if (response is null)
        return StatusCode(404, $"There is no course with id {id}"); // StatusCode(404) == NotFound
      _context.Courses.Remove(response);
      await _context.SaveChangesAsync();
      return StatusCode(204); // StatusCode(204) == NoContent
    }
  }
}