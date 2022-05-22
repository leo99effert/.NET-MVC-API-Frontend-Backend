using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Courses_API.Interfaces;
using Courses_API.ViewModels.Skill;
using Microsoft.AspNetCore.Mvc;

namespace Courses_API.Controllers
{
    [ApiController]
    [Route("api/v1/skills")]
    public class SkillController : ControllerBase
    {
        private readonly ISkillRepository _repo; 
        private readonly IMapper _mapper;
        public SkillController(ISkillRepository repo, IMapper mapper)
        {
        _mapper = mapper;
        _repo = repo;
        }

        [HttpGet()]
        public async Task<ActionResult<List<SkillViewModel>>> ListSkills() 
        {     
            var list = await _repo.ListAllSkillsAsync();
            return StatusCode(200, list); // StatusCode(200) == Ok()
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SkillViewModel>> GetSkillById(int id) 
        {
            try
            {      
                var response = await _repo.GetSkillAsync(id);
                if (response is null)
                return StatusCode(404, $"There is no skill with id {id}"); // StatusCode(404) == NotFound

                return StatusCode(200, response); // StatusCode(200) == Ok
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); // StatusCode(500) == Internal Server Error
            }
        }

        [HttpPost()]
        public async Task<ActionResult> AddTeacher(PostSkillViewModel model) 
        {
            try
            {
                await _repo.AddSkillAsync(model);
                if(await _repo.SaveAllAsync())
                {
                return StatusCode(201); // StatusCode(201) == Created
                }
                return StatusCode(500, "The skill was NOT saved successfully."); // StatusCode(500) == Internal Server Error   
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); // StatusCode(500) == Internal Server Error
            }       
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateSkill(int id, PostSkillViewModel model) 
        {
            try
            {
                await _repo.UpdateSkillAsync(id, model);
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
        public async Task<ActionResult> DeleteSkill(int id) 
        {
            await _repo.DeleteSkillAsync(id);     

            if(await _repo.SaveAllAsync())
            {
                return StatusCode(204); // StatusCode(204) == NoContent
            }

            return StatusCode(500, "There was an error"); // StatusCode(500) == Internal Server Error       
        }
    }
}