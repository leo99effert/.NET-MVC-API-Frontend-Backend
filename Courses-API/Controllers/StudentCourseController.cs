using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Courses_API.Interfaces;
using Courses_API.ViewModels.StudentCourse;
using Microsoft.AspNetCore.Mvc;

namespace Courses_API.Controllers
{
    [ApiController]
    [Route("api/v1/studentcourses")]
    public class StudentCourseController : ControllerBase
    {
        private readonly IStudentCourseRepository _repo; 
        private readonly IMapper _mapper;
        public StudentCourseController(IStudentCourseRepository repo, IMapper mapper)
        {
        _mapper = mapper;
        _repo = repo;
        }

        [HttpGet()]
        public async Task<ActionResult<List<StudentCourseViewModel>>> ListStudentCourses() 
        {     
            var list = await _repo.ListStudentCoursesAsync();
            return StatusCode(200, list); // StatusCode(200) == Ok()
        }

        [HttpGet("{studentid}")]
        public async Task<ActionResult<List<StudentCourseViewModel>>> ListStudentCourses(int studentId) 
        {     
            var list = await _repo.ListStudentCoursesByStudentIdAsync(studentId);
            return StatusCode(200, list); // StatusCode(200) == Ok()
        }

        [HttpGet("{studentid}/{courseid}")]
        public async Task<ActionResult<StudentCourseViewModel>> GetStudentCourseById(int studentId, int courseId) 
        {
            try
            {      
                var response = await _repo.GetStudentCourseAsync(studentId, courseId);
                if (response is null)
                return StatusCode(404, $"There is no skill with id {studentId}/{courseId}"); // StatusCode(404) == NotFound

                return StatusCode(200, response); // StatusCode(200) == Ok
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); // StatusCode(500) == Internal Server Error
            }
        }

        [HttpPost()]
        public async Task<ActionResult> AddStudentCourse(PostStudentCourseViewModel model) 
        {
            try
            {
                await _repo.AddStudentCourseAsync(model);
                if(await _repo.SaveAllAsync())
                {
                return StatusCode(201); // StatusCode(201) == Created
                }
                return StatusCode(500, "The studentCourse was NOT saved successfully."); // StatusCode(500) == Internal Server Error   
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); // StatusCode(500) == Internal Server Error
            }       
        }

        [HttpDelete("{studentId}/{courseId}")]
        public async Task<ActionResult> DeleteStudentCourse(int studentId, int courseId) 
        {
            await _repo.DeleteStudentCourseAsync(studentId, courseId);     

            if(await _repo.SaveAllAsync())
            {
                return StatusCode(204); // StatusCode(204) == NoContent
            }

            return StatusCode(500, "There was an error"); // StatusCode(500) == Internal Server Error       
        }
    }
}