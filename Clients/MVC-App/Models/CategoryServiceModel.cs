using MVC_APP.ViewModels;
using MVC_APP.ViewModels.Category;
using System.Text.Json;

namespace MVC_App.Models
{
    public class CategoryServiceModel
    {
        private readonly string _baseUrl;
        private readonly JsonSerializerOptions _options;
        private readonly IConfiguration _config;

        public CategoryServiceModel(IConfiguration config)
        {
            _config = config;
            _baseUrl = $"{_config.GetValue<string>("baseUrl")}/category";
            _options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<List<CategoryViewModel>> ListAllCategories()
        {
            var url = $"{_baseUrl}";

            using var http = new HttpClient();
            var response = await http.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("There was an error");
            }

            var categories = await response.Content.ReadFromJsonAsync<List<CategoryViewModel>>();
            // var result = await response.Content.ReadAsStringAsync();
            // var courses = JsonSerializer.Deserialize<List<CourseViewModel>>(result, _options);

            return categories ?? new List<CategoryViewModel>();
        }

        public async Task<List<CourseViewModel>> FindCategory(int id)
        {
            var url = $"https://localhost:7261/api/v1/courses";

            using var http = new HttpClient();
            var response = await http.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("There was an error");
            }

            var courses = await response.Content.ReadFromJsonAsync<List<CourseViewModel>>();
            if (courses == null)
                courses = new List<CourseViewModel>();

            // var result = await response.Content.ReadAsStringAsync();
            // var courses = JsonSerializer.Deserialize<List<CourseViewModel>>(result, _options);

            //   if (id == 1 || id == 2)
            //   {
            //     var categories = await response.Content.ReadFromJsonAsync<List<CategoryViewModel>>();
            //     string chosenCategory = "";
            //     if (categories != null && categories[id - 1].Name != null)
            //       chosenCategory = categories[id - 1].Name!;

            //     var filteredCourses = new List<CourseViewModel>();


            //     foreach (var course in courses)
            //     {
            //       if (course.Category == chosenCategory)
            //       {
            //         filteredCourses.Add(course);
            //       }
            //     }


            //     return filteredCourses ?? new List<CourseViewModel>();
            //   }

            return courses ?? new List<CourseViewModel>();
        }

    }
}