using Courses_API.Data;
using Courses_API.Helpers;
using Courses_API.Interfaces;
using Courses_API.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Create Database Connection
builder.Services.AddDbContext<CourseContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("Sqlite")));

// Dependency Injection for our interfaces and classes
// builder.Services.AddScoped<Interface, class that implements interface>
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
