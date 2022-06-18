using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using MVC_App.ViewModels;

namespace MVC_App.Models
{
  public class CourseServiceModel
  {
    private readonly string _baseUrl;
    private readonly JsonSerializerOptions _options;
    private readonly IConfiguration _config;

    public CourseServiceModel(IConfiguration config)
    {
      _config = config;
      _baseUrl = $"{_config.GetValue<string>("baseUrl")}/courses";
      _options = new JsonSerializerOptions
      {
        PropertyNameCaseInsensitive = true
      };
    }

    public async Task<List<CourseViewModel>> ListAllCourses()
    {
      var url = $"{_baseUrl}";

      using var http = new HttpClient();
      var response = await http.GetAsync(url);

      if (!response.IsSuccessStatusCode)
      {
        throw new Exception("There was an error");
      }

      var courses = await response.Content.ReadFromJsonAsync<List<CourseViewModel>>();
      // var result = await response.Content.ReadAsStringAsync();
      // var courses = JsonSerializer.Deserialize<List<CourseViewModel>>(result, _options);

      return courses ?? new List<CourseViewModel>();
    }

    public async Task<CourseViewModel> FindCourse(int id)
    {
      var baseUrl = _config.GetValue<string>($"baseUrl");
      var url = $"{baseUrl}/courses/{id}";
      using var http = new HttpClient();
      var response = await http.GetAsync(url);

      if(!response.IsSuccessStatusCode)
      {
        Console.WriteLine("Course was not found");
      }

      var course = await response.Content.ReadFromJsonAsync<CourseViewModel>();

      return course ?? new CourseViewModel();
    }
  }
}