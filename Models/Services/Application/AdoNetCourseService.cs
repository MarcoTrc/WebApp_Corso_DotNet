using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using MyCourse.Models.ViewModels;
using WebApp.Models.Services.Infrastructure;
using WebApp.Models.ViewModels;

namespace WebApp.Models.Services.Application
{

    // Per far si che l'applicazione recuper i dati da visualizzare da
    // un database sarà necessario per prima cosa creare il servizio applicativo e,
    // così come nel caso precedente, fare in modo che implementi l'intefaccia ICourseServices 
    // NB un servizio applicativo dovrebbe avere la responsabilità di sapere
    // cosa estrarre dal database, il come farlo sarà delegato ad un servizio
    // infrastrutturale in cui adopereremo in maniera massiccia le classi, in questo caso,
    // di ADO.NET
    public class AdoNetCourseService : ICourseService
    {

        // Per esprimere la dipendenza che un componente ha da un altro componente
        // generiamo un costruttore che fra i parametri avrà un riferimento al 
        // servizio infrastrutturale che andremo a definire (2.) (IDatabaseAccessror db)
        private readonly IDatabaseAccessor db;
        public AdoNetCourseService(IDatabaseAccessor db)
        {
            this.db = db;

        }
        public async Task<CourseDetailViewModel> GetCourseAsync(int Id)
        {
            // In questo caso avremo due SELECT che prevedono un ritorno
            // in funzione dell'Id che ricevono in fase di invocazione
            FormattableString query = $@"SELECT id, Title, Description, ImagePath, Author, Rating, Fullprice_Amount, FullPrice_Currency, CurrentPrice_Amount, CurrentPrice_Currency FROM Courses WHERE id={Id}; SELECT id, Title, Description, Duration FROM Lessons WHERE CourseId={Id}";
            

            DataSet dataSet = await db.QueryAsync(query);
            // Dalle due query otterremo due dataset distinti, uno in posizione[0],
            // l'altro in posizione [1] che gestiremo separatamente

            //Course
            var courseTable = dataSet.Tables[0];
            if (courseTable.Rows.Count != 1) {
                throw new InvalidOperationException($"Esistono più righe con lo stesso Id {Id}");
            }
            var courseRow = courseTable.Rows[0];
            var courseDetailViewModel = CourseDetailViewModel.FromDataRow(courseRow);

            //Course Lessons
            var lessonDataTable = dataSet.Tables[1];

            foreach (DataRow lessonRow in lessonDataTable.Rows)
            {
                LessonViewModel lessonViewModel = LessonViewModel.FromDataRow(lessonRow);
                courseDetailViewModel.Lessons.Add(lessonViewModel);
            }

            return courseDetailViewModel;
        }

        public async Task<List<CourseViewModel>> GetCoursesAsync()
        {
            // Per indicare quali dati estrarre da un Database avremo sicuramente bisogno
            // di una Query sql
            FormattableString query = $"SELECT id, Title, ImagePath, Author, Rating, Fullprice_Amount, FullPrice_Currency, CurrentPrice_Amount, CurrentPrice_Currency FROM Courses";
            // Ora passiamo la query al nostro oggetto db tramite un metodo
            // ( 'Query()' per esempio...). Il servizio infrastrutturale dovrà restituirci 
            // le informazioni trovate nel database in forma di un qualche oggetto.
            // In ADO.NET l'oggetto DataSet è idoneo alle nostre esigenze.  

            DataSet dataset = await db.QueryAsync(query);
            // Generato il metodo Query nell'interfaccia, che torna un oggetto
            // di tipo DataSet e prende in input la string query, non ci resta che 
            // creare l'implementazione concreta del servizio infrastrutturale
            // e lo faremo (in questo caso) tramite la classe SqLitedatabaseAccessor(5.)


            // Ora, una vota tornato il DataSet e verificata l'esistenza di
            // una proprietà 'Tables' che ci prendiamo e al assegnamo ad 
            // una variabile dataTable
            var dataTable = dataset.Tables[0];
            // in più il DataTable al suo interno ha una proprietà 'rows'
            // che contiene tutte le righe apparteneti alla tabella

            // essendo che GetCourses dovrà restituire una Lista di CourseViewModel
            // posso creare un oggetto dello stesso tipo in cui mappare tutte le righe 
            // della tabella tramite un foreach
            var courseList = new List<CourseViewModel>();

            foreach (DataRow courseRow in dataTable.Rows)
            {
                // genero una variabile 'course' assegnata ad un metodo
                // di CourseViewModel che prende in input un oggetto di tipo
                // DataRow e ritorna appunto un CorseViewModel
                var course = CourseViewModel.FromDataRow(courseRow);

                // infine aggiungo ogni riga alla lista
                courseList.Add(course);
            }

            // e ritorno la lista
            return courseList;
        }
    }
}