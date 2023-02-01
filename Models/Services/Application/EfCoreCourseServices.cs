using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyCourse.Models.ViewModels;
using webApp.Models.Services.Infrastructure;
using WebApp.Models.Services.Application;
using WebApp.Models.ViewModels;

namespace webApp.Models.Services.Application
{
    public class EfCoreCourseService : ICourseService
    {
        private readonly WebAppDbContext dbContext;
        //creo un costruttore per esprimere la dipendenza di questo servizio
        //applicativo dal servizio infrastrutturale WebAppDbContext
        public EfCoreCourseService(WebAppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<CourseDetailViewModel> GetCourseAsync(int id)
        {
           CourseDetailViewModel courseDetail = await dbContext.Courses
            .Where(course => course.Id == id)
            .Select(course => new CourseDetailViewModel
            {
                Id = (int)course.Id,
                Title = course.Title,
                Description = course.Description,
                ImagePath = course.ImagePath,
                Author = course.Author,
                Rating = course.Rating,
                CurrentPrice = course.CurrentPrice,
                FullPrice = course.FullPrice,
                Lessons = course.Lessons.Select(lesson => new LessonViewModel
                {
                    Id = (int)lesson.Id,
                    Title = lesson.Title,
                    Description = lesson.Description,
                    Duration = lesson.Duration
                })
                .ToList()
            })
            .SingleAsync();
            return courseDetail;
        }

        public async Task<List<CourseViewModel>> GetCoursesAsync()
        {
            IQueryable<CourseViewModel> queryLinq = dbContext.Courses
            .Select(course => new CourseViewModel
            {
                Id = (int)course.Id,
                Title = course.Title,
                ImagePath = course.ImagePath,
                Author = course.Author,
                Rating = course.Rating,
                CurrentPrice = course.CurrentPrice,
                FullPrice = course.FullPrice
            });
            
            List<CourseViewModel> courses = await queryLinq.ToListAsync();

            return courses;
        }
    }
}