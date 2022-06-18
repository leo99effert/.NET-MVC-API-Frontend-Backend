using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVC_App.Models;
using MVC_App.ViewModels;

namespace MVC_App.Controllers
{
  [Route("[controller]")]
  public class CoursesController : Controller
  {
    private readonly IConfiguration _config;
    public CoursesController(IConfiguration config)
    {
      _config = config;
    }

    [HttpGet()]
    public async Task<IActionResult> Index()
    {
      try
      {
        var courseService = new CourseServiceModel(_config);

        var courses = await courseService.ListAllCourses();
        return View("Index", courses);
      }
      catch (System.Exception)
      {
        throw;
      }
    }

    [HttpGet("Details/{id}")]
    public async Task<IActionResult> Details(int id)
    {
      try
      {
        var courseService = new CourseServiceModel(_config);
        var course = await courseService.FindCourse(id);
        return View("Details", course);
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