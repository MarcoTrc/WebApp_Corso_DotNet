// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using MyCourse.Models.ViewModels;
// using WebApp.Models.ValueTypes;
// using WebApp.Models.ViewModels;

// namespace WebApp.Models.Services.Application
// {
//     // 8. implementiamo l'interfaccia ICourseService
//     public class CourseService : ICourseService
//     {
//         public List<CourseViewModel> GetCourses()
//         {
//             var courseList = new List<CourseViewModel>();
//             var rand = new Random();
//             for (int i = 1; i <= 20; i++)
//             {
//                 var price = Convert.ToDecimal(rand.NextDouble() * 10 + 10);
//                 var course = new CourseViewModel
//                 {
//                     Id = i,
//                     Title = $"Corso {i}",
//                     CurrentPrice = new Money(Currency.EUR, price),
//                     FullPrice = new Money(Currency.EUR, rand.NextDouble() > 0.5 ? price : price - 1),
//                     Author = "Nome Cognome",
//                     Rating = rand.NextDouble() * 5.0,
//                     ImagePath = "~/doctor_placeholder.jpg"
//                 };
//                 courseList.Add(course);
//             }
//             return courseList;
//         }

//         public CourseDetailViewModel GetCourse(int id)
//         {
//             var rand = new Random();
//             var price = Convert.ToDecimal(rand.NextDouble() * 10 + 10);
//             var course = new CourseDetailViewModel
//             {
//                 Id = id,
//                 Title = $"Corso {id}",
//                 CurrentPrice = new Money(Currency.EUR, price),
//                 FullPrice = new Money(Currency.EUR, rand.NextDouble() > 0.5 ? price : price - 1),
//                 Author = "Nome Cognome",
//                 Rating = rand.Next(10, 50) / 10.0,
//                 ImagePath = "https://curtinvet.com.au/wp-content/uploads/2022/03/person-placeholder-female.jpg",
//                 Description = $"Descrizione {id}",
//                 Lessons = new List<LessonViewModel>()
//             };
            
//             for(var i = 1; i<= 5; i++)
//             {
//                 var lesson = new LessonViewModel
//                 {
//                     Title = $"Lezione {i}",
//                     Duration = TimeSpan.FromSeconds(rand.Next(40, 90))
//                 };
//                 course.Lessons.Add(lesson);
//             }
//             return course; 
//         }
//     }
// }
