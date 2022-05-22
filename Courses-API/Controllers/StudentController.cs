using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Courses_API.Interfaces;
using Courses_API.ViewModels.Student;
using Microsoft.AspNetCore.Mvc;

namespace Courses_API.Controllers
{
    [ApiController]
    [Route("api/v1/students")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository _repo; 
        private readonly IMapper _mapper;
        public StudentController(IStudentRepository repo, IMapper mapper)
        {
        _mapper = mapper;
        _repo = repo;
        }       

        [HttpGet()]
        public async Task<ActionResult<List<StudentViewModel>>> ListStudent() 
        {     
            var list = await _repo.ListAllStudentsAsync();
            return StatusCode(200, list); // StatusCode(200) == Ok()
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StudentViewModel>> GetStudentById(int id) 
        {
            try
            {      
                var response = await _repo.GetStudentAsync(id);
                if (response is null)
                return StatusCode(404, $"There is no student with id {id}"); // StatusCode(404) == NotFound

                return StatusCode(200, response); // StatusCode(200) == Ok
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); // StatusCode(500) == Internal Server Error
            }
        }

        [HttpPost()]
        public async Task<ActionResult> AddStudent(PostStudentViewModel model) 
        {
            try
            {
                await _repo.AddStudentAsync(model);
                if(await _repo.SaveAllAsync())
                {
                return StatusCode(201); // StatusCode(201) == Created
                }
                return StatusCode(500, "The student was NOT saved successfully."); // StatusCode(500) == Internal Server Error   
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); // StatusCode(500) == Internal Server Error
            }       
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateStudent(int id, PostStudentViewModel model) 
        {
            try
            {
                await _repo.UpdateStudentAsync(id, model);
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
        public async Task<ActionResult> DeleteStudent(int id) 
        {
            await _repo.DeleteStudentAsync(id);     

            if(await _repo.SaveAllAsync())
            {
                return StatusCode(204); // StatusCode(204) == NoContent
            }

            return StatusCode(500, "There was an error"); // StatusCode(500) == Internal Server Error       
        }
    }
}