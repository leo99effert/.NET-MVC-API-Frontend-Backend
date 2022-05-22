using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Courses_API.Models;
using Courses_API.ViewModels;
using Courses_API.ViewModels.Category;
using Courses_API.ViewModels.Course;
using Courses_API.ViewModels.Skill;
using Courses_API.ViewModels.Student;
using Courses_API.ViewModels.Teacher;

namespace Courses_API.Helpers
{
  public class AutoMapperProfiles : Profile
  {
    public AutoMapperProfiles()
    {
        // Map Where and from
        CreateMap<PostCourseViewModel, Course>().ForMember(x => x.Category, opt => opt.Ignore());
        CreateMap<Course, CourseViewModel>().ForMember(dest => dest.Category, options => 
                                              options.MapFrom(src => src.Category.Name));
        CreateMap<Course, CourseSmallViewModel>().ForMember(dest => dest.Category, options => 
                                              options.MapFrom(src => src.Category.Name));

        // CreateMap<Course, CourseViewModel>()
        //      .ForMember(dest => dest.MyDestinationProperty, options =>
        //            options.MapFrom(src => src.MySourceProperty))
        //      .ForMember(dest => dest.MyDestinatinProperty, options =>
        //            options.MapFrom(src => string.Concat(src.MySourceProperty, " ", src.MySourcePropperty)));

        CreateMap<PostCategoryViewModel, Category>();
        CreateMap<Category, CategoryViewModel>()
          .ForMember(dest => dest.CategoryId, options => options.MapFrom(src => src.Id));

        
        CreateMap<PostTeacherViewModel, Teacher>();
        CreateMap<Teacher, TeacherViewModel>();

        CreateMap<PostStudentViewModel, Student>();
        CreateMap<Student, StudentViewModel>();

        CreateMap<PostSkillViewModel, Skill>();
        CreateMap<Skill, SkillViewModel>();
    }
  }
}