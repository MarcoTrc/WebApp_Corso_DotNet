using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using webApp.Models.Services.Application;
using webApp.Models.Services.Infrastructure;
using WebApp.Models.Services.Application;
using WebApp.Models.Services.Infrastructure;

namespace webApp
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_0);
            //services.AddTransient<CourseService>();
            //services.AddTransient<ICourseService, CourseService>();
            //services.AddTransient<ICourseService, AdoNetCourseService>();
            services.AddTransient<ICourseService, EfCoreCourseService>();
            services.AddTransient<IDatabaseAccessor, SqliteDatabaseAccessor>();
            services.AddDbContext<WebAppDbContext>();
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();

            // app.Run(async (context) =>
            // {
            //     string nome = context.Request.Query["nome"];
            //     await context.Response.WriteAsync($"Hello {nome.ToUpper()}!");
            // });

            app.UseMvc(routeBuilder =>
            {
                routeBuilder.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });

            //app.UseMvcWithDefaultRoute();
        }
    }
}


// Con 'AddTransient' ASP.NET Core crea delle nuove istanze 
// dei servizi ogni volta che servono ai componenti.
// poi, quando sono state utilizzate, le rimuove.

// Con 'AddSoped' ASP.NET Core crea una nuova istanza 
// dei servizi e la riutilizza finchè siamo nel contesto.
// della stessa richiesta HTTP. Al termine della richiesta 
// il Grbage Collector la rimuove dalla memoria.

// Con 'AddSingleton' ASP.NET Core crea un' istanza 
// e la inietta a tutti i componenti che ne hanno bisogno
// anche in richieste HTTP diverse e concorrenti. L'istanza 
// esiste finchè è attiva l'applicazione
