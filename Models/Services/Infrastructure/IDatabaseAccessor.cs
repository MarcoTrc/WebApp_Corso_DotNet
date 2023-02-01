using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models.Services.Infrastructure
{
    // Creiamo l'interfaccia IDatabaseAccessor e torniamo al servizio per 
    // decidere quali membri pubblici apparterranno all'interfaccia.
    public interface IDatabaseAccessor
    {
        Task<DataSet> QueryAsync(FormattableString query);
    }
}