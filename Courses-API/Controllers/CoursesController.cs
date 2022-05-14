using Courses_API.Data;
using Courses_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Courses_API.Controllers
{
  [ApiController]
  [Route("api/v1/courses")]
  public class CoursesController : ControllerBase
  {
    private readonly CourseContext _context;
    public CoursesController(CourseContext context)
    {
      _context = context;
    }

    [HttpGet()]
    public async Task<ActionResult<List<Course>>> ListCourses() // A method that gets all courses
    {
      var response = await _context.Courses.ToListAsync();
      return StatusCode(200, response); // StatusCode(200) == Ok()
    }


    [HttpGet("{id}")]
    public ActionResult GetCourseById(int id) // A method that gets a course by Id
    {
      return StatusCode(200, "{'message': 'It Works here as well!'}"); // StatusCode(200) == Ok
    }

    [HttpPost()]
    public async Task<ActionResult<Course>> AddCourse(Course course) // A method that adds a course
    {
      // Step 1: Contact Database
      await _context.Courses.AddAsync(course);
      // Step 2: Save Course
      await _context.SaveChangesAsync();
      // Step 3: return StatusCode
      return StatusCode(201, course); // StatusCode(201) == Created

    }

    [HttpPut("{id}")]
    public ActionResult UpdateCourse(int id) // A method that updates the whole course
    {
      return StatusCode(204); // StatusCode(204) == NoContent
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteCourse(int id) // A method that deletes a course
    {
      return StatusCode(204); // StatusCode(204) == NoContent
    }
  }
}