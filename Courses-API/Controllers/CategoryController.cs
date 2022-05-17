using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Courses_API.Controllers
{
    [ApiController]
    [Route("api/v1/category")]
    public class CategoryController : ControllerBase
    {
        [HttpGet()]
        public async Task<ActionResult> ListAllCategories()
        {
            return StatusCode(200); // StatusCode(200) == Ok
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetCategoryById()
        {
            return StatusCode(200); // StatusCode(200) == Ok
        }

        [HttpPost()]
        public async Task<ActionResult> AddCategory()
        {
            return StatusCode(201); // StatusCode(201) == Created
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCategory()
        {
            return StatusCode(204); // StatusCode(204) == NoContent
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategory()
        {
            return StatusCode(204); // StatusCode(204) == NoContent
        }
    }
}