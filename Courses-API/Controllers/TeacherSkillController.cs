using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Courses_API.Interfaces;
using Courses_API.ViewModels.TeacherSkill;
using Microsoft.AspNetCore.Mvc;

namespace Courses_API.Controllers
{
    [ApiController]
    [Route("api/v1/teacherskills")]
    public class TeacherSkillController : ControllerBase
    {
        private readonly ITeacherSkillRepository _repo; 
        private readonly IMapper _mapper;
        public TeacherSkillController(ITeacherSkillRepository repo, IMapper mapper)
        {
        _mapper = mapper;
        _repo = repo;
        }

        [HttpGet()]
        public async Task<ActionResult<List<TeacherSkillViewModel>>> ListTeacherSkills() 
        {     
            var list = await _repo.ListAllTeacherSkillsAsync();
            return StatusCode(200, list); // StatusCode(200) == Ok()
        }

        [HttpGet("{teacherid}")]
        public async Task<ActionResult<List<TeacherSkillViewModel>>> ListTeacherSkills(int teacherId) 
        {     
            var list = await _repo.ListAllTeacherSkillsByTeacherIdAsync(teacherId);
            return StatusCode(200, list); // StatusCode(200) == Ok()
        }

        [HttpGet("{teacherid}/{skillid}")]
        public async Task<ActionResult<TeacherSkillViewModel>> GetTeacherSkillById(int teacherId, int skillId) 
        {
            try
            {      
                var response = await _repo.GetTeacherSkillAsync(teacherId, skillId);
                if (response is null)
                return StatusCode(404, $"There is no skill with id {teacherId}/{skillId}"); // StatusCode(404) == NotFound

                return StatusCode(200, response); // StatusCode(200) == Ok
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); // StatusCode(500) == Internal Server Error
            }
        }

        [HttpPost()]
        public async Task<ActionResult> AddTeacherSkill(PostTeacherSkillViewModel model) 
        {
            try
            {
                await _repo.AddTeacherSkillAsync(model);
                if(await _repo.SaveAllAsync())
                {
                return StatusCode(201); // StatusCode(201) == Created
                }
                return StatusCode(500, "The teacherSkill was NOT saved successfully."); // StatusCode(500) == Internal Server Error   
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); // StatusCode(500) == Internal Server Error
            }       
        }

        [HttpDelete("{teacherId}/{skillId}")]
        public async Task<ActionResult> DeleteTeacherSkill(int teacherId, int skillId) 
        {
            await _repo.DeleteTeacherSkillAsync(teacherId, skillId);     

            if(await _repo.SaveAllAsync())
            {
                return StatusCode(204); // StatusCode(204) == NoContent
            }

            return StatusCode(500, "There was an error"); // StatusCode(500) == Internal Server Error       
        }
    }
}