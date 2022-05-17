using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Courses_API.Models;
using Courses_API.ViewModels;
using Courses_API.ViewModels.Category;

namespace Courses_API.Helpers
{
  public class AutoMapperProfiles : Profile
  {
    public AutoMapperProfiles()
    {
        // Map Where and from
        CreateMap<PostCourseViewModel, Course>();
        CreateMap<Course, CourseViewModel>();

        // CreateMap<Course, CourseViewModel>()
        //      .ForMember(dest => dest.MyDestinationProperty, options =>
        //            options.MapFrom(src => src.MySourceProperty))
        //      .ForMember(dest => dest.MyDestinatinProperty, options =>
        //            options.MapFrom(src => string.Concat(src.MySourceProperty, " ", src.MySourcePropperty)));

        CreateMap<PostCategoryViewModel, CategoryViewModel>();
        CreateMap<Category, CategoryViewModel>();
    }
  }
}