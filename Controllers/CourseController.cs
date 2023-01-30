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
        private readonly ICourseService courseService;

        // 3. Per ridurre l'accoppiamento, faremo in modo che sia .NET a fornirci
        // le istanze di courseServices dall'esterno, rimuovendole dalle Actions
        // e creando un costruttore passandogli "courseService" come parametro 


        // public CourseController(CourseService courseService)
        // {
        //     this.courseService = courseService;
        // }

        // In questo modo stiamo esprimendo una dipendenza che il CourseController ha nei confronti di
        // CourseService, cioè non potrebbe funzionare se non viene fornita questa istanza.

        // 4. Per far si che .Net si occupi dell'effettiva creazione delle istanze 
        // sarà necessario spostarsi nel metodo ConfigureServices della classe Startup.cs


        // 7. Per ridurre l'accoppiamento faremo in modo che il Controller non dipenda 
        // più specificatamente dal servizio, ma da un' interfaccia, per tanto
        // andremo a creare l'interfaccia ICourseService, che sarà implementata 
        // dalla classe CourseServices(8.)
        public CourseController(ICourseService courseService)
        
        // 9.Ora naturalmente dovremo agire nuovamente nel metodo ConfigureService
        //della classe Startup per aggiornare la registrazione del servizio (10.)
        {
            this.courseService = courseService;
        }
        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Catalogo dei corsi";

            // 1. quando l'Action Index va in esecuzione viene creata l'istanza 
            // courseServices introducendo forte accoppiamento

            // 2. (eliminare) var courseServices = new CourseService();

            List<CourseViewModel> courses = await courseService.GetCoursesAsync();
            return View(courses);
        }
        public async Task<IActionResult> Detail(int id)
        {

            // 1. quando l'Action Detail va in esecuzione viene creata l'istanza 
            // courseServices introducendo forte accoppiamento

            // 2. (eliminare) var courseServices = new CourseService();

            CourseDetailViewModel viewModel = await courseService.GetCourseAsync(id);
            ViewData["Title"] = viewModel.Title;
            return View(viewModel);
        }
    }
}