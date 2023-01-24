using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models.Services.Application;
using WebApp.Models.ViewModels;


namespace WebApp.Controllers
{
    public class CourseController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Title"] = "Catalogo dei corsi";
            var courseService = new CourseService();
            List<CourseViewModel> courses = courseService.GetCourses();
            return View(courses);
        }
        public IActionResult Detail(int id)
        {
            var courseService = new CourseService();
            CourseDetailViewModel viewModel = courseService.GetCourse(id);
            ViewData["Title"] = viewModel.Title;
            return View(viewModel);
        }
    }
}