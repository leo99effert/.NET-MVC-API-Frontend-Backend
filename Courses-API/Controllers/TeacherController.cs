using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Courses_API.Interfaces;
using Courses_API.ViewModels.Teacher;
using Microsoft.AspNetCore.Mvc;

namespace Courses_API.Controllers
{
    [ApiController]
    [Route("api/v1/teachers")]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherRepository _repo; 
        private readonly IMapper _mapper;
        public TeacherController(ITeacherRepository repo, IMapper mapper)
        {
        _mapper = mapper;
        _repo = repo;
        }

        [HttpGet()]
        public async Task<ActionResult<List<TeacherViewModel>>> ListTeachers() 
        {     
            var list = await _repo.ListAllTeachersAsync();
            return StatusCode(200, list); // StatusCode(200) == Ok()
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TeacherViewModel>> GetTeacherById(int id) 
        {
            try
            {      
                var response = await _repo.GetTeacherAsync(id);
                if (response is null)
                return StatusCode(404, $"There is no teacher with id {id}"); // StatusCode(404) == NotFound

                return StatusCode(200, response); // StatusCode(200) == Ok
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); // StatusCode(500) == Internal Server Error
            }
        }

        [HttpPost()]
        public async Task<ActionResult> AddTeacher(PostTeacherViewModel model) 
        {
            try
            {
                await _repo.AddTeacherAsync(model);
                if(await _repo.SaveAllAsync())
                {
                return StatusCode(201); // StatusCode(201) == Created
                }
                return StatusCode(500, "The teacher was NOT saved successfully."); // StatusCode(500) == Internal Server Error   
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); // StatusCode(500) == Internal Server Error
            }       
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTeacher(int id, PostTeacherViewModel model) 
        {
            try
            {
                await _repo.UpdateTeacherAsync(id, model);
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
        public async Task<ActionResult> DeleteTeacher(int id) 
        {
            await _repo.DeleteTeacherAsync(id);     

            if(await _repo.SaveAllAsync())
            {
                return StatusCode(204); // StatusCode(204) == NoContent
            }

            return StatusCode(500, "There was an error"); // StatusCode(500) == Internal Server Error       
        }
    }
}