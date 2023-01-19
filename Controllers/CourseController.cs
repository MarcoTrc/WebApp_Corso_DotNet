using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class CourseController : Controller
    {
        public IActionResult Detail(string id)
        {
            return Content($"Hello from detail ho ricevuto l'ID {id}!");
        }

        public IActionResult Search(string title)
        {
            return Content($"Hai cercato {title}");
        }
    }
}