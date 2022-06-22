using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC_App.Models;

namespace MVC_App.Controllers
{
  [Route("[controller]")]
  public class AuthenticationController : Controller
  {
    private readonly IConfiguration _config;
    public AuthenticationController(IConfiguration config)
    {
      _config = config;
    }

    [HttpGet()]
    public IActionResult Index()
    {
      return View();
    }
  }
}