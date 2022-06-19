using Microsoft.AspNetCore.Mvc;
using MVC_App.Models;

namespace MVC_App.Controllers
{
  [Route("[controller]")]
  public class CategoryController : Controller
  {
    private readonly IConfiguration _config;
    public CategoryController(IConfiguration config)
    {
      _config = config;
    }

    [HttpGet()]
    public async Task<IActionResult> Index()
    {
      try
      {
        var categoryService = new CategoryServiceModel(_config);

        var categories = await categoryService.ListAllCategories();
        return View("Index", categories);
      }
      catch (System.Exception)
      {
        throw;
      }
    }

    [HttpGet()]
    [Route("courses")]
    public async Task<IActionResult> Details(int id)
    {
      try
      {
        var courseService = new CategoryServiceModel(_config);
        var courses = await courseService.FindCategory(id);
        return View("Details", courses);
      }
      catch (Exception ex)
      {       
        // Return View("Error", errorObject);
        // (or ViewBag.ErrorMessage)
        Console.WriteLine(ex.Message);
        return View("Error");
      }     
    }
  }
}