using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
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

            // 5. services.AddTransient<CourseService>();

            // in questo modo stiamo dicendo ad ASP.NET Core che deve prepararsi
            // alla gestione di oggetti di tipo "CourseService", per cui quando incontra 
            // un componente come il nostro controller, che ha una dipendenza 
            // da un "CourseService", è lui stesso che deve costruirlo e passarlo.


            // 6. Ci siamo registrati il nostro primo servizio, il "CourseService", e grazie alla Dependency
            // Injection ASP.NET Core può quindi crearne un'istanza e iniettarla, attraverso il costruttore, all'interno
            // dei componenti che ne hanno bisogno per funzionare come il Courses Controller.


            // Non potendo creare un istanza di IcourseService dal momento che non
            // possiede una propria logica implementativa, al metodo AddTrannsient
            // passiamo sia l'interfaccia che l'effettiva classe in cui è definita
            // la logica applicativa.

            // 10. services.AddTransient<ICourseService, CourseService>();

            // 6db. 'services.AddTransient<ICourseService, CourseService>()'
            // diventerà:
            services.AddTransient<ICourseService, AdoNetCourseService>();
            // 7db. registriamo anche l'altro servizio, quello infrastrutturale
            // così che ogni volta che un componente ha una dipendenza dall'interfaccia
            // IDatabaseAccessor ASP.NET Corer inietterà un istanza di SqliteDatabaseAccessor
            services.AddTransient<IDatabaseAccessor, SqliteDatabaseAccessor>();
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
