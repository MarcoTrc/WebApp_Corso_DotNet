using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models.ValueTypes;
using WebApp.Models.ViewModels;

namespace WebApp.Models.Services.Application
{
    public class CourseService
    {
        public List<CourseViewModel> GetServices()
        {
            var courseList = new List<CourseViewModel>();
            var rand = new Random();
            for (int i = 1; i <= 20; i++)
            {
                var price = Convert.ToDecimal(rand.NextDouble() * 10 + 10);
                var course = new CourseViewModel
                {
                    Id = i,
                    Title = $"Corso {i}",
                    CurrentPrice = new Money(Currency.EUR, price),
                    FullPrice = new Money(Currency.EUR, rand.NextDouble() > 0.5 ? price : price - 1),
                    Author = "Nome Cognome",
                    Rating = rand.NextDouble() * 5.0,
                    ImagePath = "~/doctor_placeholder.jpg"
                };
                courseList.Add(course);
            }
            return courseList;
        }
    }
}