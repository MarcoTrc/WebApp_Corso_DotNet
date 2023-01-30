using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models.ViewModels;

namespace WebApp.Models.Services.Application
{
    public interface ICourseService
    {
        Task<List<CourseViewModel>> GetCoursesAsync();

        Task<CourseDetailViewModel> GetCourseAsync(int id);
    }
}

// qui indichiamo semplicemente la firma di questi metodi, cioè il nome del metodo,
// il tipo restituito ed eventualmente i parametri in input.

// L'interfaccia è come se fosse un contratto, perché di fatto vincola le classi che la implementano
// a possedere tutti i membri in essa definiti.

// Di solito sarebbe utile scrivere prima l'interfaccia e poi la sua implementazione concreta.
// Questo perché permette di concentrarsi sul come si vuole che sia realizzato il servizio
// senza impelagarsi subito su dettagli e implementazioni all'interno delle classi che la implementeranno